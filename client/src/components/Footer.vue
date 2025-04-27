<script setup>
import CreateKeepModal from './CreateKeepModal.vue';


</script>


<template>
  <section class="container-fluid bg-light py-3 border-top">
    <div class="row">
      <div class="col-6">
        <div>
          <button data-bs-toggle="modal" data-bs-target="#CreateVaultModal"
            class="btn btn-secondary rounded-pill fw-bold" title="Create Vault">Create Vault</button>
        </div>
      </div>
      <div class="col-6">
        <div>
          <button data-bs-toggle="modal" data-bs-target="#CreateKeepModal"
            class="btn btn-secondary rounded-pill fw-bold" title="Create Keep">Create Keep</button>
        </div>
      </div>
    </div>
  </section>
  <!-- <CreateKeepModal /> -->
</template>


<style lang="scss" scoped></style>