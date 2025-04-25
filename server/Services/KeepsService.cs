
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
}