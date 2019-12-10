 <template>
  <div class="main">
    <div class="helloBlock">
      <span>Приветствие:</span>
      <textarea id="hellow" class="hellow" v-model="hellowText"></textarea>
    </div>

    <div class="list">
      <p>Список вопросов:</p>

      <button  type="button" class="btn btn-primary" @click="changeVisible()">Добавить ✚</button>

      <div class="customModal" v-if="showModalQwestion">
        <div class="customModalTitle">
          Добавление вопроса
          <button  type="button" class="btn btn-primary" @click="closeCustomModal()">&times;</button>
        </div>
        <div class="customModalBody">
          <div class="newQuestion">
            <span>Ваш вопрос:</span>
            <textarea id="question" class="question" v-model="newQuestionText"></textarea>
          </div>
          <div class="newVariantsBlock margin">
            <input
              type="checkbox"
              id="haveVariants"
              @click="changeCheckboxStatus()"
              v-model="checkBoxPush"
            />

            <label for="haveVariants">Варианты ответов</label>

            <div class="variants" v-if="checkBoxPush">
              <button  type="button" class="btn btn-primary" @click="addRow()">Добавить вариант ✚</button>
              <div class="addVariantBlock">
                <ul>
                  <li v-for="(input, index) in inputVariants" :key="index">
                    <div class="dFlexRow">
                      <input
                        type="text"
                        id="variantText"
                        class="variantText"
                        v-model="inputVariants[index]"
                      />
                      <button
                         type="button" 
                         class="btn btn-primary"
                        @click="deleteInputVariant(index)"
                      >Удалить &times;</button>
                    </div>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>

        <div class="customModalFooter">
          <div class="margin">
            <button  type="button" class="btn btn-primary" v-on:click="createNewTask()">Сохранить</button>
          </div>
        </div>
      </div>

      <ul class="NumMarker">
        <draggable v-model="tasks">
        <li class="questionText" v-for="(item,key, index) in tasks" :key="index">
          <div class="questionBlock">
            {{item.name}}
            <div class="btPanel">
              <button  type="button" class="btn btn-primary" @click="EditQuestion(item,key)">Редактировать ✎</button>
              <button  type="button" class="btn btn-primary" @click="DeleteQuestion(key)">Удалить &times;</button>
            </div>
          </div>

          <ul class="circleMarker">
            <li v-for="(item) in item.variants" class="variantTex" :key="item">{{item}}</li>
          </ul>
        </li>
        </draggable>
      </ul>
    </div>

    <div class="helloBlock">
      <span>Прощание:</span>
      <textarea id="goodbye" class="goodbye" v-model="goodbyeText"></textarea>
    </div>
    <div class="buttons center">
      <button  type="button" class="btn btn-primary" @click="addtoDb">Сохранить</button>
    </div>
  </div>
</template>
<script>
import draggable from "vuedraggable"
  export default {
    components:{
      draggable
    },
    data() {
      return {
        tasks: [],
        newQuestionText: "",
        hellowText: "Здравствуйте!",
        goodbyeText: "Спасибо, что помогли нам. Мы с вами обязательно свяжемся.",
        showModalQwestion: false,
        checkBoxPush: false,
        inputVariants: [],
        editKey: "",
        editIndex: "",
        isEditIndex: false
      };
    },
    methods: {
      logging(){
        console.log(this.tasks)
      },
      addtoDb(){
        this.$http
          .post('/questions/'+this.$route.params.id,this.tasks)
          .then(this.$router.push('/dashboard'))
      },
      createNewTask: function() {
        if (!this.isEditIndex) {
          let obj = {};
          obj.name = this.newQuestionText;
          obj.variants = this.inputVariants;
          obj.status = "";
          this.tasks.push(obj);

          /*question.value = "";
  				this.newQuestionText = "";
  				this.showModalQwestion = false;
  				this.checkBoxPush = false;
  				this.inputVariants = [];*/
        } else {
          /*this.tasks[this.editKey]=this.editIndex;*/
          this.tasks[this.editKey].name = this.newQuestionText;
          this.editKey = "";
          this.editIndex = "";
          this.isEditIndex = false;
        }

        question.value = "";
        this.newQuestionText = "";
        this.showModalQwestion = false;
        this.checkBoxPush = false;
        this.inputVariants = [];
      },
      deleteList: function() {
        this.tasks = [];
      },
      changeStatus: function(item) {
        item.status == "done" ? (item.status = "") : (item.status = "done");
      },
      test: function() {
        alert("тест");
      },
      changeVisible: function() {
        this.showModalQwestion = true;
      },
      closeCustomModal: function() {
        this.showModalQwestion = false;
        question.value = "";
        this.newQuestionText = "";
        (this.checkBoxPush = false), (this.inputVariants = []);
      },
      addRow: function() {
        this.inputVariants.push("");
      },
      deleteInputVariant: function(index) {
        this.inputVariants.splice(index, 1);
      },
      changeCheckboxStatus: function() {
        this.checkBoxPush = !this.checkBoxPush;
        this.inputVariants = [];
      },
      /*EditQuestion: function(index,key){*/
      EditQuestion: function(index, key) {
        this.showModalQwestion = true;
        this.newQuestionText = index.name;
        this.checkBoxPush = true;
        this.inputVariants = index.variants;
        this.editIndex = index;
        this.editKey = key;
        this.isEditIndex = true;
      },
      DeleteQuestion: function(index) {
        this.tasks.splice(index, 1);
      }
    }
  };
</script>
<style lang="scss">
  * {
    margin: 0;
    padding: 0;
  }
  .main {
    background-color: #d4e2e4;
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
    cursor: pointer;
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
    cursor: pointer;
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