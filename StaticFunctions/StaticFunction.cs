using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.StaticFunctions
{
    public static class StaticFunction
    {
        public static string GetEncodingString(string encodingString)
        {
            string decodingString = "";

            try
            {
                if (!string.IsNullOrEmpty(encodingString) && encodingString.IndexOf(',') > -1)
                {
                    string[] encodingStringArray = encodingString.Split(',');
                    byte[] encodingBytes = new byte[encodingStringArray.Length];
                    for (int i = 0; i < encodingBytes.Length; i++)
                    {
                        encodingBytes[i] = byte.Parse(encodingStringArray[i]);
                    }

                    decodingString = Encoding.Unicode.GetString(encodingBytes);
                }
                else
                    decodingString = encodingString;
            }
            catch
            {
                decodingString = encodingString;
            }

            return decodingString;
        }

        public static string SetEncodingString(string decodingString)
        {
            string encodingString = "";

            if (!string.IsNullOrEmpty(decodingString))
            {
                byte[] encodingBytes = Encoding.Unicode.GetBytes(decodingString);
                for (int i = 0; i < encodingBytes.Length; i++)
                {
                    encodingString += encodingBytes[i].ToString() + ",";
                }

                if (encodingString.Length > 0)
                    encodingString = encodingString.Substring(0, encodingString.Length - 1);
            }
            else
                encodingString = decodingString;

            return encodingString;
        }
    }
}
