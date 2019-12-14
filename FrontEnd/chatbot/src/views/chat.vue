<template>
  <div id="chat_wrap">
    <div v-if="messages.length==1 && questiontype==0" id="chat">
      <b-button @click="sendbutton(hello)">Здравствуйте</b-button>
    </div>
    <div id="chat" v-else-if="questiontype==0">
      <textarea
        placeholder="Введите сообщение"
        @keydown.enter="send"
        v-model="answer"
      />
    </div>
    <div id="chat" v-else-if="questiontype==1">
      <div class="btn-group-toggle" data-toggle="buttons"> 
        <label class="btn btn-primary" v-for="(button,index) in buttons" :key="index" :class="{ active: btnindex==index}" @click="btnindex=index">
        <input type="radio" @click="sendbutton(button.text)" autocomplete="off" />{{button.text}}
        </label>
    <div v-else-if="end" id="chat">
      <b-button @click="$router.go()">Пройти заново</b-button>
      <b-button @click="$router.push('/')">Выйти</b-button>
    </div>
    <div id="chat-form">
      <div class="wrap">
        <div class="mes" v-for="(message,index) in messages" :key="index">
          <p>{{message.text}}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import { LogLevel, HubConnectionBuilder, HttpTransportType } from "@aspnet/signalr";
  export default {
    data() {
      return {
        signalr:null,
        question:null,
        messages:[],
        history:null,
        hello:'Здравствуйте',
        bye:'',
        answer:null,
        session:null,
        questiontype:null,
        buttons:null,
        questionid:null,
        btnindex:null,
        end:false,
      };
    },
    created() {
      this.$http
        .get("/chats/getsession/" + this.$route.params.id)
        .then(response => 
		{
			(this.session = response.data.sessionId)

			this.signalr
			.start()
			.then(()=>{
			this.signalr.invoke('EnterToSession',this.session);
			
			})
			.catch(err => console.error(err.toString()));
		});
      this.signalr = new HubConnectionBuilder()
        .withUrl("/chat",HttpTransportType.LongPolling|HttpTransportType.ServerSentEvents|HttpTransportType.ForeverFrame)
        .configureLogging(LogLevel.Information)
        .build();

		this.signalr.on('EnterToSession',(message)=>{
            this.history=message
            this.question=this.history.nextQuestion
            if(this.history.questionsHistory.length) 
            {
              for(let i=0; i< this.history.questionsHistory.length; i++)
              {
                var msg=this.history.questionsHistory[i];
                this.messages.push({text:msg.text});
                this.messages.push({text:msg.answer});
              }
              this.messages.push({text:this.question.text})
              this.questiontype=this.question.questionAnswerType
              if(this.question.buttons.length) this.buttons=this.question.buttons
            }
            else {this.messages.push({text:this.question.text}); this.questiontype=this.question.questionAnswerType}
            if(this.question.buttons) this.buttons=this.question.buttons
            if(this.question.isQuestionsEnded) this.end=this.question.isQuestionsEnded
          });
          this.signalr.on('GetNextQuestion',(question)=>{
          this.question=question;
          if(!this.end){
		      this.messages.push({text:question.answerForPreviousQuestion})
          this.messages.push({text:this.question.nextQuestionText})}
          this.question.id=question.nextQuestionId
          if(question.buttons) this.buttons=question.buttons
          this.questiontype=this.question.questionAnswerType
          if(this.question.isQuestionsEnded) this.end=this.question.isQuestionsEnded
          });
    },
    methods: {
      send() {
        this.signalr.invoke('AnswerForTheQuestion',{sessionId:this.session, questionId:this.question.id, answer:this.answer})
        this.questiontype=null,
        this.buttons=null;
        this.btnindex=null
        this.answer=null;
      },
      sendbutton(answer) {
        this.signalr.invoke('AnswerForTheQuestion',{sessionId:this.session, questionId:this.question.id, answer:answer})
        this.questiontype=null,
        this.buttons=null;
        this.btnindex=null
        this.answer=null;
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
  .hidden {
    display:none;
  }
</style>

