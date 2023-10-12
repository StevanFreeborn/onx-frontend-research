<script setup lang="ts">
import { computed, toRefs } from 'vue';
import ContentIcon from '../../icons/ContentIcon.vue';
import DashboardIcon from '../../icons/DashboardIcon.vue';
import GearIcon from '../../icons/GearIcon.vue';
import ReportIcon from '../../icons/ReportIcon.vue';
import SidebarSearchButton from './SidebarSearchButton.vue';
import SidebarSearchInput from './SidebarSearchInput.vue';

const props = defineProps<{
  isCollapsed: boolean;
}>();

const { isCollapsed } = toRefs(props);
const activeNavItemClass = computed(() =>
  isCollapsed.value ? 'nav-item-active-collapsed' : 'nav-item-active'
);

const navItemClass = computed(() =>
  isCollapsed.value ? 'nav-item-collapsed' : 'nav-item'
);
</script>

<template>
  <nav class="container">
    <div class="top-container">
      <SidebarSearchButton v-if="isCollapsed" />
      <SidebarSearchInput v-else />
      <div title="Dashboards" :class="activeNavItemClass">
        <div class="nav-item-icon-container">
          <DashboardIcon class="nav-item-icon" />
        </div>
        <span v-if="isCollapsed === false" class="nav-link">Dashboards</span>
      </div>
      <RouterLink title="Reports" to="/Report">
        <div :class="navItemClass">
          <div class="nav-item-icon-container">
            <ReportIcon class="nav-item-icon" />
          </div>
          <span v-if="isCollapsed === false" class="nav-link">Reports</span>
        </div>
      </RouterLink>
      <RouterLink title="Content" to="/Content">
        <div :class="navItemClass">
          <div class="nav-item-icon-container">
            <ContentIcon class="nav-item-icon" />
          </div>
          <span v-if="isCollapsed === false" class="nav-link">Content</span>
        </div>
      </RouterLink>
    </div>
    <div class="bottom-container">
      <RouterLink title="Administration" to="/Admin" class="nav-link">
        <div :class="navItemClass">
          <div class="gear-icon-container">
            <GearIcon class="gear-icon" />
          </div>
        </div>
      </RouterLink>
    </div>
  </nav>
</template>

<style scoped>
.container {
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.top-container,
.bottom-container {
  display: flex;
  flex-direction: column;
}

.nav-item,
.nav-item-collapsed,
.nav-item-active,
.nav-item-active-collapsed,
.nav-item-footer {
  display: flex;
  align-items: center;
  padding: 15px;
  gap: 7px;
  cursor: pointer;
  border-left: 5px solid transparent;
  border-right: 5px solid transparent;
  flex: 1;
}

.nav-item-collapsed,
.nav-item-active-collapsed {
  justify-content: center;
}

.nav-item-active,
.nav-item-active-collapsed {
  border-left-color: #f7941d;
  background-color: #5b627e;
}

.nav-item:hover,
.nav-item-collapsed:hover,
.nav-item-footer:hover {
  background-color: #5b627e;
}

.nav-item-icon-container {
  display: flex;
  align-items: center;
}

.nav-item-icon {
  width: 24px;
  fill: #ffffff;
  transform: scaleX(-1);
}

.nav-link {
  display: flex;
  color: #ffffff;
  text-decoration: none;
  font-weight: 700;
  font-size: 16px;
  letter-spacing: normal;
  text-transform: uppercase;
  font-family: 'Roboto Condensed', Arial, Helvetica, sans-serif;
}

.gear-icon-container {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 100%;
}

.gear-icon {
  width: 30px;
  fill: #ffffff;
  transform: rotate(90deg);
}
</style>
