<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class ChatBox extends Vue {}
</script>

<template>
  <div>
    <iframe
      id="chatbox"
      src="https://www.giambaj.it/twitch/jchat/v2/?channel=Pyxtrick&size=3&font=4"
      height="500"
      width="350"
    >
    </iframe>
  </div>
</template>

<style lang="scss">
#chatbox {
  background-color: black !important;
}
</style>
