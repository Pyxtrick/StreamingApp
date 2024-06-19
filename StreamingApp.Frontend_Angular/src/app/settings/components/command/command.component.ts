import { Component, inject } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { CommandAndResponseDto } from 'src/api/api.service';
import { SettingsActions } from '../../state/action';
import { settingsFeature } from '../../state/reducers';

@Component({
  selector: 'app-command',
  standalone: true,
  imports: [],
  templateUrl: './command.component.html',
  styleUrl: './command.component.scss',
})
export class CommandComponent {
  private store = inject(Store);
  private commands$!: Observable<CommandAndResponseDto[]>;

  ngOnInit(): void {
    this.store.dispatch(SettingsActions.loadCommands());
    this.commands$ = this.store.select(settingsFeature.selectCommands);
  }
}
