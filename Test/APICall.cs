using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Test
{
    public class APICall
    {
        
        public static string CalSignature(string method, string requestId, string reqDate, string body_md5, string pathAndQuery, string rsaPrivateKeyBase64)
        {
            string data_to_sign = method + "|"
                 + requestId + "|"
                 + reqDate + "|"
                 + body_md5 + "|"
                 + pathAndQuery;
            string key = Encoding.UTF8.GetString(Convert.FromBase64String(rsaPrivateKeyBase64));
            var signature = RSAHelper.SignatureData(data_to_sign, key);
            return signature;
        }
        public static async Task<ApiResultBase<T>> post_sync<T>(string url, object body, string rsaPrivateKeyBase64)
        {
            ApiResultBase<T> result = default;
            string request_id = Guid.NewGuid().ToString();
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), url))
                {
                    string pathAndQuery = request.RequestUri.PathAndQuery;
                    string date = DateTime.Now.ToString("yyyyMMdd");
                    request.Headers.Add("X-Ohs-Request-Id", request_id);
                    request.Headers.Add("X-Ohs-Request-Date", date);
                    string body_md5 = "";
                    if (body != null)
                    {
                        string jsonData = JsonConvert.SerializeObject(body);
                        request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        body_md5 = HashHelper.Md5Base64(Encoding.UTF8.GetBytes(jsonData));
                    }

                    string signature = CalSignature("POST", request_id, date, body_md5, pathAndQuery, rsaPrivateKeyBase64);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Signature", signature);
                    try
                    {
                        HttpResponseMessage rs = await client.SendAsync(request);

                        if (!rs.IsSuccessStatusCode)
                        {
                            string responseData = await rs.Content.ReadAsStringAsync();
                            throw new Exception("Lỗi khi gửi yêu cầu: " + responseData);
                        }
                        else
                        {
                            string responseData = await rs.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<ApiResultBase<T>>(responseData);
                            string json = JsonConvert.SerializeObject(result);
                            Console.WriteLine(json);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
                    }
                }

            }
            Console.WriteLine($"tra ve: {result}");
            return result;
        }
        public static async Task<ApiResultBase<T>> get_async<T>(string url, Dictionary<string, string> parameters,string rsaPrivateKeyBase64)
        {
            ApiResultBase<T> result = default;
            string request_id = Guid.NewGuid().ToString();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var builder = new UriBuilder(url);
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    foreach (var param in parameters)
                    {
                        query[param.Key] = param.Value;
                    }
                    builder.Query = query.ToString();
                    url = builder.ToString();
                    using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        string pathAndQuery = request.RequestUri.PathAndQuery;
                        string date = DateTime.Now.ToString("yyyyMMdd");
                        request.Headers.Add($"X-Ohs-Request-Id", request_id);
                        request.Headers.Add($"X-Ohs-Request-Date", date);

                        string signature = CalSignature("GET", request_id, date, "", pathAndQuery, rsaPrivateKeyBase64);

                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Signature", signature);

                        HttpResponseMessage response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            string responseData = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<ApiResultBase<T>>(responseData);
                            string json = JsonConvert.SerializeObject(result);
                            Console.WriteLine(json);
                        }
                        else
                        {
                            string responseData = await response.Content.ReadAsStringAsync();
  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }


        public static async Task<ApiResultBase<T>> put_sync<T>(string url, object body, string rsaPrivateKeyBase64)
        {
            ApiResultBase<T> result = default;
            string request_id = Guid.NewGuid().ToString();
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PUT"), url))
                {
                    string pathAndQuery = request.RequestUri.PathAndQuery;
                    string date = DateTime.Now.ToString("yyyyMMdd");
                    request.Headers.Add("X-Gms-Request-Id", request_id);
                    request.Headers.Add("X-Gms-Request-Date", date);
                    string body_md5 = "";
                    if (body != null)
                    {
                        string jsonData = JsonConvert.SerializeObject(body);
                        request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        body_md5 = HashHelper.Md5Base64(Encoding.UTF8.GetBytes(jsonData));
                    }

                    string signature = CalSignature("PUT", request_id, date, body_md5, pathAndQuery, rsaPrivateKeyBase64);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Signature", signature);
                    try
                    {
                        HttpResponseMessage rs = await client.SendAsync(request);

                        if (!rs.IsSuccessStatusCode)
                        {
                            string responseData = await rs.Content.ReadAsStringAsync();
                            throw new Exception("Lỗi khi gửi yêu cầu: " + responseData);
                        }
                        else
                        {
                            string responseData = await rs.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<ApiResultBase<T>>(responseData);
                            string json = JsonConvert.SerializeObject(result);
                            Console.WriteLine(json);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
                    }
                }

            }
            Console.WriteLine($"tra ve: {result}");
            return result;
        }
        public static async Task<ApiResultBase<T>> delete_sync<T>(string url, object body, string rsaPrivateKeyBase64)
        {
            ApiResultBase<T> result = default;
            string request_id = Guid.NewGuid().ToString();
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("DELETE"), url))
                {
                    string pathAndQuery = request.RequestUri.PathAndQuery;
                    string date = DateTime.Now.ToString("yyyyMMdd");
                    request.Headers.Add("X-Gms-Request-Id", request_id);
                    request.Headers.Add("X-Gms-Request-Date", date);
                    string body_md5 = "";
                    if (body != null)
                    {
                        string jsonData = JsonConvert.SerializeObject(body);
                        request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        body_md5 = HashHelper.Md5Base64(Encoding.UTF8.GetBytes(jsonData));
                    }

                    string signature = CalSignature("DELETE", request_id, date, body_md5, pathAndQuery, rsaPrivateKeyBase64);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Signature", signature);
                    try
                    {
                        HttpResponseMessage rs = await client.SendAsync(request);

                        if (!rs.IsSuccessStatusCode)
                        {
                            string responseData = await rs.Content.ReadAsStringAsync();
                            throw new Exception("Lỗi khi gửi yêu cầu: " + responseData);
                        }
                        else
                        {
                            string responseData = await rs.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<ApiResultBase<T>>(responseData);
                            string json = JsonConvert.SerializeObject(result);
                            Console.WriteLine(json);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
                    }
                }

            }
            Console.WriteLine($"tra ve: {result}");
            return result;
        }
        public class CustomApiResponse<T>
        {
            public bool Success { get; set; }
            public int Status { get; set; }
            public int StatusCode { get; set; }
            public CustomApiParam Param { get; set; }
            public List<T> Data { get; set; }
        }

        public class CustomApiParam
        {
            public List<string> Messages { get; set; }
            public List<string> BugCodes { get; set; }
            public bool HasException { get; set; }
        }
        public class RSAHelper
        {
            public static bool VeriySignature(string data_to_sign, string signature, string publicKey)
            {
                RSA rsa = RSA.Create();
                rsa.ImportFromPem(publicKey);

                byte[] data_bytes = Encoding.UTF8.GetBytes(data_to_sign);
                byte[] sign_bytes = Convert.FromBase64String(signature);

                return rsa.VerifyData(data_bytes, sign_bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }

            public static string SignatureData(string data_to_sign, string privateKey)
            {
                RSA rsa = RSA.Create();
                rsa.ImportFromPem(privateKey);

                byte[] data_bytes = Encoding.UTF8.GetBytes(data_to_sign);

                var signature = rsa.SignData(data_bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                return Convert.ToBase64String(signature);
            }
        }
        public class HashHelper
        {
            public static string Md5Base64(byte[] data)
            {
                return Convert.ToBase64String(MD5.Create().ComputeHash(data));
            }
        }
        public class ApiResultBase<T>
        {
            public T Data { get; set; }
            public ResultParam Param { get; set; }
            public bool Success { get; set; }
        }
        public class ResultParam
        {

            public List<string> Messages { get; set; } = new List<string>();
            public List<string> BugCodes { get; set; } = new List<string>();

            private bool hasException;
            public bool HasException
            {
                get
                {
                    return hasException;
                }
                set
                {
                    //if (value) hasException = value; //Chi cho phep set true, ko cho phep set false
                    hasException = value;
                }
            }


            public ResultParam()
            {
                Messages = new List<string>();
                BugCodes = new List<string>();
            }
        }
    }
}
