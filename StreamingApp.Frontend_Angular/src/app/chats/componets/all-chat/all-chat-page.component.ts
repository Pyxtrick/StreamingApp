import { Component, OnInit } from '@angular/core';
import { MatListModule } from '@angular/material/list';
//import { MasterDataActions } from '../../state/actions';
//import { masterdataFeature } from '../../state/reducers';

@Component({
  selector: 'app-all-chat-page',
  templateUrl: './all-chat-page.component.html',
  styleUrls: ['./all-chat-page.component.scss'],
  imports: [MatListModule],
  standalone: true,
})
export class AllChatPageComponent implements OnInit {
  typesOfShoes: string[] = [
    'Boots',
    'Clogs',
    'Loafers',
    'Moccasins',
    'Sneakers',
  ];

  //private store = inject(Store);

  ngOnInit(): void {
    console.log('test');

    /**
     this.store.select(selectUserName);
     this.store.dispatch(MasterDataActions.loadTechnologies());
      this.techData$ = this.store
      .select(masterdataFeature.selectTechnologies)
      .pipe(map((e) => e));
      
     */
  }
}
