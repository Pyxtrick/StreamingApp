import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap } from 'rxjs';
import { DataContollerClient } from '../../../api/api.service';
import { SettingsActions } from './action';

@Injectable()
export class SettingsEffects {
  private actions$ = inject(Actions);
  private api = inject(DataContollerClient);

  //#region Command
  loadCommands$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(SettingsActions.loadCommands),
      switchMap(() =>
        this.api.getAllCommands().pipe(
          map((r) => {
            if (r.isSucsess) {
              return SettingsActions.loadCommandsSuccess(
                r.commandAndResponses ?? []
              );
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
              return SettingsActions.updateCommandsSuccess(
                r.commandAndResponses ?? []
              );
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
  //#endregion

  //#region Stream
  loadStreams$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(SettingsActions.loadStreams),
      switchMap(() =>
        this.api.getAllStreams().pipe(
          map((r) => {
            if (r.isSucsess) {
              return SettingsActions.loadStreamsSuccess(r.streams ?? []);
            } else {
              return SettingsActions.loadStreamsFailure();
            }
          }),
          catchError((_error) => of(SettingsActions.loadStreamsFailure()))
        )
      )
    );
  });
  //#endregion

  //#region GameInfo
  loadGameInfos$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(SettingsActions.loadGameInfos),
      switchMap(() =>
        this.api.getAllGameInfos().pipe(
          map((r) => {
            if (r.isSucsess) {
              return SettingsActions.loadGameInfosSuccess(r.gameInfos ?? []);
            } else {
              return SettingsActions.loadGameInfosFailure();
            }
          }),
          catchError((_error) => of(SettingsActions.loadGameInfosFailure()))
        )
      )
    );
  });

  updateGameInfos$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(SettingsActions.updateGameInfos),
      switchMap((payload) =>
        this.api.updateGameInfos(payload.gameInfos).pipe(
          map((r) => {
            if (r.isSucsess) {
              return SettingsActions.updateGameInfosSuccess(r.gameInfos ?? []);
            } else {
              return SettingsActions.updateGameInfosFailure();
            }
          }),
          catchError((_error) =>
            of(SettingsActions.updateGameInfosFailure(_error))
          )
        )
      )
    );
  });

  updateGameInfosSuccess$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(SettingsActions.updateGameInfosSuccess),
      map(() => SettingsActions.loadGameInfos())
    );
  });
  //#endregion
}
