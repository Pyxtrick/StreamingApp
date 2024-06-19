import { Routes } from '@angular/router';

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
    providers: [],
    loadChildren: () =>
      import('./chats/chat.routes').then((m) => m.CHAT_ROUTES),
  },
  {
    // lasy loading (gets sites when changing to path)
    path: 'settings',
    // TODO: providers: [provideState(chatFeature)],
    providers: [],
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
