import { ChatDisplay } from '../enums/ChatDisplay';
import { ChatOriginEnum } from '../enums/ChatOriginEnum';
import { ChatUserEnum } from '../enums/ChatUserEnum';
import { EffectEnum } from '../enums/EffectEnum';
import { SpecialMessgeEnum } from '../enums/SpecialMessgeEnum';
import { EmoteSet } from '../external/EmoteSet';

export interface ChatDto {
  Id: string;
  UserName: string;
  ColorHex: string;
  ReplayMessage: string;
  Message: string;
  EmoteReplacedMessage: string;
  EmoteSetdata: EmoteSet;
  Badges: string[][];
  ChatOrigin: ChatOriginEnum;
  ChatDisplay: ChatDisplay;
  Auth: ChatUserEnum[];
  SpecialMessage: SpecialMessgeEnum[];
  Effect: EffectEnum;
  Date: Date;
}
