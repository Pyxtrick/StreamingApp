import { createActionGroup, emptyProps } from '@ngrx/store';
import { CommandAndResponseDto } from 'src/api/api.service';

export const SettingsActions = createActionGroup({
  source: 'Shared',
  events: {
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
  },
});
