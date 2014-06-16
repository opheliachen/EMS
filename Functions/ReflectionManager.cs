
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace EMSSystem.Functions
{
    public class ReflectionManager
    {
        public const BindingFlags MemberAccess =
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Static | BindingFlags.Instance | BindingFlags.IgnoreCase;

        //-----------------------------------------------------------------------------------------

        #region -- AbstractAssemblyName --
        /// <summary>
        /// Abstracts the name of the assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        private static string AbstractAssemblyName(string assemblyName)
        {
            string prefix = ".\\";
            string suffix = ".dll";

            if (assemblyName.StartsWith(prefix))
            {
                assemblyName = assemblyName.Substring(prefix.Length);
            }
            if (assemblyName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
            {
                assemblyName = assemblyName.Substring(0, assemblyName.Length - suffix.Length);
            }
            return assemblyName;
        }
        #endregion

        #region -- GetAssemblyPath --
        /// <summary>
        /// Gets the assembly path (Just For THSRC Project).
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyPath()
        {
            string fullPath = "";// Assembly.GetAssembly(typeof(BackendBaseController)).Location;
            string theDirectory = Path.GetDirectoryName(fullPath);
            return string.Concat(theDirectory, "\\THSRC.dll");
        }

        /// <summary>
        /// Gets the assembly path (Just For Assigned Project).
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyPath(Type type, string projectName)
        {
            string fullPath = Assembly.GetAssembly(type).Location;
            string theDirectory = Path.GetDirectoryName(fullPath);
            return string.Concat(theDirectory, "\\" + projectName + ".dll");
        }
        #endregion

        #region -- GetType --
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="pathOrAssemblyName">Name of the path or assembly.</param>
        /// <param name="classFullName">Full name of the class.</param>
        /// <returns></returns>
        public static Type GetType(string pathOrAssemblyName, string classFullName)
        {
            try
            {
                if (!pathOrAssemblyName.Contains(Path.DirectorySeparatorChar.ToString()))
                {
                    string assemblyName = AbstractAssemblyName(pathOrAssemblyName);
                    if (!classFullName.Contains(assemblyName))
                    {
                        classFullName = String.Concat(assemblyName, ".", classFullName);
                    }
                    Assembly assembly = Assembly.Load(assemblyName);
                    return assembly.GetType(classFullName);
                }

                Assembly asm = Assembly.LoadFrom(pathOrAssemblyName);
                if (null == asm) return null;

                Type type = asm.GetType(classFullName);

                if (null == type)
                {
                    foreach (Type one in asm.GetTypes())
                    {
                        if (one.Name == classFullName)
                        {
                            type = one;
                            break;
                        }
                    }
                }
                return type;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        //-----------------------------------------------------------------------------------------
        //Field

        #region GetFieldInfo
        /// <summary>
        /// Gets the field info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filedName">Name of the filed.</param>
        /// <returns></returns>
        public FieldInfo GetFieldInfo<T>(string filedName)
        {
            return this.GetFieldInfo(typeof(T), filedName);
        }

        /// <summary>
        /// Gets the field info.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            try
            {
                FieldInfo[] infos = type.GetFields(ReflectionManager.MemberAccess);
                if (infos == null || infos.Length.Equals(0))
                {
                    return null;
                }
                foreach (FieldInfo info in infos)
                {
                    if (fieldName.Equals(info.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        return info;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            return null;
        }
        #endregion

        #region GetValueFromField
        /// <summary>
        /// Gets the value from field.
        /// </summary>
        /// <param name="Object">The object.</param>
        /// <param name="filedName">Name of the filed.</param>
        /// <returns></returns>
        public static object GetValueFromField(object Object, string filedName)
        {
            return Object.GetType().GetField(filedName, ReflectionManager.MemberAccess).GetValue(Object);
        }
        #endregion

        #region SetValueToField
        /// <summary>
        /// Sets the field value.
        /// </summary>
        /// <param name="Object">The object.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="Value">The value.</param>
        public static void SetValueToField(object Object, string fieldName, object Value)
        {
            Object.GetType().GetField(fieldName, ReflectionManager.MemberAccess).SetValue(Object, Value);
        }
        #endregion

        //-----------------------------------------------------------------------------------------
        //Property

        #region PropertyInfo

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propName">Name of the prop.</param>
        /// <returns></returns>
        public PropertyInfo GetPropertyInfo<T>(string propertyName)
        {
            return GetPropertyInfo(typeof(T), propertyName);
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propName">Name of the prop.</param>
        /// <returns></returns>
        public PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            try
            {
                PropertyInfo[] infos = type.GetProperties(ReflectionManager.MemberAccess);
                if (infos == null || infos.Length.Equals(0))
                {
                    return null;
                }
                foreach (PropertyInfo info in infos)
                {
                    if (propertyName.Equals(info.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        return info;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            return null;
        }
        #endregion

        #region GetPropertyValue
        /// <summary>
        /// GetPropertyValue.
        /// </summary>
        /// <param name="name">The property name.</param>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public object GetPropertyValue(object obj, string propertyName)
        {
            try
            {
                PropertyInfo info = GetPropertyInfo(obj.GetType(), propertyName);
                if (null != info)
                {
                    return info.GetValue(obj, new object[0]);
                }
            }
            catch
            { }
            return null;
        }
        #endregion

        #region SetPropertyValue
        /// <summary>
        /// SetPropertyValue.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="val">The val.</param>
        /// <param name="instance">The instance.</param>
        public void SetPropertyValue(object instance, string propertyName, string val)
        {
            if (null == instance) return;

            Type type = instance.GetType();
            PropertyInfo info = GetPropertyInfo(type, propertyName);

            if (null == info) return;

            try
            {
                if (info.PropertyType.Equals(typeof(string)))
                {
                    info.SetValue(instance, val, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(Boolean)))
                {
                    bool value = false;
                    value = val.ToLower().StartsWith("true");
                    info.SetValue(instance, value, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(int)))
                {
                    int value = String.IsNullOrEmpty(val) ? 0 : int.Parse(val);
                    info.SetValue(instance, value, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(double)))
                {
                    double value = 0.0d;
                    if (!String.IsNullOrEmpty(val))
                    {
                        value = Convert.ToDouble(val);
                    }
                    info.SetValue(instance, value, new object[0]);
                }
                else if (info.PropertyType.Equals(typeof(DateTime)))
                {
                    DateTime value = String.IsNullOrEmpty(val)
                        ? DateTime.MinValue
                        : DateTime.Parse(val);
                    info.SetValue(instance, value, new object[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //static

        #region GetValueFromProperty
        /// <summary>
        /// Gets the value from property.
        /// </summary>
        /// <param name="Object">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static object GetValueFromProperty(object Object, string propertyName)
        {
            return Object.GetType().GetProperty(propertyName, ReflectionManager.MemberAccess).GetValue(Object, null);
        }
        #endregion

        #region SetValueToProperty
        /// <summary>
        /// Sets the value to property.
        /// </summary>
        /// <param name="Object">The object.</param>
        /// <param name="Property">The property.</param>
        /// <param name="Value">The value.</param>
        public static void SetValueToProperty(object Object, string Property, object Value)
        {
            Object.GetType().GetProperty(Property, ReflectionManager.MemberAccess).SetValue(Object, Value, null);
        }
        #endregion

        //-----------------------------------------------------------------------------------------
        //MVC : Controller, Action Name

        #region -- GetAssemblyControllerNames --
        /// <summary>
        /// Gets the Assembly's Controller Names.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAssemblyControllerNames()
        {
            string pathOrAssemblyName = ReflectionManager.GetAssemblyPath();

            Assembly asm = Assembly.LoadFrom(pathOrAssemblyName);
            Type[] types = asm.GetTypes().Where(x => x.Name.EndsWith("Controller")).ToArray();

            List<string> controllerNames = new List<string>();
            foreach (Type typeItem in types)
            {
                controllerNames.Add(typeItem.ToString().Replace("TDCBS.Controllers.", ""));
            }
            return controllerNames.OrderBy(x => x).ToList();
        }
        #endregion

        #region -- GetAllControllerActionNames --
        /// <summary>
        /// Gets all Controller's Action Names.
        /// </summary>
        /// <returns>Dictionary的Key為Controller名稱, Value為所有Action名稱，用「,」分隔.</returns>
        public static Dictionary<string, string> GetAllControllerActionNames()
        {
            string pathOrAssemblyName = ReflectionManager.GetAssemblyPath();
            List<string> controllerNames = ReflectionManager.GetAssemblyControllerNames();

            Dictionary<string, string> actionDict = new Dictionary<string, string>();
            string actionNames = string.Empty;
            List<string> actions = new List<string>();

            foreach (var controllerName in controllerNames)
            {
                Type type = ReflectionManager.GetType(pathOrAssemblyName, controllerName);
                MethodInfo[] methodInfos = type.GetMethods();

                actions.Clear();
                foreach (MethodInfo methodInfo in methodInfos)
                {
                    actions.Add(methodInfo.Name);
                }

                actionDict.Add(controllerName, string.Join(",", actions.ToArray()));
            }

            return actionDict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Gets all Controller Action's Name by type of Attribute
        /// </summary>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns>Dictionary的Key為Controller名稱, Value為所有Action名稱，用「,」分隔.</returns>
        public static Dictionary<string, string> GetAllControllerActionNames(Type attributeType)
        {
            string pathOrAssemblyName = ReflectionManager.GetAssemblyPath();
            List<string> controllerNames = ReflectionManager.GetAssemblyControllerNames();

            Dictionary<string, string> actionDict = new Dictionary<string, string>();
            string actionNames = string.Empty;
            List<string> actions = new List<string>();

            foreach (var controllerName in controllerNames)
            {
                Type type = ReflectionManager.GetType(pathOrAssemblyName, controllerName);
                MethodInfo[] methodInfos = type.GetMethods();

                actions.Clear();
                foreach (MethodInfo methodInfo in methodInfos)
                {
                    Attribute[] attrs = Attribute.GetCustomAttributes(methodInfo, attributeType);
                    if (attrs.Where(x => x.TypeId.ToString().Contains(attributeType.Name)).Count() > 0)
                    {
                        actions.Add(methodInfo.Name);
                    }
                }

                if (actions.Count > 0)
                {
                    actionDict.Add(controllerName, string.Join(",", actions.ToArray()));
                }
            }
            return actionDict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Gets Controller's Action Name by Attribute and Controller.
        /// </summary>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetAllControllerActionNames(Type attributeType, string controllerName)
        {
            string pathOrAssemblyName = ReflectionManager.GetAssemblyPath();
            List<string> controllerNames = ReflectionManager.GetAssemblyControllerNames();

            if (!controllerNames.Contains(controllerName))
            {
                throw new ArgumentOutOfRangeException("指定的Controller名稱並不存在Assembly中.");
            }

            Dictionary<string, string> actionDict = new Dictionary<string, string>();
            string actionNames = string.Empty;
            List<string> actions = new List<string>();

            Type type = ReflectionManager.GetType(pathOrAssemblyName, controllerName);
            MethodInfo[] methodInfos = type.GetMethods();

            actions.Clear();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                Attribute[] attrs = Attribute.GetCustomAttributes(methodInfo, attributeType);
                if (attrs.Where(x => x.TypeId.ToString().Contains(attributeType.Name)).Count() > 0)
                {
                    actions.Add(methodInfo.Name);
                }
            }

            if (actions.Count > 0)
            {
                actionDict.Add(controllerName, string.Join(",", actions.ToArray()));
            }
            return actionDict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }
        #endregion

        #region -- GetControllerActionNamesByAjaxActionAttribute --
        ///// <summary>
        ///// Gets the Controller's Action Names by AjaxActionAttribute.
        ///// </summary>
        ///// <returns></returns>
        //public static Dictionary<string, string> GetControllerActionNamesByAjaxActionAttribute()
        //{
        //    string pathOrAssemblyName = ReflectionHelper.GetAssemblyPath();
        //    List<string> controllerNames = ReflectionHelper.GetAssemblyControllerNames();

        //    Dictionary<string, string> actionDict = new Dictionary<string, string>();
        //    string actionNames = string.Empty;
        //    List<string> actions = new List<string>();

        //    foreach (var controllerName in controllerNames)
        //    {
        //        Type type = ReflectionHelper.GetType(pathOrAssemblyName, controllerName);
        //        MethodInfo[] methodInfos = type.GetMethods();

        //        actions.Clear();
        //        foreach (MethodInfo methodInfo in methodInfos)
        //        {
        //            Attribute[] attrs = Attribute.GetCustomAttributes(methodInfo, typeof(AjaxActionAttribute));
        //            if (attrs.Where(x => x.TypeId.ToString().Contains("AjaxActionAttribute")).Count() > 0)
        //            {
        //                actions.Add(methodInfo.Name);
        //            }
        //        }

        //        if (actions.Count > 0)
        //        {
        //            actionDict.Add(controllerName, string.Join(",", actions.ToArray()));
        //        }
        //    }
        //    return actionDict.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        //}

        ///// <summary>
        ///// Gets the Controller's Action Names by AjaxActionAttribute.
        ///// </summary>
        ///// <param name="controllerName">Name of the controller.</param>
        ///// <returns></returns>
        //public static Dictionary<string, string> GetControllerActionNamesByAjaxActionAttribute(string controllerName)
        //{
        //    Dictionary<string, string> dict = ReflectionHelper.GetControllerActionNamesByAjaxActionAttribute();
        //    return dict.Where(x => x.Key.Equals(controllerName)).ToDictionary(x => x.Key, x => x.Value);
        //}
        #endregion

    }
}
