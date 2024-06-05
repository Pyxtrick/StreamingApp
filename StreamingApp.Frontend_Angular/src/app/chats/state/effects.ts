import { Injectable, inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap } from 'rxjs';
import { Client, CreateTechnologieRequest } from '../../../api/api.service';
import { MasterDataActions } from './actions';

@Injectable()
export class MasterdataEffects {
  private actions$ = inject(Actions);
  private api = inject(Client);

  loadTechnologies$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(MasterDataActions.loadTechnologies),
      switchMap(() =>
        this.api.cvToolApiWebMasterdataGetTechnologies().pipe(
          map((r) => {
            console.log('call load');
            return MasterDataActions.loadTechnologiesSuccess(
              r.technologies ?? []
            );
          }),
          catchError((_error) =>
            of(MasterDataActions.loadTechnologiesFailure())
          )
        )
      )
    );
  });

  updateTechnologies$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(MasterDataActions.updateTechnologies),
      switchMap((payload) =>
        this.api
          .cvToolApiWebMasterdataCreateTechnologie(
            new CreateTechnologieRequest({
              technologies: payload.technologies,
            })
          )
          .pipe(
            map((r) => {
              console.log('call update');
              return MasterDataActions.updateTechnologiesSuccess(r ?? []);
            }),
            catchError((_error) =>
              of(MasterDataActions.updateTechnologiesFailure(_error))
            )
          )
      )
    );
  });

  updateTechnologiesSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(MasterDataActions.updateTechnologiesSuccess),
      map(() => MasterDataActions.loadTechnologies())
    )
  );
}
