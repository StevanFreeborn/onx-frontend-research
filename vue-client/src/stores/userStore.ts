import jwtDecode from 'jwt-decode';
import { defineStore } from 'pinia';
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { client } from '../http/client';
import { authService } from '../services/authService';

const USER_KEY = 'onxAuth';

export type User = {
  id: string;
  expiresAt: number;
  token: string;
};

function getUserFromLocalStorage(): User | null {
  const user = localStorage.getItem(USER_KEY);
  return user === null ? null : JSON.parse(user);
}

export const useUserStore = defineStore('userStore', () => {
  const user = ref<User | null>(getUserFromLocalStorage());
  const router = useRouter();

  function logUserOut() {
    localStorage.removeItem(USER_KEY);
    user.value = null;
    router.push('/Public/Login');
  }

  function logUserIn(jwtToken: string) {
    const { sub, exp } = jwtDecode<{ sub: string; exp: number }>(jwtToken);
    const loggedInUser: User = {
      id: sub,
      expiresAt: exp * 1000,
      token: jwtToken,
    };
    localStorage.setItem(USER_KEY, JSON.stringify(loggedInUser));
    user.value = loggedInUser;
    router.push('/');
  }

  async function refreshAccessToken(originalRequest: Request) {
    const { refreshAccessToken } = authService(client());
    const unauthorizedResponse = new Response(null, { status: 401 });

    if (user.value === null) {
      logUserOut();
      return unauthorizedResponse;
    }

    const refreshResult = await refreshAccessToken(user.value.id);

    if (refreshResult.isFailed) {
      logUserOut();
      return unauthorizedResponse;
    }

    logUserIn(refreshResult.value.token);

    originalRequest.headers.set(
      'Authorization',
      `Bearer ${refreshResult.value.token}`
    );

    return await fetch(originalRequest);
  }

  function useAuthClient() {
    return client({
      authHeader: {
        Authorization: `Bearer ${user.value?.token}`,
      },
      unauthorizedResponseHandler: refreshAccessToken,
    });
  }

  return { user, logUserIn, logUserOut, useAuthClient };
});
