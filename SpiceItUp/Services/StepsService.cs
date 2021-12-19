using System.Collections.Generic;
using SpiceItUp.Models;
using SpiceItUp.Repositories;

namespace SpiceItUp
{
  public class StepsService
  {
    private readonly StepsRepository _repo;

    public StepsService(StepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Step> Get()
    {
      return _repo.Get();
    }
    internal List<Step> GetByStep(int id)
    {
      return _repo.GetByStep(id);
    }
    internal Step Get(int id)
    {
      return _repo.Get(id);
    }

    internal Step Create(Step newStep)
    {
      return _repo.Create(newStep);
    }

    internal Step Edit(Step updateStep)
    {
      Step oldStep = Get(updateStep.Id);
      oldStep.Body = updateStep.Body != null ? updateStep.Body : oldStep.Body;
      oldStep.StepNumber = updateStep.StepNumber != 0 ? updateStep.StepNumber : oldStep.StepNumber;
      return _repo.Edit(oldStep);
    }

    internal void Remove(int id)
    {
      _repo.Remove(id);
    }
  }
}