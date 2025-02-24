import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ChatDto } from 'src/app/models/dtos/ChatDto';
import { BannedTargetEnum } from 'src/app/models/enums/BannedTargetEnum';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';
import { AllChatPageComponent } from '../../components/all-chat/all-chat-page.component';
import { ConvertMessage } from '../../logic/convertMessage';
import { BannedUserDto } from './../../../models/dtos/BannedUserDto';
import { DisplayChat } from './../../models/DisplayChat';

@Component({
  selector: 'app-chats',
  standalone: true,
  imports: [AllChatPageComponent],
  templateUrl: './chats.component.html',
  styleUrl: './chats.component.scss',
})
export class ChatsComponent implements OnInit {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}

  displayChatMessages: DisplayChat[] = [];
  displayEventMessages: DisplayChat[] = [];
  displayFriendMessages: DisplayChat[] = [];
  displayModMessages: DisplayChat[] = [];
  displayVipViewerMessages: DisplayChat[] = [];
  displaySpecialMessages: DisplayChat[] = [];
  displayTranslatedMessages: DisplayChat[] = [];
  displayWatchUserMessages: DisplayChat[] = [];
  displayBotMessages: DisplayChat[] = [];
  displayModActionMessages: DisplayChat[] = [];
  displayActionMessages: DisplayChat[] = [];
  displayYoutubeMessages: DisplayChat[] = [];

  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService
        .receiveChatMessage('ReceiveChatMessage')
        .subscribe((message) => {
          if (this.displayChatMessages.length >= 100) {
            this.displayChatMessages.shift();
          }

          this.displayChatMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveEventMessage')
        .subscribe((message) => {
          if (this.displayEventMessages.length >= 100) {
            this.displayEventMessages.shift();
          }

          this.displayEventMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveFriendMessage')
        .subscribe((message) => {
          if (this.displayFriendMessages.length >= 100) {
            this.displayFriendMessages.shift();
          }

          this.displayFriendMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveModMessage')
        .subscribe((message) => {
          if (this.displayModMessages.length >= 100) {
            this.displayModMessages.shift();
          }

          this.displayModMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveVipViewerMessage')
        .subscribe((message) => {
          if (this.displayVipViewerMessages.length >= 100) {
            this.displayVipViewerMessages.shift();
          }

          this.displayVipViewerMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveSpecialMessage')
        .subscribe((message) => {
          if (this.displaySpecialMessages.length >= 100) {
            this.displaySpecialMessages.shift();
          }

          this.displaySpecialMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveTranslatedMessage')
        .subscribe((message) => {
          if (this.displayTranslatedMessages.length >= 100) {
            this.displayTranslatedMessages.shift();
          }

          this.displayTranslatedMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveWatchUserMessage')
        .subscribe((message) => {
          if (this.displayWatchUserMessages.length >= 100) {
            this.displayWatchUserMessages.shift();
          }

          this.displayWatchUserMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveBotMessage')
        .subscribe((message) => {
          if (this.displayBotMessages.length >= 100) {
            this.displayBotMessages.shift();
          }

          this.displayBotMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveModActionMessage')
        .subscribe((message) => {
          if (this.displayModActionMessages.length >= 100) {
            this.displayModActionMessages.shift();
          }

          this.displayModActionMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveChatMessage')
        .subscribe((message) => {
          if (this.displayActionMessages.length >= 100) {
            this.displayActionMessages.shift();
          }

          this.displayActionMessages.push(this.convertMessageData(message));
        });
      this.signalRService
        .receiveChatMessage('ReceiveYoutubeMessage')
        .subscribe((message) => {
          if (this.displayYoutubeMessages.length >= 100) {
            this.displayYoutubeMessages.shift();
          }

          this.displayYoutubeMessages.push(this.convertMessageData(message));
        });

      this.signalRService
        .receiveBannedMessage('ReceiveBanned')
        .subscribe((bannedMessage) => {
          const foundMessage = this.displayChatMessages.find(
            (m) => m.Id == bannedMessage.id
          );
          if (foundMessage != undefined) {
            foundMessage.Addon = this._sanitizer.bypassSecurityTrustHtml(
              this.replaceMessage(bannedMessage)
            );
          }
        });
    });
  }

  convertMessageData(chatMesssage: ChatDto): DisplayChat {
    return ConvertMessage.convertMessage(this._sanitizer, chatMesssage, false);
  }

  replaceMessage(bannedMessage: BannedUserDto): string {
    if (bannedMessage.targetEnum == BannedTargetEnum.Banned) {
      return '<span class="message-text">User Banned</span>';
    }
    if (bannedMessage.targetEnum == BannedTargetEnum.TimeOut) {
      return '<span class="message-text">User Time Out</span>';
    }
    if (bannedMessage.targetEnum == BannedTargetEnum.Message) {
      return '<span class="message-text">Message Deleted</span>';
    }

    return '';
  }
}
