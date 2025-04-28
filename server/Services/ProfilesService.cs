



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

  private List<Vault> GetVaultsForProfile(string profileId)
  {
    List<Vault> vaults = _repository.GetVaultsForProfile(profileId);
    return vaults;
  }

  // NOTE overload method
  internal List<Vault> GetVaultsForProfile(string profileId, Account userInfo)
  {
    List<Vault> vaults = GetVaultsForProfile(profileId);

    return vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userInfo?.Id);
  }
}