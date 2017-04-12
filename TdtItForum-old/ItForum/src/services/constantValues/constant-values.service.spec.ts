import { TestBed, inject } from '@angular/core/testing';

import { ConstantValuesService } from './constant-values.service';

describe('ConstantValuesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConstantValuesService]
    });
  });

  it('should ...', inject([ConstantValuesService], (service: ConstantValuesService) => {
    expect(service).toBeTruthy();
  }));
});
