import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogConfig,
} from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { Store } from '@ngrx/store';
import { Observable, map } from 'rxjs';
import {
  AuthEnum,
  CategoryEnum,
  CommandAndResponseDto,
} from 'src/api/api.service';
import { SettingsActions } from '../../state/action';
import { CommandEditComponent } from '../command-edit/command-edit.component';
import { settingsFeature } from './../../state/reducers';

@Component({
  selector: 'app-command',
  imports: [MatTableModule, MatButton, CommonModule],
  templateUrl: './command.component.html',
  styleUrl: './command.component.scss',
})
export class CommandComponent implements OnInit {
  private store = inject(Store);
  public commands$!: Observable<CommandAndResponseDto[]>;
  public commands!: CommandAndResponseDto[];

  readonly dialog = inject(MatDialog);

  displayedColumns: string[] = [
    'command',
    'response',
    'description',
    'active',
    'auth',
    'category',
    'edit',
  ];
  AuthEnum: any = AuthEnum;
  CategoryEnum: any = CategoryEnum;

  ngOnInit(): void {
    this.store.dispatch(SettingsActions.loadCommands());
    this.commands$ = this.store.select(settingsFeature.selectCommands);

    this.store
      .select(settingsFeature.selectCommands)
      .pipe(
        map((command) => {
          this.commands = command;
        })
      )
      .subscribe();
  }

  isEditVisible = false;

  showModal(command: CommandEditComponent) {
    console.log(command);
    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      command: command,
    };
    console.log('test' + dialogConfig);

    this.dialog.open(CommandEditComponent, dialogConfig);
  }

  hideModal() {
    this.isEditVisible = false;
  }
}

export class DialogDataExampleDialog {
  data = inject(MAT_DIALOG_DATA);
}
