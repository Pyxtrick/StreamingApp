import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { FormBuilder, FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { AuthEnum, CommandAndResponseDto } from 'src/api/api.service';

@Component({
  selector: 'app-command-edit',
  imports: [
    MatInputModule,
    MatSlideToggleModule,
    FormsModule,
    MatFormFieldModule,
    MatSelectModule,
  ],
  templateUrl: './command-edit.component.html',
  styleUrl: './command-edit.component.scss',
})
export class CommandEditComponent {
  command!: CommandAndResponseDto;
  auths = AuthEnum;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CommandAndResponseDto>,
    @Inject(MAT_DIALOG_DATA) data: any
  ) {
    this.command = data.command;
  }
  @Output() close = new EventEmitter<void>();

  closeEdit(): void {
    this.dialogRef.close();
  }
}
