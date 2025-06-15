import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap } from 'rxjs';
import { WebContollerClient } from '../../../api/api.service';
import { ChatsActions } from './action';

@Injectable()
export class ChatsEffects {
  private actions$ = inject(Actions);
  private api = inject(WebContollerClient);

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
}
