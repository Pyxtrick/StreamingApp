import { Component, OnInit } from '@angular/core';
import { MatListModule } from '@angular/material/list';

import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { AppSignalRService } from 'src/app/services/chat-signalr.services';

@Component({
  selector: 'app-fullscreen',
  standalone: true,
  imports: [MatListModule],
  templateUrl: './fullscreen.component.html',
  styleUrl: './fullscreen.component.scss',
})
export class FullscreenComponent implements OnInit {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}

  public htmldata: SafeHtml | undefined;

  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService
        .receiveAlertMessage('ReceiveAlert')
        .subscribe((message) => {
          this.htmldata = this._sanitizer.bypassSecurityTrustHtml(message.html);
        });
    });
  }

  private bin2String(array: []) {
    let result = '';
    for (let i = 0; i < array.length; i++) {
      result += String.fromCharCode(parseInt(array[i], 2));
    }
    return result;
  }
}
