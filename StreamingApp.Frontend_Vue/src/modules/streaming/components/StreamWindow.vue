<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class StreamWindow extends Vue {}
</script>

<template>
  <div>
    <iframe
      src="https://player.twitch.tv/?channel=pyxtrick&parent=localhost"
      height="500"
      width="350"
      allowfullscreen
    ></iframe>
  </div>
</template>

<style lang="scss"></style>
