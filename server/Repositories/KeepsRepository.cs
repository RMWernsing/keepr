



namespace keepr.Repositories;

public class KeepsRepository
{
  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
    INSERT INTO
    keeps(name, description, img, views, creator_id)
    VALUES(@Name, @Description, @Img, @Views, @CreatorId);
    
    SELECT 
    keeps.*,
    accounts.*
    FROM keeps
    INNER JOIN accounts ON accounts.id = keeps.creator_id
    WHERE keeps.id = LAST_INSERT_ID();";

    Keep createdKeep = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }, keepData).SingleOrDefault();
    return createdKeep;
  }

  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT 
    keeps.*,
    accounts.*
    FROM keeps
    INNER JOIN accounts on accounts.id = keeps.creator_id;";

    List<Keep> keep = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keep;
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
    SELECT 
    keeps.*,
    accounts.*
    FROM keeps
    INNER JOIN accounts on accounts.id = keeps.creator_id
    WHERE keeps.id = @keepId;";

    Keep keep = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { keepId }).SingleOrDefault();
    return keep;
  }

  internal void EditKeep(Keep keep)
  {
    string sql = @"
    UPDATE keeps
    SET
    name = @Name,
    description = @Description,
    img = @Img,
    views = @Views
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, keep);
    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " rows were affected and thats bad.");
    }
  }

  internal void DeleteKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { keepId });

    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " rows were affected and thats really bad");
    }
  }

  internal void IncreaseVisits(Keep keep)
  {
    string sql = @"
    UPDATE keeps 
    SET views = @Views
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, keep);

    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " rows were affected and thats bad");
    }
  }
}