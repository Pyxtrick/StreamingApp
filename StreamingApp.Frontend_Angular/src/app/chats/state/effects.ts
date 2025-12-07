import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap } from 'rxjs';
import {
  DataContollerClient,
  StreamClient,
  WebContollerClient,
} from '../../../api/api.service';
import { ChatsActions } from './action';

@Injectable()
export class ChatsEffects {
  private actions$ = inject(Actions);
  private api = inject(WebContollerClient);
  private dataApi = inject(DataContollerClient);
  private streamIpi = inject(StreamClient);

  sendHighlightMessage$ = createEffect(() => {
    console.log('message Send data');
    return this.actions$.pipe(
      ofType(ChatsActions.sendHighlightMessage),
      switchMap((payload) =>
        this.api.highlightMessage(payload.messageId).pipe(
          map(() => {
            console.log('success');
            return ChatsActions.sendHighlightMessageSuccess();
          }),
          catchError((_error) =>
            of(ChatsActions.sendHighlightMessageFailure(_error))
          )
        )
      )
    );
  });

  switchChatDirection$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ChatsActions.switchChat),
      switchMap((payload) =>
        this.streamIpi.changeChat(payload.isVerticalChat).pipe(
          map((r) => {
            if (r) {
              return ChatsActions.switchChatSuccess();
            } else {
              return ChatsActions.switchChatFailure();
            }
          }),
          catchError((_error) => of(ChatsActions.switchChatFailure()))
        )
      )
    );
  });

  switchAdsDisplay$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ChatsActions.switchAdsDisplay),
      switchMap((payload) =>
        this.streamIpi.switchAdsDisplay(payload.isDisableAdsDisplay).pipe(
          map((r) => {
            if (r) {
              return ChatsActions.switchAdsDisplaySuccess();
            } else {
              return ChatsActions.switchAdsDisplayFailure();
            }
          }),
          catchError((_error) => of(ChatsActions.sendHighlightMessageFailure()))
        )
      )
    );
  });

  loadSettings$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ChatsActions.loadSetting),
      switchMap((payload) =>
        this.dataApi.getSettingByOrigin(payload.origin).pipe(
          map((r) => {
            if (r.isSucsess) {
              return ChatsActions.loadSettingSuccess(r.setting);
            } else {
              return ChatsActions.loadSettingFailure();
            }
          }),
          catchError((_error) => of(ChatsActions.loadSettingFailure()))
        )
      )
    );
  });
}
