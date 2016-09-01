using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AgingInHome.Helpers
{
    public static class PaypalHelpers
    {
        public enum EButtonType
        {
            BuyNow = 1,
            PayNow = 2
        }

        public enum ECurrencyCode
        {
            USD = 1,
            EUR = 2
        }

        public static MvcHtmlString PayPalButton(this HtmlHelper helper,
            bool useSandbox, EButtonType buttonType, string email,
            string itemName, string itemNumber, float amount, ECurrencyCode currency,
            string completeUrl, string cancelUrl, string ipnUrl)
        {
            string action = useSandbox ? ConfigurationManager.AppSettings["PaypalSandboxUrl"] : ConfigurationManager.AppSettings["PaypalLiveUrl"];
            StringBuilder html = new StringBuilder("\r\n<form action=\"")
               .Append(action).Append("\">");
            html.AppendLine("<input type=\"hidden\" name=\"cmd\" value=\"_xclick\">");
            html.AppendLine("<input type=\"hidden\" name=\"business\" value=\"jeroen@xyz.com\" />");
            html.AppendLine("<input type=\"hidden\" name=\"item_name\" value=\"Your PDF file without watermarks\" />");
            html.AppendLine("<input type=\"hidden\" name=\"item_number\" value=\"2ca168a959bb48e28a2ddb4b4640c568\" />");
            html.AppendLine("<input type=\"hidden\" name=\"amount\" value=\"1.99\" />");
            html.AppendLine("<input type=\"hidden\" name=\"currency_code\" value=\"USD\" />");

            html.AppendLine("<input type=\"hidden\" name=\"return\" value=" + completeUrl + " />");
            html.AppendLine("<input type=\"hidden\" name=\"cancel_return\" value=" + cancelUrl + " />");
            html.AppendLine("<input type=\"hidden\" name=\"notify_url\" value=" + ipnUrl + "/>");

            html.AppendLine("<input type=\"hidden\" name=\"no_note\" value=\"1\" />");

            html.AppendLine("<input type=\"image\" name=\"submit\" id=\"paypalbtn\" src=\"https://www.sandbox.paypal.com/en_US/i/btn/btn_buynowCC_LG.gif\" alt=\"PayPal — The safer, easier way to pay online.\" />");
            html.AppendLine("<img src=\"https://www.sandbox.paypal.com/en_US/i/scr/pixel.gif\" width=\"1\" height=\"1\" alt=\"\" />");


            // ...
            // Append HTML for the hidden form fields and the payment button.
            // ...

            html.Append("\r\n</form>");

            return new MvcHtmlString(html.ToString());
        }
    }
}