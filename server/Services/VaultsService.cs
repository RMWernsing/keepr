


namespace keepr.Services;

public class VaultsService
{
  public VaultsService(VaultsRepository repository)
  {
    _repository = repository;
  }
  private readonly VaultsRepository _repository;

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repository.CreateVault(vaultData);
    return vault;
  }

  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _repository.GetVaultByid(vaultId);

    if (vault == null)
    {
      throw new Exception("Invalid ID: " + vaultId);
    }
    return vault;
  }

  internal Vault EditVault(int vaultId, Vault vaultData, Account userInfo)
  {
    Vault vault = GetVaultById(vaultId);

    if (vault.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT EDIT SOMEONE ELSES VAULT, {userInfo.Name.ToUpper()}!!!");
    }
    vault.Name = vaultData.Name ?? vault.Name;
    vault.Description = vaultData.Description ?? vault.Description;
    vault.Img = vaultData.Img ?? vault.Img;
    vault.IsPrivate = vaultData.IsPrivate ?? vault.IsPrivate;

    _repository.EditVault(vault);
    return vault;

  }

  internal string DeleteVault(int vaultId, Account userInfo)
  {
    Vault vault = GetVaultById(vaultId);

    if (vault.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT DELETE SOMEONE ELSES VAULT, {userInfo.Name.ToUpper()}!!!");
    }

    _repository.DeleteVault(vaultId);
    return vault.Name + " has been deleted";
  }
}