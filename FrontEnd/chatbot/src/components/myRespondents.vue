<template>
    <div id="respondents"  class="list">
      
      <p>Список респондентов:</p>
      <button  type="button" class="btn btn-primary">
            ✚
      </button>

      <ul>
        <li class="respondName" v-for="(respondent) in respondents" v-bind:key="respondent.key">
        <!--<li class="respondName" v-for="(respondent, index) in respondents" v-bind:key="respondents.key">-->
          <div class="dflex justibetween">
          <div>{{respondent.name}}</div>
          <div class="dflex  justifystart">
            
            <button  type="button" class="btn btn-primary">Просмотреть результаты 🔍</button>
            <button  type="button" class="btn btn-primary" @click="removeId(chat.id)">Открыть профиль 🙎 </button>
          </div>
          </div>
          <hr>
        </li>

      </ul>
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
ul li.respondName{
	font-size: 130%;
	margin-top: 2%;
    margin-bottom: 2%;
    margin-left: 2%;
	font-weight: bold;
}
ul li.respondName:hover{
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
.btnLook{
	width: 217px;
    height: 23px;
	font-size: 71%;
    vertical-align: middle;
    line-height: 11px;
    margin: 1%;
	
}
.btnLittleDelete{
	width: 168px;
	height: 23px;
	font-size: 71%;
    vertical-align: middle;
    line-height: 11px;
    margin: 1%;
}




h1 {

  margin: 10px 0 0 10px 
}

#userAddButton {

    font-size: 30px;
    background-color: rgb(96, 179, 96);
    padding: 0 15px;


}
li {

  list-style:none;  
  margin-left:30px;
  margin-top:15px;

    .seeUser {
      background-color: white;
      color:black;
      font-size:10px;
      padding: 5px 11px;

    }

    a {
    text-decoration: none;
    color: #CDCDCD;
    padding-right:5px;

    }

    a:hover {
    text-decoration: none;
    color: #CDCDCD;
    }

    hr {
    color: #CDCDCD;
    opacity: 0.5;
    }

}



</style>


<script>

export default {
 data() {
    return {
        respondents: [
            {id: 0, name: 'Петров Петр Петрович'},
            {id: 1, name: 'Иванов Иван Иванович'},
            {id: 2, name: 'Сидоров Сидор Сидорович'}
            ]}
  }
}
</script>

