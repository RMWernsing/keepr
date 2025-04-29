namespace keepr.Services;

public class VaultKeepsService
{
  public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService, KeepsService keepsService)
  {
    _repository = repository;
    _vaultsService = vaultsService;
    _keepsService = keepsService;
  }
  private readonly VaultKeepsRepository _repository;
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, Account userInfo)
  {
    Keep keep = _keepsService.GetKeepById(vaultKeepData.KeepId);

    Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId);
    if (vault.CreatorId != userInfo.Id)
    {
      throw new Exception("YOU CANNOT ADD A KEEP TO SOMEONE ELSES VAULT!");
    }

    _keepsService.IncreaseKept(keep);

    VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
    return vaultKeep;
  }

  internal List<VaultKeepKeep> GetKeepsInPublicVault(int vaultId, Account userInfo)
  {
    Vault vault = _vaultsService.GetVaultById(vaultId);

    if (vault.IsPrivate == true && vault.CreatorId != userInfo.Id)
    {
      throw new Exception("Invalid id: " + vaultId);
    }
    List<VaultKeepKeep> keeps = _repository.GetKeepsInPublicVault(vaultId);
    return keeps;
  }

  internal void DelteVaultKeep(int vaultKeepId, Account userInfo)
  {

    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);

    Keep keep = _keepsService.GetKeepById(vaultKeep.KeepId);

    _keepsService.DecreaseKept(keep);

    if (vaultKeep.CreatorId != userInfo.Id)
    {
      throw new Exception("YOU DO NOT HAVE PERMISSION TO DELETE SOMEONE ELSES KEEP FROM THEIR VAULT");
    }
    _repository.DeleteVaultKeep(vaultKeepId);

  }

  public VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception("Invalid id: " + vaultKeepId);
    }
    return vaultKeep;
  }

  internal VaultKeepKeep GetVaultKeepKeepById(int vaultKeepId)
  {
    VaultKeepKeep keep = _repository.GetVaultKeepKeepById(vaultKeepId);
    return keep;
  }
}