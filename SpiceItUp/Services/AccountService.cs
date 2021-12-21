using SpiceItUp.Models;
using SpiceItUp.Repositories;

namespace SpiceItUp.Services
{
  public class AccountService
  {
    private readonly AccountsRepository _repo;
    public AccountService(AccountsRepository repo)
    {
      _repo = repo;
    }

    internal string GetProfileEmailById(string id)
    {
      return _repo.GetById(id).Email;
    }
    internal Account GetProfileByEmail(string email)
    {
      return _repo.GetByEmail(email);
    }
    internal Account GetOrCreateProfile(Account userInfo)
    {
      Account profile = _repo.GetById(userInfo.Id);
      if (profile == null)
      {
        return _repo.Create(userInfo);
      }
      return profile;
    }

    internal Account Edit(Account editData, string userEmail)
    {
      Account original = GetProfileByEmail(userEmail);
      original.Name = editData.Name != null ? editData.Name : original.Name;
      original.Picture = editData.Picture != null ? editData.Picture : original.Picture;
      return _repo.Edit(original);
    }
  }
}