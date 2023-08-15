using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMigration.dtos
{
    public class WixProductDto
    {
        public string HandleId { get; set; }
        public string FieldType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        public string Collection { get; set; }
        public string Sku { get; set; }
        public string Ribbon { get; set; }
        public float? Price { get; set; }
        public float? Surcharge { get; set; }
        public bool Visible { get; set; }
        public string DiscountMode { get; set; }
        public string DiscountValue { get; set; }
        public string Inventory { get; set; }
        public float? Weight { get; set; }
        public float? Cost { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public Dictionary<string, string> AdditionInfos { get; set; }
        public string CustomTextField1 { get; set; }
        public string CustomTextCharLimit1 { get; set; }
        public string CustomTextMandatory1 { get; set; }
        public string CustomTextField2 { get; set; }
        public string CustomTextCharLimit2 { get; set; }
        public string CustomTextMandatory2 { get; set; }
        public string Brand { get; set; }
    }
}
