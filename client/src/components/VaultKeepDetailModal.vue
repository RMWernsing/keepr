<script setup>
import { AppState } from '@/AppState.js';
import { Keep } from '@/models/Keep.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';


const keep = computed(() => AppState.activeKeep)
const vault = computed(() => AppState.activeVault)
const account = computed(() => AppState.account)


const editableKeepData = ref({
  vaultId: ''
})

async function deleteVaultKeep(vaultKeepId) {
  try {
    const confirm = await Pop.confirm('Are you sure you want to remove this keep from your vault?')
    if (!confirm) {
      return
    }
    await vaultKeepsService.deleteVaultKeep(vaultKeepId)

  }
  catch (error) {
    Pop.error(error, 'Could not delete vault from keep')
    logger.error('COULD NOT DELETE VAULT KEEP', error)
  }
}


</script>


<template>
  <!-- Modal -->
  <!-- TODO fix the styling of the whole modal. it doesn't look great ☹️ -->
  <div class="modal fade" id="vaultKeepDetailsModal" tabindex="-1" aria-labelledby="vaultKeepDetailsModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-xl modal-fullscreen-md-down">
      <div class="modal-content">
        <div class="modal-body p-0">
          <div class="container">
            <div class="row">
              <div :style="{ backgroundImage: `url(${keep?.img})` }" class="col-md-5 img-height rounded-start">
              </div>
              <div class="col-md-7 position-relative">
                <div class="d-flex justify-content-between mt-3">
                  <div>
                  </div>
                  <div>
                    <span class="mdi mdi-eye me-2" :title="`view count is ${keep?.views}`"></span>
                    <span class="me-2" :title="`view count is ${keep?.views}`">{{ keep?.views }}</span>
                    <span class="mdi mdi-lock me-2"></span>
                    <span>0</span>
                  </div>
                  <button type="button" class="btn-close text-end" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="flex-grow">
                  <div>
                    <h1 class="text-center mt-5 pt-5">{{ keep?.name }}</h1>
                    <p class="px-3 mt-5 mobile-margin">{{ keep?.description }}</p>
                  </div>
                </div>
                <div class="position-absolute form-position d-flex justify-content-between flex-grow-1">
                  <div data-bs-dismiss="modal" @click="deleteVaultKeep(keep.vaultKeepId)"
                    v-if="account?.id == vault?.creatorId">
                  </div>
                  <RouterLink v-if="keep" :to="{ name: 'Profile', params: { profileId: keep?.creator.id } }">
                    <div :title="`navigate to ${keep.creator.name}'s profile page'`" data-bs-dismiss="modal"
                      class="d-flex gap-2 flex-wrap no-underline">
                      <img class="profile-img" :src="keep?.creator.picture"
                        :alt="`Profile picture for ${keep?.creator.name}`">
                      <span class="profile-name text-dark">{{ keep?.creator.name }}</span>
                    </div>
                  </RouterLink>
                  <div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.img-height {
  height: 85dvh;
  background-position: center;
  background-size: cover;
}

.form-position {
  bottom: 14px;
}

.mobile-margin {
  margin-bottom: 0rem;
}

.profile-img {
  height: 2.5rem;
  top: 10px;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
}

.profile-name {
  text-wrap: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
  white-space: nowrap;
}

a {
  text-decoration: none;
}

@media (max-width: 769px) {
  .mobile-margin {
    margin-bottom: 8rem;
  }
}
</style>