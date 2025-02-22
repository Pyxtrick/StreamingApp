import { KeyValue } from '@angular/common';
import { ChatUserEnum } from '../enums/AuthEnum';
import { ChatDisplay } from '../enums/ChatDisplay';
import { ChatOriginEnum } from '../enums/ChatOriginEnum';
import { EffectEnum } from '../enums/EffectEnum';
import { SpecialMessgeEnum } from '../enums/SpecialMessgeEnum';
import { EmoteSet } from '../external/EmoteSet';

export interface ChatDto {
  id: string;
  userName: string;
  colorHex: string;
  replayMessage: string;
  message: string;
  emoteReplacedMessage: string;
  emotes: EmoteSet[];
  badges: KeyValue<string, string>[];
  chatOrigin: ChatOriginEnum;
  chatDisplay: ChatDisplay;
  auth: ChatUserEnum[];
  specialMessage: SpecialMessgeEnum[];
  effect: EffectEnum;
  date: string;
}
