import { Route } from '@angular/router';
import { AllChatPageComponent } from './components/all-chat/all-chat-page.component';
import { OnScreenChatComponent } from './components/on-screen-chat/on-screen-chat.component';
import { ChatsComponent } from './pages/chats/chats.component';

export const CHAT_ROUTES: Route[] = [
  {
    path: '',
    component: ChatsComponent,
    providers: [],
  },
  {
    path: 'all',
    component: AllChatPageComponent,
    providers: [],
  },
  {
    path: 'onScreen',
    component: OnScreenChatComponent,
    providers: [],
  },
];
