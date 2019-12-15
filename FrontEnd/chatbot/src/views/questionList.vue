 <template>
  <div class="main">
    <div class="list">
      <div class="title">Конструктор чата</div>
      <b-form-group label="Вопрос приветствие:" label-for="input-welcome">
        <b-form-textarea id="input-welcome" v-model="welcome" />
      </b-form-group>
      <b-button @click="create" variant="primary">Добавить</b-button>
      <div class="question-list" v-if="questions.length">
        <span>Список вопросов:</span>
        <questionstable :questions="questions" @edit="edit" @del="deletequestion" />
      </div>
      <question
        @addvariant="addvariant"
        @deletevariant="deletevariant"
        @savequestion="editmode ? savechanges(index) : save()"
        :question="question"
      />
      <b-form-group class="mt-2" label="Вопрос завершение:" label-for="input-final">
        <b-form-textarea id="input-final" v-model="final" />
      </b-form-group>
      <b-button
        variant="success"
        @click="addtoDb"
        :disabled="(welcome!='' && final!='' && questions.length>0) ? false : true"
      >Сохранить</b-button>
    </div>
    <b-modal
      @ok="deltext ? deletefromchat(index) : deletefromqueue(index)"
      id="deletemodal"
      title="Удаление"
    >
      <p>Вы уверены что хотите удалить вопрос?</p>
      <p>
        {{deltext
        ? "Данный вопрос будет удалён из чата"
        : "Данный вопрос будет удален из очереди на добавление в чат"}}
      </p>
    </b-modal>
    <b-modal
      @ok="deltext ? deletefromquestion(index) : deletefromquestionqueue(index)"
      id="deletevariantmodal"
      title="Удаление"
    >
      <p>Вы уверены что хотите удалить вариант?</p>
      <p>
        {{deltext
        ? "Данный вариант будет удалён из вопроса"
        : "Данный вариант будет удален из очереди на добавление в вопрос"}}
      </p>
    </b-modal>
  </div>
</template>

<script>
import question from "@/components/views/questionlist/questionModal.vue";
import questionstable from "@/components/views/questionlist/questionsTable.vue";
export default {
  components: {
    question,
    questionstable
  },
  data() {
    return {
      questions: [],
      welcome: "",
      final: "",
      welcome_id: null,
      final_id: null,
      question: {
        text: "",
        buttons: [],
        questiontype: [
          { value: null, text: "Выберите тип" },
          { value: "Welcome", text: "Приветствие" },
          { value: "Normal", text: "Обычный вопрос" },
          { value: "Final", text: "Завершающий диалог" }
        ],
        type: false,
        selected: null
      },
      editmode: null,
      index: null,
      deltext: null,
      delindex: null
    };
  },
  created() {
    this.updatequestions();
  },
  methods: {
    updatequestions() {
      this.$http
        .get("/questions/" + this.$route.params.id)
        .then(response => {
          this.questions = response.data;
          this.welcome = this.questions[0].text;
          this.welcome_id = this.questions[0].id;
          this.final = this.questions[this.questions.length - 1].text;
          this.final_id = this.questions[this.questions.length - 1].id;
          this.questions.shift();
          this.questions.pop();
        })
        .catch(err => console.log(err));
    },
    create() {
      this.editmode = false;
      this.resetmodal();
      this.$bvModal.show("modal");
    },
    resetmodal() {
      this.question = {
        text: "",
        buttons: [],
        type: false,
        selected: null
      };
      this.index = null;
      this.editmode = null;
    },
    deletevariant(index) {
      if (this.question.buttons[index].id) this.deltext = true;
      else this.deltext = false;
      this.$bvModal.show("deletevariantmodal");
    },
    deletefromquestion(index) {
      this.$http
        .post("/questions/deletevariant" + this.question.buttons[index].id)
        .then(this.question.buttons.splice(index, 1));
      this.index = null;
      this.deltext = null;
    },
    deletefromquestionqueue(index) {
      this.question.buttons.splice(index, 1);
      this.index = null;
      this.deltext = null;
    },
    deletequestion(index) {
      this.index = index;
      this.$bvModal.show("deletemodal");
      if (this.questions[index].id) this.deltext = true;
      else this.deltext = false;
    },
    deletefromchat(index) {
      this.$http
        .post("questions/delete/" + this.questions[index].id)
        .then(this.questions.splice(index, 1));
      this.index = null;
      this.deltext = null;
    },
    deletefromqueue(index) {
      this.questions.splice(index, 1);
      this.index = null;
      this.deltext = null;
    },
    addvariant() {
      this.question.buttons.push({ text: "" });
    },
    save() {
      let questiontext = this.question.text;
      let questionbuttons = this.question.buttons;
      let type = this.question.type;
      if (questionbuttons.length && type) {
        this.questions.push({ text: questiontext, buttons: questionbuttons });
      } else {
        this.questions.push({ text: questiontext });
      }
      this.resetmodal();
    },
    savechanges(index) {
      let questionid = null;
      if (this.question.id) questionid = this.question.id;
      let questiontext = this.question.text;
      let questionbuttons = this.question.buttons;
      let type = this.question.type;
      if (!type && questionid == null) {
        this.questions.splice(index, 1, { text: this.question.text });
      } else if (type && questionid == null) {
        this.questions.splice(index, 1, {
          text: this.question.text,
          buttons: this.question.buttons
        });
      } else if (!type && questionid != null) {
        this.questions.splice(index, 1, {
          text: this.question.text,
          id: questionid
        });
      } else {
        this.questions.splice(index, 1, {
          text: this.question.text,
          buttons: this.question.buttons,
          id: questionid
        });
      }
      this.resetmodal();
    },
    edit(index) {
      this.$bvModal.show("modal");
      this.index = index;
      let questionedit = this.questions[index];
      this.editmode = true;
      if (questionedit.id) this.question.id = questionedit.id;
      this.question.text = questionedit.text;
      this.question.selected = questionedit.questiontype;
      if (questionedit.buttons.length) {
        this.question.buttons = questionedit.buttons;
        this.question.type = true;
      }
    },
    addtoDb() {
      if (this.final_id == null)
        this.questions.push({
          text: this.final,
          buttons: [{ text: "Закончить" }]
        });
      else this.questions.push({ id: this.final_id, text: this.final });
      if (this.welcome_id == null)
        this.questions.unshift({
          text: this.welcome,
          buttons: [{ text: "Начать" }]
        });
      else this.questions.unshift({ id: this.welcome_id, text: this.welcome });
      this;
      this.$http
        .post("/questions/" + this.$route.params.id, this.questions)
        .then(this.$router.push("/dashboard"));
    }
  }
};
</script>

<style lang="scss" scoped>
.main {
  padding: 20px;
}

.title {
  font-size: 25px;
  font-weight: bold;
  margin-bottom: 20px;
}

.question-list {
  margin: 10px 0;
}
</style>
