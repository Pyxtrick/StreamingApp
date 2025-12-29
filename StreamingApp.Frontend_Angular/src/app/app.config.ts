import { HttpClient, provideHttpClient } from '@angular/common/http';
import { ApplicationConfig, Injector, LOCALE_ID, importProvidersFrom, isDevMode, inject, provideAppInitializer } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter } from '@angular/router';
import { provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import {
  TranslateLoader,
  TranslateModule,
  TranslateService,
} from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { lastValueFrom } from 'rxjs';
import { API_BASE_URL } from 'src/api/api.service';
import { routes } from './app.routes';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

export function getBaseUrl(): string {
  return 'https://localhost:7033';
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    provideAnimations(),
    provideAppInitializer(() => {
        const initializerFn = (appInitializerFactory)(inject(TranslateService), inject(Injector));
        return initializerFn();
      }),
    importProvidersFrom(
      TranslateModule.forRoot({
        loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient],
        },
      })
    ),
    {
      // This is throwing Problems
      provide: LOCALE_ID,
      useValue: 'de-DE',
    },
    provideAnimations(),
    provideAnimationsAsync(),
    provideStore(),
    isDevMode() ? provideStoreDevtools() : [],
    { provide: API_BASE_URL, useFactory: getBaseUrl },
  ],
};

function appInitializerFactory(translateService: TranslateService) {
  return () => {
    return lastValueFrom(translateService.use('de'));
  };
}
