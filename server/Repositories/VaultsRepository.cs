

namespace keepr.Repositories;

public class VaultsRepository
{
  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"
    INSERT INTO 
    vaults(name, description, img, is_private, creator_id)
    VALUES(@Name, @Description, @Img, @IsPrivate, @CreatorId);

    SELECT 
    vaults.*,
    accounts.*
    FROM vaults
    INNER JOIN accounts ON accounts.id = vaults.creator_id
    WHERE vaults.id = LAST_INSERT_ID();";

    Vault vault = _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, vaultData).SingleOrDefault();
    return vault;
  }

  internal Vault GetVaultByid(int vaultId)
  {
    string sql = @"
    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    INNER JOIN accounts ON accounts.id = vaults.creator_id
    WHERE vaults.id = @vaultId;";

    Vault vault = _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { vaultId }).SingleOrDefault();
    return vault;
  }
}