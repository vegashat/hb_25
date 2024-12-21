import { TestBed } from '@angular/core/testing';

import { SharedWeatherService } from './shared-weather.service';

describe('SharedWeatherService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SharedWeatherService = TestBed.get(SharedWeatherService);
    expect(service).toBeTruthy();
  });
});
