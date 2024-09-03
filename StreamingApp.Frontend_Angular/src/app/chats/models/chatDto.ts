import { SafeHtml } from '@angular/platform-browser';
import { EmoteSet } from './EmoteSet';

export class ChatDto {
  Id?: string;
  UserName?: string;
  ColorHex?: string;
  ReplayMessage?: string;
  Message?: string;
  EmoteReplacedMessage?: string;
  SaveName?: SafeHtml;
  SaveReply?: SafeHtml;
  SaveMessage?: SafeHtml;
  EmoteSet?: EmoteSet[];

  Date?: Date;
}
