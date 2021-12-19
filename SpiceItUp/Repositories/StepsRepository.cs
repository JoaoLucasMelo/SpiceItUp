using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using SpiceItUp.Models;

namespace SpiceItUp.Repositories
{
  public class StepsRepository
  {
    private readonly IDbConnection _db;

    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Step> Get()
    {
      string sql = "SELECT * FROM steps;";
      return _db.Query<Step>(sql).ToList();
    }

    internal List<Step> GetByStep(int id)
    {
      string sql = @"
      SELECT * FROM steps WHERE steps.recipeId = @id
      ;";
      return _db.Query<Step>(sql, new { id }).ToList();
    }

    internal Step Create(Step newStep)
    {
      string sql = @"
      INSERT INTO steps
      (body, stepNumber, recipeId)
      VALUES
      (@Body, @StepNumber, @RecipeId);
      SELECT LAST_INSERT_ID();
      ;";
      int id = _db.ExecuteScalar<int>(sql, newStep);
      newStep.Id = id;
      return newStep;
    }

    internal Step Get(int id)
    {
      string sql = @"
      SELECT * FROM steps WHERE id = @id
      ;";
      return _db.QueryFirstOrDefault<Step>(sql, new { id });
    }

    internal Step Edit(Step updatedStep)
    {
      string sql = @"
      UPDATE steps
      SET
      body = @Body,
      stepNumber = @StepNumber
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, updatedStep);
      if (rows <= 0)
      {
        throw new Exception("Step not updated!");
      }
      return updatedStep;
    }

    internal void Remove(int id)
    {
      string sql = @"
      DELETE FROM steps WHERE id = @id LIMIT 1
      ;";
      _db.Execute(sql, new { id });
    }
  }
}