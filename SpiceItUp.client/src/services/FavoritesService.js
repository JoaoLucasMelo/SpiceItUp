import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class FavoritesService{
  async addFavorite(recipeId, accountId){
    const res = await api.post("api/favorites", {recipeId: recipeId, accountId: accountId})
    logger.log('Favorite', res.data)
    AppState.favorites.push(res.data)
  }
  async removeFavorite(id){
    const res = await api.delete("api/favorites/" + id)
    logger.log(res.data)
    let newApp = AppState.favorites.filter( f => f.id !== id)
    AppState.favorites = newApp
  }
}

export const favoritesService = new FavoritesService()