import { Route } from '@angular/router';
import { AllChatPageComponent } from './componets/all-chat/all-chat-page.component';

export const CHAT_ROUTES: Route[] = [
  {
    path: '',
    component: AllChatPageComponent,
    providers: [],
  },
];
