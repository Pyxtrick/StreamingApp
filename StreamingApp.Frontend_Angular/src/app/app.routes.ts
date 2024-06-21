import { Routes } from '@angular/router';
import { provideEffects } from '@ngrx/effects';
import { provideState } from '@ngrx/store';
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
    // TODO: providers: [provideState(chatFeature)],
    providers: [
      //provideState(ChatFeature),
      //provideEffects(ChatEffect),
    ],
    loadChildren: () =>
      import('./chats/chat.routes').then((m) => m.CHAT_ROUTES),
  },
  {
    // lasy loading (gets sites when changing to path)
    path: 'settings',
    // TODO: providers: [provideState(chatFeature)],
    providers: [provideState(settingsFeature), provideEffects(SettingsEffects)],
    loadChildren: () =>
      import('./settings/settings.routes').then((m) => m.CHAT_ROUTES),
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
