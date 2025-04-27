<script setup>
import { computed } from 'vue';
import CreateKeepModal from './components/CreateKeepModal.vue';
import CreateVaultModal from './components/CreateVaultModal.vue';
import Footer from './components/Footer.vue';
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';
import { AppState } from './AppState.js';

const account = computed(() => AppState.account)

</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <RouterView />
  </main>
  <footer v-if="account" class=" text-center sticky-bottom">
    <Footer />
  </footer>
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<style lang="scss">
body {
  background-color: var(--bs-light);
}
</style>
