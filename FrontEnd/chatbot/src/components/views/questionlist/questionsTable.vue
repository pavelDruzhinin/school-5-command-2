<template>
  <ul class="NumMarker">
    <draggable v-model="questions">
      <li class="questionText" v-for="(question,index) in questions" :key="index">
        <div class="questionBlock">
          {{question.text}}
          <div class="btPanel">
            <b-button variant="outline-success" @click="edit(index)">Редактировать</b-button>
            <b-button variant="outline-danger" @click="del(index)">Удалить</b-button>
          </div>
        </div>
        <ul class="circleMarker">
          <li
            v-for="(button,index) in question.buttons"
            class="variantTex"
            :key="index"
          >{{button.text}}</li>
        </ul>
      </li>
    </draggable>
  </ul>
</template>

<script>
import draggable from "vuedraggable";
export default {
  components: {
    draggable
  },
  props: ["questions"],
  methods: {
    edit(index) {
      return this.$emit("edit", index);
    },
    del(index) {
      return this.$emit("del", index);
    }
  }
};
</script>

<style lang="scss">

</style>
