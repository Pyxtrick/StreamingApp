import { createFeature, createReducer, on } from '@ngrx/store';
import { GameInfoDto, Model, StreamDto } from 'src/api/api.service';
import { ControlActions } from './action';

export interface ControlState {
  models: Model[];
  streams: StreamDto[];
  gameInfos: GameInfoDto[];
}

export const initialState: ControlState = {
  models: [],
  streams: [],
  gameInfos: [],
};

export const controlsFeature = createFeature({
  name: 'control',
  reducer: createReducer(
    initialState,
    //#region Command
    on(ControlActions.loadModels, (state): any => ({
      ...state,
      modelLoading: true,
    })),
    on(ControlActions.loadModelsSuccess, (state, { models }): any => ({
      ...state,
      models: models,
      modelLoading: false,
    })),

    on(ControlActions.loadToggles, (state): any => ({
      ...state,
      commandLoading: true,
    })),
    on(ControlActions.loadTogglesSuccess, (state, { toggles }): any => ({
      ...state,
      toggles: toggles,
      toggleLoading: false,
    })),

    on(ControlActions.loadItems, (state): any => ({
      ...state,
      itemLoading: true,
    })),
    on(ControlActions.loadItemsSuccess, (state, { items }): any => ({
      ...state,
      items: items,
      itemLoading: false,
    })),

    on(ControlActions.moveChangeModel, (state): any => ({
      ...state,
    })),

    on(ControlActions.triggerToggle, (state): any => ({
      ...state,
    })),

    on(ControlActions.loadMoveDeleteItem, (state): any => ({
      ...state,
    }))
    //#endregion
  ),
});
