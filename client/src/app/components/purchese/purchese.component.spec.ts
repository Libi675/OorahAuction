import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurcheseComponent } from './purchese.component';

describe('PurcheseComponent', () => {
  let component: PurcheseComponent;
  let fixture: ComponentFixture<PurcheseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PurcheseComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurcheseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
