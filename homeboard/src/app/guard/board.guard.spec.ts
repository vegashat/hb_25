import { TestBed, async, inject } from '@angular/core/testing';

import { BoardGuard } from './board.guard';

describe('BoardGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BoardGuard]
    });
  });

  it('should ...', inject([BoardGuard], (guard: BoardGuard) => {
    expect(guard).toBeTruthy();
  }));
});
