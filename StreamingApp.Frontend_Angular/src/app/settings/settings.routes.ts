import { Route } from '@angular/router';
import { CommandComponent } from './components/command/command.component';

export const CHAT_ROUTES: Route[] = [
  {
    path: '',
    component: CommandComponent,
    providers: [],
  },
];
