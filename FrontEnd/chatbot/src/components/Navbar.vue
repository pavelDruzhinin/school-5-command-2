<template>
  <b-navbar toggleable="md" type="dark" variant="dark" sticky>
    <b-navbar-brand to="/">Logo</b-navbar-brand>
    <b-navbar-toggle target="nav-collapse" />
    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item to="/">Home</b-nav-item>
      </b-navbar-nav>
      <b-navbar-nav class="ml-auto">
        <b-nav-form v-if="!isAuthenticated">
          <b-button class="my-2 my-sm-0 mr-sm-2" @click="$router.push({ name: 'Login' })" variant="outline-light">Log In</b-button>
          <b-button class="my-2 my-sm-0" @click="$router.push({ name: 'Registration' })" variant="outline-light">Register</b-button>
        </b-nav-form>
        <b-nav-form v-else>
          <b-nav-text class="mr-2 my-sm-0">{{profile.fullname}}</b-nav-text>
          <b-button class @click.prevent="logout" type="submit" variant="outline-light">Logout</b-button>
        </b-nav-form>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</template>

<script>
  import { mapState, mapGetters, mapActions } from "vuex";
  export default {
    computed: {
      ...mapState(["profile"]),
      ...mapGetters(["isAuthenticated"])
    },
    methods: {
      logout() {
        this.$store.dispatch("logout").
        then(response => this.$router.push("/"));
      }
    }
  };
</script>

<style lang="scss">

</style>
