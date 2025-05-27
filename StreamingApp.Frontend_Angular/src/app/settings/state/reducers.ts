import { createFeature, createReducer, on } from '@ngrx/store';
import {
  CommandAndResponseDto,
  GameInfoDto,
  SpecialWordDto,
  StreamDto,
  UserDto,
} from 'src/api/api.service';
import { SettingsActions } from './action';

export interface SettingsState {
  commands: CommandAndResponseDto[];
  streams: StreamDto[];
  gameInfos: GameInfoDto[];
  user: UserDto[];
  specialWords: SpecialWordDto[];
}

export const initialState: SettingsState = {
  commands: [],
  streams: [],
  gameInfos: [],
  user: [],
  specialWords: [],
};

export const settingsFeature = createFeature({
  name: 'settings',
  reducer: createReducer(
    initialState,
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
      updateGameInfosSuccess: false,
    })),
    //#endregion

    //#region User
    on(SettingsActions.loadUsers, (state): any => ({
      ...state,
      userLoading: true,
    })),
    on(SettingsActions.loadUsersSuccess, (state, { users }): any => ({
      ...state,
      users: users,
      userLoading: false,
    })),

    on(SettingsActions.updateUsers, (state): any => ({
      ...state,
      updateUsersLoading: true,
      updateUsersSuccess: false,
    })),
    //#endregion

    //#region SpecialWord
    on(SettingsActions.loadSpecialWords, (state): any => ({
      ...state,
      specialWordLoading: true,
    })),
    on(
      SettingsActions.loadSpecialWordsSuccess,
      (state, { specialWords }): any => ({
        ...state,
        specialWords: specialWords,
        specialWordLoading: false,
      })
    ),

    on(SettingsActions.updateSpecialWords, (state): any => ({
      ...state,
      updateSpecialWordsLoading: true,
      updateSpecialWordsSuccess: false,
    }))
    //#endregion
  ),
});
