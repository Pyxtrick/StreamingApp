import { createActionGroup, emptyProps } from '@ngrx/store';
import {
  CommandAndResponseDto,
  GameInfoDto,
  StreamDto,
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
  },
});
