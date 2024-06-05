import { createSelector } from '@ngrx/store';
import { ClientPrincipal } from '../models/client-principal.model';
import { sharedFeature } from './reducers';

export const selectUserName = createSelector(
  sharedFeature.selectClientPrincipal,
  (principal: ClientPrincipal | null) => {
    console.log('selectUserName', principal);
    if (principal?.claims.some((c) => c.typ === 'name')) {
      return principal.claims.find((c) => c.typ === 'name')!.val;
    }

    return principal?.userDetails ?? 'NOT LOGGED IN!';
  }
);
