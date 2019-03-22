using Newtonsoft.Json;
using Northwind.Contract.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contract.Help
{
    public static class ApiHelper
    {
        private static string GetAPIServerBasePath(EnumApiServer APIServer)
        {
            string result = string.Empty;
            string APIUrl = string.Empty;
            switch (APIServer)
            {
                case EnumApiServer.Northwind:
                    result = ConfigurationManager.AppSettings["ServiceUrl"];
                    break;
                default:
                    throw new Exception("Unknown server.");
            }

            return result;
        }

        private static string GetAPIContentType(EnumContentType ContentType)
        {
            string result = "";
            switch (ContentType)
            {
                case EnumContentType.json:
                    result = "application/json";
                    break;
                case EnumContentType.formurlencoded:
                    result = "application/x-www-form-urlencoded";
                    break;
                default:
                    throw new Exception("Unknown ContentType.");
            }

            return result;
        }

        public static string SerializeToJson<T>(this T objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }

        public static T DeserializeJson<T>(string fromJsonString)
        {
            return (T)JsonConvert.DeserializeObject(fromJsonString, typeof(T));
        }

        private static string CombinePath(string serverBasePath, string callPath)
        {
            if (serverBasePath.Length > 1 && serverBasePath.EndsWith("/")) { serverBasePath = serverBasePath.Substring(0, serverBasePath.Length - 1); }
            if (callPath.Length > 1 && callPath.StartsWith("/")) { callPath = callPath.Substring(1); }
            return serverBasePath + "/" + callPath;
        }

        public static T PostApi<T>(EnumApiServer apiServer, string controllerName, string actionName, object parameter)
        {
            return PostApi<T>(apiServer, EnumContentType.json, controllerName, actionName, null, parameter);
        }

        public static T PostApi<T>(EnumApiServer apiServer, EnumContentType contentType, string controllerName, string actionName, string getParam, object parameter, bool isJson = true)
        {
            return PostApi<T>(apiServer, contentType, EnumApiMethodType.Post, controllerName, actionName, getParam, parameter, isJson);
        }

        public static T PostApi<T>(EnumApiServer apiServer, EnumContentType contentType, EnumApiMethodType apiMethodType, string controllerName, string actionName, string getParam, object parameter, bool isJson = true)
        {
            return Api<T>(apiServer, contentType, EnumApiMethodType.Post, controllerName + "/" + actionName, getParam, parameter, isJson);
        }

        public static T GetApi<T>(EnumApiServer apiServer, string controllerName, string actionName, bool isJson = true)
        {
            return Api<T>(apiServer, EnumContentType.json, EnumApiMethodType.Get, controllerName + "/" + actionName, null, null, isJson);
        }

        public static T GetApi<T>(EnumApiServer apiServer, string controllerName, string actionName, string getParam, bool isJson = true)
        {
            return Api<T>(apiServer, EnumContentType.json, EnumApiMethodType.Get, controllerName + "/" + actionName, getParam, null, isJson);
        }

        public static T Api<T>(EnumApiServer apiServer, EnumContentType contentType, EnumApiMethodType apiMethodType, string methodName, string getParam, object parameter, bool isJson = true)
        {
           
            if (!string.IsNullOrWhiteSpace(getParam) && !getParam.StartsWith("/"))
            {
                getParam = "/" + getParam.Trim();
            }
            else if (string.IsNullOrWhiteSpace(getParam))
            {
                getParam = "";
            }

            // 整理呼叫的url
            string apiURL = CombinePath(GetAPIServerBasePath(apiServer), methodName) + getParam;

            HttpWebRequest request = HttpWebRequest.Create(apiURL) as HttpWebRequest;
            string PostTypeStr = "";
            switch (apiMethodType)
            {
                case EnumApiMethodType.Post:
                    PostTypeStr = WebRequestMethods.Http.Post;
                    break;
                case EnumApiMethodType.Get:
                    PostTypeStr = WebRequestMethods.Http.Get;
                    break;
                case EnumApiMethodType.Put:
                    PostTypeStr = WebRequestMethods.Http.Put;
                    break;
                default:
                    break;
            }
            request.Method = PostTypeStr; // 方法
            request.KeepAlive = true; //是否保持連線
            request.ContentType = GetAPIContentType(contentType);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };//for https
            // 讓這個request最多等10分鐘
            request.Timeout = 600000;
            request.MaximumResponseHeadersLength = int.MaxValue;
            request.MaximumAutomaticRedirections = int.MaxValue;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            try
            {
                // 整理成呼叫的body paramter
                if (apiMethodType != EnumApiMethodType.Get)
                {
                    string JSONParameterString = SerializeToJson<object>(parameter);
                    byte[] bs = System.Text.Encoding.UTF8.GetBytes(JSONParameterString);
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(bs, 0, bs.Length);
                    }
                }
                string jsonResult = "";
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var temp = reader.ReadToEnd();
                            jsonResult = temp;
                            //TODO:反序列化
                            if (temp == "" || temp == "null")
                            {
                                return default(T);
                            }
                            else
                            {
                                T result = default(T);
                                if (isJson)
                                {
                                    result = DeserializeJson<T>(jsonResult);
                                }
                                else
                                {
                                    result = (T)Convert.ChangeType(temp, typeof(T));
                                }
                                return result;
                            }
                        }
                    }
                }
            }
            catch (WebException webException)
            {
                if (webException.Response == null)
                {
                    throw new Exception("服務無回應", webException);
                }
                using (StreamReader reader = new StreamReader(webException.Response.GetResponseStream()))
                {
                    HttpWebResponse res = (HttpWebResponse)webException.Response;
                    var pageContent = reader.ReadToEnd();
                    T result = JsonConvert.DeserializeObject<T>(pageContent);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
