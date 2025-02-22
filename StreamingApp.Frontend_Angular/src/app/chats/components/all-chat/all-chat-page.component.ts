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
import { ChatDto } from 'src/app/models/dtos/ChatDto';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';
import { ConvertMessage } from '../../logic/convertMessage';
import { DisplayChat } from '../../models/DisplayChat';

@Component({
  selector: 'app-all-chat-page',
  standalone: true,
  templateUrl: './all-chat-page.component.html',
  styleUrls: ['./all-chat-page.component.scss'],
  imports: [MatListModule, MatTooltipModule],
  encapsulation: ViewEncapsulation.None,
})
export class AllChatPageComponent implements OnInit, AfterViewInit {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}

  @ViewChild('scrollframe') scrollFrame!: ElementRef;

  @ViewChildren('item') itemElements!: QueryList<any>;

  @Input() signalrRecive = '';

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
        .receiveChatMessage(this.signalrRecive)
        .subscribe((message) => {
          if (this.displayChatMessages.length >= 100) {
            this.displayChatMessages.shift();
          }
          this.convertMessageData(message);
        });
    });
  }

  convertMessageData(chatMesssage: ChatDto) {
    this.displayChatMessages.push(
      ConvertMessage.convertMessage(this._sanitizer, chatMesssage, false)
    );
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
