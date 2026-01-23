import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';
import { ConvertMessage } from 'src/app/chats/logic/convertMessage';
import { DisplayChat } from 'src/app/chats/models/DisplayChat';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';
import { FullscreenComponent } from '../../components/fullscreen/fullscreen.component';
import { TextMessageComponent } from '../../components/text-message/text-message.component';
import { FullScreenAlert } from '../../models/FullScreenAlert';
import { HighlightMessageComponent } from './../../components/highlight-message/highlight-message.component';

@Component({
  selector: 'app-combinded-alerts',
  imports: [
    FullscreenComponent,
    HighlightMessageComponent,
    TextMessageComponent,
  ],
  templateUrl: './combinded-alerts.component.html',
  styleUrl: './combinded-alerts.component.scss',
  encapsulation: ViewEncapsulation.None,
})
export class CombindedAlertsComponent implements OnInit, OnDestroy {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}

  public alertList: FullScreenAlert[] = [];
  private fullscreenSubscription: Subscription | undefined;

  public displayChatMessage: DisplayChat | undefined;
  private highlightSubscription: Subscription | undefined;

  public message: SafeHtml | undefined;
  private textSubscription: Subscription | undefined;

  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService
        .receiveAlertMessage('ReceiveAlert')
        .subscribe((message) => {
          this.alertList.push({
            alert: message,
            html: this._sanitizer.bypassSecurityTrustHtml(message.html),
            date: new Date(Date.now() + message.duration),
          });
          this.fullscreenSubscription = interval(
            (message.duration - 0.5) * 1000
          ).subscribe(() => this.removeFullscreenElement());
        });
      this.signalRService.receiveHighlightMessage().subscribe((message) => {
        this.displayChatMessage = ConvertMessage.convertMessage(
          this._sanitizer,
          message,
          true
        );
        this.highlightSubscription = interval(10 * 1000).subscribe(() =>
          this.removeHighlightElement()
        );
      });
      this.signalRService.receiveOnscreenMessage().subscribe((message) => {
        this.message = this._sanitizer.bypassSecurityTrustHtml(message.html);

        console.log('ad alert Called', message.duration);

        this.textSubscription = interval(message.duration * 1000).subscribe(
          () => this.removeTextElement()
        );
      });
    });
  }

  private removeFullscreenElement() {
    this.alertList = [];

    this.fullscreenSubscription && this.fullscreenSubscription.unsubscribe();
  }

  private removeHighlightElement() {
    this.displayChatMessage = undefined;

    this.highlightSubscription && this.highlightSubscription.unsubscribe();
  }

  private removeTextElement() {
    this.message = undefined;

    this.textSubscription && this.textSubscription.unsubscribe();
  }

  ngOnDestroy(): void {
    this.fullscreenSubscription && this.fullscreenSubscription.unsubscribe();
    this.highlightSubscription && this.highlightSubscription.unsubscribe();
    this.textSubscription && this.textSubscription.unsubscribe();
  }
}
