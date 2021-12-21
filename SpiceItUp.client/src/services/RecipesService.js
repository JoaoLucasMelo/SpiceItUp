import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService{
async getAll(){
  const res = await api.get('api/recipes')
  AppState.recipes = res.data
  AppState.menu = 1
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

async myRecipes(){
  let myRec = AppState.recipes.filter(f => f.creatorId === AppState.account.id )
  AppState.recipes = myRec
  AppState.menu = 2
}
async myFavorites(){
  let favorites = AppState.favorites
  let newApp = []
 for (let i = 0; i < favorites.length; i++) {
   let id = favorites[i].recipeId;
   const found = AppState.recipes.find( r => r.id === id)
   if(found){
   newApp.push(found)}
  }
 AppState.recipes = newApp
 AppState.menu = 3
}
sortBy(number){
  let sort = AppState.recipes.filter(r => r.category === AppState.categories[number])
  AppState.recipes = sort
}
}

export const recipesService = new RecipesService()