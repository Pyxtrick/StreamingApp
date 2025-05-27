import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpecialWordsComponent } from './special-words.component';

describe('SpecialWordsComponent', () => {
  let component: SpecialWordsComponent;
  let fixture: ComponentFixture<SpecialWordsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SpecialWordsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SpecialWordsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
