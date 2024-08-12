//using Department.DataAcess.Dto;
using Dept.DataAcess.Dto;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shop.DataAccess.DTOs;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Dept.DataAcess.Models;
using AutoMapper;

namespace Shop.Service.IService.Services
{
    public class DepartmentServiceClient : IDepartmentServiceClient
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ILogger<DepartmentServiceClient> _logger;

        public DepartmentServiceClient(IHttpClientFactory clientFactory, ILogger<DepartmentServiceClient> logger, IMapper mapper)
        {
            _mapper = mapper;
            _clientFactory = clientFactory;
            _logger = logger;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _mapper = mapper;
        }

        public async Task<DepartmentDto> GetDepartmentByGuidAsync(Guid departmentGuid)
        {
            var httpClient = _clientFactory.CreateClient("MicroserviceClient");
            string apiUrl = $"api/department/GetByGuid?departmentGuid={departmentGuid}";

            using (var response = await httpClient.GetAsync(apiUrl))
            {
                response.EnsureSuccessStatusCode();
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var departmentDto = await JsonSerializer.DeserializeAsync<DepartmentDto>(responseStream, _jsonOptions);
                    return departmentDto;
                }
            }
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var httpClient = _clientFactory.CreateClient("MicroserviceClient");
            string apiUrl = $"api/department";
            // Send the GET request asynchronously
            using (var response = await httpClient.GetAsync(apiUrl))
            {

                // Ensure the response is successful (status code in the 200-299 range)
                response.EnsureSuccessStatusCode();

                // Read the content from the response body as a stream
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    // Deserialize the JSON stream into a DepartmentDto object
                    var departmentDto = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<DepartmentDto>>(responseStream, _jsonOptions);

                    return departmentDto;
                }
            }
        }
        /*
         * here this method only get Department 7 tables with the help of create http client
         * but we need to acces both tables data using Guid...
         */
        public async Task<IEnumerable<AllTablesDto>> GetSevenTables()
        {
            var httpClient = _clientFactory.CreateClient("MicroserviceClient");
            string apiUrl = $"api/department/Get_seven_tables";
            // Send the GET request asynchronously
            using (var response = await httpClient.GetAsync(apiUrl))
            {
                // Ensure the response is successful (status code in the 200-299 range)
                response.EnsureSuccessStatusCode();

                // Read the content from the response body as a stream
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    // Deserialize the JSON stream into a DepartmentDto object
                    var departmentDto = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<AllTablesDto>>(responseStream, _jsonOptions);

                    return departmentDto;
                }
            }
        }
        //add departm,ent is not working------------------------
        public async Task<IEnumerable<UserDepartmentDto>> GetUsersWithDepartmentsAsync()
        {
            var httpClient = _clientFactory.CreateClient("MicroserviceClient");
            string apiUrl = $"api/department/users-with-departments";

            using (var response = await httpClient.GetAsync(apiUrl))
            {
                response.EnsureSuccessStatusCode();
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var userDepartments = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<UserDepartmentDto>>(responseStream, _jsonOptions);
                    return userDepartments;
                }
            }
        }
        public async Task<IEnumerable<AllTablesDto>> Get_all_tables()
        {
            var httpClient = _clientFactory.CreateClient("MicroserviceClient");
            string apiUrl = $"api/Department/Get_seven_tables";

            using (var response = await httpClient.GetAsync(apiUrl))
            {
                response.EnsureSuccessStatusCode();
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var custDept = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<AllTablesDto>>(responseStream, _jsonOptions);
                    return custDept;
                }
            }
        }
        public async Task<DepartmentDto> AddDepartmentAsync(DepartmentDto departmentDto)
        {
            var httpClient = _clientFactory.CreateClient("MicroserviceClient");
            string apiUrl = $"api/department";

            // Serialize the DepartmentDto object to JSON
            var departmentJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(departmentDto), Encoding.UTF8, "application/json");

            // Send the POST request asynchronously
            using (var response = await httpClient.PostAsync(apiUrl, departmentJson))
            {
                // Ensure the response is successful (status code in the 200-299 range)
                response.EnsureSuccessStatusCode();

                // Optional: Read the response content if needed
                var responseContent = await response.Content.ReadAsStringAsync();
                var addedDepartment = System.Text.Json.JsonSerializer.Deserialize<DepartmentDto>(responseContent, _jsonOptions);
                return addedDepartment;
            }
        }

        public async Task<SimpleCustomerDto> Add_Customer(SimpleCustomerDto customerDto)
        {
            var httpClient = _clientFactory.CreateClient("MicroserviceClient");
            string apiUrl = $"api/Customer";

            //// Serialize the DepartmentDto object to JSON
            //var customertJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(customerDto), Encoding.UTF8, "application/json");

            //// Send the POST request asynchronously
            //using (var response = await httpClient.PostAsync(apiUrl, customertJson))
            //{
            //    // Ensure the response is successful (status code in the 200-299 range)
            //    response.EnsureSuccessStatusCode();

            //    // Optional: Read the response content if needed
            //    var responseContent = await response.Content.ReadAsStringAsync();
            //    var addedCustomer = System.Text.Json.JsonSerializer.Deserialize<SimpleCustomerDto>(responseContent, _jsonOptions);
            //    return addedCustomer;
            //}

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(customerDto), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to add customer. Status Code: {StatusCode}, Response: {Response}", response.StatusCode, responseContent);
                    response.EnsureSuccessStatusCode();
                }

                // Log raw response content
                var responseContentString = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Response content: {ResponseContent}", responseContentString);

                if (string.IsNullOrWhiteSpace(responseContentString))
                {
                    throw new JsonException("Response content is empty or null.");
                }

                var customerResponse = JsonSerializer.Deserialize<SimpleCustomerDto>(responseContentString, _jsonOptions);
                if (customerResponse == null)
                {
                    throw new JsonException("Deserialization resulted in a null object.");
                }
                return customerResponse;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "An error occurred while sending the request.");
                throw new Exception("An error occurred while adding the customer. Please try again later.");
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "An error occurred while deserializing the response.");
                throw new Exception("An error occurred while processing the response from the server.");
            }

        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var httpClient = _clientFactory.CreateClient("MicroserviceClient");
            string apiUrl = $"api/department";
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(departmentDto), Encoding.UTF8, "application/json");
            // Send the POST request asynchronously
            using (var response = await httpClient.PostAsync(apiUrl, content))
            {
                // Ensure the response is successful (status code in the 200-299 range)
                response.EnsureSuccessStatusCode();

                // Optional: Read the response content if needed
                var responseContent = await response.Content.ReadAsStringAsync();
                var addedDepartment = System.Text.Json.JsonSerializer.Deserialize<DepartmentDto>(responseContent, _jsonOptions);
                return addedDepartment;
            }
        }
        public async Task<Cust_Add_OrderDto> Create_cust_address_order(Cust_Add_OrderDto cust_Add_OrderDto)
        {
            var httpclinet = _clientFactory.CreateClient("MicroserviceClient");
            string apiUrl = $"api/Address/Insert_4_tables";
            var contetnt = new StringContent(System.Text.Json.JsonSerializer.Serialize(cust_Add_OrderDto), Encoding.UTF8, "application/json");
            using (var response = await httpclinet.PostAsync(apiUrl, contetnt))
            {
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var add_all_tables = System.Text.Json.JsonSerializer.Deserialize<Cust_Add_OrderDto>(responseContent, _jsonOptions);
                return add_all_tables;
            }

        }
        public async Task Insert_customer_with_Department(Dept_CustDto dept_CustDto)
        {
            var httpClient = _clientFactory.CreateClient("MicroserviceClient");
            string apiUrl = $"api/Address/Insert_into_7tables";

            var customerJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(dept_CustDto), Encoding.UTF8, "application/json");

            try
            {
                using (var response = await httpClient.PostAsync(apiUrl, customerJson))
                {
                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Log the response content
                    Console.WriteLine("Response Content: " + responseContent);

                    // Deserialize the JSON response to Dept_CustDto
                    var addCust = System.Text.Json.JsonSerializer.Deserialize<Dept_CustDto>(responseContent, _jsonOptions);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                // Log the exception details
                Console.WriteLine($"Request error: {httpRequestException.Message}");
                // Optionally, you can log the inner exception if present
                if (httpRequestException.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {httpRequestException.InnerException.Message}");
                }
            }
            catch (System.Text.Json.JsonException jsonException)
            {
                // Log the exception details
                Console.WriteLine($"JSON Deserialization error: {jsonException.Message}");
            }
            catch (Exception ex)
            {
                // Log any other exception details
                Console.WriteLine($"Unexpected error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }

    }
}

