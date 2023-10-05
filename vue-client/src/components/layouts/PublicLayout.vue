<script setup lang="ts">
import dew from '../../assets/images/backgrounds/dew.jpg';
import kromlau from '../../assets/images/backgrounds/kromlau.jpg';
import marsh from '../../assets/images/backgrounds/marsh.jpg';

const images = [dew, kromlau, marsh];
const randomImage = images[Math.floor(Math.random() * images.length)];
</script>

<template>
  <main class="container" :style="{ backgroundImage: `url(${randomImage})` }">
    <router-view></router-view>
  </main>
</template>

<style scoped>
.container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}
</style>
