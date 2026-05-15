import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TwitchControllComponent } from './twitch-controll.component';

describe('TwitchControllComponent', () => {
  let component: TwitchControllComponent;
  let fixture: ComponentFixture<TwitchControllComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TwitchControllComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(TwitchControllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
