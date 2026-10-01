using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Api.Models.Dtos;
using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
using static System.Net.WebRequestMethods;
using AnhQuoc_C5_Assignment.Animations;
using System.Windows;
using System.Diagnostics;

namespace AnhQuoc_C5_Assignment
{
    public class APIProvider<T> where T : class, IMapFromModel
    {
        private readonly string objectName;
        private readonly string localHost = "https://localhost:7287/";

        public APIProvider(string objectName)
        {
            this.objectName = objectName;
        }

        // Store a single instance to prevent socket exhaustion
        private static readonly HttpClient httpClient = new HttpClient();

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Ensure TLS 1.2 is enabled for .NET Framework 4.8
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

            try
            {
                // Build request URL using relative path
                var requestUri = new Uri(new Uri(localHost), $"api/{objectName}");

                using (var response = await httpClient.GetAsync(requestUri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<IEnumerable<T>>();
                    }

                    string errorResponseBody = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Server returned HTTP {(int)response.StatusCode} ({response.ReasonPhrase}). Details:\n{errorResponseBody}");
                }
            }
            catch (Exception ex)
            {
                // Display the actual exception message for easier debugging
                MessageBox.Show($"An error occurred when fetching data:\n{ex.Message}", "API Error");
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
        public T GetById(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(localHost);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string url = $"api/{objectName}/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);

                return (T)s;
            }
            return null;
        }

        public void Post(T newItem)
        {
            var addTDto = newItem.MapToAdd();

            string jsonString = JsonConvert.SerializeObject(addTDto);

            string parameterUrl = GetParameterUrl(addTDto);
            var baseAddress = $"{localHost}api/{objectName}?" + parameterUrl;

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(jsonString);

            GetStreamAndResponse(http, bytes);
        }

        public void Put(string id, T updateItem)
        {
            var updateTDto = updateItem.MapToUpdate();

            string jsonString = JsonConvert.SerializeObject(updateTDto);
            string parameter = GetParameterUrl(updateTDto);

            var baseAddress = $"{localHost}api/{objectName}/{id}?" + parameter;

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "PUT";

            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(jsonString);

            GetStreamAndResponse(http, bytes);
        }

        public void Delete(string id)
        {
            var baseAddress = $"{localHost}api/{objectName}/" + id;

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "DELETE";

            using (var response = http.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                }
            }
        }

        #region PrivateMethods
        private string GetParameterUrl(object newItem)
        {
            var listProps = Utilitys.getPropsFromType(newItem.GetType());
            var parameters = listProps
                .Select(p =>
                {
                    var name = Uri.EscapeDataString(p.Name);
                    var valueOfProperty = Utilitys.getValueFromProperty(p, newItem);
                    if (valueOfProperty == null)
                    {
                        Utilitys.CatchExceptionError();
                    }
                    valueOfProperty = string.Empty;

                    var value = Uri.EscapeDataString(valueOfProperty.ToString());
                    return $"{name}={value}";
                });
            return string.Join("&", parameters);
        }

        private void GetStreamAndResponse(HttpWebRequest http, Byte[] bytes)
        {
            using (Stream newStream = http.GetRequestStream())
            {
                newStream.Write(bytes, 0, bytes.Length);
            }

            using (var response = http.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                }
            }
        }
        #endregion
    }
}
