using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Common
{
    /// <summary>
    /// Http请求封装类，封装常用的Get，Post请求方法
    /// </summary>
    public class HttpRequest
    {
        public static CookieContainer cookies = new CookieContainer();
        /// <summary>
        /// 向指定URL发送GET方法的请求
        /// </summary>
        /// <param name="url">发送请求的URL</param>
        /// <returns>所代表远程资源的响应结果</returns>
        public static string SendGet(string url)
        {
            string result = String.Empty;
            StreamReader reader = null;
            try
            {
                string urlNameString = url;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlNameString);
                request.Method = "GET";
                request.ContentType = "text/plain";
                request.Accept = "*/*";
                request.CookieContainer = cookies;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookies.GetCookies(response.ResponseUri);
                Stream responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                result = reader.ReadToEnd();

                reader.Close();
                responseStream.Close();
                response.Close();
                reader = null;
                responseStream = null;
                response = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送GET请求出现异常：" + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 向指定URL发送GET方法的请求
        /// </summary>
        /// <param name="url">发送请求的URL</param>
        /// <param name="param">请求参数，请求参数应该是 name1=value1&name2=value2 的形式。</param>
        /// <returns>所代表远程资源的响应结果</returns>
        public static string SendGet(string url, string param)
        {
            string result = String.Empty;
            StreamReader reader = null;
            try
            {
                string urlNameString = url + "?" + param;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlNameString);
                request.Method = "GET";
                request.ContentType = "text/plain";
                request.Accept = "*/*";
                request.CookieContainer = cookies;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookies.GetCookies(response.ResponseUri);
                Stream responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                result = reader.ReadToEnd();

                reader.Close();
                responseStream.Close();
                response.Close();
                reader = null;
                responseStream = null;
                response = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送GET请求出现异常：" + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 向指定URL发送GET方法的请求
        /// </summary>
        /// <param name="url">发送请求的URL</param>
        /// <param name="param">请求参数，请求参数应该是 name1=value1&name2=value2 的形式。</param>
        /// <param name="encoding">设置响应信息的编码格式，如utf-8</param>
        /// <returns>所代表远程资源的响应结果</returns>
        public static string SendGet(string url, string param, string encoding)
        {
            string result = String.Empty;
            StreamReader reader = null;
            try
            {
                string urlNameString = url + "?" + param;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlNameString);
                request.Method = "GET";
                request.ContentType = "text/html;charset=" + encoding;
                request.Accept = "*/*";
                request.CookieContainer = cookies;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookies.GetCookies(response.ResponseUri);
                Stream responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream, Encoding.GetEncoding(encoding));
                result = reader.ReadToEnd();

                reader.Close();
                responseStream.Close();
                response.Close();
                reader = null;
                responseStream = null;
                response = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送GET请求出现异常：" + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 向指定 URL 发送POST方法的请求
        /// </summary>
        /// <param name="url">发送请求的 URL</param>
        /// <param name="jsonData">请求参数，请求参数应该是Json格式字符串的形式。</param>
        /// <returns>所代表远程资源的响应结果</returns>
        public static string SendPost(string url, string jsonData)
        {
            string result = String.Empty;
            try
            {
                //CookieContainer cookie = new CookieContainer();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Headers.Add("x-requested-with", "XMLHttpRequest");
                request.ServicePoint.Expect100Continue = false;
                request.ContentType = "text/plain";
                request.Accept = "*/*";
                request.ContentLength = Encoding.UTF8.GetByteCount(jsonData);
                request.CookieContainer = cookies;
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {

                    writer.Write(jsonData);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookies.GetCookies(response.ResponseUri);
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        result = reader.ReadToEnd();

                        reader.Close();
                    }
                    responseStream.Close();
                }
                response.Close();
                response = null;
                request = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送GET请求出现异常：" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 向指定 URL 发送POST方法的GraphQL请求
        /// </summary>
        /// <param name="url">发送请求的 URL</param>
        /// <param name="jsonData">请求参数，请求参数应该是Json格式字符串的形式。</param>
        /// <returns>所代表远程资源的响应结果</returns>
        public static string SendPost(string url, string jsonData, bool graph)
        {
            string result = string.Empty;
            jsonData = "{\"query\": \"" + jsonData + "\"}";
            try
            {
                //CookieContainer cookie = new CookieContainer();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //request.Headers.Add("x-requested-with", "XMLHttpRequest");
                request.ServicePoint.Expect100Continue = false;
                request.ContentType = "application/json";
                request.Accept = "*/*";
                request.ContentLength = Encoding.UTF8.GetByteCount(jsonData);
                request.CookieContainer = cookies;
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {

                    writer.Write(jsonData);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookies.GetCookies(response.ResponseUri);
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        result = reader.ReadToEnd();

                        reader.Close();
                    }
                    responseStream.Close();
                }
                response.Close();
                response = null;
                request = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送GET请求出现异常：" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 向指定 URL 发送POST方法的请求
        /// </summary>
        /// <param name="url">发送请求的 URL</param>
        /// <param name="jsonData">请求参数，请求参数应该是Json格式字符串的形式。</param>
        /// <param name="encoding">设置响应信息的编码格式，如utf-8</param>
        /// <returns>所代表远程资源的响应结果</returns>
        public static string SendPost(string url, string jsonData, string encoding)
        {
            string result = String.Empty;
            try
            {
                //CookieContainer cookie = new CookieContainer();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Headers.Add("x-requested-with", "XMLHttpRequest");
                request.ServicePoint.Expect100Continue = false;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "*/*";
                request.ContentLength = Encoding.UTF8.GetByteCount(jsonData);
                request.CookieContainer = cookies;
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.GetEncoding(encoding)))
                {

                    writer.Write(jsonData);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookies.GetCookies(response.ResponseUri);
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding(encoding)))
                    {
                        result = reader.ReadToEnd();

                        reader.Close();
                    }
                    responseStream.Close();
                }
                response.Close();
                response = null;
                request = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送GET请求出现异常：" + ex.Message);
            }
            return result;
        }
    }
}
