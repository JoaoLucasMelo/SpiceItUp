<template>
  <div class="Steps">
    <div class="card border-0" v-if="edit === true">
      <div
        class="topcard rounded d-flex justify-content-center align-items-center"
      >
        <div class="col-8 text-end">
          <p class="font title my-2">Recipe Steps</p>
        </div>
        <div class="text-end col-4">
          <i
            @click="edit = !edit"
            title="Edit Ingredients"
            class="mdi pe-3 mdi-18px mdi-pencil selectable1"
            v-if="recipe.creatorId === account.id"
          ></i>
        </div>
      </div>
      <div class="middlecard pt-2">
        <div class="d-flex" v-for="s in steps" :key="s.id">
          <p class="font mx-3 mb-1">{{ s.stepNumber }}.</p>
          <p class="font mb-1">{{ s.body }}</p>
        </div>
      </div>
      <div
        class="
          bottomcard
          rounded
          d-flex
          justify-content-center
          align-items-center
        "
      >
        <form>
          <div class="input-group mb-3">
            <input
              type="text"
              class="form-control border-0 font whiteback"
              placeholder="Add a step"
              aria-label="Step"
              v-model="add.body"
            />
            <div class="input-group-append">
              <button
                @click="addStep"
                class="btn whiteback border-0"
                type="button"
              >
                <i class="mdi mdi-18px text-primary mdi-plus"></i>
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
    <div class="card border-0" v-if="edit === false">
      <div
        class="topcard rounded d-flex justify-content-center align-items-center"
      >
        <div class="col-8 text-end">
          <p class="font title my-2">Recipe Steps</p>
        </div>
        <div class="text-end col-4">
          <i
            @click="edit = !edit"
            title="Edit Ingredients"
            class="mdi pe-3 mdi-18px mdi-pencil selectable1"
          ></i>
        </div>
      </div>
      <div class="middlecard pt-2">
        <div class="d-flex" v-for="s in steps" :key="s.id">
          <i
            @click="removeStep(s.id)"
            title="Remove Ingredient"
            class="mdi mdi-close text-danger ms-3 selectable1"
          ></i>
          <p class="font mx-3 mb-1">{{ s.stepNumber }}.</p>
          <p class="font mb-1">{{ s.body }}</p>
        </div>
      </div>
      <div
        class="bottomcard rounded d-flex justify-content-end align-items-center"
      >
        <div class="px-2">
          <p
            title="Done"
            @click="edit = !edit"
            class="font pe-3 f-16 text-primary selectable1"
          >
            Done
          </p>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { stepsService } from "../services/StepsService"
export default {
  props: {
    recipe: { type: Object }
  },
  setup(props) {
    let add = ref({ ... { recipeId: props.recipe.id } })
    let edit = ref(true)
    return {
      edit,
      add,
      steps: computed(() => AppState.activeSteps),
      account: computed(() => AppState.account),
      async addStep() {
        try {
          await stepsService.addStep(add.value)
          add.value = { ... { recipeId: props.recipe.id } }
        } catch (error) {
          logger.error(error)
          Pop.toast(error, 'error')
        }
      },
      async removeStep(id) {
        try {
          await stepsService.removeStep(id)
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
.title {
  font-size: 2.5vh;
}
.topcard {
  background-color: #418848;
  color: white;
  border-bottom-left-radius: 0 !important;
  border-bottom-right-radius: 0 !important;
}
.middlecard {
  height: 30vh;
  background-color: #e6f5e8;
}
.bottomcard {
  height: 7vh;
  background-color: #e6f5e8;
  border-top-left-radius: 0 !important;
  border-top-right-radius: 0 !important;
}
.whiteback {
  background-color: #ffffff;
}
.btn:focus {
  outline: none;
  box-shadow: none;
}
</style>