export interface AlertDto {
  // Volume at with the audio is played at 100 is default
  volume: number;
  image: Blob;
  sound: Blob;
  video: Blob;
  html: string;
  videoLeght: number;
  isMute: boolean;
  duration: number;
  isSameTimet: boolean;
}
