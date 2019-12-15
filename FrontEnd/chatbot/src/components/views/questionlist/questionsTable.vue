<template>
    <ul class="NumMarker">
        <li class="questionText" v-for="(question,index) in questions" :key="index">
          <div class="questionBlock">
            {{question.text}}
            <div class="btPanel">
              <b-button size="sm" @click="edit(index)">Редактировать ✎</b-button>
              <b-button size="sm" @click="del(index)">Удалить &times;</b-button>
            </div>
          </div>

          <ul class="circleMarker">
            <li v-for="(button,index) in question.buttons" class="variantTex" :key="index">{{button.text}}</li>
          </ul>
        </li>
      </ul>
</template>
<script>
import draggable from "vuedraggable"
export default {
    components:{
        draggable
    },
    props:['questions'],
    methods:{
        edit(index){
            return this.$emit('edit',index)
        },
        del(index){
            return this.$emit('del',index)
        }
    }
}
</script>