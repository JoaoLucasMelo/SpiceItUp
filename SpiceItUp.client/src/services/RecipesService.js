import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService{
async getAll(){
  const res = await api.get('api/recipes')
  AppState.recipes = res.data
  logger.log(AppState.recipes)
}
async submit(newRecipe){
  const res = await api.post("api/recipes", newRecipe)
  logger.log(res.data)
  AppState.recipes.push(res.data)
}
}

export const recipesService = new RecipesService()