<template>
  <div class="main" >
    <div class="wrapper">
    <div id="chatsList" class="list">
      
      <p>Список чатов:</p>
      <button class="myBtn">
            ✚
      </button>

      <ul>
        <li class="chatText" v-for="(chat) in chats" v-bind:key="chat.id">
          <div class="dflex justibetween">
          <div>{{chat.name}}</div>
          <div class="dflex  justifystart">
            
            <button class="myBtn btnLittleNarrow">Редактировать ✎</button>
            <button class="myBtn btnLittleDelete" @click="removeId(chat.id)">Удалить X</button>
          </div>
          </div>
          <hr>
        </li>

      </ul>
    </div>

    <div id="myRespondents">
      <myRespondents/>
    </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>


body{
	background-color: #d4e2e4;
}
.list{
  width: 100%;
  margin: 2% 0 2% 0;
  padding: 1%;
}
.list p{
		font-size: 140%;
		color: #0e3f50;
		font-weight: bold;
		margin-bottom: 2%;
}
ul{
	list-style: none;
}
ul li{
	color: #0e3f50;
}
ul li.chatText{
	font-size: 130%;
	margin-top: 2%;
    margin-bottom: 2%;
    margin-left: 2%;
	font-weight: bold;
}
ul li.chatText:hover{
	cursor: pointer;
	filter: drop-shadow(0 0 10px #4d8496)
}
.myBtn{
	width: 120px;
	height: 50px;
	border-radius: 5px;
	border-color: #1280b5;
	background-color: #4d8496;
	color: white;
	font-size: 110%;	 
}
.myBtn:hover{
	filter: drop-shadow(0 0 10px #4d8496) saturate(200%);
	cursor: pointer;
}
.btnLittleNarrow{
	width: 141px;
  height: 23px;
	font-size: 71%;
  vertical-align: middle;
  line-height: 11px;
  margin: 1%;
	
}
.btnLittleDelete{
	width: 85px;
	height: 23px;
	font-size: 71%;
    vertical-align: middle;
    line-height: 11px;
    margin: 1%;
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
import myRespondents from './components/myRespondents.vue'

export default {
  components:{
    plusButton,
    myRespondents
  },
  data() {
    return {
      chats: [
        {id:0, name: 'Опросник для соискателей на должность менеджера'},
        {id:1, name: 'Опросник для соискателей на должность главного бухгалтера'},
        {id:2, name: 'Опросник для соискателей на должность охранника'},
        {id:3, name: 'Опросник для соискателей на должность инженера'},
        {id:4, name: 'Опросник для соискателей на должность риелтора'}
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