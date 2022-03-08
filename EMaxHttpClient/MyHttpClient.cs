namespace EMaxHttpClient
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using EMaxDevTest.Core;

    public static class MyHttpClient
    {
        public static HttpClient client = new HttpClient();
        public static string CreateResult { get; set; }
        public static Customer GetByIdResult { get; set; }
        public static IEnumerable<Customer> GetByCriteriaResult { get; set; }
        public static Customer UpdateCustomerResult { get; set; }


        public static async Task<string> CreateCustomerAsync(Customer customer)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("api/customer", customer);
            responseMessage.EnsureSuccessStatusCode();

            return responseMessage.Headers.Location.ToString();
        }

        public static async Task<Customer> GetCustomerAsync(Guid id)
        {
            Customer customer = null;
            HttpResponseMessage response = await client.GetAsync($"api/customer/{id}");
            if (response.IsSuccessStatusCode)
            {
                customer = await response.Content.ReadAsAsync<Customer>();
            }
            return customer;
        }

        public static async Task<IEnumerable<Customer>> GetCustomerBySearchCriteria(SearchCriteria searchCriteria)
        {
            IEnumerable<Customer> customer = null;
            var request = "https://localhost:5306/api/customer?";

            if (string.IsNullOrEmpty(searchCriteria.Forename))
            {
                request += $"forename={searchCriteria.Forename}";
            }

            if (string.IsNullOrEmpty(searchCriteria.Surname))
            {
                if (request.EndsWith('?'))
                {
                    request += $"surname={searchCriteria.Surname}";
                }
                else
                {
                    request += $"&surname={searchCriteria.Surname}";
                }
            }

            if (string.IsNullOrEmpty(searchCriteria.PostCode))
            {
                if (request.EndsWith('?'))
                {
                    request += $"postcode={searchCriteria.PostCode}";
                }
                else
                {
                    request += $"&postcode={searchCriteria.PostCode}";
                }
            }

            if (string.IsNullOrEmpty(searchCriteria.EmailAddress))
            {
                if (request.EndsWith('?'))
                {
                    request += $"emailaddress={searchCriteria.EmailAddress}";
                }
                else
                {
                    request += $"&emailaddress={searchCriteria.EmailAddress}";
                }
            }

            HttpResponseMessage responseMessage = await client.GetAsync(request.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                customer = await responseMessage.Content.ReadAsAsync<IEnumerable<Customer>>();
            }

            return customer;
        }

        public static async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/", customer);
            response.EnsureSuccessStatusCode();

            customer = await response.Content.ReadAsAsync<Customer>();
            return customer;
        }

        public static async Task<HttpStatusCode> DeleteCustomerAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/customer/{id}");
            return response.StatusCode;
        }

        public static async Task RunAsync(Customer customer)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                CreateResult = await CreateCustomerAsync(customer);

                // Get the product
                GetByIdResult = await GetCustomerAsync(customer.CustomerId);

                // Update the product
                customer.Forename = "Mia";

                UpdateCustomerResult = await UpdateCustomerAsync(customer);

                // Get the updated product

                // Delete the product
                await DeleteCustomerAsync(customer.CustomerId.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}