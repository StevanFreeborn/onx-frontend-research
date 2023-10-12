<script setup lang="ts">
import { computed, ref } from 'vue';
import SidebarBody from './SidebarBody.vue';
import SidebarFooter from './SidebarFooter.vue';
import SidebarHeader from './SidebarHeader.vue';

const isCollapsed = ref(false);
const asideClass = computed(() =>
  isCollapsed.value ? 'sidebar-collapsed' : 'sidebar'
);

const toggleSidebar = () => {
  isCollapsed.value = !isCollapsed.value;
};
</script>

<template>
  <aside :class="asideClass">
    <SidebarHeader :is-collapsed="isCollapsed" />
    <SidebarBody :is-collapsed="isCollapsed" />
    <SidebarFooter
      :is-collapsed="isCollapsed"
      @update:is-collapsed="toggleSidebar"
    />
  </aside>
</template>

<style scoped>
.sidebar,
.sidebar-collapsed {
  display: flex;
  flex-direction: column;
  flex: 0 0 auto;
  width: 200px;
  background-color: #405a90;
}

.sidebar-collapsed {
  width: 80px;
}
</style>
