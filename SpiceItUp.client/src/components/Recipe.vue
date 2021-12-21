<template>
  <div class="recipe">
    <div
      @click="getInfo"
      data-bs-toggle="modal"
      :data-bs-target="'#a' + recipes.id + 'a'"
      class="card cardback d-flex justify-content-between grow"
    >
      <div class="d-flex justify-content-between">
        <div class="card category rounded-pill align-self-center">
          <p class="m-0 px-2 font">{{ recipes.category }}</p>
        </div>
        <div
          v-if="
            favorites.find((f) => f.recipeId === recipes.id) &&
            user.isAuthenticated
          "
        >
          <button title="Add Favorite" class="btn" @click="removeFavorite">
            <i class="mdi-24px text-danger mdi mdi-cards-heart"></i>
          </button>
        </div>
        <div v-else-if="user.isAuthenticated">
          <button title="Remove Favorite" class="btn" @click="addFavorite">
            <i class="mdi-24px text-white mdi mdi-cards-heart-outline"></i>
          </button>
        </div>
        <div class="placement" v-else></div>
      </div>
      <div class="card blurycards border-0 m-0 rounded-0">
        <p class="title font mb-0">{{ recipes.title }}</p>
        <p class="subtitle font textover">{{ recipes.subtitle }}</p>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import { ingredientsService } from "../services/IngredientsService"
import { stepsService } from "../services/StepsService"
import Pop from "../utils/Pop"
import { favoritesService } from "../services/FavoritesService"
import { AppState } from "../AppState"
import { Modal } from "bootstrap"
export default {
  props: {
    recipes: { type: Object },
    favorites: { type: Array }
  },
  setup(props) {
    return {
      imgUrl: computed(() => `url(${props.recipes.imgUrl})`),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      async getInfo() {
        try {
          await ingredientsService.getIngredients(props.recipes.id)
          await stepsService.getSteps(props.recipes.id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      },
      async addFavorite() {
        try {
          await favoritesService.addFavorite(props.recipes.id, this.account.id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      },
      async removeFavorite() {
        try {
          let found = props.favorites.find(f => f.recipeId === props.recipes.id)
          await favoritesService.removeFavorite(found.id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.placement {
  min-height: 45px;
}
.cardback {
  height: 33vh;
  min-width: 30vh;
  background-image: v-bind(imgUrl);
  background-size: cover;
  box-shadow: 3px 4px 4px rgb(104, 104, 104);
}
.blurycards {
  height: 30% !important;
  color: white;
  background-color: #4e4e4e9f;
  width: 100%;
}
.title {
  font-size: 2.3vh;
  margin-left: 1vh;
  margin-top: 1vh;
}
.subtitle {
  font-size: 2vh;
  margin-left: 1vh;
  margin-top: 0.5vh;
  max-width: 35vh;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.category {
  height: 3.2vh;
  font-size: 1.7vh;
  color: white;
  background-color: #4e4e4e9f;
  margin-left: 0.9vh;
}
.grow {
  transition: all 0.2s ease-in-out;
}
.grow:hover {
  transform: scale(1.05);
  box-shadow: 9px 6px 6px rgb(104, 104, 104);
}
.btn:focus {
  outline: none;
  box-shadow: none;
}
.scrollbar {
  overflow-y: scroll;
  height: 76vh;
}
.scrollbar::-webkit-scrollbar {
  width: 8px;
}
.scrollbar::-webkit-scrollbar-track {
  background: #838383bd;
  width: 8px;
  border-radius: 8px;
}
.scrollbar::-webkit-scrollbar-thumb {
  background-color: #418848;
  border-radius: 8px;
}
</style>