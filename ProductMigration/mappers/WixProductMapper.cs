using Microsoft.VisualBasic.FileIO;
using ProductMigration.dtos;
using ProductMigration.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProductMigration.mappers
{
    public class WixProductMapper
    {
        public List<WixProductDto> MapToDto(List<WixProduct> wixProducts)
        {
            var resultDtoGroup = new List<WixProductDto>();
            var mainWixProduct = wixProducts.First(p => p.FieldType == WixFieldType.Product.ToString());

            var validOptionNames = new Dictionary<string, string>();
            //var options = new Dictionary<string, string>();
            PropertyInfo[] properties = mainWixProduct.GetType().GetProperties();
            foreach (var productOptionProperty in properties.Where(p => p.Name.StartsWith("ProductOptionName")))
            {
                var optionName = (string?)productOptionProperty.GetValue(mainWixProduct);

                if (IsValidOption(optionName))
                {
                    optionName = OptionNameMigration(optionName);
                    validOptionNames.Add(productOptionProperty.Name, optionName);


                    //var productOptionValueProperty = properties.FirstOrDefault(p => p.Name == "ProductOptionDescription" + productOptionProperty.Name.Replace("ProductOptionName", ""));
                    //var optionValues = productOptionValueProperty?.GetValue(wixProduct) as string;
                    //options.Add(optionName, optionValues ?? "");
                }
            }
            foreach (WixProduct wixProduct in wixProducts)
            {
                var wixProductDto = new WixProductDto
                {
                    HandleId = wixProduct.HandleId,
                    FieldType = wixProduct.FieldType,
                    Name = wixProduct.Name,
                    Description = wixProduct.Description,
                    ProductImageUrl = wixProduct.ProductImageUrl,
                    Collection = wixProduct.Collection,
                    Sku = wixProduct.Sku,
                    Ribbon = wixProduct.Ribbon,
                    Price = wixProduct.Price,
                    Surcharge = wixProduct.Surcharge,
                    Visible = wixProduct.Visible,
                    DiscountMode = wixProduct.DiscountMode,
                    DiscountValue = wixProduct.DiscountValue,
                    Inventory = wixProduct.Inventory,
                    Weight = wixProduct.Weight,
                    Cost = wixProduct.Cost,
                    Options = GetOptions(wixProduct, validOptionNames),
                    AdditionInfos = GetAdditionalInfos(wixProduct),
                    CustomTextField1 = wixProduct.CustomTextField1,
                    CustomTextCharLimit1 = wixProduct.CustomTextCharLimit1,
                    CustomTextCharLimit2 = wixProduct.CustomTextCharLimit2,
                    CustomTextField2 = wixProduct.CustomTextField2,
                    CustomTextMandatory1 = wixProduct.CustomTextMandatory1,
                    CustomTextMandatory2 = wixProduct.CustomTextMandatory2,
                    Brand = wixProduct.Brand
                };
                resultDtoGroup.Add(wixProductDto);
            }
            return resultDtoGroup;
        }

        private static Dictionary<string, string> GetAdditionalInfos(WixProduct wixProduct)
        {
            var additionalInfos = new Dictionary<string, string>();
            PropertyInfo[] properties = wixProduct.GetType().GetProperties();
            foreach (var infoProperty in properties.Where(p => p.Name.StartsWith("AdditionalInfoTitle")))
            {
                var infoTitle = (string?)infoProperty.GetValue(wixProduct);
                if (string.IsNullOrEmpty(infoTitle))
                    continue;
                
                var infoDescriptionProperty = properties.FirstOrDefault(p => p.Name == "AdditionalInfoDescription" + infoProperty.Name.Replace("AdditionalInfoTitle", ""));
                var infoDescription = infoDescriptionProperty?.GetValue(wixProduct) as string;
                additionalInfos.Add(infoTitle, infoDescription ?? "");                
            }
            return additionalInfos;
        }

        private static Dictionary<string, string> GetOptions(WixProduct wixProduct, Dictionary<string, string> validOptions)
        {
            var options = new Dictionary<string, string>();
            PropertyInfo[] properties = wixProduct.GetType().GetProperties();
            foreach (var kvp in validOptions)
            {
                var optionProperty = kvp.Key;
                var optionName = kvp.Value;
                var productOptionValueProperty = properties.FirstOrDefault(p => p.Name == "ProductOptionDescription" + optionProperty.Replace("ProductOptionName", ""));
                var optionValues = productOptionValueProperty?.GetValue(wixProduct) as string;
                if (!string.IsNullOrEmpty(optionValues))
                {
                    options.Add(optionName, optionValues);
                }
            }
            return options;
        }

        private static bool IsValidOption(string? optionName)
        {
            if (string.IsNullOrEmpty(optionName))
                return false;
            if (optionName.Equals("Make", StringComparison.OrdinalIgnoreCase))
                return false;
            if (optionName.Equals("Item Unit", StringComparison.OrdinalIgnoreCase))
                return false;
            if (optionName.Equals("Size", StringComparison.OrdinalIgnoreCase))
                return false;
            return true;
        }

        private static string OptionNameMigration(string? optionName)
        {
            if (string.IsNullOrEmpty(optionName))
                return string.Empty;

            if (optionName.Equals("Rearsets Color", StringComparison.OrdinalIgnoreCase))
            {
                return "Color";
            }
            return optionName;
        }

    }
}
