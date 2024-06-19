import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap } from 'rxjs';
import { CommandClient } from '../../../api/api.service';
import { SettingsActions } from './action';

@Injectable()
export class SettingsEffects {
  private actions$ = inject(Actions);
  private api = inject(CommandClient);

  loadCommands$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(SettingsActions.loadCommands),
      switchMap(() =>
        this.api.getAllCommands().pipe(
          map((r) => {
            if (r.isSucsess) {
              return SettingsActions.loadCommandsSuccess(r.cads ?? []);
            } else {
              return SettingsActions.loadCommandsFailure();
            }
          }),
          catchError((_error) => of(SettingsActions.loadCommandsFailure()))
        )
      )
    );
  });

  updateCommands$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(SettingsActions.updateCommands),
      switchMap((payload) =>
        this.api.updateCommands(payload.commands).pipe(
          map((r) => {
            if (r.isSucsess) {
              return SettingsActions.updateCommandsSuccess(r.cads ?? []);
            } else {
              return SettingsActions.updateCommandsFailure();
            }
          }),
          catchError((_error) =>
            of(SettingsActions.updateCommandsFailure(_error))
          )
        )
      )
    );
  });

  updateCommandsSuccess$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(SettingsActions.updateCommandsSuccess),
      map(() => SettingsActions.loadCommands())
    );
  });
}
