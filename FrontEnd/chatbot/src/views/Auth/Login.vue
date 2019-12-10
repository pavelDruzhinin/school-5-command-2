<template>
  <div class="login">
    <b-form @submit.prevent="login" class="login__form">
      <b-form-group
        id="input-group-4"
        label="Email"
        label-for="input-4"
        :class="{errInput:$v.form.Email.$error}"
      >
        <b-form-input id="input-4" v-model.lazy="form.Email" placeholder="Enter Email"></b-form-input>
        <ValidateEmail :v="$v" />
      </b-form-group>

      <b-form-group
        id="input-group-5"
        label="Password"
        label-for="input-5"
        :class="{errInput:$v.form.Password.$error}"
      >
        <b-form-input
          id="input-5"
          v-model.lazy="form.Password"
          type="password"
          placeholder="Enter Password"
        ></b-form-input>
        <ValidatePassword :v="$v" />
      </b-form-group>
      <b-form-checkbox class="mb-3" v-model="form.RememberMe">Remember Me</b-form-checkbox>
      <b-button type="submit" variant="primary">Log In</b-button>
    </b-form>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import {
  required,
  email,
  minLength,
  sameAs,
  alpha
} from "vuelidate/lib/validators";

import ValidateEmail from "@/components/Validations/ValidateEmail.vue";
import ValidatePassword from "@/components/Validations/ValidatePassword.vue";

export default {
  mixins: [validationMixin],
  components: {
    ValidateEmail,
    ValidatePassword
  },
  data() {
    return {
      form: {
        Email: "",
        Password: "",
        RememberMe: true
      }
    };
  },
  validations: {
    form: {
      Email: {
        required,
        email
      },
      Password: {
        required,
        minLength: minLength(5)
      }
    }
  },
  methods: {
    login() {
      this.$v.$touch();
      if (!this.$v.$invalid)
        this.$store
          .dispatch("login", this.form)
          .then(response => this.$router.push("/")); // здесь заменим ссылку на лк
    }
  }
};
</script>

<style lang="scss">
.login {
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  align-items: center;
  justify-content: center;
  &__form {
    max-width: 480px;
  }
}
.form-group {
  .error {
    display: none;
    font-size: 20.4px;
    color: rgba(4, 0, 255, 0.425);
  }
  &.errInput {
    .error {
      display: inline;
    }
  }
}
</style>