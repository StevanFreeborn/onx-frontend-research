<script setup lang="ts">
import SidebarSearchButton from './SidebarSearchButton.vue';
import SidebarSearchInput from './SidebarSearchInput.vue';

defineProps<{
  isCollapsed: boolean;
}>();
</script>

<template>
  <nav class="container">
    <div class="top-container">
      <SidebarSearchButton v-if="isCollapsed" />
      <SidebarSearchInput v-else />
    </div>
  </nav>
</template>

<style scoped></style>
