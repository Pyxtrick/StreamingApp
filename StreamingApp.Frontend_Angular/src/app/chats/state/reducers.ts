import { createFeature, createReducer, on } from '@ngrx/store';
import { ChatDto } from 'src/app/models/dtos/ChatDto';
import { ChatsActions } from './action';

export interface ChatsState {
  allChat: ChatDto[];
  twitchChat: ChatDto[];
  youtubeChat: ChatDto[];
  modChat: ChatDto[];
  friendChat: ChatDto[];
  event: ChatDto[];
  modEvent: ChatDto[];
}

export const initialState: ChatsState = {
  allChat: [],
  twitchChat: [],
  youtubeChat: [],
  modChat: [],
  friendChat: [],
  event: [],
  modEvent: [],
};

export const chatsFeature = createFeature({
  name: 'chats',
  reducer: createReducer(
    initialState,

    //#region All Chat
    on(ChatsActions.loadAllChat, (state): any => ({
      ...state,
    })),
    on(ChatsActions.addAllChat, (state, { chat }): any => ({
      allchat: chat,
    })),
    //#endregion

    //#region Twitch Chat
    on(ChatsActions.loadTwitchChat, (state): any => ({
      ...state,
    })),
    on(ChatsActions.addTwitchChat, (state): any => ({
      ...state,
    })),
    //#endregion

    //#region Youtube Chat
    on(ChatsActions.loadYoutubeChat, (state): any => ({
      ...state,
    })),
    on(ChatsActions.addYoutubeChat, (state): any => ({
      ...state,
    })),
    //#endregion

    //#region Mod Chat
    on(ChatsActions.loadModChat, (state): any => ({
      ...state,
    })),
    on(ChatsActions.addModChat, (state): any => ({
      ...state,
    })),
    //#endregion

    //#region Friend Chat
    on(ChatsActions.loadFriendChat, (state): any => ({
      ...state,
    })),
    on(ChatsActions.addFriendChat, (state): any => ({
      ...state,
    })),
    //#endregion

    //#region Event
    on(ChatsActions.loadEvent, (state): any => ({
      ...state,
    })),
    on(ChatsActions.addEvent, (state): any => ({
      ...state,
    })),
    //#endregion

    //#region Mod Event
    on(ChatsActions.loadModEvents, (state): any => ({
      ...state,
    })),
    on(ChatsActions.addModEvents, (state): any => ({
      ...state,
    }))
    //#endregion
  ),
});
