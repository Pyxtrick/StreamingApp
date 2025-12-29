import { Component } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { CategoryComponent } from '../../components/category/category.component';
import { CommandComponent } from '../../components/command/command.component';
import { SpecialWordsComponent } from '../../components/special-words/special-words.component';
import { StreamComponent } from '../../components/stream/stream.component';
import { UsersComponent } from '../../components/users/users.component';

@Component({
  selector: 'app-setting',
  imports: [
    MatTabsModule,
    CommandComponent,
    StreamComponent,
    CategoryComponent,
    SpecialWordsComponent,
    UsersComponent,
  ],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss',
})
export class SettingsComponent {}
