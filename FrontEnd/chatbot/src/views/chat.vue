<template>
  <div id="chat_wrap">
    <div id="chat">
      <textarea
        placeholder="Введите сообщение"
        v-model="text.message"
        @keydown.enter="send"
        :disabled="disabled"
      />
    </div>
    <div id="chat-form">
      <div class="wrap">
        <div class="mes" v-for="(chat,index) of chat" :key="index">
          <small>
            <strong>{{chat.user}}</strong>
          </small>
          <p>{{chat.message}} {{chat.radio}}</p>
          <ul v-show="chat.buttons" v-for="(variant, index) of chat.variants" :key="index">
            <li>
              <input
                type="radio"
                name="variant"
                v-on:change="send"
                :value="variant"
                v-model="text.radio"
              />
              {{variant}}
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import Message from "../components/Message";
  import ChatForm from "../components/ChatForm";
  import { LogLevel, HubConnectionBuilder } from "@aspnet/signalr";
  export default {
    data() {
      return {
        count: 0,
        questions: [
          {
            user: "Admin",
            message: "Привет",
            buttons: false,
            variants: "",
            disInput: false
          },
          {
            user: "Admin",
            message: "Как дела",
            buttons: false,
            variants: "",
            disInput: false
          },
          {
            user: "Admin",
            message: "Как в школе?",
            buttons: true,
            variants: ["Нормально", "Всё плохо"],
            disInput: true
          },
          {
            user: "Admin",
            message: "А Коля выйдет?",
            buttons: false,
            variants: "",
            disInput: false
          }
        ],
        disabled: false,
        text: {
          user: "Respondent",
          message: "",
          buttons: false,
          variants: "",
          disInput: false,
          radio: ""
        },
        chat: []
      };
    },
    created() {
      // debugger;
      this.$http
        .get("/chats/getsession/" + this.$route.params.id)
        .then(response => (this.session = response.data.sessionId));
      let signalr = new HubConnectionBuilder()
        .withUrl("/chat")
        .configureLogging(LogLevel.Information)
        .build();
      signalr
        .start()
        .then(() => {
          // signalr.invoke('EnterToSession',this.session);
          // signalr.on('EnterToSession',(message)=>this.messages.push(message));
          // signalr.on('GetNextQuestion',(message)=>this.messages.push(message));
        })
        .catch(err => console.error(err.toString()));

      //
    },
    methods: {
      send() {
        this.chat.push({ ...this.text });
        this.text.message = "";
        this.count = this.count + 1;
        this.chat.push(this.questions[this.count]);
        if (this.questions[this.count].disInput === false) {
          this.disabled = false;
        } else {
          this.disabled = true;
        }
      }
    }
  };
</script>

<style>
  textarea {
    width: 100%;
    height: 100%;
  }
  #chat_wrap {
    overflow: hidden;
    display: grid;
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
    grid-area: chat;
  }
  #chat_form {
    overflow: scroll;
    top: 0;
    left: 0;
    right: 0;
    background: #06e0f0;
    padding: 1rem;
    overflow-y: auto;
    grid-area: input;
  }
</style>

