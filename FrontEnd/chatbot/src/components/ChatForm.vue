<template>
    <textarea placeholder="Введите сообщение" v-model="text" @keydown.enter="send" />
</template>

<style scoped>
    textarea {
      width: 100%;
      height: 100%;
      
    }
</style>
<script>
export default {
  data: () => ({ text: "" }),
  methods: {
    send() {
      отправить_сообщение;
    }
  }
};
</script>