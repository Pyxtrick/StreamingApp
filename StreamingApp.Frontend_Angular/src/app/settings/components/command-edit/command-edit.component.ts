import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { FormBuilder, FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {
  AuthEnum,
  CategoryEnum,
  CommandAndResponseDto,
} from 'src/api/api.service';

@Component({
  selector: 'app-command-edit',
  imports: [
    MatInputModule,
    MatSlideToggleModule,
    FormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatGridListModule,
  ],
  templateUrl: './command-edit.component.html',
  styleUrl: './command-edit.component.scss',
})
export class CommandEditComponent {
  command!: CommandAndResponseDto;
  authList = Object.keys(AuthEnum).filter((x) => isNaN(parseInt(x)));
  authEnum = AuthEnum;

  categoryList = Object.keys(CategoryEnum).filter((x) => isNaN(parseInt(x)));
  categoryEnum = CategoryEnum;

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
