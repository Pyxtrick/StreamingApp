import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { Store } from '@ngrx/store';
import { map, Observable } from 'rxjs';
import { StreamDto } from 'src/api/api.service';
import { SettingsActions } from '../../state/action';
import { settingsFeature } from '../../state/reducers';

@Component({
  selector: 'app-stream',
  standalone: true,
  imports: [CommonModule, MatTableModule],
  templateUrl: './stream.component.html',
  styleUrl: './stream.component.scss',
})
export class StreamComponent implements OnInit {
  private store = inject(Store);
  public streams$!: Observable<StreamDto[]>;
  public streams!: StreamDto[];
  displayedColumns: string[] = ['streamTitle', 'streamStart', 'streamEnd'];

  ngOnInit(): void {
    this.store.dispatch(SettingsActions.loadStreams());
    this.streams$ = this.store.select(settingsFeature.selectStreams);

    this.store
      .select(settingsFeature.selectStreams)
      .pipe(
        map((stream) => {
          this.streams = stream;
        })
      )
      .subscribe();
  }
}
