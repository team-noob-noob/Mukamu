import { TestBed } from '@angular/core/testing';

import { SinukaService } from './sinuka.service';

describe('SinukaService', () => {
  let service: SinukaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SinukaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
