<script setup>
import { profilesService } from '@/services/ProfilesService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';


const editableProfileData = ref({
  name: '',
  picture: '',
  coverImg: ''
})

async function editProfile() {
  try {
    await profilesService.editProfile(editableProfileData.value)
  }
  catch (error) {
    Pop.error(error, 'Could not edit account')
    logger.error('COULD NOT EDIT ACCOUNT', error)
  }
}

</script>


<template>
  <!-- Modal -->
  <div class="modal fade" id="EditAccount" tabindex="-1" aria-labelledby="EditAccountLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="EditAccountLabel">Edit Account</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form>
            <form>
              <div class="mb-3">
                <label for="name" class="form-label">Name</label>
                <input v-model="editableProfileData.name" type="text" class="form-control" id="name"
                  aria-describedby="nameHelp" maxlength="100" minlength="2">
              </div>
              <div class="mb-3">
                <label for="img" class="form-label">Profile Image</label>
                <input v-model="editableProfileData.picture" type="url" class="form-control" id="img" maxlength="1000">
              </div>
              <div class="mb-3">
                <label for="coverImg" class="form-label">Cover Image</label>
                <input v-model="editableProfileData.coverImg" type="url" class="form-control" id="coverImg"
                  maxlength="2000">
              </div>
              <button type="submit" class="btn btn-primary">Submit</button>
            </form>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>