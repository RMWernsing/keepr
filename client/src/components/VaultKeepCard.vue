<script setup>
import { Keep } from '@/models/Keep.js';
import KeepDetailsModal from './KeepDetailsModal.vue';
import { Pop } from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { keepsService } from '@/services/KeepsService.js';
import { computed } from 'vue';
import { AppState } from '@/AppState.js';
import { useRoute } from 'vue-router';
import VaultKeepDetailModal from './VaultKeepDetailModal.vue';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';

const route = useRoute()

const account = computed(() => AppState.account)
const vault = computed(() => AppState.activeVault)

defineProps({
  keep: { type: Keep, required: true }
})

async function getKeepById(keepId) {
  try {
    await keepsService.getKeepById(keepId)
  }
  catch (error) {
    Pop.error(error, 'Could not get keep details')
    logger.error('COULD NOT GET KEEP BY ID', error)
  }
}

// TODO update keep function so that it deleted the keep from the vault, not the original keep
async function deleteVaultKeep(keep) {
  try {
    const confirm = await Pop.confirm(`Are you sure you want to delete ${keep?.name} from this vault?`)
    if (!confirm) {
      return
    }
    await vaultKeepsService.deleteVaultKeep(keep?.vaultKeepId)
    Pop.success('You deleted the keep successfully!!!')
  }
  catch (error) {
    Pop.error(error, 'Could not delete keep')
    logger.error('COULD NOT DELETE KEEP', error)
  }
}

</script>


<template>
  <div type="button" :title="`Open details page for ${keep.name}`" class="mb-3 image-container">
    <div @click="getKeepById(keep.id)">

      <img data-bs-toggle="modal" data-bs-target="#vaultKeepDetailsModal" :src="keep.img"
        :alt="'An image of ' + keep.name" class="w-100 rounded shadow-lg image-container-img">
      <span data-bs-toggle="modal" data-bs-target="#vaultKeepDetailsModal" class="overlay-text fw-bold text-light">{{
        keep.name }}</span>
      <!-- <img class="overlay-img d-none d-md-block" :src="keep.creator.picture"
        :alt="`Profile picture for ${keep.creator.name}`"> -->
    </div>
    <span v-if="account?.id == vault?.creatorId" :title="`Delete ${keep?.name} from vault`"
      class="text-danger fs-2 mdi mdi-delete-circle position-absolute overlay-delete-button"
      @click="deleteVaultKeep(keep)"></span>
  </div>
  <VaultKeepDetailModal />
</template>


<style lang="scss" scoped>
.image-container {
  position: relative;
  display: inline-block;
}

.image-container-img {
  display: block;
  width: 100%;
  height: 100%;
}

.overlay-text {
  position: absolute;
  bottom: 10px;
  left: 10px;
  // transform: translate(-50%, -50%);
  text-shadow: 1px 1px 5px black;
  font-size: 20px;
  text-align: start;
}

@media(max-width: 769px) {
  .overlay-text {
    font-size: 16px;
    left: 10px;
    bottom: 10px;
  }
}

.overlay-img {
  position: absolute;
  bottom: 10px;
  right: 10px;
  // transform: translate(-50%, -50%);
  aspect-ratio: 1/1;
  border-radius: 50%;
  height: 3rem;
  object-fit: cover;
}

@media(max-width: 769px) {
  .overlay-img {
    right: 10px;
    bottom: 10px;
  }
}

.overlay-delete-button {
  position: absolute;
  top: -2px;
  right: 6px;
  // transform: translate(-50%, -50%);
  aspect-ratio: 1/1;
  border-radius: 50%;
  width: 2rem;
  text-shadow: 1px 1px 5px rgb(0, 0, 0);
}

@media(max-width: 769px) {
  .overlay-delete-button {
    right: -2px;
    top: -5px;
  }
}
</style>