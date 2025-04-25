<script setup>
import { ref, watch } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
}

watch(theme, () => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}, { immediate: true })

</script>

<template>
  <nav class="navbar navbar-expand-md bg-light border-bottom border-secondary">
    <div class="container gap-2 d-flex justify-content-between">
      <div>
        <RouterLink :to="{ name: 'Home' }" class="d-flex align-items-center text-dark">
          <button class="btn btn-secondary rounded-pill fw-bold" title="Navigate to Home">Home</button>
        </RouterLink>
      </div>
      <div class="border border-dark rounded p-1 pe-4 pb-0">
        <span class="fw-bold">the <br> keepr <br> co.</span>
      </div>
      <div>
        <!-- collapse button -->
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-links"
          aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
          <span class="mdi mdi-menu text-dark"></span>
        </button>
        <!-- collapsing menu -->
        <div class="collapse navbar-collapse " id="navbar-links">
          <ul class="navbar-nav">
            <li>
            </li>
          </ul>
          <!-- LOGIN COMPONENT HERE -->
          <div class="ms-auto">
          </div>
          <Login />
        </div>
      </div>
    </div>
  </nav>
</template>

<style lang="scss" scoped>
a {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
