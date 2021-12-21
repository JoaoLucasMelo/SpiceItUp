<template>
  <div class="ProfileCanvas">
    <div
      class="offcanvas offcanvas-end"
      tabindex="-1"
      id="offcanvasExample"
      aria-labelledby="offcanvasLabel"
    >
      <div class="offcanvas-header">
        <h3 class="offcanvas-title" id="offcanvasLabel">Profile</h3>
        <button
          type="button"
          class="btn-close text-reset"
          data-bs-dismiss="offcanvas"
          aria-label="Close"
        ></button>
      </div>
      <div class="offcanvas-body">
        <div
          class="d-flex justify-content-center flex-column align-items-center"
        >
          <i
            @click="edit = !edit"
            aria-label="edit profile"
            class="mdi mdi-pencil selectable1 align-self-end"
            >Edit Profile</i
          >
          <div v-if="edit === true">
            <img
              :src="account.picture"
              alt="user photo"
              height="120"
              width="120"
              class="picrounded elevation-3"
            />
          </div>
          <div class="text-center mb-3" v-if="edit === true">
            <h4 class="mt-2">{{ account.name }}</h4>
            <h5 class="mt-2">{{ account.email }}</h5>
          </div>
        </div>

        <div v-if="edit === false">
          <form>
            <div class="d-flex flex-column mb-3 mt-4">
              <div class="input-group">
                <span class="input-group-text border-dark">Name</span>
                <input
                  class="form-control border-dark"
                  type="text"
                  placeholder=""
                  name="name"
                  v-model="editProfile.name"
                />
              </div>
            </div>
            <div class="d-flex flex-column">
              <div class="input-group">
                <span class="input-group-text border-dark">Picture URL</span>
                <input
                  class="form-control border-dark"
                  type="text"
                  placeholder=""
                  name="pic-url"
                  v-model="editProfile.picture"
                />
              </div>
            </div>
            <div class="d-flex justify-content-center mt-3">
              <i
                type="submit"
                @click="editAccount"
                v-if="edit === false"
                class="
                  mdi mdi-content-save
                  selectable1
                  align-self-end
                  border
                  p-1
                  rounded
                  bg-light
                "
              >
                Save
              </i>
            </div>
          </form>
        </div>
        <div>
          <button @click="logout" class="btn btn-outline-danger">Logout</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { accountService } from "../services/AccountService"
import { computed, ref } from "@vue/reactivity"
import { AuthService } from "../services/AuthService"
export default {
  setup() {
    let edit = ref(true)
    let editProfile = ref({})
    return {
      edit,
      editProfile,
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      async editAccount() {
        try {
          await accountService.editAccount(editProfile.value)
          edit.value = !edit.value
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message, 'error')
        }
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.picrounded {
  border-radius: 5%;
  object-fit: cover;
}
</style>