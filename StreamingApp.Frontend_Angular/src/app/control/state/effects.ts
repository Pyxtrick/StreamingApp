import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap } from 'rxjs';
import { VtubeStudioContollerClient } from '../../../api/api.service';
import { ControlActions } from './action';

@Injectable()
export class ControlEffects {
  private actions$ = inject(Actions);
  private api = inject(VtubeStudioContollerClient);

  //#region Command
  loadModels$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ControlActions.loadModels),
      switchMap(() =>
        this.api.getModels().pipe(
          map((r) => {
            if (r.length >= 0) {
              return ControlActions.loadModelsSuccess(r ?? []);
            } else {
              return ControlActions.loadModelFailure();
            }
          }),
          catchError((_error) => of(ControlActions.loadModelFailure()))
        )
      )
    );
  });

  loadToggles$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ControlActions.loadToggles),
      switchMap(() =>
        this.api.getToggles().pipe(
          map((r) => {
            if (r.length >= 0) {
              return ControlActions.loadTogglesSuccess(r ?? []);
            } else {
              return ControlActions.loadTogglesFailure();
            }
          }),
          catchError((_error) => of(ControlActions.loadTogglesFailure()))
        )
      )
    );
  });

  loadItems$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ControlActions.loadItems),
      switchMap(() =>
        this.api.getItems().pipe(
          map((r) => {
            if (r.length >= 0) {
              return ControlActions.loadItemsSuccess(r ?? []);
            } else {
              return ControlActions.loadItemsFailure();
            }
          }),
          catchError((_error) => of(ControlActions.loadItemsFailure()))
        )
      )
    );
  });

  /** Returns Void
  moveChangeModel$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ControlActions.moveChangeModel),
      switchMap((payload) =>
        this.api.moveOrChangeModel(payload.modelData).pipe(
          map((r) => {
            return ControlActions.loadModelsSuccess(r ?? []);
          }),
          catchError((_error) => of(ControlActions.loadModelFailure()))
        )
      )
    );
  });
  **/

  /** Returns Void
  triggerToggle$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ControlActions.triggerToggle),
      switchMap((payload) =>
        this.api.triggerToggle(payload.toggleId).pipe(
          map((r) => {
            return ControlActions.loadTogglesSuccess(r ?? []);
          }),
          catchError((_error) => of(ControlActions.loadTogglesFailure()))
        )
      )
    );
  });
  */

  /** Returns Void
  loadMoveDeleteItem$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ControlActions.loadMoveDeleteItem),
      switchMap((payload) =>
        this.api.setOrMoveOrDeletItem(payload.item).pipe(
          map((r) => {
            return ControlActions.loadItemsSuccess(r ?? []);
          }),
          catchError((_error) => of(ControlActions.loadItemsFailure()))
        )
      )
    );
  });
  */

  //#endregion
}
