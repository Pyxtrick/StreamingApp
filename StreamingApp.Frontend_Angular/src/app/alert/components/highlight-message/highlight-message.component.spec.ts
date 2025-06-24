import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HighlightMessageComponent } from './highlight-message.component';

describe('HighlightMessageComponent', () => {
  let component: HighlightMessageComponent;
  let fixture: ComponentFixture<HighlightMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HighlightMessageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HighlightMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
