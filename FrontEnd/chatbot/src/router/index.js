import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '@/store'

const ifAuth = (to,from,next) => {
  if(store.getters.isAuthenticated) next('/')
  else next();
}
const ifNotAuth = (to, from, next) => {
  if(!store.getters.isAuthenticated) next('/auth/login')
  else next()
}

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    components: {
      default: () => import('../views/Home.vue'),
      navbar: () => import('../components/Navbar.vue')
    }
  },
  {
    path: '/auth',
    beforeEnter:ifAuth,
    components: {
      default: () => import('../views/Auth/Index.vue'),
      navbar: () => import('../components/Navbar.vue')
    },
    children: [
      {
        path: 'registration',
        name: 'Registration',
        component: () => import('../views/Auth/Registration.vue')
      },
      {
        path: 'login',
        name: 'Login',
        component: () => import('../views/Auth/Login.vue')
      }
    ]
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
  linkExactActiveClass: "active"
})

export default router
