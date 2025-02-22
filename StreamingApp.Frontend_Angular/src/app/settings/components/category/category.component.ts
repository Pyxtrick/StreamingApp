import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { Store } from '@ngrx/store';
import { map, Observable } from 'rxjs';
import { SettingsActions } from '../../state/action';
import { settingsFeature } from '../../state/reducers';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CommonModule, MatTableModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss',
})
export class CategoryComponent implements OnInit {
  private store = inject(Store);
  public gameInfos$!: Observable<GameInfo[]>;
  public gameInfos!: GameInfo[];
  displayedColumns: string[] = [
    'game',
    'message',
    'startDate',
    'endDate',
    'gameCategory',
  ];

  GameCategoryEnum: any = GameCategoryEnum;

  ngOnInit(): void {
    this.store.dispatch(SettingsActions.loadGameInfos());
    this.gameInfos$ = this.store.select(settingsFeature.selectGameInfo);

    this.store
      .select(settingsFeature.selectGameInfo)
      .pipe(
        map((command) => {
          this.gameInfos = command;
        })
      )
      .subscribe();
  }
}
