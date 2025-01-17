import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  Pipe,
  QueryList,
  ViewChild,
  ViewChildren,
  ViewEncapsulation,
} from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DomSanitizer } from '@angular/platform-browser';
import { ChatDto } from 'src/app/models/dtos/ChatDto';
import { ChatDisplay } from 'src/app/models/enums/ChatDisplay';
import { ChatOriginEnum } from 'src/app/models/enums/ChatOriginEnum';
import { ChatUserEnum } from 'src/app/models/enums/ChatUserEnum';
import { EffectEnum } from 'src/app/models/enums/EffectEnum';
import { SpecialMessgeEnum } from 'src/app/models/enums/SpecialMessgeEnum';
import { AppSignalRService } from 'src/app/services/app-signalr.services';
import { ConvertMessage } from '../../logic/convertMessage';
import { DisplayChat } from '../../models/DisplayChat';

@Component({
  selector: 'app-all-chat-page',
  templateUrl: './all-chat-page.component.html',
  styleUrls: ['./all-chat-page.component.scss'],
  imports: [MatListModule, MatTooltipModule],
  standalone: true,
  encapsulation: ViewEncapsulation.None,
})
export class AllChatPageComponent implements OnInit, AfterViewInit {
  constructor(
    private _sanitizer: DomSanitizer,
    private signalRService: AppSignalRService
  ) {}

  @ViewChild('scrollframe') scrollFrame!: ElementRef;

  @ViewChildren('item') itemElements!: QueryList<any>;

  private scrollContainer: any;
  private isNearBottom = true;

  @Pipe({
    name: 'sanitizeHtml',
  })
  //TODO: Change
  chatMessages: ChatDto[] = [
    {
      Id: '1',
      UserName: 'name',
      ColorHex: '#FFFFF',
      ReplayMessage: 'NO YOU',
      Message:
        'hello test kappa kappa kldsafkl ajdshfk ajhdsfkjahsdfkljahsdf kljashdflkj asdh fkljasdhfkljasdhfklj ahsd flk jahsd flk jhasd lkfj hasdlkfjh  hello test dlsakfjöla kappa kappa kldsafkl ajdshfk ajhdsfkj ahsdfkljahsdf kljashdflkj asdh fkljasdh fkljasd hfklj ahsd flk jahsd flk jhasd lkfj hasdlkfjh',
      EmoteReplacedMessage: 'hello test kappa',
      EmoteSetdata: {
        emotes: [
          {
            Id: '1',
            Name: 'kappa',
            StartIndex: 1,
            EndIndex: 2,
            ImageUrl: 'assets/3x.webp',
          },
        ],
        rawEmoteSetString: '',
      },
      Badges: [
        ['kappa', 'assets/3x.webp'],
        ['2,1', 'assets/3x.webp'],
      ], // Check if correct
      ChatOrigin: ChatOriginEnum.Twtich,
      ChatDisplay: ChatDisplay.allChat,
      Auth: [ChatUserEnum.Streamer],
      SpecialMessage: [SpecialMessgeEnum.Undefined],
      Effect: EffectEnum.none,
      Date: new Date(),
    },
  ];

  displayChatMessages: DisplayChat[] = [];

  ngOnInit(): void {
    this.convertAllData();
  }

  sendMessage(message: string): void {
    this.signalRService.sendMessage(message);
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

  convertAllData() {
    this.chatMessages.forEach((chatMessage) => {
      this.displayChatMessages.push(
        ConvertMessage.convertMessage(this._sanitizer, chatMessage)
      );
    });
  }

  //TODO: Remove and check how it comes from the backend
  addData() {
    this.chatMessages.push({
      Id: '1',
      UserName: 'name',
      ColorHex: '#FFFFF',
      ReplayMessage: 'NO YOU',
      Message: 'hello test kappa kappa',
      EmoteReplacedMessage: 'hello test kappa',
      EmoteSetdata: {
        emotes: [
          {
            Id: '1',
            Name: 'kappa',
            StartIndex: 1,
            EndIndex: 2,
            ImageUrl: 'assets/3x.webp',
          },
        ],
        rawEmoteSetString: '',
      },
      Badges: [],
      ChatOrigin: ChatOriginEnum.Twtich,
      ChatDisplay: ChatDisplay.allChat,
      Auth: [ChatUserEnum.Streamer],
      SpecialMessage: [SpecialMessgeEnum.Undefined],
      Effect: EffectEnum.none,
      Date: new Date(),
    });
    if (this.chatMessages.length >= 100) {
      this.chatMessages.shift();
    }
    this.convertAllData();
  }

  //TODO: Remove
  removeData() {
    this.chatMessages.shift();
  }
}
