import { createActionGroup, emptyProps } from '@ngrx/store';
import { ChatDto } from 'src/app/models/dtos/ChatDto';

export const ChatsActions = createActionGroup({
  source: 'chat',
  events: {
    //#region All Chat
    'load all chat': emptyProps(),
    'load all chat success': (chats: ChatDto[]) => ({
      chats,
    }),

    'add all chat': (chat: ChatDto) => ({
      chat,
    }),
    //#endregion
    //#region Twitch Chat
    'load twitch chat': emptyProps(),
    'load twitch chat success': (chats: ChatDto[]) => ({
      chats,
    }),

    'add twitch chat': (chat: ChatDto) => ({
      chat,
    }),
    //#endregion
    //#region Youtube Chat
    'load youtube chat': emptyProps(),
    'load youtube chat success': (chats: ChatDto[]) => ({
      chats,
    }),

    'add youtube chat': (chat: ChatDto) => ({
      chat,
    }),
    //#endregion
    //#region Mod Chat
    'load mod chat': emptyProps(),
    'load mod chat success': (chats: ChatDto[]) => ({
      chats,
    }),

    'add mod chat': (chat: ChatDto) => ({
      chat,
    }),
    //#endregion
    //#region Friend Chat
    'load friend chat': emptyProps(),
    'load friend chat success': (chats: ChatDto[]) => ({
      chats,
    }),

    'add friend chat': (chat: ChatDto) => ({
      chat,
    }),
    //#endregion
    //#region Event
    'load event': emptyProps(),
    'load event success': (chats: ChatDto[]) => ({
      chats,
    }),

    'add event': (chat: ChatDto) => ({
      chat,
    }),
    //#endregion
    //#region Mod Event
    'load mod events': emptyProps(),
    'load mod events success': (chats: ChatDto[]) => ({
      chats,
    }),

    'add mod events': (chat: ChatDto) => ({
      chat,
    }),
    //#endregion

    //#region Friend Chat
    'send highlight message': (messageId: string) => ({
      messageId,
    }),

    'send highlight message success': emptyProps(),
    'send highlight message failure': (message?: string) => ({
      message,
    }),
    //#endregion
  },
});
