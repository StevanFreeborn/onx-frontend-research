<script setup lang="ts">
import { reactive, toRefs } from 'vue';

const props = defineProps<{
  padding: number;
}>();

const { padding } = toRefs(props);
const style = reactive({
  padding: `${padding.value}px`,
});
</script>

<template>
  <div class="search-container" :style="style">
    <input type="text" placeholder="Search All Content" />
    <div class="search-icon-container">
      <!-- TODO: search icon -->
    </div>
  </div>
</template>

<style scoped>
.search-container {
}

.search-icon-container {
}
</style>
