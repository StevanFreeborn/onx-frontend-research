<script setup lang="ts">
import { computed, onMounted, onUnmounted, ref } from 'vue';
import SearchIcon from '../../icons/SearchIcon.vue';
import SidebarSearchInput from './SidebarSearchInput.vue';

const isInputDisplayed = ref(false);
const containerRef = ref<HTMLDivElement | null>(null);

const inputClass = computed(() =>
  isInputDisplayed.value ? 'search-slider-slide' : 'search-slider'
);

function handleOutsideClick(event: MouseEvent) {
  if (
    isInputDisplayed.value &&
    containerRef.value &&
    containerRef.value.contains(event.target as Node) === false
  ) {
    isInputDisplayed.value = false;
    containerRef.value?.querySelector('input')?.blur();
  }
}

onMounted(() => {
  document.addEventListener('click', handleOutsideClick);
});

onUnmounted(() => {
  document.removeEventListener('click', handleOutsideClick);
});

function handleSearchButtonClick() {
  isInputDisplayed.value = true;
  containerRef.value?.querySelector('input')?.focus();
}
</script>

<template>
  <div ref="containerRef" class="search-container-collapsed">
    <button
      type="button"
      class="search-icon-container-collapsed"
      @click="handleSearchButtonClick"
    >
      <SearchIcon />
    </button>
    <div :class="inputClass">
      <SidebarSearchInput :padding="0" />
    </div>
  </div>
</template>

<style scoped>
.search-container-collapsed {
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  padding: 23px;
}

.search-slider,
.search-slider-slide {
  position: absolute;
  left: -250px;
  top: 7px;
  z-index: 2;
  padding: 15px;
  background-color: #6f82ab;
  border-top-right-radius: 3px;
  border-bottom-right-radius: 3px;
  transition: transform 0.5s ease;
}

.search-slider-slide {
  transform: translateX(250px);
}

.search-icon-container-collapsed {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 34px;
  height: 34px;
  padding: 1px 4px;
  border: solid 1px #acb0bb;
  background-color: #f4f5f8;
  border-left: 0;
  border-top-right-radius: 3px;
  border-bottom-right-radius: 3px;
  border-radius: 50%;
  cursor: pointer;
}
</style>
