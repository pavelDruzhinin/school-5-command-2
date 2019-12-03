<template>
  <div class="registration">
    <b-form @submit.prevent="register" @reset.prevent="onReset" class="registration__form">
      <b-form-group 
        id="input-group-1" 
        label="First Name:" 
        label-for="input-1"
        :class="{errInput:$v.form.FirstName.$error}">
        <b-form-input 
        id="input-1" 
        v-model.lazy="form.FirstName" 
        placeholder="Enter First Name"></b-form-input>
        <ValidateFirstName :v="$v" />
      </b-form-group>

      <b-form-group 
        id="input-group-2" 
        label="Last Name:" 
        label-for="input-2"
        :class="{errInput:$v.form.LastName.$error}">
        <b-form-input 
          id="input-2" 
          v-model.lazy="form.LastName" 
          placeholder="Enter Last Name"></b-form-input>
        <ValidateLastName :v="$v" />
      </b-form-group>

      <b-form-group 
        id="input-group-3" 
        label="Middle Name:" 
        label-for="input-3"
        :class="{errInput:$v.form.MiddleName.$error}">
        <b-form-input 
          id="input-3" 
          v-model.lazy="form.MiddleName" 
          placeholder="Enter Middle Name"></b-form-input>
        <ValidateMiddleName :v="$v" />
      </b-form-group>

      <b-form-group
        id="input-group-4"
        label="Email"
        label-for="input-4"
        :class="{errInput:$v.form.Email.$error}">
        <b-form-input 
          id="input-4" 
          v-model.lazy="form.Email" 
          placeholder="Enter Email"></b-form-input>
        <ValidateEmail :v="$v" />
      </b-form-group>

      <b-form-group
        id="input-group-5"
        label="Password"
        label-for="input-5"
        :class="{errInput:$v.form.Password.$error}">
        <b-form-input
          id="input-5"
          v-model.lazy="form.Password"
          type="password"
          placeholder="Enter Password"
        ></b-form-input>
        <ValidatePassword :v="$v" />
      </b-form-group>

      <b-form-group
        id="input-group-6"
        label="Confirm Password"
        label-for="input-6"
        :class="{errInput:$v.form.ConfirmPassword.$error}">
        <b-form-input
          id="input-5"
          v-model.lazy="form.ConfirmPassword"
          type="password"
          placeholder="Confirm Password"
        ></b-form-input>
        <ValidatePassConfirm :v="$v" />
      </b-form-group>

      <b-button type="submit" variant="primary">Submit</b-button>
      <b-button type="reset" variant="danger">Reset</b-button>
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
  import ValidatePassConfirm from "@/components/Validations/ValidatePassConfirm.vue"
  import ValidateFirstName from "@/components/Validations/ValidateFirstName.vue"
  import ValidateLastName from "@/components/Validations/ValidateLastName.vue"
  import ValidateMiddleName from "@/components/Validations/ValidateMiddleName.vue"

  export default {
    components: {
      ValidateEmail,
      ValidatePassword,
      ValidatePassConfirm,
      ValidateFirstName,
      ValidateLastName,
      ValidateMiddleName
    },
    mixins: [validationMixin],
    data() {
      return {
        form: {
          Email: "",
          FirstName: "",
          LastName: "",
          MiddleName: "",
          Password: "",
          ConfirmPassword:""
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
          minLength: minLength(4)
        },
        ConfirmPassword: {
          required,
          minLength: minLength(4),
          sameAsPassword: sameAs("Password")
        },
        FirstName:{
          required,
          alpha
        },
        LastName:{
          required,
          alpha
        },
        MiddleName: {
          required,
          alpha
        }
      }
    },
    methods: {
      register() {
        this.$v.$touch();
        if(!this.$v.$invalid) this.$store.dispatch("register", this.form)
                                  .then(response=>this.$router.push("/"));
      },
      onReset() {
        this.$v.$reset();
        // Reset our form values
        this.form.Email = "";
        this.form.FirstName = "";
        this.form.LastName = "";
        this.form.MiddleName = "";
        this.form.Password="",
        this.form.ConfirmPassword=""
      }
    }
  };
</script>

<style lang="scss">
  .registration {
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