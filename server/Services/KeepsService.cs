



namespace keepr.Services;

public class KeepsService
{
  public KeepsService(KeepsRepository keepsRepository)
  {
    _repository = keepsRepository;
  }
  private readonly KeepsRepository _repository;

  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _repository.CreateKeep(keepData);
    return keep;
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAllKeeps();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repository.GetKeepById(keepId);
    if (keep == null)
    {
      throw new Exception("Invalid Id: " + keepId);
    }
    return keep;
  }

  internal Keep EditKeep(Keep keepData, int keepId, Account userInfo)
  {
    Keep keep = GetKeepById(keepId);

    if (keep.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT EDIT ANOTHER PERSONS KEEP, {userInfo.Name.ToUpper()}!");
    }

    keep.Name = keepData.Name ?? keep.Name;
    keep.Description = keepData.Description ?? keep.Description;
    keep.Img = keepData.Img ?? keep.Img;
    // keep.Views = keepData.Views ?? keep.Views;

    _repository.EditKeep(keep);
    return keep;

  }

  internal string DeleteKeep(int keepId, Account userInfo)
  {
    Keep keep = GetKeepById(keepId);

    if (keep.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT DELETE SOMEONE ELSES KEEP, {userInfo.Name.ToUpper()}!!!");
    }

    _repository.DeleteKeep(keepId);
    return keep.Name + " has been deleted.";
  }

  internal Keep IncreaseViews(int keepId)
  {
    Keep keep = GetKeepById(keepId);

    keep.Views++;
    _repository.IncreaseVisits(keep);
    return keep;
  }

  internal void IncreaseKept(Keep keep)
  {
    keep.Kept++;
    _repository.IncreaseKept(keep);
  }

  internal void DecreaseKept(Keep keep)
  {
    keep.Kept--;
    _repository.DecreaseKept(keep);
  }
}