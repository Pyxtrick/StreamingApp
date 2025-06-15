import { Route } from '@angular/router';
import { FullscreenComponent } from './pages/fullscreen/fullscreen.component';
import { HighlightMessageComponent } from './pages/highlight-message/highlight-message.component';

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
];
