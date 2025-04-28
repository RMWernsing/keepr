<script setup>
import { AppState } from '@/AppState.js';
import EditAccountModal from '@/components/EditAccountModal.vue';
import KeepCard from '@/components/KeepCard.vue';
import KeepDetailsModal from '@/components/KeepDetailsModal.vue';
import { profilesService } from '@/services/ProfilesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()

const profile = computed(() => AppState.account)
const vaults = computed(() => AppState.vaults)
const keeps = computed(() => AppState.keeps)

onMounted(() => {
  getProfileVaults()
  getProfileKeeps()
})

async function getProfileVaults() {
  try {
    await profilesService.getProfileVaults(profile?.value.id)
  }
  catch (error) {
    Pop.error(error, 'Could not get keeps')
    logger.error('COULD NOT GET KEEPS', error)
  }
}

async function getProfileKeeps() {
  try {
    await profilesService.getProfileKeeps(profile?.value.id)
  }
  catch (error) {
    Pop.error(error, 'Could not get profiles keeps')
    logger.error('COULD NOT GET PROFILES KEEPS', error)
  }
}
</script>


<template>
  <!-- TODO loading page for when data is taking a while to load in and to handle ghost data -->
  <section v-if="profile" class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-12 col-lg-8 col-sm-12 position-relative">
        <div class="">
          <img v-if="profile?.coverImg" class="w-100 cover-img rounded" :src="profile?.coverImg"
            :alt="`${profile?.name}'s cover image'`">
          <img v-else class="w-100 cover-img rounded"
            src="https://images.unsplash.com/photo-1503453363464-743ee9ce1584?q=80&w=2944&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            alt="placeholder image">
        </div>
        <img class="profile-img position-absolute border shadow-lg" :src="profile?.picture"
          :alt="`${profile?.name}'s profile picture'`">
      </div>
      <div class="col-md-8">
        <div class="text-center">
          <h1 class="mt-5 pt-1">{{ profile?.name }}</h1>
          <span class="fs-4">{{ vaults?.length }} vaults | {{ keeps?.length }} keeps</span>
          <div>
            <span class="fs-5">Edit Account</span>
            <button class="btn btn-primary ms-3" title="Edit Account" data-bs-toggle="modal"
              data-bs-target="#EditAccount">
              <span class="mdi mdi-account-box-outline text-light"></span>
            </button>
          </div>
        </div>
      </div>
      <div class="col-12">
        <div class="row mt-5">
          <div class="col-12">
            <h2 class="mb-5">Vaults</h2>
          </div>
          <div v-for="vault in vaults" :key="vault.id" class="col-lg-3 col-md-4 col-sm-6 position-relative mb-1">
            <div class="mb-4">
              <img class="w-100 vault-img rounded" :src="vault.img" :alt="`image for the ${vault.name} vault`">
            </div>
            <p class="position-absolute vault-name fs-5 fw-bold">{{ vault.name }}</p>
          </div>
        </div>
      </div>
      <div class="col-md-12">
        <h2 class="mb-5">Keeps</h2>
        <div class="masonry-container">
          <div v-for="keep in keeps" :key="keep.id">
            <KeepCard :keep="keep" />
          </div>
        </div>
      </div>
    </div>
  </section>
  <KeepDetailsModal />
  <EditAccountModal />
</template>


<style lang="scss" scoped>
.cover-img {
  height: 400px;
  object-fit: cover;
  object-position: center;
}

.profile-img {
  position: absolute;
  top: 100%;
  left: 51%;
  transform: translate(-50%, -50%);
  height: 6rem;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
  object-position: center;
}

.vault-img {
  height: 8rem;
  object-fit: cover;
  object-position: center;
}

.vault-name {
  top: 97px;
  left: 21px;
  color: aliceblue;
  text-shadow: 1px 1px 5px black;
}

.masonry-container {
  columns: 300px;
}

@media(max-width: 769px) {
  .masonry-container {
    columns: 150px
  }
}

.masonry-container>* {
  display: inline-block;
  break-inside: avoid;
}
</style>