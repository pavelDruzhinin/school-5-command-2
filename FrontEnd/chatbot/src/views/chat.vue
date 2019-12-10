<template>
  <div id="chat_wrap">
    <div id="chat">
    <ChatForm /> 
    </div>
    <div id="chat-form">
     <Message />
    </div>
    
  </div>
</template>

<script>
import Message from "../components/Message";
import ChatForm from "../components/ChatForm";
export default {
  components:{
    Message,
    ChatForm
  }
};
</script>

<style>
#chat_wrap {
  
  
  overflow: hidden;
  display:grid;
  grid-template-areas: 
  "chat"
  "input";
  grid-template-rows: 1fr 60px;
}
#chat {
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  height: 80px;
  background: #5e5f5f;
  grid-area:chat;
}
#chat_form {
  overflow:scroll;
  top: 0;
  left: 0;
  right: 0;
  background: #06e0f0;
  padding: 1rem;
  overflow-y: auto;
  grid-area: input;
}
</style>

