import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { Store } from '@ngrx/store';
import { Observable, map } from 'rxjs';
import {
  AuthEnum,
  CategoryEnum,
  CommandAndResponseDto,
} from 'src/api/api.service';
import { SettingsActions } from '../../state/action';
import { settingsFeature } from './../../state/reducers';

@Component({
  selector: 'app-command',
  standalone: true,
  imports: [CommonModule, MatTableModule],
  templateUrl: './command.component.html',
  styleUrl: './command.component.scss',
})
export class CommandComponent implements OnInit {
  private store = inject(Store);
  public commands$!: Observable<CommandAndResponseDto[]>;
  public commands!: CommandAndResponseDto[];
  displayedColumns: string[] = [
    'command',
    'response',
    'description',
    'active',
    'auth',
    'category',
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
}
