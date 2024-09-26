import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OnScreenChatComponent } from './on-screen-chat.component';

describe('OnScreenChatComponent', () => {
  let component: OnScreenChatComponent;
  let fixture: ComponentFixture<OnScreenChatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OnScreenChatComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OnScreenChatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
