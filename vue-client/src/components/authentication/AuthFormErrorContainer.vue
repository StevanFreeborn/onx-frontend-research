<script setup lang="ts">
defineProps<{
  errors: string[];
}>();
</script>

<template>
  <div v-if="errors.length > 0" class="error-container">
    <p v-for="error in errors" :key="error" class="error-message">
      {{ error }}
    </p>
  </div>
</template>

<style scoped>
.error-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 2px;
  background-color: #ffffffd9;
  font-weight: 700;
  margin: 1em -20px;
  padding: 10px 5px;
  text-align: center;
}

.error-container > p {
  color: #e74c3c;
}
</style>
