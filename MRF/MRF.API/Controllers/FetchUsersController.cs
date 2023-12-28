using Microsoft.AspNetCore.Mvc;
using MRF.Utility;

// In your Controller
public class FetchUsersController : ControllerBase
{
    private readonly AzureADService _azureADService;

    public FetchUsersController(AzureADService azureADService)
    {
        _azureADService = azureADService;
    }

    [HttpGet("users")]
    public async Task<ActionResult<string>> GetUsers()
    {
        string accessToken = await _azureADService.GetAccessToken();
        string users = await _azureADService.GetUsers(accessToken);

        return Ok(users);
    }

    [HttpGet("groups")]
    public async Task<ActionResult<string>> GetGroups()
    {
        string accessToken = await _azureADService.GetAccessToken();
        string groups = await _azureADService.GetGroups(accessToken);

        return Ok(groups);
    }
}
