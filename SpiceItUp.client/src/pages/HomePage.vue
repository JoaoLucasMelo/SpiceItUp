<template>
  <div class="home col-12">
    <div class="row pb-2">
      <div class="col-md-4">
        <img class="image ms-2 mt-2" src="../assets/img/spice.png" alt="" />
      </div>
      <div
        class="
          col-md-5
          align-items-center
          justify-content-end
          d-flex
          flex-column
        "
      >
        <div class="">
          <div class="input-group mb-3">
            <form @submit.prevent="querySearch">
              <div class="d-flex">
                <input
                  type="text"
                  class="form-control border-2 border-primary font"
                  placeholder="Search Recipes..."
                  aria-label="Recipient's username"
                  aria-describedby="basic-addon2"
                  v-model="userInput"
                />
                <div class="input-group-append">
                  <button class="btn bord btn-primary border-1" type="submit">
                    <i class="mdi mdi-18px text-white mdi-magnify"></i>
                  </button>
                </div>
              </div>
            </form>
          </div>
        </div>
        <div class="card elevation-3 d-flex flex-row">
          <button
            @click="all"
            :class="menu === 1 ? 'btnmenufix' : ''"
            class="border-0 font f-18 m-0 py-1 px-3 px-md-5 btns"
          >
            Home
          </button>
          <button
            @click="myRecipes"
            :class="menu === 2 ? 'btnmenufix' : ''"
            class="border-0 font f-18 m-0 py-1 px-3 px-md-4 btns"
            :disabled="!user.isAuthenticated"
          >
            My Recipes
          </button>
          <button
            @click="myFavorites"
            :class="menu === 3 ? 'btnmenufix' : ''"
            class="border-0 font f-18 m-0 py-1 px-3 px-md-5 btns"
            :disabled="!user.isAuthenticated"
          >
            Favorites
          </button>
        </div>
      </div>
      <div
        class="col-md-3 d-flex justify-content-center justify-content-md-end"
      >
        <div
          class="
            card
            elevation-3
            mt-2
            d-flex
            justify-content-center
            profilecard
          "
          v-if="user.isAuthenticated"
        >
          <div
            v-if="user.isAuthenticated"
            data-bs-toggle="offcanvas"
            href="#offcanvasExample"
            class="d-flex selectable1"
          >
            <img class="ms-1 rounded profile" :src="account.picture" alt="" />
            <div class="d-flex flex-column justify-content-center">
              <p class="m-0 ms-2 font">{{ account.name }}</p>
              <p class="m-0 ms-2 font">{{ account.email }}</p>
            </div>
          </div>
        </div>
        <div
          class="
            card
            elevation-3
            mt-2
            d-flex
            justify-content-center
            profilecard1
          "
          v-if="!user.isAuthenticated"
        >
          <div>
            <p class="font m-0 text-center">Login and start Sharing!</p>
          </div>
          <div class="text-center">
            <button @click="login" class="btn text-white btn-primary pt-1">
              Login
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="row scrollbar mt-2">
      <div class="col-12 d-md-flex flex-wrap selectable1 mt-3">
        <div class="col-md-3 p-4" v-for="r in recipes" :key="r.id">
          <Recipe :favorites="favorites" :recipes="r" />
          <RecipeModal :recipes="r" :favorites="favorites" />
        </div>
      </div>
    </div>
    <div>
      <div
        :class="user.isAuthenticated ? 'sortbtn' : 'sortbtn1'"
        class="btn-group dropup sortbtn"
      >
        <button
          type="button"
          class="btn rounded sort pt-1 font"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          Category
        </button>
        <ul class="dropdown-menu font">
          <li>
            <a @click="sortByAll" class="dropdown-item selectable1">All</a>
          </li>
          <li>
            <a @click="sortBy(0)" class="dropdown-item selectable1">Pasta</a>
          </li>
          <li>
            <a @click="sortBy(1)" class="dropdown-item selectable1">Burger</a>
          </li>
          <li>
            <a @click="sortBy(2)" class="dropdown-item selectable1">Salad</a>
          </li>
          <li>
            <a @click="sortBy(3)" class="dropdown-item selectable1">Soup</a>
          </li>
          <li>
            <a @click="sortBy(4)" class="dropdown-item selectable1">Fish</a>
          </li>
          <li>
            <a @click="sortBy(5)" class="dropdown-item selectable1">Beef</a>
          </li>
          <li>
            <a @click="sortBy(6)" class="dropdown-item selectable1">Other</a>
          </li>
        </ul>
      </div>
    </div>
    <div v-if="user.isAuthenticated">
      <button
        data-bs-toggle="modal"
        data-bs-target="#newRecipe"
        title="Add Recipe"
        class="btn grow addbtn"
      >
        <i class="mdi px-1 mdi-48px mdi-plus"></i>
      </button>
    </div>
    <NewRecipeModal />
    <ProfileOffCanvas />
  </div>
</template>

<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { onMounted, watchEffect } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { recipesService } from "../services/RecipesService"
import { accountService } from "../services/AccountService"
import { AuthService } from "../services/AuthService"
export default {
  setup() {
    let userInput = ref("")
    onMounted(async () => {
      try {
        await recipesService.getAll()
      } catch (error) {
        logger.error(error)
        Pop.toast(error, 'error')
      }
    })
    watchEffect(async () => {
      try {
        await accountService.getFavorites()
      } catch (error) {
        logger.error(error)
        Pop.toast(error, 'error')
      }
    })
    return {
      userInput,
      account: computed(() => AppState.account),
      favorites: computed(() => AppState.favorites),
      recipes: computed(() => AppState.recipes),
      user: computed(() => AppState.user),
      menu: computed(() => AppState.menu),
      async login() {
        AuthService.loginWithPopup()
      },
      async myRecipes() {
        try {
          await this.all()
          recipesService.myRecipes()
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      },
      async all() {
        try {
          await recipesService.getAll()
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      },
      async myFavorites() {
        try {
          await this.all()
          recipesService.myFavorites()
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      },
      async sortBy(number) {
        try {
          if (this.menu === 1) {
            await recipesService.getAll()
            recipesService.sortBy(number)
          }
          else if (this.menu === 3) {
            await recipesService.getAll()
            recipesService.myFavorites()
            recipesService.sortBy(number)
          }
          else {
            await recipesService.getAll()
            recipesService.myRecipes()
            recipesService.sortBy(number)
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      },
      async sortByAll() {
        try {
          if (this.menu === 1) {
            await recipesService.getAll()
          }
          else if (this.menu === 3) {
            await recipesService.getAll()
            recipesService.myFavorites()
          }
          else {
            await recipesService.getAll()
            recipesService.myRecipes()
          }
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      },
      async querySearch() {
        try {
          await recipesService.querySearch(userInput.value)
          userInput.value = ("")
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      }
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
  width: 9vh;
  object-fit: cover;
}
.profilecard {
  width: 36vh;
  height: 10vh;
}
.profilecard1 {
  width: 20vh;
  height: 15vh;
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
.btnmenufix {
  background-color: white;
  box-shadow: 0 6px #418848;
}
.addbtn {
  border-radius: 50%;
  color: white;
  background-color: #418848;
  position: absolute;
  bottom: 25px;
  right: 25px;
  box-shadow: 6px 4px 4px rgb(104, 104, 104);
}
.sortbtn {
  border-radius: 50%;
  color: white;
  background-color: #418848;
  position: absolute;
  bottom: 130px;
  right: 25px;
}
.sortbtn1 {
  border-radius: 50%;
  color: white;
  background-color: #418848;
  position: absolute;
  bottom: 30px;
  right: 25px;
}

.sort {
  color: white;
  background-color: #418848;
  box-shadow: 5px 3px 3px rgb(104, 104, 104);
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
.form-control {
  border-top-right-radius: 0;
  border-bottom-right-radius: 0;
}
.scrollbar {
  overflow-y: scroll;
  height: 74vh;
}
.scrollbar::-webkit-scrollbar {
  width: 7px;
}
.scrollbar::-webkit-scrollbar-track {
  background: transparent;
}
.scrollbar::-webkit-scrollbar-thumb {
  background-color: #418848;
  border-radius: 10px;
}
@media only screen and (max-width: 500px) {
  .image {
    height: 14vh;
  }
  .profilecard {
    width: 33vh;
    height: 8vh;
  }
  .profile {
    height: 6vh;
    width: 6vh;
    object-fit: cover;
  }
  .scrollbar {
    overflow-y: scroll;
    height: 63vh;
  }
}
</style>
