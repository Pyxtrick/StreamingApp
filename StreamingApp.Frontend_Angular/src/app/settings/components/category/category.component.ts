import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { Store } from '@ngrx/store';
import { map, Observable } from 'rxjs';
import { GameCategoryEnum, GameInfoDto } from 'src/api/api.service';
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
  public gameInfos$!: Observable<GameInfoDto[]>;
  public gameInfos!: GameInfoDto[];
  displayedColumns: string[] = ['game', 'message', 'gameCategory'];

  GameCategoryEnum: any = GameCategoryEnum;

  ngOnInit(): void {
    this.store.dispatch(SettingsActions.loadGameInfos());
    this.gameInfos$ = this.store.select(settingsFeature.selectGameInfos);

    this.store
      .select(settingsFeature.selectGameInfos)
      .pipe(
        map((gameInfos) => {
          this.gameInfos = gameInfos;
        })
      )
      .subscribe();
  }
}
