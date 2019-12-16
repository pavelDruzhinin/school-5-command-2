<template>
  <ul>
    <li class="question-text" v-for="(question,index) in questions" :key="index">
      <div class="question-block my-2">
        {{question.text}}
        <div class="my-2">
          <b-button
            class="mr-2"
            variant="outline-success"
            size="sm"
            @click="edit(index)"
          >Редактировать</b-button>
          <b-button size="sm" variant="outline-danger" @click="del(index)">Удалить</b-button>
        </div>
      </div>
      <ul class="question-variant">
        <li
          v-for="(button,index) in question.buttons"
          class="variantTex"
          :key="index"
        >{{button.text}}</li>
      </ul>
    </li>
  </ul>
</template>

<script>
export default {
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

<style lang="scss" scoped>
.question-text {
  list-style: none;
}
.question-variant {
  list-style: disc;
}
</style>
