<script setup>
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';

const editableVaultData = ref({
  name: '',
  description: '',
  img: '',
  isPrivate: false
})

async function createVault() {
  try {
    await vaultsService.createVault(editableVaultData.value)
    editableVaultData.value = {
      name: '',
      description: '',
      img: '',
      isPrivate: false
    }
  }
  catch (error) {
    Pop.error(error, 'Could not create vaults')
    logger.error('COULD NOT CREATE VAULTS', error)
  }
}

</script>


<template>
  <div class="modal fade" id="CreateVaultModal" tabindex="-1" aria-labelledby="CreateVaultModalLabel"
    aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="CreateVaultModalLabel">Create Vault</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form>
            <form @submit.prevent="createVault()">
              <div class="mb-3">
                <label for="name" class="form-label">Name</label>
                <input v-model="editableVaultData.name" type="text" class="form-control" id="name"
                  aria-describedby="nameHelp" maxlength="50" minlength="2" required>
              </div>
              <div class="mb-3">
                <label for="description" class="form-label">Description</label>
                <textarea v-model="editableVaultData.description" type="text" class="form-control" id="description"
                  maxlength="1000" required></textarea>
              </div>
              <div class="mb-3">
                <label for="img" class="form-label">Image</label>
                <input v-model="editableVaultData.img" type="url" class="form-control" id="img" maxlength="1000"
                  required>
              </div>
              <img v-if="editableVaultData.img" class="w-100 mb-2" :src="editableVaultData.img"
                alt="Keep Image Preview">
              <button data-bs-dismiss="modal" type="submit" class="btn btn-success" title="submit form">Submit</button>
            </form>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>