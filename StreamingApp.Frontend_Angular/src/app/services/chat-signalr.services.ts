import { inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { ChatDto } from '../models/dtos/ChatDto';

@Injectable({
  providedIn: 'root',
})
export class AppSignalRService {
  private store = inject(Store);
  public hubConnection: signalR.HubConnection;

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
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }

  receiveChatMessage(): Observable<ChatDto> {
    return new Observable<ChatDto>((observer) => {
      this.hubConnection.on('ReceiveChatMessage', (message: ChatDto) => {
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
