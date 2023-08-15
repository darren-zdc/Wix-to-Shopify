using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using ProductMigration.models;
using System.IO;
using ProductMigration.extensions;
using ProductMigration.mappers;
using ProductMigration.dtos;

namespace ProductMigration
{
    public class Program
    {
        public const int MAX_OPTION_NUM = 3;
        public const string PRODUCT_CATEGORY = "Motor Vehicle Parts";
        private const string VENDER = "Arashi";
        private const string WEIGHT_UNIT = "kg";
        private const string FULFILLMENT_SERVICE = "manual";
        private const string INVENTORY_POLICY = "continue";
        private const string INVENTORY_TRACKER = "shopify";

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

            List<WixProductDto> wixProductDtos = MapWixProductsToDtos(wixProducts);
            (HashSet<ShopifyProduct> shopifyProductSet, HashSet<WixProductDto> invalidWixProducts) migrationResult = MigrateWixProductsToShopifyProducts(wixProductDtos);

            WriteShopifyProductsToPath(migrationResult.shopifyProductSet, shopifyImportProductsPath);
            WriteInvalidWixProductsToPath(migrationResult.invalidWixProducts, "../../../output/invalid-wix-products.csv");
        }

        private static List<WixProductDto> MapWixProductsToDtos(List<WixProduct> wixProducts)
        {     
            var wixProductMapper = new WixProductMapper();
            return wixProducts.GroupBy(p => p.HandleId).Select(g => wixProductMapper.MapToDto(g.ToList())).SelectMany(p => p).ToList();
        }

        private static (HashSet<ShopifyProduct>, HashSet<WixProductDto>) MigrateWixProductsToShopifyProducts(List<WixProductDto> wixProductDtos)
        {
            var shopifyProductSet = new HashSet<ShopifyProduct>();
            var invalidWixProducts = new HashSet<WixProductDto>();
            foreach (var wixProductDtoGroup in wixProductDtos.GroupBy(p => p.HandleId))
            {
                var wixMainProduct = wixProductDtoGroup.First(g => g.FieldType == WixFieldType.Product.ToString());                
                List<ShopifyProduct> shopifyProducts = new List<ShopifyProduct>();
                                
                ShopifyProduct shopifyProduct = new ShopifyProduct
                {
                    Handle = Guid.NewGuid().ToString(),
                    Title = wixMainProduct.Name,
                    Body = wixMainProduct.GetBody(),
                    Vendor = VENDER,
                    ProductCategory = PRODUCT_CATEGORY,
                    Tags = wixMainProduct.GetTags(),
                    Published = wixMainProduct.Visible,
                    GiftCard = false,
                    Option1Name = wixMainProduct?.Options?.Keys?.ElementAtOrDefault(0) ?? "",
                    Option2Name = wixMainProduct?.Options?.Keys?.ElementAtOrDefault(1) ?? "",
                    Option3Name = wixMainProduct?.Options?.Keys?.ElementAtOrDefault(2) ?? "",
                    IncludedCanada = true,
                    IncludedInternational = true,
                    IncludedUnitedStates = true,
                    Status = "active"
                };

                var optionNames = wixMainProduct?.Options?.Keys;
                if (optionNames?.Count > MAX_OPTION_NUM) {
                    invalidWixProducts.UnionWith(wixProductDtoGroup);
                    continue;
                }

                bool isMainShopifyProductAdded = false;
                foreach (var wixProductDto in wixProductDtoGroup.Where(g => g.FieldType == WixFieldType.Variant.ToString()))
                {
                    if (isMainShopifyProductAdded)
                    {
                        shopifyProduct = new ShopifyProduct { Handle = shopifyProduct.Handle };
                    }
                    else
                    {
                        isMainShopifyProductAdded = true;
                    }
                    shopifyProduct.VariantSKU = wixProductDto.Sku;
                    shopifyProduct.Option1Value = optionNames?.ElementAtOrDefault(0) == default ? "" : wixProductDto.Options[optionNames?.ElementAt(0)];
                    shopifyProduct.Option2Value = optionNames?.ElementAtOrDefault(1) == default ? "" : wixProductDto.Options[optionNames?.ElementAt(1)];
                    shopifyProduct.Option3Value = optionNames?.ElementAtOrDefault(2) == default ? "" : wixProductDto.Options[optionNames?.ElementAt(2)];
                    shopifyProduct.VariantGrams = wixProductDto.Weight == null ? null : wixProductDto.Weight.Value * 1000;
                    shopifyProduct.VariantInventoryTracker = INVENTORY_TRACKER;
                    shopifyProduct.VariantInventoryQty = 100;
                    shopifyProduct.VariantInventoryPolicy = INVENTORY_POLICY;
                    shopifyProduct.VariantFulfillmentService = FULFILLMENT_SERVICE;
                    shopifyProduct.VariantPrice = wixMainProduct?.Price + wixProductDto.Surcharge ?? 0;
                    shopifyProduct.VariantCompareAtPrice = null;
                    shopifyProduct.VariantRequiresShipping = true;
                    shopifyProduct.VariantTaxable = true;
                    shopifyProduct.VariantBarcode = null;
                    shopifyProduct.ImageSrc = null;
                    shopifyProduct.ImagePosition = null;
                    shopifyProduct.ImageAltText = null;
                    shopifyProduct.GiftCard = false;
                    shopifyProduct.VariantWeightUnit = WEIGHT_UNIT;
                    shopifyProduct.CostPerItem = wixProductDto.Cost;

                    shopifyProducts.Add(shopifyProduct);
                }
                shopifyProductSet.UnionWith(shopifyProducts);
            }

            return (shopifyProductSet, invalidWixProducts);
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


        private static void WriteInvalidWixProductsToPath(HashSet<WixProductDto> invalidWixProducts, string invalidWixProductsPath)
        {
            using (var writer = new StreamWriter(invalidWixProductsPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(invalidWixProducts);
            }
        }

    }
}