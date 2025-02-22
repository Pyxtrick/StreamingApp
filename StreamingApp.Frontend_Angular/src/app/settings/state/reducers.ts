import { createFeature, createReducer, on } from '@ngrx/store';
import {
  CommandAndResponseDto,
  GameInfoDto,
  StreamDto,
} from 'src/api/api.service';
import { SettingsActions } from './action';

export interface SettingsState {
  commands: CommandAndResponseDto[];
  streams: StreamDto[];
  gameInfo: GameInfoDto[];
}

export const initialState: SettingsState = {
  commands: [],
  streams: [],
  gameInfo: [],
};

export const settingsFeature = createFeature({
  name: 'settings',
  reducer: createReducer(
    initialState,
    //Technologie relevant action
    //#region Command
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
    })),
    //#endregion

    //#region Stream
    on(SettingsActions.loadStreams, (state): any => ({
      ...state,
      commandLoading: true,
    })),
    on(SettingsActions.loadStreamsSuccess, (state, { streams }): any => ({
      ...state,
      streams: streams,
      streamLoading: false,
    })),
    //#endregion

    //#region GameInfo
    on(SettingsActions.loadGameInfos, (state): any => ({
      ...state,
      commandLoading: true,
    })),
    on(SettingsActions.loadGameInfosSuccess, (state, { gameInfos }): any => ({
      ...state,
      gameInfos: gameInfos,
      gameInfoLoading: false,
    })),

    on(SettingsActions.updateGameInfos, (state): any => ({
      ...state,
      updateGameInfosLoading: true,
      updateGameInofsSuccess: false,
    }))
    //#endregion
  ),
});
