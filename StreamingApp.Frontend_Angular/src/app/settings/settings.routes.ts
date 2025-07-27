import { Route } from '@angular/router';
import { SettingsComponent } from './pages/settings/settings.component';

export const SETTINGS_ROUTES: Route[] = [
  {
    path: '',
    component: SettingsComponent,
    providers: [],
  },
];
