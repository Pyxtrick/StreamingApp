import { Routes } from '@angular/router';
import { provideEffects } from '@ngrx/effects';
import { provideState } from '@ngrx/store';
import { ChatsEffects } from './chats/state/effects';
import { chatsFeature } from './chats/state/reducers';
import { SettingsEffects } from './settings/state/effects';
import { settingsFeature } from './settings/state/reducers';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'chats',
  },
  {
    // lasy loading (gets sites when changing to path)
    path: 'chats',
    providers: [provideState(chatsFeature), provideEffects(ChatsEffects)],
    loadChildren: () =>
      import('./chats/chat.routes').then((m) => m.CHAT_ROUTES),
  },
  {
    // lasy loading (gets sites when changing to path)
    path: 'settings',
    providers: [provideState(settingsFeature), provideEffects(SettingsEffects)],
    loadChildren: () =>
      import('./settings/settings.routes').then((m) => m.CHAT_ROUTES),
  },
  {
    // lasy loading (gets sites when changing to path)
    path: 'alert',
    loadChildren: () =>
      import('./alert/alert.routes').then((m) => m.CHAT_ROUTES),
  },
  /**{
    // not lasy loading (loads all paths form the beginning)
    path: 'chats',
    children: [
      {
        path: '',
        component: Component,
        providers: [],
      },
    ],
  },**/
];
