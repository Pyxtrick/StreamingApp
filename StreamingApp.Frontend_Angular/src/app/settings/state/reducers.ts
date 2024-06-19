import { createFeature, createReducer, on } from '@ngrx/store';
import { CommandAndResponseDto } from 'src/api/api.service';
import { SettingsActions } from './action';

export interface SettingsState {
  commands: CommandAndResponseDto[];
}

export const initialState: SettingsState = {
  commands: [],
};

export const settingsFeature = createFeature({
  name: 'masterdata',
  reducer: createReducer(
    initialState,
    //Technologie relevant action
    //#Region
    on(SettingsActions.loadCommands, (state): any => ({
      ...state,
      commandLoading: true,
    })),
    on(SettingsActions.loadCommandsSuccess, (state, { commands }): any => ({
      ...state,
      commands: commands,
      commandLoading: false,
    })),

    on(SettingsActions.updateCommands, (state): any => ({
      ...state,
      updateCommandsLoading: true,
      updateCommandsSuccess: false,
    }))

    //#Endregion
  ),
});
