<script setup>
import { AppState } from '@/AppState.js';
import VaultKeepCard from '@/components/VaultKeepCard.vue';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute()
const router = useRouter()

const vault = computed(() => AppState.activeVault)
const account = computed(() => AppState.account)
const keeps = computed(() => AppState.vaultKeeps)

onMounted(() => {
  getVaultById()
  getKeepsForVault()
})

async function getVaultById() {
  try {
    const vaultId = route.params.vaultId
    await vaultsService.getVaultById(vaultId)
  }
  catch (error) {
    Pop.error(error, 'Could not get vault',)
    logger.error('COULD NOT GET VAULT', error)
    router.push({ name: 'Home' })
  }
}

async function privateVault() {
  try {
    let vaultStatus = true
    if (vault.value.isPrivate == true) {
      vaultStatus = false
    }
    else {
      vaultStatus = true
    }
    const vaultId = route.params.vaultId
    await vaultsService.privateVault(vaultStatus, vaultId)
  }
  catch (error) {
    Pop.error(error, 'Could not private vault')
    logger.error('COULD NOT PRIVATE VAULT', error)
  }
}

async function deleteVault() {
  try {
    const confirm = await Pop.confirm(`Are you sure you want to delete ${vault?.value.name}?`)
    if (!confirm) {
      return
    }
    const vaultId = route.params.vaultId
    await vaultsService.deleteVault(vaultId)
    Pop.success('You successfully delete your vault!')
    router.push({ name: 'Profile', params: { profileId: account?.value.id } })
  }
  catch (error) {
    Pop.error(error);
  }
}

async function getKeepsForVault() {
  try {
    const vaultId = route.params.vaultId
    await vaultsService.getKeepsForVault(vaultId)
  }
  catch (error) {
    Pop.error(error, 'Could not get keeps')
    logger.error('COULD NOT GET KEEPS', error)
  }
}
</script>


<template>
  <!-- TODO add delete function to delete vault, kick user off the page -->
  <!-- TODO push user of the page if they try to use an invalid vault id -->
  <!-- TODO add a loading page -->
  <!-- TODO make the profile picture for keep accurate -->
  <section v-if="vault" class="container">
    <div class="row justify-content-center mt-4 ">
      <div class="col-md-6">
        <div class="image-height bg-danger rounded-4 d-flex flex-column justify-content-end align-items-center"
          :style="{ backgroundImage: `url(${vault.img})` }">
          <div class="text-light fs-3 pb-3 fw-bold text-shadow text-center">
            <p>
              {{ vault.name }}
            </p>
            <p class="fs-5">by {{ vault.creator.name }}</p>
          </div>
        </div>
        <div v-if="vault?.creatorId == account?.id" class="d-flex justify-content-between mt-3">
          <div>
            <button @click="deleteVault()" class="btn btn-danger" :title="`Delete the ${vault?.name} vault`">Delete
              Vault</button>
          </div>
          <div v-if="!vault.isPrivate">
            <button @click="privateVault()" class="btn btn-secondary fw-bold"
              :title="`Set ${vault?.name} to private`">Private Vault <span class="mdi mdi-lock"></span></button>
          </div>
          <div v-else>
            <button @click="privateVault()" class="btn btn-secondary fw-bold"
              :title="`set ${vault?.name} to public`">Un-Private Vault <span
                class="mdi mdi-lock-open-variant"></span></button>
          </div>
        </div>
        <!-- TODO make it so that it says 1 keep instead of 1 keeps -->
        <div class=" d-flex justify-content-center">
          <span class="mt-2 text-center bg-primary text-light p-2 rounded-pill fs-5 px-3">{{ keeps?.length }}
            Keeps</span>
        </div>
      </div>
      <div v-if="keeps" class="col-12">
        <div class="masonry-container mt-4">
          <div v-for="keep in keeps" :key="keep.id">
            <VaultKeepCard :keep="keep" />
          </div>
        </div>
      </div>
      <div v-if="keeps.length == 0" class="col-12">
        <div class="text-center mt-5">
          <h2>You have no keeps in this vault ☹️</h2>
        </div>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
.image-height {
  height: 25dvh;
  background-position: center;
  background-size: cover;
}

.text-shadow {
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