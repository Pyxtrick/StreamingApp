import { Component, Input, OnInit } from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { DomSanitizer } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';
import { FullScreenAlert } from 'src/app/alert/models/FullScreenAlert';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';

@Component({
  selector: 'app-fullscreen',
  imports: [MatListModule],
  templateUrl: './fullscreen.component.html',
  styleUrl: './fullscreen.component.scss',
})
export class FullscreenComponent implements OnInit {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}

  @Input() alertList: FullScreenAlert[] = [];
  @Input() useSignalR = true;

  private subscription: Subscription | undefined;

  ngOnInit(): void {
    if (this.useSignalR) {
      this.signalRService.startConnection().subscribe(() => {
        this.signalRService
          .receiveAlertMessage('ReceiveAlert')
          .subscribe((message) => {
            this.alertList.push({
              alert: message,
              html: this._sanitizer.bypassSecurityTrustHtml(message.html),
              date: new Date(Date.now() + message.duration),
            });
            this.subscription = interval(message.duration * 1000).subscribe(
              () => this.removeElement()
            );
          });
      });
    }
  }

  /**private removeElement() {
    this.htmldata = undefined;

    this.subscription && this.subscription.unsubscribe();
  }**/

  private removeElement() {
    const date = new Date();
    date.setSeconds(date.getSeconds() - 20);

    this.alertList = this.alertList.filter(
      (t) => t.date!.getTime() >= date.getTime()
    );
  }

  ngOnDestroy() {
    this.subscription && this.subscription.unsubscribe();
  }

  private bin2String(array: []) {
    let result = '';
    for (let i = 0; i < array.length; i++) {
      result += String.fromCharCode(parseInt(array[i], 2));
    }
    return result;
  }
}
