using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using SpiceItUp.Models;

namespace SpiceItUp.Repositories
{
  public class RecipesRepository
  {
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Recipe> Get()
    {
      string sql = @"SELECT
      r.*,
      a.*
      FROM recipes r
      JOIN accounts a ON r.creatorId = a.id
      ;";
      return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
      {
        recipe.Creator = account;
        return recipe;
      }, splitOn: "id").ToList();
    }


    internal Recipe Get(int id)
    {
      string sql = @"SELECT * FROM recipes WHERE id = @id;";
      return _db.QueryFirstOrDefault<Recipe>(sql, new { id });
    }
    internal Recipe Create(Recipe newRecipe)
    {
      string sql = @"
      INSERT INTO recipes
      (title, subtitle, category, imgUrl, creatorId)
      VALUES
      (@Title, @Subtitle, @Category, @ImgUrl, @CreatorId);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newRecipe);
      newRecipe.Id = id;
      return newRecipe;
    }

    internal Recipe Edit(Recipe updatedRecipe)
    {
      string sql = @"
      UPDATE recipes
      SET
      title = @Title,
      subtitle = @Subtitle,
      category = @Category
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, updatedRecipe);
      if (rows <= 0)
      {
        throw new Exception("Recipe not updated!");
      }
      return updatedRecipe;
    }


    internal void Remove(int id)
    {
      string sql = @"
      DELETE FROM recipes WHERE id = @id LIMIT 1
      ;";
      _db.Execute(sql, new { id });
    }
    internal List<Recipe> Search(string search)
    {
      string sql = @"
      SELECT
      recipe.*,
      account.*
      FROM
      recipes recipe
      JOIN accounts account ON recipe.creatorId = account.id AND recipe.title LIKE @search
      ;";
      return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
              {
                recipe.Creator = account;
                return recipe;
              }, new { search }, splitOn: "id").ToList();
    }
  }
}
