import { DomSanitizer } from '@angular/platform-browser';
import { ChatDto } from 'src/app/models/dtos/ChatDto';
import { SpecialMessgeEnum } from 'src/app/models/enums/SpecialMessgeEnum';

// TODO: can be done in the backend except bypassSecurityTrustHtml
// check for the other view (display on Stream)

export class ConvertMessage {
  public static convertMessage(sanitizer: DomSanitizer, chatMessage: ChatDto) {
    let badges = '';

    chatMessage.Badges.forEach((badge) => {
      console.log(badge);
      badges +=
        '<div class="tooltip bottom">' +
        '<img class="badges-image" src="' +
        badge[1] +
        '" /> <span class="tooltiptext"><p>' +
        badge[0] +
        '</p></span></div>';
    });

    badges += '';

    const finalName =
      '<span class="name-date">' +
      this.formatDate(chatMessage.Date!) +
      '</span>' +
      badges +
      '<span class="name-text" style="color: ' +
      chatMessage.ColorHex +
      ' !important;">' +
      chatMessage.UserName +
      '</span>';

    const isFirstMessage =
      chatMessage.SpecialMessage.find(
        (t) =>
          t === SpecialMessgeEnum.FirstStreamMessage ||
          t === SpecialMessgeEnum.FirstMessage
      ) != undefined
        ? chatMessage.SpecialMessage.find(
            (t) => t === SpecialMessgeEnum.FirstMessage
          ) != undefined
          ? 'background-color: blue'
          : 'background-color: green'
        : '';

    const finalReply =
      '<span class="message-reply">' + chatMessage.ReplayMessage + '</span>';

    let finalMessage = isFirstMessage;

    chatMessage.Message?.split(' ').forEach((element) => {
      const foundData = chatMessage.EmoteSetdata.emotes?.find(
        (m) => m.Name === element
      );
      if (foundData != null) {
        finalMessage +=
          '<div class="tooltip bottom">' +
          '<img class="badges-image" src="' +
          foundData.ImageUrl +
          '" /> <span class="tooltiptext"><span>' +
          foundData.Name +
          '</span></span></div>';
      } else {
        finalMessage = finalMessage + ' ' + element + '';
      }
    });
    finalMessage += '</span>';

    return {
      Id: chatMessage.Id,
      SaveName: sanitizer.bypassSecurityTrustHtml(finalName), // This needs to be done that style is correctly implemented,
      SaveReply: sanitizer.bypassSecurityTrustHtml(finalReply),
      SaveMessage: sanitizer.bypassSecurityTrustHtml(finalMessage), // This needs to be done that style is correctly implemented,
      Date: chatMessage.Date,
    };
  }

  private static formatDate(date: Date): string {
    return `${date.toLocaleDateString(
      'ch-DE'
    )} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
  }
}
