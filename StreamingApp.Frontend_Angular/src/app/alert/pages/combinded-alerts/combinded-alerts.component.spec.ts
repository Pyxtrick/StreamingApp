import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombindedAlertsComponent } from './combinded-alerts.component';

describe('CombindedAlertsComponent', () => {
  let component: CombindedAlertsComponent;
  let fixture: ComponentFixture<CombindedAlertsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CombindedAlertsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CombindedAlertsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
