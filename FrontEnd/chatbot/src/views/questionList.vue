 <template>
  <div class="main">
    <div class="list">
      <b-button @click="create">Добавить ✚</b-button>
      <div v-if="questions.length">
      <p>Список вопросов:</p>
      <questionstable 
      :questions="questions"
      @edit="edit"
      @del="deletequestion"
      />
      </div>
      <div v-else><p>у вас пока нет вопросов</p></div>
      <question 
      @addvariant="addvariant" 
      @deletevariant="deletevariant" 
      @savequestion="editmode ? savechanges(index) : save()"
      :question="question" />
      <b-button @click="addtoDb">Сохранить</b-button>
    </div>
    <b-modal @ok="deltext ? deletefromchat(index) : deletefromqueue(index)" id="deletemodal" title=Удаление>
      <p>Вы уверены что хотите удалить вопрос ?</p>
      <p>{{deltext 
        ? "Данный вопрос будет удалён из чата" 
        : "Данный вопрос будет удален из очереди на добавление в чат"}}
      </p>
      </b-modal>
    <b-modal @ok="deltext ? deletefromquestion(index) : deletefromquestionqueue(index)" id="deletevariantmodal" title=Удаление>
      <p>Вы уверены что хотите удалить вариант ?</p>
      <p>{{deltext 
        ? "Данный вариант будет удалён из вопроса" 
        : "Данный вариант будет удален из очереди на добавление в вопрос"}}
      </p>
      </b-modal>
  </div>
</template>
<script>
import question from '@/components/views/questionlist/questionModal.vue'
import questionstable from '@/components/views/questionlist/questionsTable.vue'
  export default {
    components:{
      question,
      questionstable
    },
    data() {
      return {
        questions:[],
        question:{
          text:'',
          buttons:[],
          questiontype:[
            {value:null, text:'Выберите тип'}, 
            {value:'Welcome', text:'Приветствие'}, 
            {value:'Normal', text:'Обычный вопрос'},
            {value:'Final', text:'Завершающий диалог'}
            ],
          type:false,
          selected:null
        },
        editmode:null,
        index:null,
        deltext:null,
      };
    },
    created(){
      this.updatequestions()
    },
    methods: {
      updatequestions(){
        this.$http.get("/questions/"+this.$route.params.id)
        .then(response=>this.questions=response.data)
        .catch(err=>console.log(err))
      },
      create(){
        this.editmode=false
        this.resetmodal();
        this.$bvModal.show("modal")
      },
      resetmodal(){
        this.question={
        text:'',
        buttons:[],
        questiontype:[
            {value:null, text:'Выберите тип'}, 
            {value:'Welcome', text:'Приветствие'}, 
            {value:'Normal', text:'Обычный вопрос'},
            {value:'Final', text:'Завершающий диалог'}
            ],
        type:false,
        selected:null,
        }
        this.index=null
        this.editmode=null
      },
      deletevariant(index){
        if(this.question.buttons[index].id) this.deltext=true
          else this.deltext=false
        this.$bvModal.show("deletevariantmodal")
      },
      deletefromquestion(index){
        this.$http
          .post('/questions/deletevariant'+this.question.buttons[index].id)
          .then(this.question.buttons.splice(index,1))
          this.index=null;
          this.deltext=null;
      },
      deletefromquestionqueue(index){
        this.question.buttons.splice(index,1);
        this.index=null;
        this.deltext=null;
      },
      deletequestion(index){
        if(this.questions[index].id) this.deltext=true
          else this.deltext=false
        this.$bvModal.show("deletemodal")
      },
      deletefromchat(index){
        this.$http
        .post("questions/delete/"+this.questions[index].id)
        .then(this.questions.splice(index,1));
        this.index=null;
        this.deltext=null;
      },
      deletefromqueue(index){
        this.questions.splice(index,1);
        this.index=null;
        this.deltext=null
      },
      deletequestion(index){
        if(this.questions[index].id) this.deltext=true
          else this.deltext=false
        this.$bvModal.show("deletemodal")
      },
      addvariant(){
        this.question.buttons.push({text:''})
      },
      save(){
        let questiontext = this.question.text;
        let questionbuttons = this.question.buttons;
        let type = this.question.type
        if(questionbuttons.length && type) 
          {
            this.questions.push({text:questiontext,buttons:questionbuttons})
          }
          else {this.questions.push({text:questiontext})}
        this.resetmodal()
      },
      savechanges(index){
        let questionid=null;
        if(this.question.id) questionid=this.question.id
        let questiontext = this.question.text;
        let questionbuttons = this.question.buttons;
        let type = this.question.type
        if(!type && questionid==null) 
          {
            this.questions.splice(index,1, {text:this.question.text})
          } 
        else if(type && questionid==null)
          {
            this.questions.splice(index,1, {text:this.question.text, buttons:this.question.buttons})
          }
        else if(!type && questionid!=null)
          {
            this.questions.splice(index,1, {text:this.question.text, id:questionid})
          }
        else 
          {
            this.questions.splice(index,1, {text:this.question.text, buttons:this.question.buttons, id:questionid})
          }
        this.resetmodal();
      },
      edit(index){
        this.$bvModal.show("modal");
        this.index=index;
        let questionedit = this.questions[index];
        this.editmode=true;
        if(questionedit.id) this.question.id=questionedit.id;
        this.question.text=questionedit.text;
        this.question.selected=questionedit.questiontype;
        if(questionedit.buttons.length) {this.question.buttons=questionedit.buttons; this.question.type=true;} 
      },
      addtoDb(){
        this.$http
          .post('/questions/'+this.$route.params.id,this.questions)
          .then(this.$router.push('/dashboard'))
      },
    }
  };
</script>
<style lang="scss">
  * {
    margin: 0;
    padding: 0;
  }
  .header {
    // background-image: url("bg2.jpg");
    width: 100%;
    height: 100px;
    text-align: center;
    letter-spacing: 1.5em;
  }
  .profile {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    flex-wrap: nowrap;
    margin: 5%;
  }
  .addVariantBlock {
    /*display: flex;
  	flex-direction: row;
  	justify-content: space-between;
  	flex-wrap: nowrap;
  	align-items: center;*/
    margin-top: 2%;
  }

  .list,
  .helloBlock,
  .newQuestion {
    margin: 5%;
  }
  .profile {
    border-bottom: solid 3px #1280b5;
    border-top: solid 3px #1280b5;
  }
  .info {
    margin: 2%;
    width: 70%;
  }
  .photo {
    margin: 2%;
    width: 30%;
  }

  .profile img {
    width: 100%;
    border-radius: 50%;
  }
  .info p {
    font-size: 200%;
    margin-bottom: 2%;
  }
  .toDoList {
    width: 50%;
    margin: 2%;
  }
  .buttons {
    width: 50%;
    margin: 2%;
    margin-left: 23%;
    margin-bottom: 2%;
  }
  .center {
    text-align: center;
  }
  .myBtn {
    width: 120px;
    height: 50px;
    border-radius: 5px;
    border-color: #1280b5;
    background-color: #4d8496;
    color: white;
    font-size: 110%;
  }
  .btnNarrow {
    width: 170px;
    height: 30px;
  }
  .btnLittleNarrow {
    width: 141px;
    height: 23px;
    font-size: 71%;
    vertical-align: middle;
    line-height: 11px;
    margin: 1%;
  }
  .btnLittleDelete {
    width: 85px;
    height: 23px;
    font-size: 71%;
    vertical-align: middle;
    line-height: 11px;
    margin: 1%;
  }

  .myBtn:hover {
    filter: drop-shadow(0 0 10px #4d8496) saturate(200%);
    // cursor: pointer;
  }
  .litleBtn {
    width: 50px;
    height: 50px;
  }
  .closeBtn {
    width: 30px;
    height: 30px;
    float: right;
    line-height: 14px;
    vertical-align: middle;
    margin-top: 0.5%;
  }
  .deleteBtn {
    width: 103px;
    height: 25px;
    margin: 2% 0 2% 2%;
    text-align: center;
    line-height: 20px;
  }
  .helloBlock span,
  .newQuestion span,
  .list p {
    font-size: 140%;
    color: #0e3f50;
    font-weight: bold;
    margin-bottom: 2%;
  }
  ul li.questionText {
    font-size: 130%;
    margin-top: 2%;
    margin-bottom: 2%;
    margin-left: 2%;
    font-weight: bold;
  }
  ul li.questionText:hover {
    // cursor: pointer;
    filter: drop-shadow(0 0 10px #4d8496);
  }
  ul li.variantTex {
    font-size: 110%;
    margin: 2%;
    font-weight: normal;
    filter: none;
  }
  ul li.variantTex:hover {
    cursor: auto;
    filter: none;
  }
  ul.NumMarker {
    list-style: decimal;
  }
  ul.circleMarker {
    list-style: circle;
  }

  .question {
    margin-bottom: 2%;
  }
  .question,
  .hellow,
  .goodbye {
    width: 71%;
    height: 100px;
    margin-left: 2%;
    border-radius: 5px;
  }

  .done {
    color: green;
  }
  .done:before {
    content: "✔ ";
  }
  .helloBlock,
  .newQuestion {
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;
    align-items: flex-start;
  }
  .variantsBlock {
    margin-left: 4%;
  }
  .variant:before {
    content: "•";
  }
  .questionText {
    font-weight: bold;
  }
  .variantsBlock p {
    font-weight: normal;
  }
  .customModal {
    box-shadow: 0px 1px 12px rgba(0, 0, 0, 0.4);
    /*left: calc(50vw - 300px);*/
    left: 25%;
    position: fixed;
    z-index: 999;
    width: 600px;
    top: 10%;
    border-radius: 5px;
    overflow: hidden;
  }
  .customModal .customModalTitle {
    background-color: #eee;
    text-align: left;
    padding: 8px 12px;
    font-size: 1.5em;
  }
  .customModal .customModalTitle .close {
    line-height: 32px;
    color: #5c4084;
  }
  .customModal .customModalBody {
    background-color: #fff;
    padding: 8px 12px;
    text-align: left;
    padding: 12px;
  }

  .customModal .customModalFooter {
    background-color: #eee;
    padding: 4px 12px;
    text-align: right;
  }

  .dFlexRow {
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;
    align-items: center;
  }
  .customFooter {
    text-align: right;
    margin-right: 5%;
  }
  .margin {
    margin: 2% 5% 2% 5%;
  }
  .variantText {
    width: 483px;
    height: 25px;
  }
  ul {
    list-style: none;
  }
  ul li {
    color: #0e3f50;
  }
  .qwestionBlock {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    flex-wrap: nowrap;
    justify-content: flex-start;
  }
  .qw {
    width: 70%;
  }
  .btnPanel {
    width: 30%;
  }
  .btPanel,
  .questionBlock {
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    flex-wrap: nowrap;
    align-content: center;
    line-height: 24px;
  }
</style>