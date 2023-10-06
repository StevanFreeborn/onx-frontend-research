<script setup language="ts">
import logo from '../../assets/images/logos/testing-demo-logo.svg';
</script>

<template>
  <div class="container">
    <div class="inner-container">
      <div className="logo-container">
        <img :src="logo" alt="company logo" />
      </div>
      <div class="copyright-container">
        <p></p>
        <p></p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.container {
  display: flex;
  flex-direction: column;
  width: 380px;
  height: 520px;
  box-shadow: 0 0 15px 0 rgba(0, 0, 0, 0.35);
  background: linear-gradient(
    -180deg,
    rgba(16, 49, 116, 0.8) 30%,
    rgba(74, 135, 227, 0.8) 46%,
    rgba(61, 88, 141, 0.8) 93%
  );
  border-radius: 3px;
  border: 1px solid #acb0bb;
}

.inner-container {
  height: 100%;
  padding: 30px 20px 20px 20px;
  display: flex;
  flex-direction: column;
}

.logo-container {
  display: flex;
  justify-content: center;
  padding: 20px 20px 30px 20px;
}

.logo-container img {
  width: 240px;
  height: 72px;
}

.copyright-container {
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  flex: 1;
}

.copyright-container > p {
  text-align: center;
  color: #d4d4d4;
  font-size: 11px;
  letter-spacing: normal;
  font-family: 'Roboto Condensed', Arial, Helvetica, sans-serif;
}
</style>
