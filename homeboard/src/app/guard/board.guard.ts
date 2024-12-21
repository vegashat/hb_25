import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class BoardGuard implements CanActivate {

  constructor(private router : Router, private cookieService : CookieService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

      const boardId = localStorage.getItem("board_id");
      //const boardId = this.cookieService.get('board-id');
      if(!boardId){
        this.router.navigate(['settings']);
        return false
      }
      return true;
  }

}
