﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RestoreMySQL
{
    class FileOperator
    {
        public void CreateFiles(string fileName)
        {
            if (!File.Exists(fileName))
            {
                using (FileStream fs = File.Create(fileName))
                {
                    fs.Close();
                }
            }
        }

        public void WriteData(string fileName, string data) //Write new data
        {
            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(data);
            sw.Close();
        }

        public void WriteData(string fileName, string data, string separator) //Write with exist data
        {
            string tempData;

            tempData = ReadData(fileName);

            if (tempData != null)
                tempData = tempData + separator + data;
            else
                tempData = data;

            StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(tempData);
            sw.Close();
        }

        public string ReadData(string fileName)
        {
            try
            {
                string data = null;
                StreamReader sr = new StreamReader(fileName);

                while (sr.Peek() > 0)
                {
                    data = data + sr.ReadLine();
                }

                sr.Close();
                return data;
            }
            catch
            {
                return "File is not Exist!!";
            }
        }

        private string GetDataFromArray(string[] arrayName)
        {
            string data = null;

            if (arrayName != null && arrayName.Length > 0)
            {
                for (int i = 0; i < arrayName.Length; i++)
                {
                    if (arrayName[i] != null && arrayName[i].Length > 0)
                        data = data + arrayName[i] + ";";
                }
            }

            return data;
        }

        public void DeleteFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                
            }
            catch
            {
                
            }
        }

        public bool CheckFileExist(string fileName)
        {
            return File.Exists(fileName);
        }

        public void CreateDirectory(string fileName)
        {
            try
            {
                if (!Directory.Exists(fileName))
                    Directory.CreateDirectory(fileName);
            }
            catch
            {
            }
        }

        public void DeleteDirectory(string fileName)
        {
            try
            {
                Directory.Delete(fileName, true);
            }
            catch
            {
            }
        }
    }
}
