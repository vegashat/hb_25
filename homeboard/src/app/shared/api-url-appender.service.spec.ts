import { TestBed } from '@angular/core/testing';

import { ApiUrlAppenderService } from './api-url-appender.service';

describe('ApiUrlAppenderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApiUrlAppenderService = TestBed.get(ApiUrlAppenderService);
    expect(service).toBeTruthy();
  });
});
