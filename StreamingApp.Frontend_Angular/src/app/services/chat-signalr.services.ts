import { inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Store } from '@ngrx/store';
import { Observable, Subscriber } from 'rxjs';
import { AlertDto } from '../models/dtos/AlertDto';
import { BannedUserDto } from '../models/dtos/BannedUserDto';
import { ChatDto } from '../models/dtos/ChatDto';

@Injectable({
  providedIn: 'root',
})
export class AppSignalRService {
  private store = inject(Store);
  public hubConnection: signalR.HubConnection;
  public observer?: Subscriber<void>;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7033/chathub') // SignalR hub URL
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
          this.observer = observer;
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }

  receiveSpecificChatMessage(): Observable<ChatDto> {
    return new Observable<ChatDto>((observer) => {
      this.hubConnection.on('ReceiveChatMessage', (message: ChatDto) => {
        observer.next(message);
      });
    });
  }

  receiveChatMessage(method: string): Observable<ChatDto> {
    return new Observable<ChatDto>((observer) => {
      this.hubConnection.on(method, (message: ChatDto) => {
        observer.next(message);
      });
    });
  }

  receiveBannedMessage(method: string): Observable<BannedUserDto> {
    return new Observable<BannedUserDto>((observer) => {
      this.hubConnection.on(method, (message: BannedUserDto) => {
        observer.next(message);
      });
    });
  }

  receiveAlertMessage(method: string): Observable<AlertDto> {
    return new Observable<AlertDto>((observer) => {
      this.hubConnection.on(method, (message: AlertDto) => {
        observer.next(message);
      });
    });
  }

  receiveHighlightMessage(): Observable<ChatDto> {
    return new Observable<ChatDto>((observer) => {
      this.hubConnection.on('ReceiveHighlightMessage', (message: ChatDto) => {
        observer.next(message);
      });
    });
  }

  //#region Just for Debugging / testing
  receiveMessage(): Observable<string> {
    return new Observable<string>((observer) => {
      this.hubConnection.on('ReceiveMessage', (message: string) => {
        console.log(message);
        observer.next(message);
      });
    });
  }
  //#endregion

  //#region Not working currently
  sendMessage(message: string): void {
    this.hubConnection
      .invoke('SendMessage', message)
      .catch((err) => console.log(err));
    //this.hubConnection.send('SendMessage', 'test').catch((err) => console.log(err));
  }
  //#endregion
}
