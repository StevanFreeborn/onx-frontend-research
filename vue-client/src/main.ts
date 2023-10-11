import { createPinia } from 'pinia';
import { createApp } from 'vue';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import App from './App.vue';
import ProtectedLayout from './components/layouts/ProtectedLayout.vue';
import PublicLayout from './components/layouts/PublicLayout.vue';
import { useUserStore } from './stores/userStore';
import './style.css';
import DashboardView from './views/DashboardView.vue';
import LoginView from './views/LoginView.vue';

const routes: RouteRecordRaw[] = [
  {
    path: '/Public',
    component: PublicLayout,
    children: [
      {
        path: '/Public/Login',
        component: LoginView,
        beforeEnter: () => {
          const { user } = useUserStore();

          if (user !== null) {
            return '/';
          }

          return true;
        },
      },
    ],
  },
  {
    path: '/',
    component: ProtectedLayout,
    redirect: '/Dashboard',
    beforeEnter: () => {
      const { user } = useUserStore();

      if (user === null) {
        return '/Public/Login';
      }

      return true;
    },
    children: [
      {
        path: '/Dashboard',
        component: DashboardView,
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes: routes,
});

const pinia = createPinia();
const app = createApp(App);

app.use(pinia);
app.use(router);

app.mount('#app');
