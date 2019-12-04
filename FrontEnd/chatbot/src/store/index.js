import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    profile: {
    }
  },
  getters: {
    isAuthenticated: state => state.profile.email
  },
  mutations: {
    setProfile(state, profile) {
      state.profile = profile;
    }
  },
  actions: {
    register({ commit }, model) {
      return new Promise((resolve, reject) => {
        axios
          .post('/account/register', model)
          .then(response => { commit('setProfile', response.data); resolve(response) })
          .catch(err => reject(err))
      })
    },
    login({ commit }, model) {
      return new Promise((resolve, reject) => {
        axios
          .post('/account/login', model)
          .then(response => { commit('setProfile', response.data); resolve(response) })
          .catch(err => reject(err))
      })
    },
    logout({ commit }) {
      return new Promise((resolve, reject) => {
        axios
          .post('/account/logout')
          .then(() => {
            commit('setProfile', {});
            resolve()
          })
          .catch(err => reject(err))
      })
    },
    restoreContext({ commit }) {
      return new Promise((resolve,reject) =>{
        axios
        .get('/account/context')
        .then(response => {commit('setProfile', response.data); resolve(response)})
      })
    }
  },
})
