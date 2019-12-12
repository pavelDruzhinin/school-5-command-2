<template>
  <b-modal
    title="Добавление вопроса"
    @ok="savequestion()"
    id="modal"
  >
    <b-form-group label="Вопрос:" label-for="input-1">
      <b-form-textarea id="input-1" v-model="question.text" />
    </b-form-group>
    <b-form-group>
      <b-form-checkbox id="input-2" v-model="question.type">Варианты ответа</b-form-checkbox>
    </b-form-group>
    <b-form-group v-if="question.type" label="Варианты ответов:" label-for="variants">
      <b-button @click="addvariant()">Добавить вариант ✚</b-button>
      <b-list-group id="variants">
        <draggable v-model="question.buttons">
          <b-list-group-item v-for="(button,index) in question.buttons" :key="index">
            <b-form-input v-model="button.text" />
            <b-button @click="deletevariant(index)">Удалить &times;</b-button>
          </b-list-group-item>
        </draggable>
      </b-list-group>
    </b-form-group>
  </b-modal>
</template>

<script>
import draggable from "vuedraggable"
  export default {
    props: ['question'],
    components:{
        draggable
    },
    methods: {
      addvariant() {
        return this.$emit("addvariant");
      },
      deletevariant(index){
          return this.$emit('deletevariant',index)
      },
      resetmodal(){
          return this.$emit('resetmodal')   
      },
      savequestion(){
          return this.$emit('savequestion')   
      }
    }
  };
</script>