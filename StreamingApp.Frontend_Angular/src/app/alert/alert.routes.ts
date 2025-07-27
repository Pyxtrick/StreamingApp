import { Route } from '@angular/router';
import { FullscreenComponent } from './components/fullscreen/fullscreen.component';
import { HighlightMessageComponent } from './components/highlight-message/highlight-message.component';
import { TextMessageComponent } from './components/text-message/text-message.component';
import { CombindedAlertsComponent } from './pages/combinded-alerts/combinded-alerts.component';

export const ALERT_ROUTES: Route[] = [
  {
    path: '',
    component: CombindedAlertsComponent,
    providers: [],
  },
  {
    path: 'Fullscreen',
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
