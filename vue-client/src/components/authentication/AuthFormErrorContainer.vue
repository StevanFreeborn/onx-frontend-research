<script setup lang="ts">
defineProps<{
  errors: string[];
}>();
</script>

<template>
  <div v-if="errors.length > 0" class="error-container">
    <div v-for="error in errors" :key="error" class="error-message">
      {{ error }}
    </div>
  </div>
</template>

<style scoped></style>
