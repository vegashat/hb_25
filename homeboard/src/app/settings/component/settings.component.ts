import { Component, OnInit, ViewEncapsulation, NgZone } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { BoardService } from '../../shared/board.service';
import { CookieService } from 'ngx-cookie-service';
@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SettingsComponent implements OnInit {

  boards = environment.boards;
  constructor(private _ngZone : NgZone, private router : Router, private _boardService : BoardService, private cookieService : CookieService) { }

  ngOnInit() {
  }

  setBoard(boardId) : void {
    localStorage.setItem("board_id", boardId)
    //this.cookieService.set("board-id", boardId);
    this._boardService.setBoard(boardId);
    this._ngZone.run(() => {
      this.router.navigate(['board']);
    });
  }
}
