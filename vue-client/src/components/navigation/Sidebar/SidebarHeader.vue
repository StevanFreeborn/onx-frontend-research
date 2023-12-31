<script setup lang="ts">
import { computed, onMounted, onUnmounted, ref, toRefs } from 'vue';
import defaultProfilePicture from '../../../assets/images/defaults/profile-picture.png';
import logo from '../../../assets/images/logos/testing-demo-logo-180-47.svg';
import { authService } from '../../../services/authService';
import { useUserStore } from '../../../stores/userStore';
import LogoutIcon from '../../icons/LogoutIcon.vue';
import PreferencesIcon from '../../icons/PreferencesIcon.vue';
import ProfileIcon from '../../icons/ProfileIcon.vue';

const props = defineProps<{
  isCollapsed: boolean;
}>();

const { isCollapsed } = toRefs(props);
const isProfileModalOpen = ref(false);
const modalRef = ref<HTMLDivElement | null>(null);

function handleOutsideClick(event: MouseEvent) {
  if (
    isProfileModalOpen.value &&
    modalRef.value &&
    modalRef.value.contains(event.target as Node) === false
  ) {
    isProfileModalOpen.value = false;
  }
}

onMounted(() => {
  document.addEventListener('click', handleOutsideClick);
});

onUnmounted(() => {
  document.removeEventListener('click', handleOutsideClick);
});

const logoContainerClass = computed(() => {
  return isCollapsed.value ? 'logo-container-collapsed' : 'logo-container';
});

const profileContainerClass = computed(() => {
  return isCollapsed.value
    ? 'profile-container-collapsed'
    : 'profile-container';
});

const username = 'John Doe';
const role = 'Administrator';

function handleProfilePictureButtonClick() {
  isProfileModalOpen.value = !isProfileModalOpen.value;
}

function handleProfileButtonClick() {
  alert('Should open profile modal');
  isProfileModalOpen.value = false;
}

function handlePreferencesClick() {
  alert('Should open preferences modal');
  isProfileModalOpen.value = false;
}

const { logUserOut, useAuthClient } = useUserStore();
const { logout } = authService(useAuthClient());

async function handleLogoutButtonClick() {
  const logoutResult = await logout();
  if (logoutResult.isFailed) {
    alert('Error logging out');
    return;
  } else {
    logUserOut();
  }

  isProfileModalOpen.value = false;
}
</script>

<template>
  <header class="container">
    <div :class="logoContainerClass">
      <RouterLink to="/">
        <img :src="logo" alt="testing-demo-logo" class="logo" />
      </RouterLink>
    </div>
    <div :class="profileContainerClass">
      <div ref="modalRef" style="position: relative">
        <button
          class="profile-picture-button"
          type="button"
          @click="handleProfilePictureButtonClick"
        >
          <img alt="default-profile" :src="defaultProfilePicture" />
        </button>
        <div v-if="isProfileModalOpen" class="profile-modal">
          <ul>
            <li v-if="isCollapsed" class="profile-modal-info">
              <div class="profile-modal-username">{{ username }}</div>
              <div class="profile-modal-current-role">{{ role }}</div>
            </li>
            <hr v-if="isCollapsed" />
            <li>
              <button type="button" @click="handleProfileButtonClick">
                <ProfileIcon class="modal-icon" />
                <span>Profile</span>
              </button>
            </li>
            <li>
              <button type="button" @click="handlePreferencesClick">
                <PreferencesIcon class="modal-icon" />
                <span>Preferences</span>
              </button>
            </li>
            <hr />
            <li>
              <button type="button" @click="handleLogoutButtonClick">
                <LogoutIcon class="modal-icon" />
                <span>Logout</span>
              </button>
            </li>
          </ul>
        </div>
      </div>
      <div v-if="isCollapsed === false">
        <div class="profile-info-container">
          <div class="username">{{ username }}</div>
          <div class="current-role">{{ role }}</div>
        </div>
      </div>
    </div>
  </header>
</template>

<style scoped>
.container {
  background-color: #103174;
  padding: 10px 10px 20px 10px;
}

.logo-container,
.logo-container-collapsed {
  display: flex;
  align-items: center;
  justify-content: center;
}

.logo-container img {
  width: 180px;
}

.logo-container-collapsed img {
  width: 60px;
}

.profile-container,
.profile-container-collapsed {
  display: flex;
  align-items: center;
  gap: 10px;
}

.profile-container-collapsed {
  justify-content: center;
}

.profile-picture-button {
  display: flex;
  justify-content: center;
  align-items: center;
  background: none;
  border: none;
}

.profile-picture-button > img {
  height: 48px;
  width: 48px;
  border: 0;
  border-radius: 50%;
  cursor: pointer;
  vertical-align: middle;
}

.profile-modal-container {
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
}

.profile-modal {
  position: absolute;
  top: 121%;
  left: 0px;
  background-color: #ffffff;
  box-shadow: 0 2px 10px 0 rgba(0, 0, 0, 0.2);
  border-radius: 0.25rem;
  border: solid 1px #acb0bb;
  z-index: 100;
}

.profile-modal::after {
  content: '';
  position: absolute;
  top: -10px;
  left: 13px;
  border-left: 10px solid transparent;
  border-right: 10px solid transparent;
  border-bottom: 10px solid #ffffff;
}

.profile-modal ul {
  margin: 0.5rem 0;
  list-style: none;
}

.profile-modal button {
  display: flex;
  align-items: center;
  justify-content: flex-start;
  gap: 0.5rem;
  background: none;
  border: none;
  width: 100%;
  cursor: pointer;
  padding: 0.25rem 1rem;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  font-size: 15px;
}

.profile-modal button:hover {
  background-color: #fddfbc;
}

.profile-modal hr {
  margin: 0.25rem 0;
}

.profile-info-container > div {
  font-family: 'Roboto Condensed', Arial, Helvetica, sans-serif;
}

.username,
.profile-modal-username {
  color: #ccc;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.current-role,
.profile-modal-current-role {
  font-weight: 300;
  font-size: 12px;
  color: #ccc;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  line-height: 18px;
  max-width: 120px;
}

.profile-modal-info {
  padding: 0.25rem 1rem;
  white-space: nowrap;
  font-weight: 400;
}

.profile-modal-username,
.profile-modal-current-role {
  color: #103174;
}

.profile-modal-username {
  font-weight: 600;
}

.modal-icon {
  width: 20px;
  height: 20px;
}
</style>
