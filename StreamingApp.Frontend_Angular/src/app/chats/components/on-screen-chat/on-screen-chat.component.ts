import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
  ViewEncapsulation,
} from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { DomSanitizer } from '@angular/platform-browser';
import { ChatDto } from 'src/app/models/dtos/ChatDto';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';
import { ConvertMessage } from '../../logic/convertMessage';
import { DisplayChat } from '../../models/DisplayChat';

@Component({
  selector: 'app-on-screen-chat',
  imports: [MatListModule],
  templateUrl: './on-screen-chat.component.html',
  styleUrl: './on-screen-chat.component.scss',
  standalone: true,
  encapsulation: ViewEncapsulation.None,
})
export class OnScreenChatComponent implements OnInit, AfterViewInit {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}
  @ViewChild('scrollframe') scrollFrame!: ElementRef;

  @ViewChildren('item') itemElements!: QueryList<any>;

  private scrollContainer: any;
  private isNearBottom = true;

  displayChatMessages: DisplayChat[] = [];

  ngAfterViewInit(): void {
    this.scrollContainer = this.scrollFrame.nativeElement;
    this.itemElements.changes.subscribe((_) => this.onItemElementsChanged());
  }

  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService
        .receiveChatMessage('ReceiveChatMessage')
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
    });
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

  //#region Auto Scroll to Bottom when at bottom
  private onItemElementsChanged(): void {
    if (this.isNearBottom) {
      this.scrollToBottom();
    }
  }

  private scrollToBottom(): void {
    this.scrollContainer.scroll({
      top: this.scrollContainer.scrollHeight,
      left: 0,
      behavior: 'smooth',
    });
  }

  private isUserNearBottom(): boolean {
    const threshold = 150;
    const position =
      this.scrollContainer.scrollTop + this.scrollContainer.offsetHeight;
    const height = this.scrollContainer.scrollHeight;
    return position > height - threshold;
  }

  scrolled(event: any): void {
    this.isNearBottom = this.isUserNearBottom();
  }
  //#endregion
}
