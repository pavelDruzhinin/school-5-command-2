<template>
  <main id="main">
    <div id="chatsList">
      <h1>Список чатов</h1>
      <ul>

        <li v-for="chat in chats" v-bind:key="chat.id">
          <div class="dflex justibetween">
          <div><h2>{{chat.name}}</h2></div>
          <div class="dflex  justifystart">
            
            <button type="button" class="btn btn-primary">Редактировать</button>
            <button type="button" class="btn btn-primary"  @click="removeId(chat.id)">X</button>

          </div>
          </div>
          <hr>
        </li>

      </ul>
    </div>
    <div id="plusButton" class="justifycenter dflex">
      <plusButton @click="openmodal" />
    </div>
    <div id="respondents">
      <respondents/>
    </div>
    <ModalChat @click="addchat" :chat="chat"/>
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
import plusButton from '@/components/plusButton.vue'
import respondents from '@/components/respondents.vue'
import ModalChat from '@/components/ModalChat'

export default {
  components:{
    plusButton,
    respondents,
    ModalChat
  },
  data() {
    return {
      chats: [
        {id:0, name: 'Чат номер 1'},
        {id:1, name: 'Чат номер 2'}
      ],
      chat:{Name:''}
    }
  },
  methods: {
    removeId: function(id) {
      let chats = this.chats;
      this.chats = chats.filter((chat) => chat.id != id);
    },
    openmodal(){
      this.$bvModal.show("modal1")
    },
    addchat(){
      this.$http.post('/chats/add',this.chat).then(response=>{
        let chatdata = response.data;
        this.$router.push('/edit/'+chatdata.id)
      })
    }
  }
  }
</script>