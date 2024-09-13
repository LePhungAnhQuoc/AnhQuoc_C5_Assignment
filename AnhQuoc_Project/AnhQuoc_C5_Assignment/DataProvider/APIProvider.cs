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

        public IEnumerable<T> GetAll()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(localHost);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            int nLoad = 0;
            while (nLoad < 5)
            {
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync($"api/{objectName}").Result;
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var datas = response.Content.ReadAsAsync<IEnumerable<T>>().Result;
                        return datas;
                    }
                    return null;
                }
                catch
                {
                    nLoad++;
                }
            }
            MessageBox.Show("An error when fetching data, please restart app again");
            Environment.Exit(0);
            return null;
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

            StringBuilder parameter = new StringBuilder();
            foreach (var prop in listProps)
            {
                parameter.Append($"{prop.Name} = " + Utilitys.getValueFromProperty(prop, newItem) + "&");
            }
            parameter.Remove(parameter.Length - 1, 1);
            return parameter.ToString();
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
