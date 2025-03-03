import { Route } from '@angular/router';
import { FullscreenComponent } from './pages/fullscreen/fullscreen.component';

export const CHAT_ROUTES: Route[] = [
  {
    path: '',
    component: FullscreenComponent,
    providers: [],
  },
];
