<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class NotFound extends Vue {}
</script>

<template>
  <v-row no-gutters justify="center" align="center" class="ma-0">
    <v-col cols="12" sm="10" class="not-found-wrapper">
      <div class="svg-wrapper">
        <!--<img class="logo" src="@/assets/notFoundIcon.svg" />-->
      </div>
      <div class="content-wrapper">
        <h1 class="not-found-title">
          404.
        </h1>
        <h2 class="not-found-sub-title">
          Page Not Found
        </h2>
      </div>
    </v-col>
  </v-row>
</template>

<style lang="scss" scoped>
.svg-wrapper {
  text-align: center;
  display: block;
  margin-bottom: 4rem;

  .logo {
    width: 67%;
    max-width: 800px;
  }
}

.not-found-sub-title,
.not-found-title {
  text-align: center;
}

.not-found-title {
  font-size: 4rem;
  font-weight: 300;
}

.not-found-sub-title {
  font-size: 2rem;
  font-weight: 300;
}
</style>
