<template>
  <main id="main">
    <div id="chatsList">
      <h1>Список чатов</h1>
      <ul>

        <li v-for="(chat, index) in chats" v-bind:key="chat.id">
          <div class="dflex  justifystart">
            <div><h2>{{chat.name}}</h2></div>
            <a href="">ред.</a>
            <a @click="$delete(chats, index)">удал.</a>
          </div>
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
.dflex {
  display:flex;
}

.aligncenter {
  align-items: center;
}

.justifystart {
  justify-content: flex-start;
}

.justifycenter {
  justify-content: center;
}

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
  }
  }
</script>