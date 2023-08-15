using ProductMigration.dtos;
using ProductMigration.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductMigration.extensions
{
    public static class WixProductExtension
    {

        //public static Dictionary<string, string[]> GetOptions(this WixProduct wixProduct)
        //{
        //    var options = new Dictionary<string, string[]>();
        //    PropertyInfo[] properties = wixProduct.GetType().GetProperties();
        //    foreach (var productOptionProperty in properties.Where(p => p.Name.StartsWith("ProductOptionName")))
        //    {
        //        var optionName = (string?)productOptionProperty.GetValue(wixProduct);
        //        if (optionName == null)
        //            break;

        //        if (IsValidOption(optionName))
        //        {
        //            optionName = OptionNameMigration(optionName);
        //            var productOptionValueProperty = properties.FirstOrDefault(p => p.Name == "ProductOptionDescription" + productOptionProperty.Name.Replace("ProductOptionName", ""));
        //            var optionValues = (productOptionValueProperty?.GetValue(wixProduct) as string)?.Split(';');
        //            options.Add(optionName, optionValues ?? new string[1] );                
        //        }
        //    }
        //    return options;
        //}

        
        //public static string GetVariantSKUByOptions(IEnumerable<WixProductDto> wixProductDtos)
        //{
        //    foreach (var wixProductDto in wixProductDtos)
        //    {
        //        PropertyInfo[] properties = wixProduct.GetType().GetProperties();
        //        foreach (var productOptionProperty in properties.Where(p => p.Name.StartsWith("ProductOptionName")))
        //        {
        //            var optionName = (string?)productOptionProperty.GetValue(wixProduct);
        //            if (optionName == null)
        //                break;

        //            if (IsValidOption(optionName))
        //            {
        //                optionName = OptionNameMigration(optionName);
        //                var productOptionValueProperty = properties.FirstOrDefault(p => p.Name == "ProductOptionDescription" + productOptionProperty.Name.Replace("ProductOptionName", ""));
        //                var optionValues = (productOptionValueProperty?.GetValue(wixProduct) as string)?.Split(';');
        //                options.Add(optionName, optionValues ?? new string[1]);
        //            }
        //        }
        //    }
        //    return wixProducts.First(p => options.Any).Select(p => p.Sku)
        //}

        public static string GetBody(this WixProductDto wixProductDto)
        {
            return wixProductDto.Description
                    + string.Join(" ", wixProductDto?.AdditionInfos?.Select(i => CombineAdditionInformation(i.Key, i.Value)) ?? new string[1]);
        }
        public static string GetTags(this WixProductDto wixProductDto)
        {
            return wixProductDto.Description;
        }

        private static string CombineAdditionInformation(string? title, string? description)
        {
            return $@"<p><strong>{title}</strong></p>{description}";
        }
    }
}
