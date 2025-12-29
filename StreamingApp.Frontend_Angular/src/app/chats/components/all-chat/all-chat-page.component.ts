import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  Input,
  QueryList,
  ViewChild,
  ViewChildren,
  ViewEncapsulation,
} from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Store } from '@ngrx/store';
import { DisplayChat } from '../../models/DisplayChat';
import { ChatsActions } from '../../state/action';

@Component({
    selector: 'app-all-chat-page',
    templateUrl: './all-chat-page.component.html',
    styleUrls: ['./all-chat-page.component.scss'],
    imports: [MatListModule, MatTooltipModule, MatToolbarModule, MatIconModule],
    encapsulation: ViewEncapsulation.None
})
export class AllChatPageComponent implements AfterViewInit {
  @ViewChild('scrollframe') scrollFrame!: ElementRef;

  @ViewChildren('item') itemElements!: QueryList<any>;

  private store = inject(Store);

  private scrollContainer: any;
  private isNearBottom = true;

  @Input() displayChatMessages: DisplayChat[] = [];

  ngAfterViewInit(): void {
    this.scrollContainer = this.scrollFrame.nativeElement;
    this.itemElements.changes.subscribe((_) => this.onItemElementsChanged());
  }

  OnMessageClicked(messageId: string) {
    console.log('messageId before', messageId);
    this.store.dispatch(ChatsActions.sendHighlightMessage(messageId));
    console.log('messageId after', messageId);
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
  //#endregion
}
