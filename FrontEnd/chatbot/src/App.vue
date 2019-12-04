<template>
  <div class="layout">
    <div class="layout__navbar">
      <router-view name="navbar" />
    </div>
    <div id="main" class="layout__content">
      <router-view/>
    </div>
  </div>
</template>

<style lang="scss">
  .layout {
    min-height: 100vh;
    display: flex;
    flex-direction: column;
    &__content {
      display: flex;
      flex-direction: column;
      flex-grow: 1;
    }
  }

</style>

<script>
export default {
}
</script>