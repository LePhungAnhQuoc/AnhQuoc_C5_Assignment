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

namespace AnhQuoc_C5_Assignment
{
    public class APIProvider<T> where T : class
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
            HttpResponseMessage response = httpClient.GetAsync($"api/{objectName}").Result;

            if (response.IsSuccessStatusCode)
            {
                var datas = response.Content.ReadAsAsync<IEnumerable<T>>().Result;
                return datas;
            }
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

        public void Post(T newItem, string keyName)
        {
            string jsonString = JsonConvert.SerializeObject(newItem);

            string parameterUrl = GetParameterUrl(newItem);
            var baseAddress = $"{localHost}api/{objectName}?" + parameterUrl;

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(jsonString);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();


            var response = http.GetResponse();
            var stream = response.GetResponseStream();
        }

        public void Put(string id, T updateItem, string keyName)
        {
            UpdateChildDto updateChildDto = new UpdateChildDto
            {
                IdAdult = "R03",
                Status = false,
                ModifiedAt = DateTime.Now
            };

            string jsonString = JsonConvert.SerializeObject(updateChildDto);

            string parameter = GetParameterUrl(updateChildDto);

            var baseAddress = $"{localHost}api/{objectName}/{id}?" + parameter;

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "PUT";

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(jsonString);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();
            var stream = response.GetResponseStream();
        }

        public void Delete(string id)
        {
            var baseAddress = $"{localHost}api/{objectName}/" + id;

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "DELETE";

            var response = http.GetResponse();
            var stream = response.GetResponseStream();
        }

        #region PrivateMethods
        private string GetParameterUrl(object newItem)
        {
            var listProps = Utilities.getPropsFromType(newItem.GetType());

            StringBuilder parameter = new StringBuilder();
            foreach (var prop in listProps)
            {
                parameter.Append($"{prop.Name} = " + Utilities.getValueFromProperty(prop, newItem) + "&");
            }
            parameter.Remove(parameter.Length - 1, 1);
            return parameter.ToString();
        }
        #endregion
    }
}
