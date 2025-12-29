
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';
import { FullscreenComponent } from 'src/app/alert/components/fullscreen/fullscreen.component';
import { HighlightMessageComponent } from 'src/app/alert/components/highlight-message/highlight-message.component';
import { TextMessageComponent } from 'src/app/alert/components/text-message/text-message.component';
import { ConvertMessage } from 'src/app/chats/logic/convertMessage';
import { DisplayChat } from 'src/app/chats/models/DisplayChat';
import { AlertDto } from 'src/app/models/dtos/AlertDto';
import { ChatDto } from 'src/app/models/dtos/ChatDto';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';
import { OnScreenChatHorizontalComponent } from '../../../chats/components/on-screen-chat-horizontal/on-screen-chat-horizontal.component';
import { OnScreenChatComponent } from '../../../chats/components/on-screen-chat/on-screen-chat.component';

@Component({
    selector: 'app-combined',
    imports: [
    FullscreenComponent,
    HighlightMessageComponent,
    TextMessageComponent,
    OnScreenChatHorizontalComponent,
    OnScreenChatComponent
],
    templateUrl: './combined.component.html',
    styleUrl: './combined.component.scss',
    encapsulation: ViewEncapsulation.None
})
export class CombinedComponent implements OnInit, OnDestroy {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}

  public htmldata: SafeHtml | undefined;
  public htmldataList: AlertDto[] = [];
  private fullscreenSubscription: Subscription | undefined;

  public displayChatMessage: DisplayChat | undefined;
  public displayChatMessageList: ChatDto[] = [];
  private highlightSubscription: Subscription | undefined;

  public message: SafeHtml | undefined;
  public messageList: AlertDto[] = [];
  private textSubscription: Subscription | undefined;

  public displayChatMessages: DisplayChat[] = [];
  private chatSubscription: Subscription | undefined;

  public isVerticalChat = false;

  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService
        .receiveAlertMessage('ReceiveAlert')
        .subscribe((message) => {
          if (this.htmldataList.length != 0 && this.htmldata != undefined) {
            this.htmldataList.push(message);
          } else {
            this.htmldata = this._sanitizer.bypassSecurityTrustHtml(
              message.html
            );
            this.fullscreenSubscription = interval(
              (message.duration - 0.5) * 1000
            ).subscribe(() => this.removeFullscreenElement());
          }
        });
      this.signalRService.receiveHighlightMessage().subscribe((message) => {
        if (
          this.displayChatMessageList.length != 0 &&
          this.displayChatMessage != undefined
        ) {
          this.displayChatMessageList.push(message);
        } else {
          this.displayChatMessage = ConvertMessage.convertMessage(
            this._sanitizer,
            message,
            true
          );
          this.highlightSubscription = interval(10 * 1000).subscribe(() =>
            this.removeHighlightElement()
          );
        }
      });
      this.signalRService.receiveOnscreenMessage().subscribe((message) => {
        if (this.messageList.length != 0 && this.message != undefined) {
          this.messageList.push(message);
        } else {
          this.message = this._sanitizer.bypassSecurityTrustHtml(message.html);
          this.textSubscription = interval(message.duration * 1000).subscribe(
            () => this.removeTextElement()
          );
        }
      });
      this.signalRService
        .receiveChatMessage('ReceiveOnScreenChatMessage')
        .subscribe((message) => {
          if (this.displayChatMessages.length >= 100) {
            this.displayChatMessages.shift();
          }
          this.convertMessageData(message);
        });
      this.signalRService
        .receiveBannedMessage('ReceiveBanned')
        .subscribe((message) => {
          const foundMessage = this.displayChatMessages.find(
            (m) => m.Id == message.id
          );

          if (foundMessage != undefined) {
            this.displayChatMessages.splice(
              this.displayChatMessages.indexOf(foundMessage),
              1
            );
          }
        });
      this.signalRService.receiveSwitchChat().subscribe((isVerticalChat) => {
        this.isVerticalChat = isVerticalChat;
      });
    });

    this.chatSubscription = interval(1000).subscribe(() =>
      this.removeChatElement()
    );
  }

  private removeFullscreenElement() {
    if (this.htmldataList.length != 0) {
      const message = this.htmldataList[0];
      this.htmldata = this._sanitizer.bypassSecurityTrustHtml(message.html);
      this.htmldataList.splice(0);

      this.fullscreenSubscription && this.fullscreenSubscription.unsubscribe();

      this.fullscreenSubscription = interval(10 * 1000).subscribe(() =>
        this.removeFullscreenElement()
      );
    } else {
      this.htmldata = undefined;

      this.fullscreenSubscription && this.fullscreenSubscription.unsubscribe();
    }
  }

  private removeHighlightElement() {
    if (this.displayChatMessageList.length != 0) {
      const message = this.displayChatMessageList[0];
      this.displayChatMessage = ConvertMessage.convertMessage(
        this._sanitizer,
        message,
        true
      );
      this.displayChatMessageList.splice(0);

      this.highlightSubscription && this.highlightSubscription.unsubscribe();

      this.highlightSubscription = interval(10 * 1000).subscribe(() =>
        this.removeHighlightElement()
      );
    } else {
      this.displayChatMessage = undefined;

      this.highlightSubscription && this.highlightSubscription.unsubscribe();
    }
  }

  private removeTextElement() {
    if (this.messageList.length != 0) {
      const message = this.messageList[0];
      this.message = this._sanitizer.bypassSecurityTrustHtml(message.html);
      this.messageList.splice(0);

      this.textSubscription && this.textSubscription.unsubscribe();

      this.textSubscription = interval(
        (message.duration - 0.5) * 1000
      ).subscribe(() => this.removeTextElement());
    } else {
      this.message = undefined;

      this.textSubscription && this.textSubscription.unsubscribe();
    }
  }

  private removeChatElement() {
    const date = new Date();
    date.setSeconds(date.getSeconds() - 10);

    const data = this.displayChatMessages.filter(
      (t) => t.Date!.getTime() <= date.getTime()
    );
    data.forEach(() => {
      this.displayChatMessages.shift();
    });
  }

  ngOnDestroy(): void {
    this.fullscreenSubscription && this.fullscreenSubscription.unsubscribe();
    this.highlightSubscription && this.highlightSubscription.unsubscribe();
    this.textSubscription && this.textSubscription.unsubscribe();
    this.chatSubscription && this.chatSubscription.unsubscribe();
  }

  convertMessageData(chatMessage: ChatDto) {
    // For Dot Between two Texts (Text.Text)
    if (
      new RegExp('([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?').test(
        chatMessage.message
      ) != true
    ) {
      this.displayChatMessages.push(
        ConvertMessage.convertMessage(this._sanitizer, chatMessage, true)
      );
    }
  }
}
