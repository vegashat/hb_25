import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Nest } from '../models/weather';
import { MeteoWeather } from '../models/meteo-weather';

@Injectable({
  providedIn: 'root'
})
export class SharedWeatherService {

  private currentWeather = new BehaviorSubject<MeteoWeather>(null);
  private currentThermostat = new BehaviorSubject<Nest>(null);
  constructor() { }

  setWeather(weather:MeteoWeather) {
    this.currentWeather.next(weather);
  }

  getWeather() : Observable<MeteoWeather> {
    return this.currentWeather.asObservable();
  }

  setThermostat(nest: Nest) {
    this.currentThermostat.next(nest);
  }

  getThermostat() : Observable<Nest> {
    return this.currentThermostat.asObservable();
  }
}
