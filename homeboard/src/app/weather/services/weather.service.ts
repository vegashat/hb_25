import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable} from 'rxjs/Observable';
import { IWeather, Nest } from '../models/weather';
import { environment } from 'src/environments/environment';
import { ApiUrlAppenderService } from 'src/app/shared/api-url-appender.service';
import { map, tap } from 'rxjs/operators';
import { MeteoWeather } from '../models/meteo-weather';

@Injectable()
export class WeatherService {

  constructor(private _http : HttpClient, private _apiUrlAppender : ApiUrlAppenderService) { }

  getWeather() : Observable<MeteoWeather> {
    let url = this._apiUrlAppender.appendBoardId(environment.urls.weather);
    return  this._http.get<MeteoWeather>(url).pipe(
      tap(
       //console.log
      ),
      map(response => { 
        return response as MeteoWeather;
      })

    )
  }

  getNest() : Observable<Nest> {
    let url = this._apiUrlAppender.appendBoardId(environment.urls.nest );
    return  this._http.get<Nest>(url).pipe(
      tap(
       //console.log
      ),
      map(response => { 
        return response as Nest;
      })
    );
  }
}
