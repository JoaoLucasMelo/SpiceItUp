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
        <div>
          <button class="btn">
            <i class="mdi-24px text-white mdi mdi-heart"></i>
          </button>
        </div>
      </div>
      <div class="card blurycards border-0 m-0 rounded-0">
        <p class="title font mb-0">{{ recipes.title }}</p>
        <p class="subtitle font">{{ recipes.subtitle }}</p>
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
export default {
  props: {
    recipes: { type: Object }
  },
  setup(props) {
    return {
      imgUrl: computed(() => `url(${props.recipes.imgUrl})`),
      async getInfo() {
        try {
          await ingredientsService.getIngredients(props.recipes.id)
          await stepsService.getSteps(props.recipes.id)
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