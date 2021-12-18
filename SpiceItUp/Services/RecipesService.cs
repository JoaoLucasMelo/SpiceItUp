using System;
using System.Collections.Generic;
using SpiceItUp.Models;
using SpiceItUp.Repositories;

namespace SpiceItUp.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
      _repo = repo;
    }

    internal List<Recipe> Get()
    {
      return _repo.Get();
    }
    internal Recipe Get(int id)
    {
      return _repo.Get(id);
    }

    internal Recipe Create(Recipe newRecipe)
    {
      _repo.Create(newRecipe);
      return newRecipe;
    }

    internal Recipe Edit(Recipe updateRecipe)
    {
      Recipe oldRecipe = Get(updateRecipe.Id);
      oldRecipe.Title = updateRecipe.Title != null ? updateRecipe.Title : oldRecipe.Title;
      oldRecipe.Category = updateRecipe.Category != null ? updateRecipe.Category : oldRecipe.Category;
      oldRecipe.Subtitle = updateRecipe.Subtitle != null ? updateRecipe.Subtitle : oldRecipe.Subtitle;
      return _repo.Edit(oldRecipe);


    }

    internal void Remove(int id, string userId)
    {
      Recipe recipe = Get(id);
      if (recipe.CreatorId != userId)
      {
        throw new Exception("Not Authorized");
      }
      _repo.Remove(id);
    }

  }
}