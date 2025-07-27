import { Route } from '@angular/router';
import { CombinedComponent } from './pages/combined/combined.component';

export const MAIN_ROUTES: Route[] = [
  {
    path: '',
    component: CombinedComponent,
    providers: [],
  },
];
