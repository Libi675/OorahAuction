import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GiftToUserComponent } from './gift-to-user.component';

describe('GiftToUserComponent', () => {
  let component: GiftToUserComponent;
  let fixture: ComponentFixture<GiftToUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GiftToUserComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GiftToUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
