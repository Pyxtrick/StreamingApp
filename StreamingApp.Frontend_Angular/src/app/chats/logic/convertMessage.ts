import { DomSanitizer } from '@angular/platform-browser';
import { ChatDto } from 'src/app/models/dtos/ChatDto';
import { SpecialMessgeEnum } from 'src/app/models/enums/SpecialMessgeEnum';

// TODO: can be done in the backend except bypassSecurityTrustHtml
// check for the other view (display on Stream)

export class ConvertMessage {
  public static convertMessage(
    sanitizer: DomSanitizer,
    chatMessage: ChatDto,
    isOnScreen: boolean
  ) {
    let badges = '';

    chatMessage.badges.forEach((badge) => {
      badges +=
        '<div class="tooltip bottom">' +
        '<img class="badges-image" src="' +
        badge.value +
        '" /> <span class="tooltiptext"><p>' +
        badge.key +
        '</p></span></div>';
    });

    badges += '';

    let finalName = '';

    if (isOnScreen) {
      finalName =
        badges +
        '<span class="name-text" style="color: ' +
        chatMessage.colorHex +
        ' !important;">' +
        chatMessage.userName +
        '</span>';
    } else {
      finalName =
        '<span class="name-date" style="color: white">' +
        this.formatDate(new Date(Date.parse(chatMessage.date))) +
        '</span>' +
        badges +
        '<span class="name-text" style="color: ' +
        chatMessage.colorHex +
        ' !important;">' +
        chatMessage.userName +
        '</span>';
    }

    const isFirstMessage =
      chatMessage.specialMessage.find(
        (t) =>
          t === SpecialMessgeEnum.FirstStreamMessage ||
          t === SpecialMessgeEnum.FirstMessage
      ) != undefined
        ? chatMessage.specialMessage.find(
            (t) => t === SpecialMessgeEnum.FirstMessage
          ) != undefined
          ? 'background-color: blue'
          : 'background-color: green'
        : '';

    let finalReply = '';
    if (chatMessage.replayMessage != null) {
      finalReply =
        '<span class="message-reply">' + chatMessage.replayMessage + '</span>';
    }

    let finalMessage = isFirstMessage + '<span>';

    chatMessage.message?.split(' ').forEach((element) => {
      if (chatMessage.emoteSetdata != null) {
        const foundData = chatMessage.emoteSetdata.emotes?.find(
          (m) => m.Name === element
        );
        if (foundData != null) {
          finalMessage +=
            '<div class="tooltip bottom">' +
            '<img class="badges-image" src="' +
            foundData.StaticURL +
            '" /> <span class="tooltiptext"><span>' +
            foundData.Name +
            '</span></span></div>';
        } else {
          finalMessage = finalMessage + ' ' + element + '';
        }
      } else {
        finalMessage = finalMessage + ' ' + element + '';
      }
    });
    finalMessage += '</span>';

    console.log(finalName);

    return {
      Id: chatMessage.id,
      SaveName: sanitizer.bypassSecurityTrustHtml(finalName), // This needs to be done that style is correctly implemented,
      SaveReply: sanitizer.bypassSecurityTrustHtml(finalReply),
      SaveMessage: sanitizer.bypassSecurityTrustHtml(finalMessage), // This needs to be done that style is correctly implemented,
      Date: new Date(chatMessage.date),
    };
  }

  private static formatDate(date: Date): string {
    return `${date.toLocaleDateString(
      'ch-DE'
    )} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
  }
}
