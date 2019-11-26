import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import css from './main.scss'
import chatsList from './chatsList.vue'

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(chatsList)
}).$mount('#app')
