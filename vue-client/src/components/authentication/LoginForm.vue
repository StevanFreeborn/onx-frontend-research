<script setup lang="ts">
import { ref } from 'vue';
import { client } from '../../http/client';
import { authService } from '../../services/authService';
import { useUserStore } from '../../stores/userStore';
import LockIcon from '../icons/LockIcon.vue';
import ProfileIcon from '../icons/ProfileIcon.vue';
import AuthFormErrorContainer from './AuthFormErrorContainer.vue';
import AuthInput from './AuthInput.vue';

const { logUserIn } = useUserStore();
const { login } = authService(client());

type FormState = {
  username: string;
  password: string;
  errors: string[];
};

const formState = ref<FormState>({
  username: '',
  password: '',
  errors: [],
});

async function handleFormSubmit() {
  formState.value.errors = [];

  if (!formState.value.username) {
    formState.value.errors.push('Username is required');
  }

  if (!formState.value.password) {
    formState.value.errors.push('Password is required');
  }

  if (formState.value.errors.length) {
    return;
  }

  const loginResult = await login(
    formState.value.username,
    formState.value.password
  );

  if (loginResult.isFailed) {
    formState.value.errors.push(loginResult.error.message);
    return;
  }

  logUserIn(loginResult.value.token);
}
</script>

<template>
  <form @submit.prevent="handleFormSubmit">
    <AuthInput v-model="formState.username" type="text" placeholder="Username">
      <template #icon>
        <ProfileIcon />
      </template>
    </AuthInput>

    <AuthInput
      v-model="formState.password"
      type="password"
      placeholder="Password"
    >
      <template #icon>
        <LockIcon />
      </template>
    </AuthInput>

    <div class="action-container">
      <RouterLink class="forgot-password-link" to="/Public/ForgotPassword">
        Forgot Password
      </RouterLink>
      <button class="login-button" type="submit">Login</button>
    </div>

    <AuthFormErrorContainer :errors="formState.errors" />
  </form>
</template>

<style scoped>
.action-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.forgot-password-link {
  color: #fff;
  text-decoration: none;
  font-size: 11px;
}

.forgot-password-link:hover {
  text-decoration: underline;
}

.login-button {
  border: 0;
  background: #f7941d;
  color: #fff;
  padding: 7.5px 15px;
  border-radius: 3px;
  cursor: pointer;
}

.login-button:hover {
  background-color: #df8419;
}
</style>
