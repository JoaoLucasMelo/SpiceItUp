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
            <div class="card modalstyle d-flex flex-row">
              <div class="col-4 backrecipe"></div>
              <div class="col-8 backright">
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
                  <div class="col-6 ps-5 pe-3 pt-2">
                    <Steps :recipe="recipes" />
                  </div>
                  <div class="col-6 pe-5 ps-3 pt-2">
                    <Ingredients :recipe="recipes" />
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
export default {
  props: {
    recipes: { type: Object }
  },
  setup(props) {
    return {
      imgUrl: computed(() => `url(${props.recipes.imgUrl})`)
    }
  }
}
</script>


<style lang="scss" scoped>
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
</style>