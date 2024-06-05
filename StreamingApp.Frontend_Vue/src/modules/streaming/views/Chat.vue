<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import StreamWindow from "@/modules/streaming/components/StreamWindow.vue";
import ChatBox from "@/modules/streaming/components/ChatBox.vue";

@Component({
  components: {
    StreamWindow,
    ChatBox,
  },
})
export default class Chat extends Vue {}
</script>

<template>
  <div id="app">
    <stream-window></stream-window>
    <chat-box></chat-box>
  </div>
</template>

<style lang="scss"></style>
