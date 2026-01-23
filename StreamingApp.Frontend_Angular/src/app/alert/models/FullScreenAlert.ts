import { SafeHtml } from '@angular/platform-browser';
import { AlertDto } from '../../models/dtos/AlertDto';

export interface FullScreenAlert {
  // Volume at with the audio is played at 100 is default
  alert: AlertDto;
  html: SafeHtml;
  date: Date;
}
