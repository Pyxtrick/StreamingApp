import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandEditComponent } from './command-edit.component';

describe('CommandEditComponent', () => {
  let component: CommandEditComponent;
  let fixture: ComponentFixture<CommandEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommandEditComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CommandEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
