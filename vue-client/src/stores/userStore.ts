import jwtDecode from 'jwt-decode';
import { defineStore } from 'pinia';
import { ref } from 'vue';

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

  function logUserOut() {
    localStorage.removeItem(USER_KEY);
    user.value = null;
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
  }

  return { user, logUserIn, logUserOut };
});
