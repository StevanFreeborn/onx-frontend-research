<script setup lang="ts">
import { reactive, ref } from 'vue';
import SidebarSearchInput from './SidebarSearchInput.vue';

const isInputDisplayed = ref(false);
const containerRef = ref<HTMLDivElement | null>(null);

const classes = reactive({
  'search-slider': isInputDisplayed.value === false,
  'search-slider-slide': isInputDisplayed.value === true,
});
</script>

<template>
  <div ref="containerRef" class="search-container-collapsed">
    <button type="button" class="search-icon-container-collapsed">
      <!-- TODO: search icon -->
    </button>
    <div :class="classes">
      <SidebarSearchInput :padding="0" />
    </div>
  </div>
</template>

<style scoped>
.search-container-collapsed {
}

.search-slider {
}

.search-slider-slide {
}

.search-icon-container-collapsed {
}
</style>
