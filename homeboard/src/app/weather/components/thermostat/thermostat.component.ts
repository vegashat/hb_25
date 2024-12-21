import { Component, OnInit } from '@angular/core';
import { IWeather, Nest } from '../../models/weather';
import { SharedWeatherService } from '../../services/shared-weather.service';

@Component({
  selector: 'app-thermostat',
  templateUrl: './thermostat.component.html',
  styleUrls: ['./thermostat.component.css']
})
export class ThermostatComponent implements OnInit {

  thermostat: Nest;
  thermostatOn: boolean = false;
  heating: boolean = false;
  cooling: boolean = false;

  constructor(private _sharedWeatherService : SharedWeatherService) { }

  ngOnInit() {
    this.getThermostat();
  }

  getThermostat():void {
    this._sharedWeatherService.getThermostat().subscribe(result =>
    {
      this.thermostat = result;
      this.heating = this.thermostat.hvacState == "HEATING";
      this.cooling = this.thermostat.hvacState == "COOLING";
    });
  }
}
