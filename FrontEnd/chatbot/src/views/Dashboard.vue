<template>
  <div class="lk-page">
    <div class="title">
      <!-- <span>Личный кабинет + fullname User</span> -->
    </div>
    <div class="lk-page__wrapper">
      <div class="chat-create mb-4">
        <span class="title"></span>
        <b-button variant="outline-dark" @click="openmodal">Создать чат</b-button>
        <ModalChat @click="addchat" :chat="chat" />
      </div>
      <div class="chat-view mb-4">
        <span></span>
        <b-button v-b-toggle.collapse-1 variant="outline-dark">Чат листы</b-button>
        <b-collapse id="collapse-1" class="mt-4">
          <b-card>
            <p class="card-text">Список созданных чатов</p>
            <ul class="chat-view__card">
              <li class="chat-view__item mb-4" v-for="chat in chats" :key="chat.id">
                {{ chat.name }}
                <div class="chat-view__inner mt-4">
                  <b-button variant="outline-success mr-4" @click="edit(chat.id)">Редактировать</b-button>
                  <b-button variant="outline-danger mr-4" @click="removeId(chat.id)">Удалить</b-button>
                  <b-button variant="outline-primary mr-4" @click="share(chat.id)">Поделиться</b-button>
                  <b-button variant="outline-dark" @click="trychat(chat.id)">Пройти чат</b-button>
                </div>
              </li>
            </ul>
          </b-card>
        </b-collapse>
      </div>
      <div class="user-view">
        <b-button v-b-toggle.collapse-2 variant="outline-dark">Пользователи</b-button>
        <b-collapse id="collapse-2" class="mt-4">
          <b-card>
            <p class="card-text ml-2">Список пользователей которые прошли анкетирование</p>
            <ul class="user-view__card">
              <li
                class="mb-4 chat-view__item"
                v-for="(respondent,index) in respondents"
                v-bind:key="index"
              >
                {{ respondent.userName }}
                <div class="user-view__inner mt-4">
                  <b-button variant="outline-primary mr-4">Открыть профиль</b-button>
                  <b-button variant="outline-success">Посмотреть результаты</b-button>
                </div>
              </li>
            </ul>
          </b-card>
        </b-collapse>
      </div>
    </div>
    <b-modal title="Поделиться" id="share">
      <b-form-input v-model="link" />
    </b-modal>
  </div>
</template>

<script>
import ModalChat from "@/components/ModalChat";

export default {
  components: {
    ModalChat
  },
  data() {
    return {
      chats: [],
      chat: { Name: "" },
      respondents: [],
      link: null
    };
  },
  created() {
    this.getchats();
    this.getrespondents();
  },
  methods: {
    getchats() {
      this.$http.get("/chats/").then(response => (this.chats = response.data));
    },
    getrespondents() {
      this.$http
        .get("/chats/respondents")
        .then(response => (this.respondents = response.data));
    },
    share(id) {
      this.$bvModal.show("share");
      this.link = "localhost:8080/chat/" + id;
    },
    trychat(id) {
      this.$router.push("/chat/" + id);
    },
    removeId(id) {
      let chats = this.chats;
      this.chats = chats.filter(chat => chat.id != id);
    },
    openmodal() {
      this.$bvModal.show("modal1");
    },
    edit(id) {
      this.$router.push("/edit/" + id);
    },
    addchat() {
      this.$http.post("/chats/add", this.chat).then(response => {
        let chatdata = response.data;
        this.$router.push("/edit/" + chatdata.id);
      });
    }
  }
};
</script>

<style lang="scss" scoped>
.lk-page__wrapper {
  display: flex;
  justify-content: center;
  flex-direction: column;
  padding: 20px;
}

.chat-view {
  &__item {
    list-style: none;
    font-size: 18px;
  }
}

.card-text {
  font-size: 18px;
  font-weight: bold;
}
</style>
