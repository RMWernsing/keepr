

namespace keepr.Services;

public class ProfilesService
{
  public ProfilesService(ProfilesRepository profilesRepository)
  {
    _repository = profilesRepository;
  }
  private readonly ProfilesRepository _repository;

  internal Profile GetProfileById(string profileId)
  {
    Profile profile = _repository.GetProfileById(profileId);
    return profile;
  }

  internal List<Keep> GetKeepsForProfile(string profileId)
  {
    List<Keep> keeps = _repository.GetKeepsForProfile(profileId);
    return keeps;
  }
}