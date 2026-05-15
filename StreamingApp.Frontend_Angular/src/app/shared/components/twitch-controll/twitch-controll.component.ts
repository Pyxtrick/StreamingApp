import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-twitch-controll',
  imports: [MatInputModule, FormsModule],
  templateUrl: './twitch-controll.component.html',
  styleUrl: './twitch-controll.component.scss',
})
export class TwitchControllComponent {
  twitch!: TwitchDto;

  constructor(
    private dialogRef: MatDialogRef<null>,
    @Inject(MAT_DIALOG_DATA) data: any
  ) {
    this.twitch = data.command;
  }

  closeEdit(): void {
    console.log(this.twitch);
    this.dialogRef.close();
  }
}

// Remplace interface from Backend / api.service
export interface TwitchDto {
  title: string;
  notification: string;
  category: string;
}
