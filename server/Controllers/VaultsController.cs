namespace keepr.Controllers;

public class VaultsController
{
  public VaultsController(VaultsService vaultsService)
  {
    _vaultsService = vaultsService;
  }
  private readonly VaultsService _vaultsService;

}