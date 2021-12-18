using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using SpiceItUp.Models;

namespace SpiceItUp.Repositories
{
  public class IngredientsRepository
  {
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Ingredient> Get()
    {
      string sql = "SELECT * FROM ingredients;";
      return _db.Query<Ingredient>(sql).ToList();
    }
    internal Ingredient Get(int id)
    {
      string sql = "SELECT * FROM ingredients WHERE id = @id;";
      return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
    }
    internal Ingredient Create(Ingredient newIngredient)
    {
      string sql = @"
      INSERT INTO ingredients
      (name, quantity, recipeId)
      VALUES
      (@Name, @Quantity, @RecipeId);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newIngredient);
      newIngredient.Id = id;
      return newIngredient;
    }

    internal List<Ingredient> GetByRecipe(int id)
    {
      string sql = @"
      SELECT * FROM ingredients WHERE ingredients.recipeId = @id
      ;";
      return _db.Query<Ingredient>(sql, new { id }).ToList();
    }


    internal Ingredient Edit(Ingredient updatedIngredient)
    {
      string sql = @"
      UPDATE ingredients
      SET
      name = @Name,
      Quantity = @Quantity
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, updatedIngredient);
      if (rows <= 0)
      {
        throw new Exception("Ingredient not updated!");
      }
      return updatedIngredient;
    }
    internal void Remove(int id)
    {
      string sql = @"
      DELETE FROM ingredients WHERE id = @id LIMIT 1
      ;";
      _db.Execute(sql, new { id });
    }
  }
}