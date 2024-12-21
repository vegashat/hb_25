import { Component, OnInit, OnDestroy } from '@angular/core';
import { timer, Subject } from 'rxjs';
import { takeUntil} from 'rxjs/operators';

import { IWeather, Nest } from '../models/weather';
import { environment } from 'src/environments/environment';
import { WeatherService } from '../services/weather.service';
import { SharedWeatherService } from '../services/shared-weather.service';
import { MeteoWeather } from '../models/meteo-weather';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent implements OnInit, OnDestroy {

  private _unsubscribe : Subject<void> = new Subject();
  private _interval : number = (environment.intervals.weather * 1000);

  constructor(private _weatherService : WeatherService, private _sharedWeatherService: SharedWeatherService) {
   }

  weather : MeteoWeather;
  hasThermostat : boolean = true;
  ngOnInit() {
    this.getWeather();
    this.getNest();
  }

  getWeather() : void {
    timer(0, this._interval).pipe(
      takeUntil(this._unsubscribe)
    )
    .subscribe(() => {
      this._weatherService.getWeather()
          .subscribe(result => {
            this.weather = result;
            this.hasThermostat = false;
            this._sharedWeatherService.setWeather(this.weather);
        });
      });
  }

  getNest() : void {
    timer(0, this._interval).pipe(
      takeUntil(this._unsubscribe)
    )
    .subscribe(() => {
      this._weatherService.getNest()
          .subscribe(result => {
            if(result && result.ambientTemperature > 0){
              this.hasThermostat = true; 
              this._sharedWeatherService.setThermostat(result);
            }
        });
      });
  }

  ngOnDestroy(){
    this._unsubscribe.next();
    this._unsubscribe.complete();
  }
}
