using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;

namespace EMSSystem.Functions
{
    class XMLFileOperator
    {
        string filePath;
        XDocument loadDoc;
        NormalFunctions func;

        #region Control XML Files

        public void CreateXMLFiles(string fileName, string comment, string root)
        {
            try 
            {
                XDocument newDoc = new XDocument(
                               new XDeclaration("1.0", "utf-8", "yes"),
                               new XComment(comment),
                               new XElement(root));

                newDoc.Save(fileName);
            }
            catch { }
        }

        public bool CheckXMLExist(string fileName)
        {
            try
            {
                XDocument checkDoc = XDocument.Load(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DeleteXMLFile(string fileName)
        {
            try { }
            catch { }
            if (CheckXMLExist(fileName))
                File.Delete(fileName);
        }

        #endregion
    }
}
