using ProductMigration.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMigration.extensions
{
    internal static class WixProductExtension
    {
        internal static string GetBody(this WixProduct wixProduct)
        {
            return wixProduct.Description
                    + wixProduct.AdditionalInfoTitle1 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle1, wixProduct.AdditionalInfoDescription1) : ""
                    + wixProduct.AdditionalInfoTitle2 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle2, wixProduct.AdditionalInfoDescription2) : ""
                    + wixProduct.AdditionalInfoTitle3 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle3, wixProduct.AdditionalInfoDescription3) : ""
                    + wixProduct.AdditionalInfoTitle4 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle4, wixProduct.AdditionalInfoDescription4) : ""
                    + wixProduct.AdditionalInfoTitle5 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle5, wixProduct.AdditionalInfoDescription5) : ""
                    + wixProduct.AdditionalInfoTitle6 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle6, wixProduct.AdditionalInfoDescription6) : "";
        }
        internal static string GetTags(this WixProduct wixProduct)
        {
            return wixProduct.Description
                    + wixProduct.AdditionalInfoTitle1 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle1, wixProduct.AdditionalInfoDescription1) : ""
                    + wixProduct.AdditionalInfoTitle2 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle2, wixProduct.AdditionalInfoDescription2) : ""
                    + wixProduct.AdditionalInfoTitle3 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle3, wixProduct.AdditionalInfoDescription3) : ""
                    + wixProduct.AdditionalInfoTitle4 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle4, wixProduct.AdditionalInfoDescription4) : ""
                    + wixProduct.AdditionalInfoTitle5 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle5, wixProduct.AdditionalInfoDescription5) : ""
                    + wixProduct.AdditionalInfoTitle6 != null ? CombineAdditionInformation(wixProduct.AdditionalInfoTitle6, wixProduct.AdditionalInfoDescription6) : "";
        }

        private static string CombineAdditionInformation(string? title, string? description)
        {
            return $@"<p><strong>{title}</strong></p>{description}";
        }
    }
}
