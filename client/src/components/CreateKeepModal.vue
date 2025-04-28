<script setup>
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';


const editableKeepData = ref({
  name: '',
  description: '',
  img: ''
})

async function createKeep() {
  try {
    await keepsService.createKeep(editableKeepData.value)
  }
  catch (error) {
    Pop.error(error, 'Could not create keep')
    logger.error('COULD NOT CREATE KEEP', error)
  }
}
</script>


<template>
  <!-- TODO hide modal when finished creating keep -->
  <div class="modal fade" id="CreateKeepModal" tabindex="-1" aria-labelledby="CreateKeepModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="CreateKeepModalLabel">Create keep</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form>
            <form @submit.prevent="createKeep()">
              <div class="mb-3">
                <label for="name" class="form-label">Name</label>
                <input v-model="editableKeepData.name" type="text" class="form-control" id="name"
                  aria-describedby="nameHelp" maxlength="50" minlength="2" required>
              </div>
              <div class="mb-3">
                <label for="description" class="form-label">Description</label>
                <textarea v-model="editableKeepData.description" type="text" class="form-control" id="description"
                  maxlength="1000" required></textarea>
              </div>
              <div class="mb-3">
                <label for="img" class="form-label">Image</label>
                <input v-model="editableKeepData.img" type="url" class="form-control" id="img" maxlength="1000"
                  required>
              </div>
              <img v-if="editableKeepData.img" class="w-100 mb-2" :src="editableKeepData.img" alt="Keep Image Preview">
              <button type="submit" class="btn btn-success" title="submit form">Submit</button>
            </form>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>