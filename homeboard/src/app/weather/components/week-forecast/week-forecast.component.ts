import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { timeStamp } from 'console';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { IWeather} from '../../models/weather';
import { SharedWeatherService } from '../../services/shared-weather.service';
import { Forecast, MeteoWeather } from '../../models/meteo-weather';

@Component({
  selector: 'app-week-forecast',
  templateUrl: './week-forecast.component.html',
  styleUrls: ['./week-forecast.component.css']
})
export class WeekForecastComponent implements OnInit, OnDestroy{

  weather: MeteoWeather;
  forecasts: Forecast[];
  private _unsubscribe : Subject<void> = new Subject();

  constructor(private _sharedWeatherService : SharedWeatherService) { }

  ngOnInit() {
    this.getWeather();
  }

  getWeather():void {
    this._sharedWeatherService.getWeather().pipe(takeUntil(this._unsubscribe))
      .subscribe(result =>
      {
        this.weather = result;
        this.forecasts = this.weather.forecasts.slice(0,5);
      });

    this._sharedWeatherService.getThermostat().pipe(takeUntil(this._unsubscribe))
      .subscribe(result =>
      {
        if(result){
          this.forecasts = this.weather.forecasts.slice(0,4);
        }
      });
  }

  ngOnDestroy(){
    this._unsubscribe.next();
    this._unsubscribe.complete();
  }
}
