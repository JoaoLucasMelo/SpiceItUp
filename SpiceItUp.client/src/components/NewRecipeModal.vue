<template>
  <div class="newRecipe">
    <div
      class="modal fade"
      id="newRecipe"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog mt-5 modal-lg">
        <div class="modal-content">
          <div class="modal-body">
            <div class="card modalstyle">
              <div class="">
                <p class="m-0 ms-4 font text-white f-20 py-1">New Recipe</p>
              </div>
            </div>
            <div class="card cardinputs p-3">
              <div class="mb-3">
                <form>
                  <div class="d-flex">
                    <div class="col-7 pe-4">
                      <label class="form-label font">Recipe's Title</label>
                      <input
                        required
                        type="text"
                        class="form-control font border-0"
                        placeholder="Enter title..."
                      />
                    </div>
                    <div class="col-5">
                      <label class="form-label font">Category</label>
                      <select
                        required
                        class="form-select border-0 font"
                        aria-label="Default select example"
                      >
                        <option selected>Select Category</option>
                        <option value="1">Pasta</option>
                        <option value="2">Burguer</option>
                        <option value="3">Salad</option>
                        <option value="4">Soup</option>
                        <option value="5">Fish</option>
                        <option value="5">Beef</option>
                      </select>
                    </div>
                  </div>
                  <div class="mt-3">
                    <label class="form-label font">Sub Title</label>
                    <input
                      required
                      type="text"
                      class="form-control font border-0"
                      placeholder="Enter title..."
                    />
                    <div class="d-flex justify-content-between mt-1">
                      <label class="brief font ps-2"
                        >A brief description of the recipe</label
                      >
                      <label class="brief font pe-2">0/50</label>
                    </div>
                    <div class="text-end mt-3">
                      <button class="btn text-primary me-4">Cancel</button>
                      <button class="btn btn-primary text-white">Submit</button>
                    </div>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.modal-content {
  background-color: rgba(240, 248, 255, 0);
  border: 0;
}
.modalstyle {
  width: 100%;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
  background-color: #418848;
  border: 1px;
}

.cardinputs {
  width: 100%;
  border-top-left-radius: 0;
  border-top-right-radius: 0;
  border: 0;
}
.brief {
  color: rgb(129, 129, 129);
  font-size: 1.5vh;
  font-style: italic;
}
</style>