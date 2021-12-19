<template>
  <div class="home col-12">
    <div class="row">
      <div class="col-4">
        <img class="image ms-2 mt-2" src="../assets/img/spice.png" alt="" />
      </div>
      <div
        class="col-5 align-items-center justify-content-end d-flex flex-column"
      >
        <div class="">
          <div class="input-group mb-3">
            <input
              type="text"
              class="form-control border-2 border-primary font"
              placeholder="Search Recipes..."
              aria-label="Recipient's username"
              aria-describedby="basic-addon2"
            />
            <div class="input-group-append">
              <button class="btn bord btn-primary border-1" type="button">
                <i class="mdi mdi-18px text-white mdi-magnify"></i>
              </button>
            </div>
          </div>
        </div>
        <div class="card elevation-3 d-flex flex-row">
          <button class="border-0 font f-18 m-0 py-1 px-5 btns">Home</button>
          <button class="border-0 font f-18 m-0 py-1 px-4 btns">
            My Recipes
          </button>
          <button class="border-0 font f-18 m-0 py-1 px-5 btns">
            Favorites
          </button>
        </div>
      </div>
      <div class="col-3 d-flex justify-content-center">
        <div
          class="
            card
            elevation-3
            mt-2
            d-flex
            justify-content-center
            profilecard
          "
        >
          <div class="d-flex">
            <img class="ms-1 rounded profile" :src="account.picture" alt="" />
            <div class="d-flex flex-column justify-content-center">
              <p class="m-0 ms-2 font">{{ account.name }}</p>
              <p class="m-0 ms-2 font">{{ account.email }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <div class="col-3" v-for="r in recipes" :key="r.id"></div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { recipesService } from "../services/RecipesService"
export default {
  setup() {
    onMounted(async () => {
      try {
        await recipesService.getAll()
      } catch (error) {
        logger.error(error)
        Pop.toast(error, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      recipes: computed(() => AppState.recipes)
    }
  }
}
</script>

<style scoped lang="scss">
.image {
  height: 23vh;
}
.profile {
  height: 9vh;
}
.profilecard {
  width: 36vh;
  height: 10vh;
}
.textcenter {
  font-size: 5vh;
  margin-top: 4vh;
}
.bord {
  border-bottom-left-radius: 0%;
  border-top-left-radius: 0%;
}
.btns {
  background-color: white;
}
.btns:hover {
  box-shadow: 0 6px #418848;
}
</style>
