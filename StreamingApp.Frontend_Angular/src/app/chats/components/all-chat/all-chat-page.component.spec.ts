import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllChatPageComponent } from './all-chat-page.component';

describe('AllChatPageComponent', () => {
  let component: AllChatPageComponent;
  let fixture: ComponentFixture<AllChatPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllChatPageComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AllChatPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
