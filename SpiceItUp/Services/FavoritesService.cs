using System.Collections.Generic;
using SpiceItUp.Models;
using SpiceItUp.Repositories;

namespace SpiceItUp.Services
{
  public class FavoritesService
  {
    private readonly FavoritesRepository _repo;

    public FavoritesService(FavoritesRepository repo)
    {
      _repo = repo;
    }

    internal List<Favorite> Get(string id)
    {
      return _repo.Get(id);
    }

    internal Favorite Create(Favorite newFavorite)
    {
      return _repo.Create(newFavorite);
    }

    internal void Remove(int id)
    {
      _repo.Remove(id);
    }
  }
}