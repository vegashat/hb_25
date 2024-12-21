import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class BoardService {

  board = new BehaviorSubject<string>("1");
  constructor(private cookieService : CookieService) {
    const localStorageBoardId = localStorage.getItem('board_id');
    //const storageBoardIdCookie = this.cookieService.get('board_id');

    if(localStorageBoardId != null){
      this.board.next(localStorageBoardId);
    }
   }

  public setBoard(boardId: string) {
    this.board.next(boardId);
  }

  public getBoard() : Observable<string> {
    return this.board.asObservable();
  }
}
