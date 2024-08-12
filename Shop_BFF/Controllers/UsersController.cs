using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Shop_BFF.DTOs;
using Shop_BFF.DTOs.usersInfo;
using System.Text;
using System.Text.Json;

namespace Shop_BFF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ShopApiClient");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UsersInfoDto>>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("/api/UsersInfo/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<UsersInfoDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (users != null)
                {
                    // Order users by UserName
                    var orderedUsers = users.OrderBy(u => u.UserName).ToList();
                    return Ok(orderedUsers);
                }
                return StatusCode(500, "Error deserializing the response");
            }

            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<UsersInfoDto>> GetById(Guid usersInfoGuid)
        {
            var response = await _httpClient.GetAsync($"/api/UsersInfo/GetByGuId?usersInfoGuid={usersInfoGuid}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<UsersInfoDto>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound();
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            //var message = new MessageDto(linkCandidateToTenantOperation, Constants.InsertMessage, Constants.NotInsertMessage);
            //message.Record = candidateDto;

            //return message;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            var json = JsonSerializer.Serialize(userDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/UsersInfo/CreateUser", content);

            if (response.IsSuccessStatusCode)
            {
                return Ok("User successfully added to Database");
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UsersInfoDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest("User ID mismatch");
            }

            var json = JsonSerializer.Serialize(userDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/UsersInfo/UpdateUser?id={id}", content);

            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/UsersInfo/DeleteUser?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
