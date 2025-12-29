import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
  ViewEncapsulation,
} from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DomSanitizer } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';
import { ChatDto } from 'src/app/models/dtos/ChatDto';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';
import { ConvertMessage } from '../../logic/convertMessage';
import { DisplayChat } from '../../models/DisplayChat';

@Component({
    selector: 'app-on-screen-chat-horizontal',
    imports: [MatListModule, MatTooltipModule],
    templateUrl: './on-screen-chat-horizontal.component.html',
    styleUrl: './on-screen-chat-horizontal.component.scss',
    encapsulation: ViewEncapsulation.None
})
export class OnScreenChatHorizontalComponent implements OnInit, AfterViewInit {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}
  @ViewChild('scrollframe') scrollFrame!: ElementRef;
  @ViewChildren('item') itemElements!: QueryList<any>;

  @Input() useSignalR = true;
  @Input() displayChatMessages: DisplayChat[] = [];

  private scrollContainer: any;
  private isNearBottom = true;

  private subscription: Subscription | undefined;

  ngAfterViewInit(): void {
    this.scrollContainer = this.scrollFrame.nativeElement;
    this.itemElements.changes.subscribe((_) => this.onItemElementsChanged());
  }

  ngOnInit(): void {
    if (this.useSignalR) {
      this.signalRService.startConnection().subscribe(() => {
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
      });

      this.subscription = interval(1000).subscribe(() => this.removeElement());
    }
  }

  private removeElement() {
    const date = new Date();
    date.setSeconds(date.getSeconds() - 20);

    const data = this.displayChatMessages.filter(
      (t) => t.Date!.getTime() <= date.getTime()
    );
    data.forEach(() => {
      this.displayChatMessages.shift();
    });
  }

  ngOnDestroy() {
    this.subscription && this.subscription.unsubscribe();
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
      left: this.scrollContainer.scrollLeft + 9999,
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

  formatDate(date: Date): string {
    return `${date.toLocaleDateString(
      'ch-DE'
    )} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
  }

  //#endregion
}
