import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService{
  async getIngredients(id){
    const res = await api.get("api/recipes/" + id + "/ingredients")
    AppState.activeIngredients = res.data
    logger.log('Ingredients',AppState.activeIngredients)
  }
  async addIngredient(data) {
      const res = await api.post('api/ingredients', data)
      logger.log('New Ingredient', res.data)
      AppState.activeIngredients.push(res.data)
  }
  async removeIngredient(id){
    const res = await api.delete('api/ingredients/' + id)
    logger.log(res.data)
    const newApp = AppState.activeIngredients.filter(i => i.id !== id)
    AppState.activeIngredients = newApp
  }
}
export const ingredientsService = new IngredientsService()