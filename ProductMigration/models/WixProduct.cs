using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMigration.models
{
    public enum WixProductType
    {
        AdjustableRearsets,
        FrontBrakeRotors,
        FrontRearRotorSet,
        RearBrakeRotors,
        WheelRim,
        Other
    }

    public enum WixFieldType
    {
        Product,
        Variant
    }


    public class WixProduct
    {
        [Name("handleId")]
        public string HandleId { get; set; } 

        [Name("fieldType")]
        public string FieldType { get; set; } 

        [Name("name")]
        public string Name { get; set; } 

        [Name("description")]
        public string Description { get; set; } 
        
        [Name("productImageUrl")]
        public string ProductImageUrl { get; set; } 

        [Name("collection")]
        public string Collection { get; set; } 
        
        [Name("sku")]
        public string Sku { get; set; } 
        
        [Name("ribbon")]
        public string Ribbon { get; set; } 
        
        [Name("price")]
        public float? Price { get; set; } 
        
        [Name("surcharge")]
        public float? Surcharge { get; set; } 
        
        [Name("visible")]
        public bool Visible { get; set; } 
        
        [Name("discountMode")]
        public string DiscountMode { get; set; } 
        
        [Name("discountValue")]
        public string DiscountValue { get; set; } 
        
        [Name("inventory")]
        public string Inventory { get; set; } 
        
        [Name("weight")]
        public float? Weight { get; set; } 
        
        [Name("cost")]
        public float? Cost { get; set; } 
        
        [Name("productOptionName1")]
        public string ProductOptionName1 { get; set; } 
        
        [Name("productOptionType1")]
        public string ProductOptionType1 { get; set; } 
        
        [Name("productOptionDescription1")]
        public string ProductOptionDescription1 { get; set; } 
        
        [Name("productOptionName2")]
        public string ProductOptionName2 { get; set; } 
        
        [Name("productOptionType2")]
        public string ProductOptionType2 { get; set; } 
        
        [Name("productOptionDescription2")]
        public string ProductOptionDescription2 { get; set; } 
        
        [Name("productOptionName3")]
        public string ProductOptionName3 { get; set; } 
        
        [Name("productOptionType3")]
        public string ProductOptionType3 { get; set; } 
        
        [Name("productOptionDescription3")]
        public string ProductOptionDescription3 { get; set; } 
        
        [Name("productOptionName4")]
        public string ProductOptionName4 { get; set; } 
        
        [Name("productOptionType4")]
        public string ProductOptionType4 { get; set; } 
        
        [Name("productOptionDescription4")]
        public string ProductOptionDescription4 { get; set; } 
        
        [Name("productOptionName5")]
        public string ProductOptionName5 { get; set; } 
        
        [Name("productOptionType5")]
        public string ProductOptionType5 { get; set; } 
        
        [Name("productOptionDescription5")]
        public string ProductOptionDescription5 { get; set; } 
        
        [Name("productOptionName6")]
        public string ProductOptionName6 { get; set; } 
        
        [Name("productOptionType6")]
        public string ProductOptionType6 { get; set; } 
        
        [Name("productOptionDescription6")]
        public string ProductOptionDescription6 { get; set; } 
        
        [Name("additionalInfoTitle1")]
        public string? AdditionalInfoTitle1 { get; set; } 
        
        [Name("additionalInfoDescription1")]
        public string? AdditionalInfoDescription1 { get; set; } 
        
        [Name("additionalInfoTitle2")]
        public string? AdditionalInfoTitle2 { get; set; } 
        
        [Name("additionalInfoDescription2")]
        public string? AdditionalInfoDescription2 { get; set; } 
        
        [Name("additionalInfoTitle3")]
        public string? AdditionalInfoTitle3 { get; set; } 
        
        [Name("additionalInfoDescription3")]
        public string? AdditionalInfoDescription3 { get; set; } 
        
        [Name("additionalInfoTitle4")]
        public string? AdditionalInfoTitle4 { get; set; } 
        
        [Name("additionalInfoDescription4")]
        public string? AdditionalInfoDescription4 { get; set; } 
        
        [Name("additionalInfoTitle5")]
        public string? AdditionalInfoTitle5 { get; set; } 
        
        [Name("additionalInfoDescription5")]
        public string? AdditionalInfoDescription5 { get; set; } 
        
        [Name("additionalInfoTitle6")]
        public string? AdditionalInfoTitle6 { get; set; } 
        
        [Name("additionalInfoDescription6")]
        public string? AdditionalInfoDescription6 { get; set; } 
        
        [Name("customTextField1")]
        public string CustomTextField1 { get; set; } 
        
        [Name("customTextCharLimit1")]
        public string CustomTextCharLimit1 { get; set; } 
        
        [Name("customTextMandatory1")]
        public string CustomTextMandatory1 { get; set; } 
        
        [Name("customTextField2")]
        public string CustomTextField2 { get; set; } 
        
        [Name("customTextCharLimit2")]
        public string CustomTextCharLimit2 { get; set; } 
        
        [Name("customTextMandatory2")]
        public string CustomTextMandatory2 { get; set; } 
        
        [Name("brand")]
        public string Brand { get; set; } 
    }
}
