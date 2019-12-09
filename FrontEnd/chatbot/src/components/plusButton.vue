<template>
    <div id="plusButton">
        <button class="button" id="addButton" @click="click">
            +
        </button>
    </div>
</template>

<style lang="scss" scoped>
.dflex {
  display:flex;
}

.aligncenter {
  align-items: center;
}

#addButton {
    background-color: rgb(89, 89, 231);
    font-size: 50px;
      padding: 15px 32px;
}

</style>


<script>
export default {
  methods:{
    click(){
      return this.$emit('click');
    }
  }
}
</script>
