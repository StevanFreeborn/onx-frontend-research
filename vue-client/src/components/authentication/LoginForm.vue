<script setup lang="ts">
import { ref } from 'vue';
import AuthFormErrorContainer from './AuthFormErrorContainer.vue';
import AuthInput from './AuthInput.vue';

const formState = ref({
  username: '',
  password: '',
  errors: [],
});
</script>

<template>
  <form>
    <AuthInput
      v-model="formState.username"
      type="text"
      placeholder="Username"
    />

    <AuthInput
      v-model="formState.password"
      type="password"
      placeholder="Password"
    />

    <div class="field">
      <div class="control">
        <button class="submit">Submit</button>
      </div>
    </div>
    <AuthFormErrorContainer :errors="formState.errors" />
  </form>
</template>

<style scoped></style>
