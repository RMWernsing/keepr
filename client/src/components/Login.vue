<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { AuthService } from '../services/AuthService.js';


const identity = computed(() => AppState.identity)
const account = computed(() => AppState.account)

function login() {
  AuthService.loginWithRedirect()
}
function logout() {
  AuthService.logout()
}

</script>

<template>
  <!-- TODO when on profile page, hitting the manage account button does not reactivly change the active profile in the appstate -->
  <span class="navbar-text">
    <button class="btn selectable text-green" @click="login" v-if="!identity">
      Login
    </button>
    <div v-else>
      <div class="dropdown">
        <div role="button" class="bg-dark selectable no-select" data-bs-toggle="dropdown" aria-expanded="false"
          title="open account menu">
          <div v-if="account?.picture || identity?.picture" class="bg-light">
            <img :src="account?.picture || identity?.picture" alt="account photo" height="40" class="user-img shadow" />
          </div>
        </div>
        <div v-if="account" class="dropdown-menu dropdown-menu-sm-end dropdown-menu-start p-0" role="menu"
          title="account menu">
          <div class="list-group">
            <!-- <RouterLink v-if="keep" :to="{ name: 'Profile', params: { profileId: keep?.creator.id } }"> -->
            <RouterLink :to="{ name: 'Profile', params: { profileId: account?.id } }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </RouterLink>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<style lang="scss" scoped>
.user-img {
  height: 40px;
  width: 40px;
  border-radius: 100px;
  object-fit: cover;
  object-position: center;
}
</style>
