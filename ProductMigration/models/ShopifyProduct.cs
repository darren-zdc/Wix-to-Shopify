using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ProductMigration.models
{
    internal enum ShopifyProductType
    {
        RearSets,
        BrakeRotors,
        Wheels
    }

    internal class ShopifyProduct
    {
        public string Handle { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Vendor { get; set; }
        [Name("Product Category")]
        public string ProductCategory { get; set; }
        public string Type { get; set; }
        public string Tags { get; set; }
        public bool Published { get; set; }

        [Name("Option1 Name")]
        public string Option1Name { get; set; }
        [Name("Option1 Value")]
        public string Option1Value { get; set; }
        [Name("Option2 Name")]
        public string Option2Name { get; set; }
        [Name("Option2 Value")]
        public string Option2Value { get; set; }
        [Name("Option3 Name")]
        public string Option3Name { get; set; }
        [Name("Option3 Value")]
        public string Option3Value { get; set; }
        [Name("Variant SKU")]
        public string VariantSKU { get; set; }
        [Name("Variant Grams")]
        public float VariantGrams { get; set; }
        [Name("Variant Inventory Tracker")]
        public string VariantInventoryTracker { get; set; }
        [Name("Variant Inventory Qty")]
        public float VariantInventoryQty { get; set; }
        [Name("Variant Inventory Policy")]
        public string VariantInventoryPolicy { get; set; }
        [Name("Variant Fulfillment Service")]
        public string VariantFulfillmentService { get; set; }

        [Name("Variant Price")]
        public float VariantPrice { get; set; }
        [Name("Variant Compare At Price")]
        public float VariantCompareAtPrice { get; set; }
        [Name("Variant Requires Shipping")]
        public bool VariantRequiresShipping { get; set; }
        [Name("Variant Taxable")]
        public bool VariantTaxable { get; set; }
        [Name("Variant Barcode")]
        public string VariantBarcode { get; set; }
        [Name("Image Src")]
        public string ImageSrc { get; set; }
        [Name("Image Position")]
        public int ImagePosition { get; set; }
        [Name("Image Alt Text")]
        public string ImageAltText { get; set; }
        [Name("Gift Card")]
        public bool GiftCard { get; set; }
        [Name("SEO Title")]
        public string SEOTitle { get; set; }
        [Name("SEO Description")]
        public string SEODescription { get; set; }

        [Name("Google Shopping / Google Product Category")]
        public string GoogleShoppingCategory { get; set; }

        [Name("Google Shopping / Gender")]
        public string GoogleShoppingGender { get; set; }

        [Name("Google Shopping / Age Group")]
        public string GoogleShoppingAge { get; set; }

        [Name("Google Shopping / MPN")]
        public string GoogleShoppingMPN { get; set; }

        [Name("Google Shopping / Condition")]
        public string GoogleShoppingCondition { get; set; }

        [Name("Google Shopping / Custom Product")]
        public string GoogleShoppingCustomProduct { get; set; }

        [Name("Google Shopping / Custom Label 0")]
        public string GoogleShoppingLabel0 { get; set; }

        [Name("Google Shopping / Custom Label 1")]
        public string GoogleShoppingLabel1 { get; set; }

        [Name("Google Shopping / Custom Label 2")]
        public string GoogleShoppingLabel2 { get; set; }

        [Name("Google Shopping / Custom Label 3")]
        public string GoogleShoppingLabel3 { get; set; }

        [Name("Google Shopping / Custom Label 4")]
        public string GoogleShoppingLabel4 { get; set; }

        [Name("Variant Image")]
        public string VariantImage { get; set; }

        [Name("Variant Weight Unit")]
        public string VariantWeightUnit { get; set; }

        [Name("Variant Tax Code")]
        public string VariantTaxCode { get; set; }

        [Name("Cost per item")]
        public float CostPerItem { get; set; }

        [Name("Included / Canada")]
        public bool IncludedCanada { get; set; }

        [Name("Included / International")]
        public bool IncludedInternational { get; set; }

        [Name("Price / International")]
        public float PriceInternational { get; set; }

        [Name("Compare At Price / International")]
        public float CompareAtPriceInternational { get; set; }

        [Name("Included / United States")]
        public bool IncludedUnitedStates { get; set; }

        [Name("Price / United States")]
        public float PriceUnitedStates { get; set; }

        [Name("Compare At Price / United States")]
        public float CompareAtPriceUnitedStates { get; set; }

        [Name("Status")]
        public string Status { get; set; }
    }
}
