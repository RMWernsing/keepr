namespace keepr.Services;

public class VaultKeepsService
{
  public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
  {
    _repository = repository;
    _vaultsService = vaultsService;
  }
  private readonly VaultKeepsRepository _repository;
  private readonly VaultsService _vaultsService;

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
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
    if (vaultKeep.CreatorId != userInfo.Id)
    {
      throw new Exception("YOU DO NOT HAVE PERMISSION TO DELETE SOMEONE ELSES KEEP FROM THEIR VAULT");
    }
    _repository.DeleteVaultKeep(vaultKeepId);

  }

  private VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null)
    {
      throw new Exception("Invalid id: " + vaultKeepId);
    }
    return vaultKeep;
  }
}