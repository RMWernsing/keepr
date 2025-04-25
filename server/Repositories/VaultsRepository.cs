


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

  internal void EditVault(Vault vault)
  {
    string sql = @"
    UPDATE vaults
    SET 
    name = @Name,
    description = @Description,
    img = @Img,
    is_private = @IsPrivate
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, vault);

    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " rows were affected and that is bad");
    }
  }

  internal void DeleteVault(int vaultId)
  {
    string sql = "DELETE FROM vaults WHERE id = @vaultId";

    int rowsAffected = _db.Execute(sql, new { vaultId });

    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " rows have been affected and thats very bad");
    }
  }
}