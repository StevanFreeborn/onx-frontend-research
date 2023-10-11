<script setup lang="ts">
import SidebarNav from '../navigation/Sidebar/SidebarNav.vue';
</script>

<template>
  <div class="container">
    <SidebarNav />
    <main class="main">
      <router-view></router-view>
    </main>
  </div>
</template>

<style scoped>
.container {
  display: flex;
  min-width: 1000px;
  background-color: #f5f5f5;
  height: 100%;
}

.main {
  flex: 1;
}
</style>
