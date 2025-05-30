namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider)
  {
    _keepsService = keepsService;
    _auth0Provider = auth0Provider;
  }
  private readonly KeepsService _keepsService;
  private readonly Auth0Provider _auth0Provider;

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep keep = _keepsService.CreateKeep(keepData);
      return Ok(keep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _keepsService.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try
    {
      // Keep keep = _keepsService.GetKeepById(keepId);
      Keep keep = _keepsService.IncreaseViews(keepId);
      return Ok(keep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPut("{keepId}")]
  public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep keepData, int keepId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Keep keep = _keepsService.EditKeep(keepData, keepId, userInfo);
      return Ok(keep);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }

  }

  [Authorize]
  [HttpDelete("{keepId}")]
  public async Task<ActionResult<string>> DeleteKeep(int keepId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _keepsService.DeleteKeep(keepId, userInfo);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}