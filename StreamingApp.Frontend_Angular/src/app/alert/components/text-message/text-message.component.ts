import { Component, OnDestroy, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';

@Component({
  selector: 'app-text-message',
  standalone: true,
  imports: [],
  templateUrl: './text-message.component.html',
  styleUrl: './text-message.component.scss',
})
export class TextMessageComponent implements OnInit, OnDestroy {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}

  message: string | undefined;

  private subscription: Subscription | undefined;

  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.receiveOnscreenMessage().subscribe((message) => {
        this.message = message;

        this.subscription = interval(10 * 1000).subscribe(() =>
          this.removeElement()
        );
      });
    });
  }

  private removeElement() {
    this.message = undefined;

    this.subscription && this.subscription.unsubscribe();
  }

  ngOnDestroy(): void {
    this.subscription && this.subscription.unsubscribe();
  }
}
