<template>
  <div class="recipeModal">
    <div
      class="modal fade"
      :id="'a' + recipes.id + 'a'"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-xl">
        <div class="modal-content border-0">
          <div class="modal-body d-flex justify-content-center">
            <div class="card modalstyle d-flex flex-md-row">
              <div class="col-md-4 backrecipe">
                <div
                  class="text-end me-2"
                  v-if="favorites.find((f) => f.recipeId === recipes.id)"
                >
                  <button
                    title="Add Favorite"
                    class="btn"
                    @click="removeFavorite"
                  >
                    <i class="mdi-36px text-danger mdi mdi-cards-heart"></i>
                  </button>
                </div>
                <div class="text-end me-2" v-else-if="user.isAuthenticated">
                  <button
                    title="Remove Favorite"
                    class="btn"
                    @click="addFavorite"
                  >
                    <i
                      class="mdi-36px text-white mdi mdi-cards-heart-outline"
                    ></i>
                  </button>
                </div>
              </div>
              <div class="col-md-8 backright">
                <div>
                  <div class="d-flex justify-content-between">
                    <div class="d-flex">
                      <div>
                        <p class="title font">{{ recipes.title }}</p>
                      </div>
                      <div
                        class="
                          card
                          category
                          align-self-end
                          rounded-pill
                          border-0
                          ms-4
                        "
                      >
                        <p class="m-0 px-2 font margtop">
                          {{ recipes.category }}
                        </p>
                      </div>
                      <div
                        class="align-self-center mt-4 ms-4"
                        v-if="recipes.creatorId === account.id"
                      >
                        <i
                          title="Remove Recipe"
                          @click="remove"
                          class="
                            mdi mdi-18px
                            text-secondary
                            delete
                            mdi-trash-can
                            selectable1
                          "
                        ></i>
                      </div>
                    </div>
                    <div>
                      <i
                        data-bs-dismiss="modal"
                        aria-label="Close"
                        class="mdi selectable1 mdi-24px me-2 mdi-close"
                      ></i>
                    </div>
                  </div>
                  <div>
                    <p class="subtitle font">{{ recipes.subtitle }}</p>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-6 ps-5 pe-3 pt-2">
                    <Steps :recipe="recipes" />
                  </div>
                  <div class="col-md-6 pe-5 ps-3 pt-2">
                    <Ingredients :recipe="recipes" />
                  </div>
                </div>
                <div class="row justify-content-end">
                  <div
                    class="
                      card
                      published
                      font
                      d-flex
                      flex-row
                      align-items-center
                    "
                  >
                    <i class="me-2 f-10"> Published by: </i
                    >{{ recipes.creator.name }}
                    <img
                      class="profilepic ms-2"
                      :src="recipes.creator.picture"
                      alt=""
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { recipesService } from "../services/RecipesService"
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
      async remove() {
        try {
          await recipesService.remove(props.recipes.id)
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
.published {
  width: 38vh;
  margin-top: 1vh;
  margin-right: 4vh;
  font-size: 2vh;
  color: #48884f;
  border: 0;
}
.backright {
  background-color: white;
  height: 60vh;
  border-top-right-radius: 1%;
  border-bottom-right-radius: 1%;
}
.backrecipe {
  height: 60vh;
  background-image: v-bind(imgUrl);
  background-size: cover;
  border-radius: 1%;
  border-bottom-right-radius: 0%;
  border-top-right-radius: 0%;
}
.modalstyle {
  width: 90%;
}
.modal-content {
  background-color: rgba(240, 248, 255, 0);
}
.card {
  background-color: rgba(240, 248, 255, 0);
}
.title {
  color: #42ac4c;
  font-size: 3vh;
  font-weight: 600;
  margin-top: 2.5vh;
  margin-left: 2.5vh;
  margin-bottom: 0;
}
.subtitle {
  color: #696969;
  font-size: 2.2vh;
  font-weight: 500;
  margin-left: 2.5vh;
  margin-top: 0.5vh;
  margin-bottom: 0;
}
.category {
  height: 3.2vh;
  font-size: 1.7vh;
  color: white;
  background-color: #4e4e4e9f;
  margin-left: 0.9vh;
  margin-bottom: 0.6vh;
  padding-top: 2.5px;
}
.profilepic {
  object-fit: cover;
  height: 25px;
  width: 25px;
  border-radius: 50%;
}
.delete:hover {
  color: red !important;
}
.btn:focus {
  outline: none;
  box-shadow: none;
}
@media only screen and (max-width: 500px) {
}
</style>