<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class CustomNavBarComponent extends Vue {
  notifications: any[] = [];
  userId = 0; // set userId based on logged in user data
  showNotificationBadge = false;
  onClickNotifications() {
    this.$socket.invoke("RegisterForChat", this.userId);
  }
}
</script>

<template>
  <div>
    <b-button
      v-b-toggle.sidebar-1
      class="notification-button"
      @click="onClickNotifications"
    >
      <i class="fas fa-bell"></i>
      <span
        v-if="showNotificationBadge"
        class="position-absolute notification-badge bg-danger border border-light rounded-circle ml-1"
      >
      </span>
    </b-button>
    <b-sidebar
      id="sidebar-1"
      class="my-side-bar"
      title="Notifications"
      right
      shadow
    >
      <div class="px-3 py-2 ">
        <b-alert
          v-for="notification in notifications"
          :key="notification.id"
          show
          :variant="notification.notificationEvent"
          class="notification-content"
        >
          <h5 class="alert-heading"></h5>
          <p></p>
          <hr />
          <p class="mb-0 notification-footer"></p>
        </b-alert>
      </div>
    </b-sidebar>
  </div>
</template>

<style lang="scss" scoped>
.notification-button {
  border-radius: 30px;
}
.notification-content {
  text-align: left !important;
}
.notification-footer {
  text-align: right !important;
}
.notification-badge {
  padding: 5px;
}
</style>
