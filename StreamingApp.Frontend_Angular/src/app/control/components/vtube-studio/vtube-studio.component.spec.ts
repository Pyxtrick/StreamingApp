import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VtubeStudioComponent } from './vtube-studio.component';

describe('VtubeStudioComponent', () => {
  let component: VtubeStudioComponent;
  let fixture: ComponentFixture<VtubeStudioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VtubeStudioComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VtubeStudioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
