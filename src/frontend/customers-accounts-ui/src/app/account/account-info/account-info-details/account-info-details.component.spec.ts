import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountInfoDetailsComponent } from './account-info-details.component';

describe('AccountInfoDetailsComponent', () => {
  let component: AccountInfoDetailsComponent;
  let fixture: ComponentFixture<AccountInfoDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountInfoDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountInfoDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
