<template>
  <div id="home">

    <div class="dflex justifycenter">
      <h1>Конструктор чатов 3000 ентерпрайс лимитед</h1>
    </div>
    <div class="dflex justifycenter">
      <button type="button" class="btn btn-primary">ЗАРЕГИСТРИРУЙТЕСЬ</button>
    </div>
    <div class="dflex justifycenter">
      <p id="afterbutton">И вы сможете:</p>
    </div>
    <div class="dflex justifycenter">
      <div class="dflex aligncenter justifybetween" id="cardsbox">
        <div class="card" style="width: 18rem;">
          <img class="card-img-top" src="../assets/card1.jpg" alt="Card image cap">
          <div class="card-body">
            <h5 class="card-title">Проводить социологические опросы</h5>
            <p class="card-text"></p>
            <a href="#" class="btn btn-primary">начать!</a>
          </div>
        </div>
                <div class="card" style="width: 18rem;">
          <img class="card-img-top" src="../assets/card2.jpg" alt="Card image cap">
          <div class="card-body">
            <h5 class="card-title">Искать работников без присутствия HR-менеджеров</h5>
            <p class="card-text"></p>
            <a href="#" class="btn btn-primary">к делу!</a>
          </div>
        </div>
                <div class="card" style="width: 18rem;">
          <img class="card-img-top" src="../assets/card3.jpg" alt="Card image cap">
          <div class="card-body">
            <h5 class="card-title">Оценивать работу своих сервисов</h5>
            <p class="card-text"></p>
            <a href="#" class="btn btn-primary">вперёд!</a>
          </div>
        </div>

      </div>
    </div>
    <div class="dflex justifycenter">
      <p>Для того, чтобы начать использовать конструктор, вам нужно зарегистрироваться.</p>
    </div>
    <div class="dflex justifycenter">
    <p>После регистрации вам откроется возможность создать своего чат бота, просматривать список созданных вами чатов, а также просматривать список пройденных вами чат-бот анкет</p>
    </div>
  </div>
</template>

<script>

export default {
}
</script>

<style lang="scss" scoped>
  .dflex {
    display:flex;
  }
  .justifybetween {
    justify-content: space-between;
  }
  .aligncenter {
    align-items: center;
  }
  .justifycenter {
    justify-content: center;
  }
  .imgcontainer {
    width:200px;
    height: 200px;
  }
  #home{
      button {
        margin-top:50px;
      }
      #afterbutton {
        font-size: 30px;
      }
    
    #cardsbox {
      width:65%;
      text-align: center;


       }
    
    ul{
      list-style-type:decimal;
    }
  }
</style>