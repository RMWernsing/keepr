<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';


const keep = computed(() => AppState.activeKeep)

</script>


<template>
  <!-- Modal -->
  <div class="modal fade" id="keepDetailsModal" tabindex="-1" aria-labelledby="keepDetailsModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-xl modal-fullscreen-md-down">
      <div class="modal-content">
        <div class="modal-body p-0">
          <div class="container">
            <div class="row">
              <div :style="{ backgroundImage: `url(${keep?.img})` }" class="col-md-6 img-height rounded-start">
              </div>
              <div class="col-md-6 position-relative">
                <div class="d-flex justify-content-between mt-3">
                  <div>
                  </div>
                  <div>
                    <span class="mdi mdi-eye me-2"></span>
                    <span class="me-2">0</span>
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
                <div class="position-absolute form-position d-flex justify-content-between">
                  <form>
                    <div class="d-flex gap-2">
                      <select class="form-select w-50" aria-label="Default select example">
                        <option selected>Open this select menu</option>
                        <option value="1">One</option>
                        <option value="2">Two</option>
                        <option value="3">Three</option>
                      </select>
                      <button class="btn btn-primary">Save</button>
                    </div>
                  </form>
                  <div>
                    <img class="profile-img" :src="keep?.creator.picture"
                      :alt="`Profile picture for ${keep?.creator.name}`">
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
  height: 2rem;
  right: 10px;
}

@media (max-width: 769px) {
  .mobile-margin {
    margin-bottom: 8rem;
  }
}
</style>