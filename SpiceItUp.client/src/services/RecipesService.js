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
async remove(id){
  const res = await api.delete("api/recipes/" + id)
  logger.log(res.data)
  let newApp = AppState.recipes.filter( r => r.id !== id )
  AppState.recipes = newApp
}

}

export const recipesService = new RecipesService()