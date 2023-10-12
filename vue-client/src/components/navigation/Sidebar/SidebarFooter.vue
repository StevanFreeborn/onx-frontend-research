<script setup lang="ts">
import { computed, onMounted, onUnmounted, ref, toRefs } from 'vue';
import { client } from '../../../http/client';
import { SystemStatus, statusService } from '../../../services/statusService';
import LeftArrowIcon from '../../icons/LeftArrowIcon.vue';
import QuestionMarkIcon from '../../icons/QuestionMarkIcon.vue';
import RightArrowIcon from '../../icons/RightArrowIcon.vue';

const props = defineProps<{
  isCollapsed: boolean;
}>();

defineEmits<{
  (e: 'update:isCollapsed'): void;
}>();

const { isCollapsed } = toRefs(props);
const status = ref(SystemStatus.None);
const footerClass = computed(() => {
  return isCollapsed.value ? 'container-collapsed' : 'container';
});
const statusStyle = computed(() => {
  let color = '#2fcc66';

  switch (status.value) {
    case SystemStatus.Critical:
      color = 'red';
      break;
    case SystemStatus.Major:
      color = 'orange';
      break;
    case SystemStatus.Minor:
      color = 'yellow';
      break;
    case SystemStatus.Maintenance:
      color = 'blue';
      break;
    case SystemStatus.None:
    default:
      break;
  }

  return {
    backgroundColor: color,
  };
});
const { getStatus } = statusService(client({ includeCredentials: false }));

const interval = ref(0);

onMounted(() => {
  interval.value = setInterval(async () => {
    const statusResult = await getStatus();
    if (statusResult.isSuccess) {
      status.value = statusResult.value.status;
    }
  }, 15000);

  getStatus().then(statusResult => {
    if (statusResult.isSuccess) {
      status.value = statusResult.value.status;
    }
  });
});

onUnmounted(() => {
  clearInterval(interval.value);
});
</script>

<template>
  <footer :class="footerClass">
    <div title="Collapse" class="footer-item">
      <button
        type="button"
        class="footer-icon-container"
        @click="$emit('update:isCollapsed')"
      >
        <RightArrowIcon v-if="isCollapsed" class="footer-icon" />
        <LeftArrowIcon v-else class="footer-icon" />
      </button>
    </div>
    <div title="Help" class="footer-item">
      <a href="/Help/OnspringDocumentation.htm" class="footer-icon-container">
        <QuestionMarkIcon class="footer-icon" />
      </a>
    </div>
    <div title="All Systems Operational" class="footer-item">
      <div class="footer-icon-container">
        <div class="status-icon" :style="statusStyle"></div>
      </div>
    </div>
  </footer>
</template>

<style scoped>
.container,
.container-collapsed {
  display: flex;
}

.container-collapsed {
  flex-direction: column-reverse;
}

.footer-item {
  display: flex;
  flex: 1 0 auto;
  border: 1px solid #536a9b;
  border-bottom: 0;
  justify-content: center;
  height: 60px;
  flex-shrink: 0;
}

.footer-item:hover {
  background-color: #5b627e;
}

.footer-icon-container {
  display: flex;
  align-items: center;
  justify-content: center;
  background: transparent;
  border: none;
  cursor: pointer;
}

.footer-icon {
  width: 30px;
  fill: #ffffff;
}

.status-icon {
  top: 4px;
  width: 24px;
  height: 24px;
  border: 2px solid #fff;
  background-color: #2fcc66;
  border-radius: 50%;
}
</style>
