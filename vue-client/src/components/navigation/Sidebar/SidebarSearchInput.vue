<script setup lang="ts">
import { computed, toRefs } from 'vue';
import SearchIcon from '../../icons/SearchIcon.vue';

const props = defineProps<{
  padding?: number;
}>();

const { padding } = toRefs(props);
const style = computed(() => {
  return {
    padding: `${padding?.value}px`,
  };
});
</script>

<template>
  <div class="search-container" :style="style">
    <input type="text" placeholder="Search All Content" />
    <button type="button" class="search-icon-button">
      <SearchIcon class="search-icon" />
    </button>
  </div>
</template>

<style scoped>
.search-container {
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 10px;
}

.search-container > input {
  width: 151px;
  height: 34px;
  border-radius: 3px;
  border-top-right-radius: 0;
  border-bottom-right-radius: 0;
  padding: 1px 1px 1px 10px;
  outline: none;
  border: solid 1px #acb0bb;
  background-color: #ffffff;
}

.search-container > input::placeholder {
  font-style: italic;
  color: #cacaca;
}

.search-container > input:focus-visible {
  outline: none;
}

.search-icon-button {
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
  cursor: pointer;
}

.search-icon-button:hover {
  background-color: #fddfbc;
}

.search-icon {
  width: 22px;
  fill: #7f7f7f;
  transform: scaleX(-1);
  cursor: pointer;
}
</style>
