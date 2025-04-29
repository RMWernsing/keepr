



namespace keepr.Repositories;

public class VaultKeepsRepository
{
  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
    INSERT INTO 
    vault_keeps(keep_id, vault_id, creator_id)
    VALUES(@KeepId, @VaultId, @CreatorId);

    SELECT * FROM vault_keeps WHERE id = LAST_INSERT_ID();";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).SingleOrDefault();
    return vaultKeep;
  }

  internal List<VaultKeepKeep> GetKeepsInPublicVault(int vaultId)
  {
    string sql = @"
    SELECT 
    keeps.*,
    vault_keeps.*,
    accounts.*
    FROM vault_keeps
    INNER JOIN keeps ON keeps.id = vault_keeps.keep_id
    INNER JOIN accounts on accounts.id = vault_keeps.creator_id
    WHERE vault_keeps.vault_id = @vaultId;";

    List<VaultKeepKeep> keeps = _db.Query(sql, (VaultKeepKeep keep, VaultKeep vaultKeep, Profile account) =>
    {
      keep.VaultKeepId = vaultKeep.Id;
      keep.Creator = account;
      return keep;
    }, new { vaultId }).ToList();
    return keeps;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = @"SELECT * FROM vault_keeps WHERE id = @vaultKeepId;";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).SingleOrDefault();
    return vaultKeep;
  }

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    string sql = "DELETE FROM vault_keeps WHERE id = @vaultKeepId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { vaultKeepId });
    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " rows were affected and that is bad");
    }
  }

  internal VaultKeepKeep GetVaultKeepKeepById(int vaultKeepId)
  {
    string sql = @"
    SELECT 
    keeps.*,
    vault_keeps.*,
    accounts.*
    FROM vault_keeps
    INNER JOIN keeps ON keeps.id = vault_keeps.keep_id
    INNER JOIN accounts on accounts.id = vault_keeps.creator_id
    WHERE vault_keeps.vault_id = @vaultKeepId;";

    VaultKeepKeep keep = _db.Query(sql, (VaultKeepKeep keep, VaultKeep vaultKeep, Profile account) =>
    {
      keep.VaultKeepId = vaultKeep.Id;
      keep.Creator = account;
      return keep;
    }, new { vaultKeepId }).SingleOrDefault();
    return keep;
  }
}