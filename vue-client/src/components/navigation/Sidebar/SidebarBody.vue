<script setup lang="ts">
defineProps<{
  isCollapsed: boolean;
}>();
</script>

<template>
  <div>
    <div>{{ isCollapsed }}</div>
  </div>
</template>

<style scoped></style>
