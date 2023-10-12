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
  <div class="form-group">
    <div class="icon-container">
      <slot name="icon"></slot>
    </div>
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

<style scoped>
.form-group {
  display: flex;
  width: 100%;
  border: 1px solid #acb0bb;
  background-color: #ffffff;
  margin-bottom: 10px;
  border-radius: 3px;
}

.form-group input {
  height: 30px;
  width: 100%;
  border-top-right-radius: 3px;
  border-bottom-right-radius: 3px;
  border: none;
  padding: 1px 1px 1px 10px;
  font-family: Roboto, Arial, Helvetica, sans-serif;
  letter-spacing: 0.25px;
  color: #555;
}

.form-group input::placeholder {
  font-style: italic;
  color: #cacaca;
}

.form-group input:focus-visible {
  outline: none;
}

.icon-container {
  background-color: #f4f5f8;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 30px;
  height: 30px;
  border-right: 1px solid #acb0bb;
  color: #7f7f7f;
}
</style>
