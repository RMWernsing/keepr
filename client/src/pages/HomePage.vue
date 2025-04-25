<script setup>
import { AppState } from '@/AppState.js';
import Example from '@/components/Example.vue';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

onMounted(() => {
  getAllKeeps()
})

const keeps = computed(() => AppState.keeps)

async function getAllKeeps() {
  try {
    keepsService.getAllKeeps()
  }
  catch (error) {
    Pop.error(error, 'Could not get keeps')
    logger.error('COULD NOT GET KEEPS', error)
  }
}

</script>

<template>
  <section class="container mt-5">
    <div class="row">
      <div class="col-md-12">
        <div class="masonry-container">
          <div v-for="keep in keeps" :key="keep.id">
            <KeepCard :keep="keep" />
          </div>
        </div>
        <!-- <img :src="keep.img" alt="" class="w-100"> -->
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
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
