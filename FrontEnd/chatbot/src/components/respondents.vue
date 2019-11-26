<template>
    <div id="plusButton">
        <div class="dflex justifystart">
            <h1>Список респондентов</h1>
            <button class="button" id="userAddButton">
                +
            </button>
        </div>
        
        <ul>
            <li v-for="(respondent, index) in respondents">
                <div class="dflex  justifystart">
                    <div><h2>{{respondent.name}}</h2></div>
                </div>
            </li>
        </ul>
    </div>
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

h1 {

  margin: 10px 0 0 10px 
}

#userAddButton {

    font-size: 30px;
    background-color: blue;
      padding: 0 15px;

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

export default {
 data() {
    return {
        respondents: [
            {name: 'user1'},
            {name: 'user2'},
            {name: 'user3'}
            ]}
  }
}
</script>

