using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Drawing;

namespace EMSSystem.Functions
{
    public class Configuration
    {
        public static int PrintFont
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PrintFont"]);
            }
        }
        public static int PrintExtraFont
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PrintExtraFont"]);
            }
        }
        public static int PrintTitleFont
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PrintTitleFont"]);
            }
        }
        public static FontStyle PrintFontStyle
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["PrintBoldFont"]) ? FontStyle.Bold : FontStyle.Regular;
            }
        }
        public static string SystemType = ConfigurationManager.AppSettings["SystemType"];
    }
}
