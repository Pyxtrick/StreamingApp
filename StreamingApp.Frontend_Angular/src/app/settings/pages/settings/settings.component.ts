import { Component } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { CommandComponent } from '../../components/command/command.component';
import { StreamComponent } from '../../components/stream/stream.component';

@Component({
  selector: 'app-setting',
  standalone: true,
  imports: [MatTabsModule, CommandComponent, StreamComponent],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss',
})
export class SettingsComponent {}
