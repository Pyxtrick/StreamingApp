import { BannedTargetEnum } from '../enums/BannedTargetEnum.js';

export interface BannedUserDto {
  id: string;
  userName: string;
  lastName: string;
  reson: string;
  targetEnum: BannedTargetEnum;
  date: string;
}
