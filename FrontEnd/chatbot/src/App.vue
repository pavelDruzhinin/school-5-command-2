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
  #main {
    height: 100%;
    width: 100%;
    background-image: url(./assets/back.jpg);
    background-repeat: no-repeat;
    background-position: top center;
    background-attachment: fixed;
    background-size: cover;
    

  }
  .layout {
    
    min-height: 100vh;
    display: flex;
    flex-direction: column;
    background: #f9f8f8;
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