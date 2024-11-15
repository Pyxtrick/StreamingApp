import { inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { ChatsActions } from '../chats/state/action';
import { ChatDto } from '../models/dtos/ChatDto';
import { ChatDisplay } from '../models/enums/ChatDisplay';

@Injectable({
  providedIn: 'root',
})
export class AppSignalRService {
  private store = inject(Store);
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7033/realtimehub') // SignalR hub URL
      // TODO: Change to get url from somewhere else (security)
      .build();
  }

  startConnection(): Observable<void> {
    return new Observable<void>((observer) => {
      this.hubConnection
        .start()
        .then(() => {
          console.log('Connection established with SignalR hub');
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }

  receiveMessage(): Observable<ChatDto> {
    return new Observable<ChatDto>((observer) => {
      this.hubConnection.on('ReceiveMessage', (message: ChatDto) => {
        observer.next(message);
        this.saveMessage(message);
      });
    });
  }

  sendMessage(message: ChatDto): void {
    this.hubConnection.invoke('SendMessage', message);
  }

  saveMessage(message: ChatDto) {
    // TODO: Save to the diffrent ngrx store locations
    switch (message.ChatDisplay) {
      case ChatDisplay.allChat: {
        console.log('allChat');
        this.store.dispatch(ChatsActions.addAllChat(message));
        ChatsActions.loadAllChat;
        break;
      }
      case ChatDisplay.twitchChat: {
        console.log('allChat');
        this.store.dispatch(ChatsActions.addTwitchChat(message));
        break;
      }
      case ChatDisplay.youtubeChat: {
        console.log('allChat');
        this.store.dispatch(ChatsActions.addTwitchChat(message));
        break;
      }
      case ChatDisplay.modChat: {
        console.log('allChat');
        break;
      }
      case ChatDisplay.friendChat: {
        console.log('allChat');
        break;
      }
      case ChatDisplay.event: {
        console.log('allChat');
        break;
      }
      case ChatDisplay.modEvent: {
        console.log('allChat');
        break;
      }
      default: {
        console.log('nothing found');
        break;
      }
    }
  }
}
