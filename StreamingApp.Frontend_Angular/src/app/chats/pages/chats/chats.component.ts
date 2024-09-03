import { Component } from '@angular/core';
import { AllChatPageComponent } from '../../componets/all-chat/all-chat-page.component';

@Component({
  selector: 'app-chats',
  standalone: true,
  imports: [AllChatPageComponent],
  templateUrl: './chats.component.html',
  styleUrl: './chats.component.scss',
})
export class ChatsComponent {}
