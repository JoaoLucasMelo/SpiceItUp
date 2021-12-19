using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using SpiceItUp.Models;

namespace SpiceItUp.Repositories
{
  public class FavoritesRepository
  {
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Favorite> Get(string id)
    {
      string sql = @"
      SELECT * FROM favorites WHERE accountId = @id;
      ;";
      return _db.Query<Favorite>(sql, new { id }).ToList();
    }

    internal Favorite Create(Favorite newFavorite)
    {
      string sql = @"
      INSERT INTO favorites
      (accountId, recipeId)
      VALUES
      (@AccountId, @RecipeId);
      SELECT LAST_INSERT_ID();
      ;";
      int id = _db.ExecuteScalar<int>(sql, newFavorite);
      newFavorite.Id = id;
      return newFavorite;
    }

    internal void Remove(int id)
    {
      string sql = @"
      DELETE FROM favorites WHERE id = @id LIMIT 1
      ;";
      _db.Execute(sql, new { id });
    }
  }
}