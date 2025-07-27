import {
  Component,
  Input,
  OnDestroy,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';

@Component({
  selector: 'app-text-message',
  standalone: true,
  imports: [],
  templateUrl: './text-message.component.html',
  styleUrl: './text-message.component.scss',
  encapsulation: ViewEncapsulation.None,
})
export class TextMessageComponent implements OnInit, OnDestroy {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}

  @Input() message: SafeHtml | undefined;
  @Input() useSignalR = true;

  private subscription: Subscription | undefined;

  ngOnInit(): void {
    if (this.useSignalR) {
      this.signalRService.startConnection().subscribe(() => {
        this.signalRService.receiveOnscreenMessage().subscribe((message) => {
          this.message = this._sanitizer.bypassSecurityTrustHtml(message.html);

          console.log('ad alert Called', message.duration);

          this.subscription = interval(message.duration * 1000).subscribe(() =>
            this.removeElement()
          );
        });
      });
    }
  }

  private removeElement() {
    this.message = undefined;

    this.subscription && this.subscription.unsubscribe();
  }

  ngOnDestroy(): void {
    this.subscription && this.subscription.unsubscribe();
  }
}
