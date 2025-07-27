import {
  Component,
  Input,
  OnDestroy,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';
import { ConvertMessage } from 'src/app/chats/logic/convertMessage';
import { DisplayChat } from 'src/app/chats/models/DisplayChat';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';

@Component({
  selector: 'app-highlight-message',
  imports: [],
  templateUrl: './highlight-message.component.html',
  styleUrl: './highlight-message.component.scss',
  standalone: true,
  encapsulation: ViewEncapsulation.None,
})
export class HighlightMessageComponent implements OnInit, OnDestroy {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}

  @Input() displayChatMessage: DisplayChat | undefined;
  @Input() useSignalR = true;

  private subscription: Subscription | undefined;

  ngOnInit(): void {
    if (this.useSignalR) {
      this.signalRService.startConnection().subscribe(() => {
        this.signalRService.receiveHighlightMessage().subscribe((message) => {
          this.displayChatMessage = ConvertMessage.convertMessage(
            this._sanitizer,
            message,
            true
          );

          this.subscription = interval(10 * 1000).subscribe(() =>
            this.removeElement()
          );
        });
      });
    }
  }

  private removeElement() {
    this.displayChatMessage = undefined;

    this.subscription && this.subscription.unsubscribe();
  }

  ngOnDestroy(): void {
    this.subscription && this.subscription.unsubscribe();
  }
}
