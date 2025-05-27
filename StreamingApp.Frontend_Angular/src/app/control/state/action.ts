import { createActionGroup, emptyProps } from '@ngrx/store';
import { Item, Model, MoveModelData, Toggle } from 'src/api/api.service';

export const ControlActions = createActionGroup({
  source: 'Shared',
  events: {
    //#region Vtube-studio
    'load models': emptyProps(),
    'load models success': (models: Model[]) => ({
      models,
    }),
    'load model failure': (message?: string) => ({
      message,
    }),

    'load toggles': emptyProps(),
    'load toggles success': (toggles: Toggle[]) => ({
      toggles,
    }),
    'load toggles failure': (message?: string) => ({
      message,
    }),

    'load items': emptyProps(),
    'load items success': (items: Item[]) => ({
      items,
    }),
    'load items failure': (message?: string) => ({
      message,
    }),

    'move change model': (modelData: MoveModelData) => ({
      modelData,
    }),
    'trigger toggle': (toggleId: string) => ({
      toggleId,
    }),
    'load move delete item': (item: Item) => ({
      item,
    }),
    //#endregion
  },
});
