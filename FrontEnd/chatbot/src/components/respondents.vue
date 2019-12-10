<template>
    <div id="plusButton">
        <div class="dflex justibetween">
            <h1>Список респондентов</h1>
            <button  type="button" class="btn btn-primary" id="userAddButton">
                +
            </button>
        </div>
        
        <ul>
            <li v-for="(respondent, index) in respondents" v-bind:key="respondents.key">
                <div class="dflex  justibetween">
                    <h2>{{respondent.name}}</h2>
                    <div class="dflex justifystart">

                      <button  type="button" class="btn btn-primary">
                        Посмотреть результаты
                      </button>

                      <button  type="button" class="btn btn-primary">
                        Открыть профиль
                      </button>
                    </div>
                </div>
                <hr>
            </li>
        </ul>
    </div>
</template>

<style lang="scss" scoped>

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
            {id: 0, name: 'user1'},
            {id: 1, name: 'user2'},
            {id: 2, name: 'user3'}
            ]}
  }
}
</script>

