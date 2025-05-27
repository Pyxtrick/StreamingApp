import { createActionGroup, emptyProps } from '@ngrx/store';
import {
  CommandAndResponseDto,
  GameInfoDto,
  SpecialWordDto,
  StreamDto,
  UserDto,
} from 'src/api/api.service';

export const SettingsActions = createActionGroup({
  source: 'Shared',
  events: {
    //#region Commands
    'load commands': emptyProps(),
    'load commands success': (commands: CommandAndResponseDto[]) => ({
      commands,
    }),
    'load commands failure': (message?: string) => ({
      message,
    }),

    'update commands': (commands: CommandAndResponseDto[]) => ({
      commands,
    }),
    'update commands success': (commands: CommandAndResponseDto[]) => ({
      commands,
    }),
    'update commands failure': (message?: string) => ({
      message,
    }),
    //#endregion

    //#region Stream
    'load streams': emptyProps(),
    'load streams success': (streams: StreamDto[]) => ({
      streams,
    }),
    'load streams failure': (message?: string) => ({
      message,
    }),
    //#endregion

    //#region GameInfo
    'load gameInfos': emptyProps(),
    'load gameInfos success': (gameInfos: GameInfoDto[]) => ({
      gameInfos,
    }),
    'load gameInfos failure': (message?: string) => ({
      message,
    }),

    'update gameInfos': (gameInfos: GameInfoDto[]) => ({
      gameInfos,
    }),
    'update gameInfos success': (gameInfos: GameInfoDto[]) => ({
      gameInfos,
    }),
    'update gameInfos failure': (message?: string) => ({
      message,
    }),
    //#endregion

    //#region User
    'load users': emptyProps(),
    'load users success': (users: UserDto[]) => ({
      users,
    }),
    'load users failure': (message?: string) => ({
      message,
    }),

    'update users': (users: UserDto[]) => ({
      users,
    }),
    'update users success': (users: UserDto[]) => ({
      users,
    }),
    'update users failure': (message?: string) => ({
      message,
    }),
    //#endregion

    //#region SpecialWord
    'load specialWords': emptyProps(),
    'load specialWords success': (specialWords: SpecialWordDto[]) => ({
      specialWords,
    }),
    'load specialWords failure': (message?: string) => ({
      message,
    }),

    'update specialWords': (specialWords: SpecialWordDto[]) => ({
      specialWords,
    }),
    'update specialWords success': (specialWords: SpecialWordDto[]) => ({
      specialWords,
    }),
    'update specialWords failure': (message?: string) => ({
      message,
    }),
    //#endregion
  },
});
