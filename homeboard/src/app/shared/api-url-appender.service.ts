import { Injectable, OnInit } from '@angular/core';
import { BoardService } from './board.service';

@Injectable({
  providedIn: 'root'
})
export class ApiUrlAppenderService {

  private _boardId : string;
  constructor(private _boardService : BoardService) {
    this._boardService.getBoard().subscribe(result => {
      this._boardId = result;
    })
   }

  public appendBoardId(url: string) : string {
    //Add trailing slash if it doesn't exist
    url = url.replace(/\/?$/, '/');
    url += this._boardId;

    return url;
  }
}
