
using System;
using System.Collections.Generic;

namespace EMSSystem.Functions
{
    public class SaltedHashManager : SaltedHash
    {
        #region 單筆加密後傳回字串
        public T EncordSingleData<T>(T singleData, List<string> columns)
        {
            List<string> Decrypt = new List<string>();
            object encordedData = null, result = string.Empty;
            foreach (var item in columns)
            {
                encordedData = ReflectionManager.GetValueFromProperty(singleData, item);

                if (encordedData != null)
                {
                    result = EncryptDerivedKey(encordedData.ToString());
                    ReflectionManager.SetValueToProperty(singleData, item, result);
                }
            }
            return singleData;
        }
        #endregion

        #region 單筆解密後傳回字串
        /// <summary>
        /// 單筆解密後傳回字串
        /// </summary>
        /// <typeparam name="T">回傳自訂型別</typeparam>
        /// <param name="singleData">單筆資料</param>
        /// <param name="columns">欄位</param>
        /// <param name="salt">Salt</param>
        /// <param name="UltraKey">UltraKey</param>
        /// <returns></returns>
        public T DecordSingleData<T>(T singleData, List<string> columns)
        {
            List<string> Decrypt = new List<string>();
            object encordedData = null, result = string.Empty;
            foreach (var item in columns)
            {

                encordedData = ReflectionManager.GetValueFromProperty(singleData, item);

                if (encordedData != null && !string.IsNullOrEmpty(encordedData.ToString()))
                {
                    result = DecryptDerivedKey(encordedData.ToString());
                    ReflectionManager.SetValueToProperty(singleData, item, result);
                }
            }
            return singleData;
        }
        #endregion

        #region 多筆加密後傳回字串
        public List<T> EncordMultipleData<T>(IEnumerable<T> listData, List<string> columns)
        {
            List<T> resultData = new List<T>();
            string encordedData = string.Empty, result = string.Empty;
            object decordedData = null;
            foreach (var item in listData)
            {
                foreach (var fieldName in columns)
                {
                    decordedData = ReflectionManager.GetValueFromProperty(item, fieldName);

                    if (decordedData != null)
                    {
                        encordedData = decordedData.ToString();
                        result = EncryptDerivedKey(encordedData);
                        ReflectionManager.SetValueToProperty(item, fieldName, result);
                    }
                }
                resultData.Add(item);
            }
            return resultData;
        }
        #endregion

        #region 多筆解密後傳回字串
        public IEnumerable<T> DecordMultipleData<T>(IEnumerable<T> listData, List<string> columns)
        {
            foreach (var item in listData)
                DecordSingleData(item, columns);

            return listData;
        }
        #endregion    

        #region 字串加密
        public string EncryptData(string encordedData)
        {
            return EncryptDerivedKey(encordedData);
        }
        #endregion

        #region 字串解密 
        public string DecryptData(string encordedData)
        {
            return DecryptDerivedKey(encordedData);
        }
        #endregion
    }
}
