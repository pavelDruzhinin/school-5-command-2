<template>
  <div class="layout">
    <div class="layout__navbar">
      <router-view name="navbar" />
    </div>
    <div class="layout__content">
      <router-view />
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style lang="scss">
@import url("https://fonts.googleapis.com/css?family=Lato:300,400,700|Noto+Sans:400,700&display=swap&subset=cyrillic");

.layout {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  background: #f8f9fa;
  font-family: 'Lato', sans-serif;
  &__content {
    display: flex;
    flex-direction: column;
    flex-grow: 1;
  }
}
</style>
