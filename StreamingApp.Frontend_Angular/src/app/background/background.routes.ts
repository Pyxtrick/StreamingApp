import { Route } from '@angular/router';
import { BackgroundComponent } from './pages/background/background.component';

export const BACKGROUND_ROUTES: Route[] = [
  {
    path: '',
    component: BackgroundComponent,
    providers: [],
  },
];
