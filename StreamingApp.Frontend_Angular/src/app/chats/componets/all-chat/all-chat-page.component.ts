import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  Pipe,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DomSanitizer } from '@angular/platform-browser';
import { ChatDto } from '../../models/chatDto';
//import { MasterDataActions } from '../../state/actions';
//import { masterdataFeature } from '../../state/reducers';

@Component({
  selector: 'app-all-chat-page',
  templateUrl: './all-chat-page.component.html',
  styleUrls: ['./all-chat-page.component.scss'],
  imports: [MatListModule, MatTooltipModule],
  standalone: true,
})
export class AllChatPageComponent implements OnInit, AfterViewInit {
  constructor(private _sanitizer: DomSanitizer) {}

  @ViewChild('scrollframe') scrollFrame!: ElementRef;

  @ViewChildren('item') itemElements!: QueryList<any>;

  private scrollContainer: any;
  private isNearBottom = true;

  @Pipe({
    name: 'sanitizeHtml',
  })
  chatMessages: ChatDto[] = [
    {
      Id: '1',
      UserName: 'name',
      ColorHex: '#FFFFF',
      ReplayMessage: 'NO YOU',
      Message: 'hello test kappa kappa',
      EmoteReplacedMessage: 'hello test kappa',
      EmoteSet: [
        {
          Name: 'kappa',
          Url: 'assets/3x.webp',
        },
      ],
      Date: new Date(),
    },
    {
      Id: '1',
      UserName: 'Pyxtrick',
      ColorHex: '#004daa',
      ReplayMessage: '',
      Message: 'hello test kappa',
      EmoteReplacedMessage: 'hello test kappa',
      Date: new Date(),
    },
    {
      Id: '1',
      UserName: 'SumimascuseMe',
      ColorHex: 'rgb(23, 186, 34)',
      ReplayMessage: '',
      Message: 'hello test kappa',
      EmoteReplacedMessage: 'hello test kappa',
      Date: new Date(),
    },
    {
      Id: '1',
      UserName: 'traxl',
      ColorHex: 'rgb(0, 255, 127)',
      ReplayMessage: '',
      Message: 'hello test kappa',
      EmoteReplacedMessage: 'hello test kappa',
      Date: new Date(),
    },
    {
      Id: '1',
      UserName: 'BeefEater1990',
      ColorHex: 'rgb(81, 207, 238)',
      ReplayMessage: '',
      Message:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut' +
        'labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco kappa' +
        'laboris kappa nisi ut aliquip ex ea commodo consequat. kappa Duis aute irure dolor in reprehenderit in' +
        'voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat' +
        'non proident, sunt in culpa kappa qui officia deserunt mollit anim id est',
      EmoteReplacedMessage: 'hello test kappa',
      Date: new Date(),
      EmoteSet: [
        {
          Name: 'kappa',
          Url: 'assets/3x.webp',
        },
      ],
    },
  ];

  //private store = inject(Store);

  ngOnInit(): void {
    /**
     this.store.select(selectUserName);
     this.store.dispatch(MasterDataActions.loadTechnologies());
      this.techData$ = this.store
      .select(masterdataFeature.selectTechnologies)
      .pipe(map((e) => e));
      
     */

    this.convertData();
  }

  ngAfterViewInit(): void {
    this.scrollContainer = this.scrollFrame.nativeElement;
    this.itemElements.changes.subscribe((_) => this.onItemElementsChanged());
  }

  //#region Auto Scroll to Bottom when at bottom
  private onItemElementsChanged(): void {
    if (this.isNearBottom) {
      this.scrollToBottom();
    }
  }

  private scrollToBottom(): void {
    this.scrollContainer.scroll({
      top: this.scrollContainer.scrollHeight,
      left: 0,
      behavior: 'smooth',
    });
  }

  private isUserNearBottom(): boolean {
    const threshold = 150;
    const position =
      this.scrollContainer.scrollTop + this.scrollContainer.offsetHeight;
    const height = this.scrollContainer.scrollHeight;
    return position > height - threshold;
  }

  scrolled(event: any): void {
    this.isNearBottom = this.isUserNearBottom();
  }

  formatDate(date: Date): string {
    return `${date.toLocaleDateString(
      'ch-DE'
    )} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
  }

  //#endregion

  // TODO: can be done in the backend except bypassSecurityTrustHtml
  // check for the other view (display on Stream)
  convertData() {
    this.chatMessages.forEach((chatMessage) => {
      const finalName =
        '<p style="font-size: smaller; margin-bottom: 0px !important; display: inline-block; margin-right: 5px;">' +
        this.formatDate(chatMessage.Date!) +
        '</p><p style="font-size: large; font-weight: bold; color: ' +
        chatMessage.ColorHex +
        ' !important; margin-bottom: 0px !important; display: inline-block;">' +
        chatMessage.UserName +
        '</p>';

      let finalMessage =
        '<p style="font-size: smaller; font-style: oblique; color: white; margin-bottom: 0px;">' +
        chatMessage.ReplayMessage +
        '</p><p style="color: white; margin-bottom: 0px; word-wrap: break-word; white-space: pre-wrap;" >';

      chatMessage.Message?.split(' ').forEach((element) => {
        const foundData = chatMessage.EmoteSet?.find((m) => m.Name === element);
        if (foundData != null) {
          finalMessage +=
            ' ' + '<img src="' + foundData.Url + '" height="20" />';
        } else {
          finalMessage = finalMessage + ' ' + element;
        }
      });
      finalMessage += '</p>';

      chatMessage.SaveName = this._sanitizer.bypassSecurityTrustHtml(finalName); // This needs to be done that style is correctly implemented

      chatMessage.SaveMessage =
        this._sanitizer.bypassSecurityTrustHtml(finalMessage); // This needs to be done that style is correctly implemented
    });
  }

  addData() {
    this.chatMessages.push({
      Id: '1',
      UserName: 'name',
      ColorHex: '#FFFFF',
      ReplayMessage: '',
      Message: 'hello test kappa',
      EmoteReplacedMessage: 'hello test kappa',
      EmoteSet: [
        {
          Name: 'kappa',
          Url: 'assets/3x.webp',
        },
      ],
      Date: new Date(),
    });
    if (this.chatMessages.length >= 100) {
      this.chatMessages.shift();
    }
    this.convertData();
  }

  removeData() {
    this.chatMessages.shift();
  }
}
