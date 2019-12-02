import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    components: {
      default: () => import("../views/Home.vue"),
      navbar: () => import("../components/Navbar.vue")
    }
  },
  {
    path: "/auth",
    components: {
      default: () => import("../views/Auth/Index.vue"),
      navbar: () => import("../components/Navbar.vue")
    },
    children: [
      {
        path: "registration",
        name: "Registration",
        component: () => import("../views/Auth/Registration.vue")
      },
      {
        path: "login",
        name: "Login",
        component: () => import("../views/Auth/Login.vue")
      }
    ]
  },
  {
    path: "/chatsList",
    name: "chatsList",
    components: {
      default: () => import("../chatsList.vue"),
      navbar: () => import("../components/Navbar.vue")
    }
  },
  {
    path: "/myChatList",
    name: "myChatList",
    components: {
      default: () => import("../myChatList.vue"),
      navbar: () => import("../components/Navbar.vue")
    }
  },
  {
    path: "/questionlist",
    name: "questionList",
    components: {
      default: () => import("../views/questionList.vue"),
      navbar: () => import("../components/Navbar.vue")
    }
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
  linkExactActiveClass: "active"
});

export default router;
