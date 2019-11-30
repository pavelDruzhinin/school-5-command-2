<template>
  <main id="main">
    <div id="chatsList">
      <h1>Список чатов</h1>
      <ul>

        <li v-for="(chat, index) in chats" v-bind:key="chat.id">
          <div class="dflex justibetween">
          <div><h2>{{chat.name}}</h2></div>
          <div class="dflex  justifystart">
            
            <button class="button editChat">Редактировать</button>
            <button class="button deleteChat" @click="removeId(chat.id)">X</button>
          </div>
          </div>
          <hr>
        </li>

      </ul>
    </div>
    <div id="plusButton" class="justifycenter dflex">
      <plusButton/>
    </div>

    <div id="respondents">
      <respondents/>
    </div>
  </main>
</template>

<style lang="scss" scoped>


h1 {

  margin: 10px 0 0 10px 
}
li {

  list-style:none;  
  margin-left:30px;
  margin-top:15px;

    a {
    text-decoration: none;
    color: #CDCDCD;
    padding-right:5px;

    }

    a:hover {
    text-decoration: none;
    color: #CDCDCD;
    }

      .editChat {
      background-color: white;
      color:black;
      font-size:10px;
      padding: 5px 11px;

    }

          .deleteChat {
      background-color: rgb(236, 96, 96);
      color:black;
      font-size:10px;
      padding: 5px 11px;

    }

    hr {
      color: #CDCDCD;
      opacity: 0.5;
    }

}



</style>

<script>
import plusButton from './components/plusButton.vue'
import respondents from './components/respondents.vue'

export default {
  components:{
    plusButton,
    respondents
  },
  data() {
    return {
      chats: [
        {id:0, name: 'Чат номер 1'},
        {id:1, name: 'Чат номер 2'}
      ]
    }
  },
  methods: {
    removeId: function(id) {
      let chats = this.chats;
      this.chats = chats.filter((chat) => chat.id != id);
    }
  }
  }
</script>