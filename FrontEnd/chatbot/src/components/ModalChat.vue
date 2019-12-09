<template>
  <b-modal id="modal1" title="Добавить чат" hide-footer>
    <b-input-group>
      <b-form-input type="text" v-model="chat.Name"/>
      <b-button class="ml-3" @click="add">Добавить</b-button>
    </b-input-group>
  </b-modal>
</template>

<script>
  export default {
    props:['chat'],
    methods: {
      add() {
        return this.$emit("click");
      }
    }
  };
</script>