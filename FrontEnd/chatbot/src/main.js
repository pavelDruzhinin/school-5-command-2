import Vue from 'vue'
import BootstrapVue from 'bootstrap-vue';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import router from './router';
import store from './store';
import axios from 'axios';
import App from './App.vue'
// import css from './main.scss'


Vue.config.productionTip = false

Vue.prototype.$http = axios

Vue.use(BootstrapVue);


//store.dispatch('restoreContext').then(()=>
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')//)
