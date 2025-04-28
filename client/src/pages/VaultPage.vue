<script setup>
import { AppState } from '@/AppState.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()

const vault = computed(() => AppState.activeVault)
const account = computed(()=> AppState.account)

onMounted(() => {
  getVaultById()
})

async function getVaultById() {
  try {
    const vaultId = route.params.vaultId
    await vaultsService.getVaultById(vaultId)
  }
  catch (error) {
    Pop.error(error, 'Could not get vault',)
    logger.error('COULD NOT GET VAULT', error)
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

// async function deleteVault(){
//   try {
    
//   }
//   catch (error){
//     Pop.error(error, 'Could not delete keep')
//   }
// }

</script>


<template>
  <!-- TODO add delete function to delete vault, kick user off the page -->
   <!-- TODO push user of the page if they try to use an invalid vault id -->
    <!-- TODO add a loading page -->
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
        <div v-if="vault?.creatorId == account?.id" class="d-flex justify-content-between mt-2">
          <div>
            <button class="btn btn-danger">Delete Vault</button>
          </div>
          <div v-if="!vault.isPrivate">
            <button @click="privateVault()" class="btn btn-secondary fw-bold">Private Vault <span
                class="mdi mdi-lock"></span></button>
          </div>
          <div v-else>
            <button @click="privateVault()" class="btn btn-secondary fw-bold">Un-Private Vault <span
                class="mdi mdi-lock-open-variant"></span></button>
          </div>
        </div>
        <div class=" d-flex justify-content-center">
          <span class="mt-2 text-center bg-primary text-light p-2 rounded-pill fs-5 px-3">0 Keeps</span>
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
</style>