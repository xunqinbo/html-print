using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace PrintTest
{
    public class HtmlPrinterHelper
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string name);

        public const  string RegKeyName = @"Software\Microsoft\Internet Explorer\PageSetup";
        private static string _footer;
        private static string _header;

        public static void ClearWebDocumentHeaderAndFooter()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegKeyName, true))
            {
                if (key != null)
                {
                    if(_footer == null)
                        _footer = key.GetValue("footer").ToString();
                    if(_header == null)
                        _header = key.GetValue("header").ToString();
                    key.SetValue("footer", "");
                    key.SetValue("header", "");
                }
            }
        }

        public static void ResumeWebDocumentHeaderAndFooter()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegKeyName, true))
            {
                if (key != null)
                {
                    key.SetValue("footer", _footer);
                    key.SetValue("header", _header);
                    _footer = null;
                    _header = null;
                }
            }
        }
    }
}