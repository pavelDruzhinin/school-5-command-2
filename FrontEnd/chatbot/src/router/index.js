import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '@/store'
import multiguard from 'vue-router-multiguard'
import axios from 'axios'

const ifAuth = (to, from, next) => {
  if (store.getters.isAuthenticated) next('/')
  else next();
}
const ifNotAuth = (to, from, next) => {
  if (!store.getters.isAuthenticated) next('/auth/login')
  else next()
}
const ifNotOwnChat = (to,from,next) =>{
  let id = to.params.id;
  let status = 200
  axios.get('/chats/isuserchat/'+id)
  .then(()=>next())
  .catch(()=>next('/dashboard'))
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
    path: '/chat',
    name: 'chat',
    components: {
      default: () => import('../views/chat.vue'),
      navbar: () => import('../components/Navbar.vue')
    }
  },
  {
    path: '/edit/:id',
    name: 'edit',
    components: {
      default: () => import('@/views/questionList.vue'),
      navbar: () => import('@/components/Navbar.vue')
    },
    beforeEnter:multiguard([
      ifNotAuth,
      ifNotOwnChat
    ])
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    components: {
      default: () => import('@/views/Dashboard.vue'),
      navbar: () => import('@/components/Navbar.vue')
    },
    // beforeEnter: ifNotAuth
  },
  {
    path: '/auth',
    beforeEnter: ifAuth,
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
  },
  {
    path:'*',
    redirect:'/'
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
  linkExactActiveClass: "active"
})

export default router
