import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ApiUrlAppenderService } from 'src/app/shared/api-url-appender.service';

@Injectable({
  providedIn: 'root'
})

export class ImageService{

  constructor(private _http : HttpClient, private _apiUrlAppender : ApiUrlAppenderService) { }

  getImageUrl() : Observable<string> {
    const url = this._apiUrlAppender.appendBoardId(environment.urls.photo);
    return this._http.get(url).pipe(
      map((result: any) => {
        return `${environment.urls.pic}/${result.guid}.jpg`
      } 
      ));
  }
}
