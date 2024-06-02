import { TestBed } from '@angular/core/testing';

import { GoogleApiServiceService } from './google-api-service.service';

describe('GoogleApiServiceService', () => {
  let service: GoogleApiServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GoogleApiServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
