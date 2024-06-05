import VueSignalR from "@latelier/vue-signalr";
import Vue from "vue";
import App from "./App.vue";
import { router } from "./router";
import store from "./store";
import vuetify from "@/plugins/vuetify";

Vue.config.productionTip = false;
Vue.use(VueSignalR, "http://localhost:5001/api/public/hub");

new Vue({
  vuetify,
  router,
  store,
  components: {
    App,
  },
  created() {
    this.$socket.start({
      log: true, // Active only in development for debugging.
    });
  },
  render: h => h(App),
}).$mount("#app");
