import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-fullscreen',
  standalone: true,
  imports: [],
  templateUrl: './fullscreen.component.html',
  styleUrl: './fullscreen.component.scss',
})
export class FullscreenComponent implements OnInit {
  constructor(
    private sanitizer: DomSanitizer,
    private httpClient: HttpClient
  ) {}

  public htmldata: SafeHtml | undefined;

  ngOnInit(): void {
    this.httpClient
      .get('assets/pages/ScrolText.html', { responseType: 'text' })
      .subscribe((data) => {
        const newData = data.replace(
          '[breaking-news-message]',
          'Pyxtrick has been murderd'
        );

        this.htmldata = this.sanitizer.bypassSecurityTrustHtml(newData);
      });
  }

  public htmltext = '';
}
