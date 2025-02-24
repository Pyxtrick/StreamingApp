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

    let finalMessage = isFirstMessage + '<span class="message-text">';

    // TODO: Add More XSS Checks later
    // For any message staring < and ending with >
    if (new RegExp('(\\<).*([\\w-]).*(\\>)').test(chatMessage.message)) {
      chatMessage.message = chatMessage.message.replaceAll('<', '');
      chatMessage.message = chatMessage.message.replaceAll('>', '');
    }

    chatMessage.message?.split(' ').forEach((element) => {
      if (chatMessage.emotes != null) {
        const foundData = chatMessage.emotes.find((m) => m.name === element);

        if (foundData != null) {
          finalMessage +=
            '<div class="tooltip bottom">' +
            '<img class="badges-image" src="' +
            foundData.staticURL +
            '" /> <span class="tooltiptext"><span>' +
            foundData.name +
            '</span></span></div>';
        } else {
          finalMessage = finalMessage + ' ' + element + '';
        }
      } else {
        finalMessage = finalMessage + ' ' + element + '';
      }
    });
    finalMessage += '</span>';

    return {
      Id: chatMessage.id,
      SaveName: sanitizer.bypassSecurityTrustHtml(finalName), // This needs to be done that style is correctly implemented,
      SaveReply: sanitizer.bypassSecurityTrustHtml(finalReply),
      SaveMessage: sanitizer.bypassSecurityTrustHtml(finalMessage), // This needs to be done that style is correctly implemented,
      Addon: sanitizer.bypassSecurityTrustHtml(''),
      Date: new Date(chatMessage.date),
    };
  }

  private static formatDate(date: Date): string {
    return `${date.toLocaleDateString(
      'ch-DE'
    )} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
  }
}
