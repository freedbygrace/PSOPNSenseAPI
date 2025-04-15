using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense cron API
    /// </summary>
    public class CronService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CronService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public CronService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets all cron jobs
        /// </summary>
        /// <returns>A list of cron jobs</returns>
        public async Task<CronJobListResponse> GetJobsAsync()
        {
            _logger.Information("Getting cron jobs");
            
            var endpoint = "cron/settings/searchJobs";
            return await _apiClient.GetAsync<CronJobListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a cron job by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the cron job</param>
        /// <returns>The cron job</returns>
        public async Task<CronJobResponse> GetJobAsync(string uuid)
        {
            _logger.Information($"Getting cron job with UUID {uuid}");
            
            var endpoint = $"cron/settings/getJob/{uuid}";
            return await _apiClient.GetAsync<CronJobResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new cron job
        /// </summary>
        /// <param name="job">The cron job to create</param>
        /// <returns>The response containing the UUID of the new cron job</returns>
        public async Task<CronJobCreateResponse> CreateJobAsync(CronJobConfig job)
        {
            _logger.Information("Creating cron job");
            
            var endpoint = "cron/settings/addJob";
            var data = new { job = job };
            return await _apiClient.PostAsync<CronJobCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates a cron job
        /// </summary>
        /// <param name="uuid">The UUID of the cron job to update</param>
        /// <param name="job">The updated cron job</param>
        /// <returns>The response indicating success</returns>
        public async Task<CronJobUpdateResponse> UpdateJobAsync(string uuid, CronJobConfig job)
        {
            _logger.Information($"Updating cron job with UUID {uuid}");
            
            var endpoint = $"cron/settings/setJob/{uuid}";
            var data = new { job = job };
            return await _apiClient.PostAsync<CronJobUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a cron job
        /// </summary>
        /// <param name="uuid">The UUID of the cron job to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<CronJobDeleteResponse> DeleteJobAsync(string uuid)
        {
            _logger.Information($"Deleting cron job with UUID {uuid}");
            
            var endpoint = $"cron/settings/delJob/{uuid}";
            return await _apiClient.PostAsync<CronJobDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Toggles a cron job
        /// </summary>
        /// <param name="uuid">The UUID of the cron job to toggle</param>
        /// <param name="enabled">Whether the cron job should be enabled</param>
        /// <returns>The response indicating success</returns>
        public async Task<CronJobToggleResponse> ToggleJobAsync(string uuid, bool enabled)
        {
            _logger.Information($"Toggling cron job with UUID {uuid} to {(enabled ? "enabled" : "disabled")}");
            
            var endpoint = $"cron/settings/{(enabled ? "enable" : "disable")}Job/{uuid}";
            return await _apiClient.PostAsync<CronJobToggleResponse>(endpoint);
        }

        /// <summary>
        /// Applies cron changes
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<CronApplyResponse> ApplyChangesAsync()
        {
            _logger.Information("Applying cron changes");
            
            var endpoint = "cron/service/reconfigure";
            return await _apiClient.PostAsync<CronApplyResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting cron jobs
    /// </summary>
    public class CronJobListResponse
    {
        /// <summary>
        /// Gets or sets the total number of jobs
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
        /// Gets or sets the jobs
        /// </summary>
        [JsonProperty("rows")]
        public List<CronJob> Rows { get; set; }
    }

    /// <summary>
    /// Cron job
    /// </summary>
    public class CronJob
    {
        /// <summary>
        /// Gets or sets the UUID of the job
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the description of the job
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the command of the job
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the minutes of the job
        /// </summary>
        [JsonProperty("minutes")]
        public string Minutes { get; set; }

        /// <summary>
        /// Gets or sets the hours of the job
        /// </summary>
        [JsonProperty("hours")]
        public string Hours { get; set; }

        /// <summary>
        /// Gets or sets the days of the job
        /// </summary>
        [JsonProperty("days")]
        public string Days { get; set; }

        /// <summary>
        /// Gets or sets the months of the job
        /// </summary>
        [JsonProperty("months")]
        public string Months { get; set; }

        /// <summary>
        /// Gets or sets the weekdays of the job
        /// </summary>
        [JsonProperty("weekdays")]
        public string Weekdays { get; set; }

        /// <summary>
        /// Gets or sets whether the job is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// Response for getting a cron job
    /// </summary>
    public class CronJobResponse
    {
        /// <summary>
        /// Gets or sets the job
        /// </summary>
        [JsonProperty("job")]
        public CronJobDetail Job { get; set; }
    }

    /// <summary>
    /// Cron job details
    /// </summary>
    public class CronJobDetail
    {
        /// <summary>
        /// Gets or sets the description of the job
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the command of the job
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the minutes of the job
        /// </summary>
        [JsonProperty("minutes")]
        public string Minutes { get; set; }

        /// <summary>
        /// Gets or sets the hours of the job
        /// </summary>
        [JsonProperty("hours")]
        public string Hours { get; set; }

        /// <summary>
        /// Gets or sets the days of the job
        /// </summary>
        [JsonProperty("days")]
        public string Days { get; set; }

        /// <summary>
        /// Gets or sets the months of the job
        /// </summary>
        [JsonProperty("months")]
        public string Months { get; set; }

        /// <summary>
        /// Gets or sets the weekdays of the job
        /// </summary>
        [JsonProperty("weekdays")]
        public string Weekdays { get; set; }

        /// <summary>
        /// Gets or sets whether the job is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// Cron job configuration
    /// </summary>
    public class CronJobConfig
    {
        /// <summary>
        /// Gets or sets the description of the job
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the command of the job
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the minutes of the job
        /// </summary>
        [JsonProperty("minutes")]
        public string Minutes { get; set; } = "*";

        /// <summary>
        /// Gets or sets the hours of the job
        /// </summary>
        [JsonProperty("hours")]
        public string Hours { get; set; } = "*";

        /// <summary>
        /// Gets or sets the days of the job
        /// </summary>
        [JsonProperty("days")]
        public string Days { get; set; } = "*";

        /// <summary>
        /// Gets or sets the months of the job
        /// </summary>
        [JsonProperty("months")]
        public string Months { get; set; } = "*";

        /// <summary>
        /// Gets or sets the weekdays of the job
        /// </summary>
        [JsonProperty("weekdays")]
        public string Weekdays { get; set; } = "*";

        /// <summary>
        /// Gets or sets whether the job is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; } = "1";
    }

    /// <summary>
    /// Response for creating a cron job
    /// </summary>
    public class CronJobCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the new job
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response for updating a cron job
    /// </summary>
    public class CronJobUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for deleting a cron job
    /// </summary>
    public class CronJobDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for toggling a cron job
    /// </summary>
    public class CronJobToggleResponse
    {
        /// <summary>
        /// Gets or sets the result of the toggle
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for applying cron changes
    /// </summary>
    public class CronApplyResponse
    {
        /// <summary>
        /// Gets or sets the status of the apply
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
