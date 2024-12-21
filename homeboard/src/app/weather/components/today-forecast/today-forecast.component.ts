import { Component, OnInit, OnDestroy } from '@angular/core';
import { SharedWeatherService } from '../../services/shared-weather.service';
import { IWeather } from '../../models/weather';
import { Subject, timer } from 'rxjs';
import { environment } from 'src/environments/environment';
import { takeUntil } from 'rxjs/operators';
import { Forecast, MeteoWeather } from '../../models/meteo-weather';

@Component({
  selector: 'app-today-forecast',
  templateUrl: './today-forecast.component.html',
  styleUrls: ['./today-forecast.component.css']
})
export class TodayForecastComponent implements OnInit, OnDestroy{

  private _unsubscribe: Subject<void> = new Subject();
  private _switch_interval: number = (environment.intervals.feels_switch * 1000);
  showFeels: boolean = false;
  
  weather: MeteoWeather;
  forecast: Forecast[];
  constructor(private _sharedWeatherService : SharedWeatherService) { }

  ngOnInit() {
    this.getWeather();
    this.setupIntervalSwitch();
  }

  getWeather():void {
    this._sharedWeatherService.getWeather().subscribe(result =>
    {
      this.weather = result;
      this.forecast = result.forecasts.slice(0,1);
    });
  }

  setupIntervalSwitch() {
    timer(this._switch_interval, this._switch_interval).pipe(
      takeUntil(this._unsubscribe)
    )
      .subscribe(() => {
        this.showFeels = !this.showFeels;
      });
  }

  ngOnDestroy(){
    this._unsubscribe.next();
    this._unsubscribe.complete();
  }
}
