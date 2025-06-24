import { Route } from '@angular/router';
import { FullscreenComponent } from './components/fullscreen/fullscreen.component';
import { HighlightMessageComponent } from './components/highlight-message/highlight-message.component';
import { TextMessageComponent } from './components/text-message/text-message.component';

export const CHAT_ROUTES: Route[] = [
  {
    path: '',
    component: FullscreenComponent,
    providers: [],
  },
  {
    path: 'HighlightMessage',
    component: HighlightMessageComponent,
    providers: [],
  },
  {
    path: 'TextMessage',
    component: TextMessageComponent,
    providers: [],
  },
];
