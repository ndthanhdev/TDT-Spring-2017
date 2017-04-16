import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VerifyPostComponent } from './verify-post.component';

describe('VerifyPostComponent', () => {
  let component: VerifyPostComponent;
  let fixture: ComponentFixture<VerifyPostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VerifyPostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerifyPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
