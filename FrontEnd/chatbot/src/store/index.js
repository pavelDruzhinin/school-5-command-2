import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    profile: {
      FullName:'Alexei Freid Freidovich',
      email:'a@a.com'
    }
  },
  getters: {
    isAuthenticated: state => state.profile.FullName && state.profile.email
  },
  mutations: {
    setProfile(state, profile) {
      state.profile = profile;
    }
  },
  actions: {
    register({ commit }, model) {
      return axios
        .post('account/register', model)
        .then(response => commit('setProfile', response.data))
    },
    login({ commit }, credentials) {
      return axios
        .post('account/login', credentials)
        .then(response => commit('setProfile', response.data))
    },
    logout({ commit }) {
      // return axios
        // .post('account/logout')
        // .then(() => 
        commit('setProfile', {})
        // )
    },
    restoreContext({ commit}) {
      return axios
      .get('account/context')
        .then(response => commit('setProfile', response.data))
    }
  },
})
