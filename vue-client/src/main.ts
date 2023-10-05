import { createApp } from 'vue';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import App from './App.vue';
import PublicLayout from './components/layouts/PublicLayout.vue';
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
      },
    ],
  },
  {
    path: '/',
    redirect: '/Dashboard',
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

const app = createApp(App);

app.use(router);

app.mount('#app');
