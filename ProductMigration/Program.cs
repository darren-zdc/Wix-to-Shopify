using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using ProductMigration.models;
using System.IO;
using ProductMigration.extensions;

namespace ProductMigration
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var wixExportedProductsPath = args[0];
            var shopifyImportProductsPath = args[1];
            if (wixExportedProductsPath is null)
            {
                Console.WriteLine("Please enter a path to Wix exported products");
                return;
            }
            else if (!File.Exists(wixExportedProductsPath))
            {
                Console.WriteLine($"File does not exist at path: {Path.GetFullPath(wixExportedProductsPath)}");
                return;
            }

            if (shopifyImportProductsPath is null)
            {
                Console.WriteLine("Please enter a path for output Shopify import product path");
            }
            

            List<WixProduct> wixProducts = ReadWixProductsFromPath(wixExportedProductsPath);
            HashSet<ShopifyProduct> shopifyProductSet = MigrateWixProductsToShopifyProducts(wixProducts);

            WriteShopifyProductsToPath(shopifyProductSet, shopifyImportProductsPath);

        }

        private static HashSet<ShopifyProduct> MigrateWixProductsToShopifyProducts(List<WixProduct> wixProducts)
        {
            var shopifyProductSet = new HashSet<ShopifyProduct>();

            foreach (var wixProductGroup in wixProducts.GroupBy(p => p.HandleId))
            {
                var wixMainProduct = wixProductGroup.First(g => g.FieldType == WixFieldType.Product.ToString());
                List<ShopifyProduct> shopifyProducts = new List<ShopifyProduct>();
                ShopifyProduct shopifyProduct = new ShopifyProduct
                {
                    Handle = Guid.NewGuid().ToString(),
                    Title = wixMainProduct.Name,
                    Body = wixMainProduct.GetBody(),
                    Vendor = "Arashi",
                    ProductCategory = "Motor Vehicle Parts",
                    Tags = wixMainProduct.GetTags(),
                    Published = true,
                    GiftCard = false,
                    IncludedCanada = true,
                    IncludedInternational = true,
                    IncludedUnitedStates = true,
                    Status = "active"
                };
                shopifyProducts.Add(shopifyProduct);
                foreach (var wixProduct in wixProductGroup.Where(g => g.FieldType == WixFieldType.Variant.ToString()))
                {


                }
                shopifyProductSet.UnionWith(shopifyProducts);
            }

            return shopifyProductSet;
        }

        private static List<WixProduct> ReadWixProductsFromPath(string wixExportedProductsPath)
        {
            var wixProducts = new List<WixProduct>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {

            };
            using (var reader = new StreamReader(wixExportedProductsPath))
            using (var csv = new CsvReader(reader, config))
            {
                wixProducts = csv.GetRecords<WixProduct>().ToList();
                Console.WriteLine($"Number of products read: {wixProducts.Count()}");
            }

            return wixProducts;
        }

        private static void WriteShopifyProductsToPath(HashSet<ShopifyProduct> shopifyProductSet, string shopifyImportProductsPath)
        {
            using (var writer = new StreamWriter(shopifyImportProductsPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(shopifyProductSet);
            }
        }
    }
}