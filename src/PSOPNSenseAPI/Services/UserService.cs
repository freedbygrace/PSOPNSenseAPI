using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense user API
    /// </summary>
    public class UserService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public UserService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>A list of users</returns>
        public async Task<UserListResponse> GetUsersAsync()
        {
            _logger.Information("Getting users");
            
            var endpoint = "core/user/searchUser";
            return await _apiClient.GetAsync<UserListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a user by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the user</param>
        /// <returns>The user</returns>
        public async Task<UserResponse> GetUserAsync(string uuid)
        {
            _logger.Information($"Getting user with UUID {uuid}");
            
            var endpoint = $"core/user/getUser/{uuid}";
            return await _apiClient.GetAsync<UserResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">The user to create</param>
        /// <returns>The response containing the UUID of the new user</returns>
        public async Task<UserCreateResponse> CreateUserAsync(UserConfig user)
        {
            _logger.Information("Creating user");
            
            var endpoint = "core/user/addUser";
            var data = new { user = user };
            return await _apiClient.PostAsync<UserCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="uuid">The UUID of the user to update</param>
        /// <param name="user">The updated user</param>
        /// <returns>The response indicating success</returns>
        public async Task<UserUpdateResponse> UpdateUserAsync(string uuid, UserConfig user)
        {
            _logger.Information($"Updating user with UUID {uuid}");
            
            var endpoint = $"core/user/setUser/{uuid}";
            var data = new { user = user };
            return await _apiClient.PostAsync<UserUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="uuid">The UUID of the user to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<UserDeleteResponse> DeleteUserAsync(string uuid)
        {
            _logger.Information($"Deleting user with UUID {uuid}");
            
            var endpoint = $"core/user/delUser/{uuid}";
            return await _apiClient.PostAsync<UserDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Gets all groups
        /// </summary>
        /// <returns>A list of groups</returns>
        public async Task<GroupListResponse> GetGroupsAsync()
        {
            _logger.Information("Getting groups");
            
            var endpoint = "core/user/searchGroups";
            return await _apiClient.GetAsync<GroupListResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting users
    /// </summary>
    public class UserListResponse
    {
        /// <summary>
        /// Gets or sets the total number of users
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the number of rows per page
        /// </summary>
        [JsonProperty("rowCount")]
        public int RowCount { get; set; }

        /// <summary>
        /// Gets or sets the current page
        /// </summary>
        [JsonProperty("current")]
        public int Current { get; set; }

        /// <summary>
        /// Gets or sets the users
        /// </summary>
        [JsonProperty("rows")]
        public List<User> Rows { get; set; }
    }

    /// <summary>
    /// User information
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the UUID of the user
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the username of the user
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user
        /// </summary>
        [JsonProperty("fullname")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email of the user
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets whether the user is enabled
        /// </summary>
        [JsonProperty("disabled")]
        public string Disabled { get; set; }

        /// <summary>
        /// Gets or sets the groups of the user
        /// </summary>
        [JsonProperty("groups")]
        public string Groups { get; set; }
    }

    /// <summary>
    /// Response for getting a user
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Gets or sets the user
        /// </summary>
        [JsonProperty("user")]
        public UserDetail User { get; set; }
    }

    /// <summary>
    /// User details
    /// </summary>
    public class UserDetail
    {
        /// <summary>
        /// Gets or sets the username of the user
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user
        /// </summary>
        [JsonProperty("fullname")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email of the user
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets whether the user is enabled
        /// </summary>
        [JsonProperty("disabled")]
        public string Disabled { get; set; }

        /// <summary>
        /// Gets or sets the groups of the user
        /// </summary>
        [JsonProperty("groups")]
        public List<string> Groups { get; set; }

        /// <summary>
        /// Gets or sets the authorizations of the user
        /// </summary>
        [JsonProperty("authorizations")]
        public List<string> Authorizations { get; set; }
    }

    /// <summary>
    /// User configuration
    /// </summary>
    public class UserConfig
    {
        /// <summary>
        /// Gets or sets the username of the user
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user
        /// </summary>
        [JsonProperty("fullname")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email of the user
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets whether the user is enabled
        /// </summary>
        [JsonProperty("disabled")]
        public string Disabled { get; set; } = "0";

        /// <summary>
        /// Gets or sets the groups of the user
        /// </summary>
        [JsonProperty("groups")]
        public List<string> Groups { get; set; }

        /// <summary>
        /// Gets or sets the authorizations of the user
        /// </summary>
        [JsonProperty("authorizations")]
        public List<string> Authorizations { get; set; }
    }

    /// <summary>
    /// Response for creating a user
    /// </summary>
    public class UserCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the new user
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response for updating a user
    /// </summary>
    public class UserUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for deleting a user
    /// </summary>
    public class UserDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for getting groups
    /// </summary>
    public class GroupListResponse
    {
        /// <summary>
        /// Gets or sets the total number of groups
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the number of rows per page
        /// </summary>
        [JsonProperty("rowCount")]
        public int RowCount { get; set; }

        /// <summary>
        /// Gets or sets the current page
        /// </summary>
        [JsonProperty("current")]
        public int Current { get; set; }

        /// <summary>
        /// Gets or sets the groups
        /// </summary>
        [JsonProperty("rows")]
        public List<Group> Rows { get; set; }
    }

    /// <summary>
    /// Group information
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Gets or sets the UUID of the group
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the name of the group
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the group
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the scope of the group
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets whether the group is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }
    }
}
