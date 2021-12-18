using System.Collections.Generic;
using SpiceItUp.Models;
using SpiceItUp.Repositories;

namespace SpiceItUp.Services
{
  public class IngredientsService
  {
    private readonly IngredientsRepository _repo;

    public IngredientsService(IngredientsRepository repo)
    {
      _repo = repo;
    }

    internal List<Ingredient> Get()
    {
      return _repo.Get();
    }
    internal Ingredient Get(int id)
    {
      return _repo.Get(id);
    }

    internal Ingredient Create(Ingredient newIngredient)
    {
      return _repo.Create(newIngredient);
    }

    internal List<Ingredient> GetByRecipe(int id)
    {
      return _repo.GetByRecipe(id);
    }

    internal Ingredient Edit(Ingredient updateIngredient)
    {
      Ingredient oldIngredient = Get(updateIngredient.Id);
      oldIngredient.Name = updateIngredient.Name != null ? updateIngredient.Name : oldIngredient.Name;
      oldIngredient.Quantity = updateIngredient.Quantity != 0 ? updateIngredient.Quantity : oldIngredient.Quantity;
      return _repo.Edit(oldIngredient);
    }
    internal void Remove(int id)
    {
      Ingredient ingredient = Get(id);
      _repo.Remove(id);
    }
  }
}
