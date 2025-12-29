import { registerLocaleData } from '@angular/common';
import ch from '@angular/common/locales/de-CH';
import { Component, LOCALE_ID } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
registerLocaleData(ch);

@Component({
    selector: 'app-root',
    imports: [
    RouterModule,
    TranslateModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatListModule
],
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    providers: [{ provide: LOCALE_ID, useValue: 'de-CH' }]
})
export class AppComponent {
  title = 'Streaming.Web';
}
