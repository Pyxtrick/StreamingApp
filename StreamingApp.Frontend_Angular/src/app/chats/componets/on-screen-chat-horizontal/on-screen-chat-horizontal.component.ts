import {
  AfterViewInit,
  Component,
  ElementRef,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { MatListModule } from '@angular/material/list';
import { DisplayChat } from '../../models/DisplayChat';

@Component({
  selector: 'app-on-screen-chat',
  standalone: true,
  imports: [MatListModule],
  templateUrl: './on-screen-chat-horizontal.component.html',
  styleUrl: './on-screen-chat-horizontal.component.scss',
})
export class OnScreenChatHorizontalComponent implements AfterViewInit {
  @ViewChild('scrollframe') scrollFrame!: ElementRef;

  @ViewChildren('item') itemElements!: QueryList<any>;

  private scrollContainer: any;
  private isNearBottom = true;

  displayChatMessages: DisplayChat[] = [];

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
}
