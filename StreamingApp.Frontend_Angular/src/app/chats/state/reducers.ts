import { createFeature, createReducer, on } from '@ngrx/store';
import { TechnologieDTO } from '../../../api/api.service';
import { MasterDataActions } from './actions';

export interface MasterdataState {
  masterdataLoading: boolean;
  technologies: TechnologieDTO[];
}

export const initialState: MasterdataState = {
  masterdataLoading: false,
  technologies: [],
};

export const masterdataFeature = createFeature({
  name: 'masterdata',
  reducer: createReducer(
    initialState,
    //Technologie relevant action
    //#Region
    on(MasterDataActions.loadTechnologies, (state) => ({
      ...state,
      technologieLoading: true,
    })),
    on(
      MasterDataActions.loadTechnologiesSuccess,
      (state, { technologies }) => ({
        ...state,
        technologies: technologies,
        technologieLoading: false,
      })
    ),

    on(MasterDataActions.updateTechnologies, (state) => ({
      ...state,
      updateTechnologiesLoading: true,
      updateTechnologieSuccess: false,
    }))

    //#Endregion
  ),
});
