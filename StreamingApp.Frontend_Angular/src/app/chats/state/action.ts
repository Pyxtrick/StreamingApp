import { createActionGroup, emptyProps } from '@ngrx/store';
import { TechnologieDTO } from '../../../api/api.service';

export const MasterDataActions = createActionGroup({
  source: 'Shared',
  events: {
    'load technologies': emptyProps(),
    'load technologies success': (technologies: TechnologieDTO[]) => ({
      technologies,
    }),
    'load technologies failure': emptyProps(),

    'update technologies': (technologies: TechnologieDTO[]) => ({
      technologies,
    }),
    'update technologies success': (technologies: TechnologieDTO[]) => ({
      technologies,
    }),
    'update technologies failure': (message?: string) => ({
      message,
    }),
  },
});
