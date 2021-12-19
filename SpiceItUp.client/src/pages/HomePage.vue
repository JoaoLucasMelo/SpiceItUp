<template>
  <div class="home col-12">
    <div class="row">
      <div class="col-6">
        <img class="image mt-2" src="../assets/img/spice.png" alt="" />
      </div>
      <div class="col-6">
        <div class="d-flex">
          <img class="profile" :src="user.picture" alt="" />
          <p class="ms-3">{{ user.name }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
export default {
  setup() {
    return {
      user: computed(() => AppState.user)
    }
  }
}
</script>

<style scoped lang="scss">
.image {
  height: 23vh;
}
.profile {
  height: 5vh;
  border-radius: 50%;
}
</style>
