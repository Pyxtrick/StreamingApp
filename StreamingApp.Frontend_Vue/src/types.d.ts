import Vue, { ComponentOptions } from "vue";

declare module "vue/types/vue" {
  interface Vue {
    $socket;
  }
}
declare module "vue/types/options" {
  interface ComponentOptions<V extends Vue> {
    sockets?: any;
  }
}
