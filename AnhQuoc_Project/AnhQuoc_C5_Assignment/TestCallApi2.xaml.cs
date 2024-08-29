using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for TestCallApi2.xaml
    /// </summary>
    public partial class TestCallApi2 : Window
    {
        static HttpClient client = new HttpClient();
        static string objectName = "Users";
        static string localHost = "http://localhost:7287/";

        static void ShowUser(User user)
        {
            Debug.WriteLine($"Username: {user.Username}\tPass: " +
                $"{user.Password}\tNote: {user.Note}\tStatus: {user.Status}");
        }

        static async Task<Uri> CreateUserAsync(User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/{objectName}", user);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<User> GetUserAsync(string path)
        {
            User user = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
        }


        static async Task<User> UpdateUserAsync(User user)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/{objectName}/{user.Id}", user);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated user from the response body.
            user = await response.Content.ReadAsAsync<User>();
            return user;
        }

        static async Task<HttpStatusCode> DeleteUserAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/{objectName}/{id}");
            return response.StatusCode;
        }
        
        public TestCallApi2()
        {
            InitializeComponent();

            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri(localHost);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new user
                User user = new User
                {
                    Username = "James",
                    Password = "J123",
                    Note = "",
                    Status = true,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };

                var url = await CreateUserAsync(user);
                Debug.WriteLine($"Created at {url}");

                // Get the user
                user = await GetUserAsync(url.PathAndQuery);
                ShowUser(user);

                // Update the user
                Debug.WriteLine("Updating pass...");
                user.Password = "new pass";
                await UpdateUserAsync(user);

                // Get the updated user
                user = await GetUserAsync(url.PathAndQuery);
                ShowUser(user);

                // Delete the User
                var statusCode = await DeleteUserAsync(user.Id);
                Debug.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
