import Vue from "vue";
import Router from "vue-router";
import NotFound from "@/components/NotFound.vue";

Vue.use(Router);

export const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "root",
      redirect: to => {
        return {
          name: "chat",
        };
      },
    },
    {
      path: "/chat",
      name: "chat",
      meta: {
        requiresAuth: true,
      },
      component: () => import("@/modules/streaming/views/Chat.vue"),
    },
    { path: "/404", component: NotFound },
    { path: "*", redirect: "/404" },
  ],
});
