<template>
  <div class="landing-page">
    <div class="header">
      <h1 class="title">
        Чат
        <span class="title-color">БОТ</span>
      </h1>
      <span class="subtitle">Создать чат бота быстро и легко</span>
    </div>
    <div class="content">
      <p>Зарегистрируйтесь и соберите своего первого чат бота в конструкторе</p>
      <b-button
        class="btn"
        @click="isAuthenticated ? $router.push({name: 'Dashboard'}) : $router.push({ name: 'Registration' })"
        variant="outline-light"
      >{{isAuthenticated ? "Начать работу" : "Попробовать бесплатно"}}</b-button>
    </div>
    <div class="cards__title">
      <span>Возможности платформы</span>
    </div>
    <div class="wrapper">
      <div class="cards">
        <article class="card card--1">
          <div class="card__img"></div>
          <div class="card__img--hover"></div>
          <div class="card__info">
            <span class="card__category">Преимущества</span>
            <h3 class="card__title">Проводить социологические опросы</h3>
          </div>
        </article>
        <article class="card card--2">
          <div class="card__img"></div>
          <div class="card__img--hover"></div>
          <div class="card__info">
            <span class="card__category">Преимущества</span>
            <h3 class="card__title">Поиск соискателей без присутствия HR-менеджеров</h3>
          </div>
        </article>
        <article class="card card--3">
          <div class="card__img"></div>
          <div class="card__img--hover"></div>
          <div class="card__info">
            <span class="card__category">Преимущества</span>
            <h3 class="card__title">Оценка работы своих сервисов</h3>
          </div>
        </article>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  computed: {
      ...mapGetters(["isAuthenticated"])
    },
};
</script>

<style lang="scss" scoped>
.header {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 4%;
}
.title {
  font-size: 42px;
  font-weight: bold;
  &-color {
    color: #007bff;
  }
}

.subtitle {
  font-size: 18px;
}

.content {
  background: #007bff;
  margin: 2% 0px;
  height: 200px;
  color: #ffffff;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  p {
    text-align: center;
    font-size: 18px;
  }
  .btn {
    font-weight: bold;
  }
}

.wrapper {
  display: flex;
  justify-content: center;
  margin: 2% 0;
}

.cards__title {
  text-align: center;
  font-size: 24px;
  font-weight: bold;
}

.cards {
  width: 100%;
  display: flex;
  display: -webkit-flex;
  justify-content: center;
  -webkit-justify-content: center;
  max-width: 820px;
  @media screen and (max-width: 826px) {
    flex-wrap: wrap;
  }
}

.card--1 .card__img,
.card--1 .card__img--hover {
  background-image: url("../assets/social.png");
}

.card--2 .card__img,
.card--2 .card__img--hover {
  background-image: url("../assets/hr.png");
}

.card--3 .card__img,
.card--3 .card__img--hover {
  background-image: url("../assets/service.png");
}

.card__img {
  visibility: hidden;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  width: 100%;
  height: 235px;
  border-top-left-radius: 12px;
  border-top-right-radius: 12px;
}

.card__img--hover {
  transition: 0.2s all ease-out;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  width: 100%;
  position: absolute;
  height: 235px;
  border-top-left-radius: 12px;
  border-top-right-radius: 12px;
  top: 0;
}

.card {
  margin-right: 25px;
  transition: all 0.4s cubic-bezier(0.175, 0.885, 0, 1);
  background-color: #fff;
  width: 33.3%;
  position: relative;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0px 13px 10px -7px rgba(0, 0, 0, 0.1);
  @media screen and (max-width: 826px) {
    width: 100%;
    margin: 0 10px 30px 10px;
  }
}

.card:nth-last-child(1) {
  margin-right: 0px;
}

.card:hover {
  box-shadow: 0px 30px 18px -8px rgba(0, 0, 0, 0.1);
  transform: scale(1.1, 1.1);
}

.card__info {
  z-index: 2;
  background-color: #fff;
  border-bottom-left-radius: 12px;
  border-bottom-right-radius: 12px;
  padding: 16px 24px 24px 24px;
}

.card__category {
  text-transform: uppercase;
  font-size: 13px;
  letter-spacing: 2px;
  font-weight: 500;
  color: #868686;
}

.card__title {
  margin-top: 5px;
  margin-bottom: 10px;
  font-size: 24px;
}

.card:hover .card__img--hover {
  height: 100%;
  opacity: 0.3;
}

.card:hover .card__info {
  background-color: transparent;
  position: relative;
}

.card:hover .card__info-hover {
  opacity: 1;
}
</style>
