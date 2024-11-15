import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OnScreenChatHorizontalComponent } from './on-screen-chat-horizontal.component';

describe('OnScreenChatHorizontalComponent', () => {
  let component: OnScreenChatHorizontalComponent;
  let fixture: ComponentFixture<OnScreenChatHorizontalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OnScreenChatHorizontalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(OnScreenChatHorizontalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
