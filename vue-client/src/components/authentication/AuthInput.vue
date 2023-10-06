<script setup lang="ts">
defineProps<{
  type: string;
  modelValue: string;
  placeholder: string;
  readOnly?: boolean;
}>();

defineEmits<{
  (e: 'update:modelValue', value: string): void;
}>();
</script>

<template>
  <div>
    <div></div>
    <input
      :type="type"
      :value="modelValue"
      :placeholder="placeholder"
      :readOnly="readOnly"
      @input="
        $emit('update:modelValue', ($event.target as HTMLInputElement).value)
      "
    />
  </div>
</template>

<style scoped></style>
