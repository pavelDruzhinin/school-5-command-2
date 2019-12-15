<template>
  <b-modal title="Добавление вопроса" scrollable @ok="savequestion()" id="modal">
    <b-form-group label="Вопрос:" label-for="input-1">
      <b-form-textarea id="input-1" v-model="question.text" />
    </b-form-group>
    <b-form-group>
      <b-form-checkbox class="text" id="input-2" v-model="question.type">Варианты ответа</b-form-checkbox>
    </b-form-group>
    <b-form-group v-if="question.type" label="Варианты ответов:" label-for="variants">
      <b-button @click="addvariant()" variant="primary">Добавить</b-button>
      <b-list-group id="variants">
        <draggable v-model="question.buttons">
          <b-list-group-item class="answer" v-for="(button,index) in question.buttons" :key="index">
            <b-form-textarea v-model="button.text" />
            <b-button class="mt-3" @click="deletevariant(index)" variant="danger">Удалить</b-button>
          </b-list-group-item>
        </draggable>
      </b-list-group>
    </b-form-group>
  </b-modal>
</template>

<script>
import draggable from "vuedraggable";
export default {
  props: ["question"],
  components: {
    draggable
  },
  methods: {
    addvariant() {
      return this.$emit("addvariant");
    },
    deletevariant(index) {
      return this.$emit("deletevariant", index);
    },
    resetmodal() {
      return this.$emit("resetmodal");
    },
    savequestion() {
      return this.$emit("savequestion");
    }
  }
};
</script>

<style lang="scss" scoped>
  .answer {
    border: none;
    padding: 0;
    margin-top: 16px;
  }
</style>
