<script setup>
import { Keep } from '@/models/Keep.js';
import KeepDetailsModal from './KeepDetailsModal.vue';
import { Pop } from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { keepsService } from '@/services/KeepsService.js';


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

</script>


<template>
  <div type="button" :title="`Open details page for ${keep.name}`" data-bs-toggle="modal"
    data-bs-target="#keepDetailsModal" class="mb-3 position-relative image-container">
    <div @click="getKeepById(keep.id)">

      <img :src="keep.img" :alt="'An image of ' + keep.name" class="w-100 rounded shadow-lg image-container-img">
      <span class="overlay-text fw-bold text-light">{{ keep.name }}</span>
      <img class="overlay-img d-none d-md-block" :src="keep.creator.picture"
        :alt="`Profile picture for ${keep.creator.name}`">
    </div>
  </div>
  <!-- <KeepDetailsModal /> -->
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
</style>