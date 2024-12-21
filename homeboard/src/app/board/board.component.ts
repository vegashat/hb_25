import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../settings/services/settings.service';
import { ISetting } from '../settings/models/settings';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {

  constructor(private _settingsService : SettingsService) { }
  settingsLoaded : boolean = false;
  displayWeather : boolean = true;


  ngOnInit() {
    this.loadSettings();
  }

  loadSettings() : void {
    var setting = this._settingsService.getSetting("ShowWeather", "true");
    this.displayWeather = setting[0] == "true";
    this.settingsLoaded = setting[1];
    if(!this.settingsLoaded){
      setTimeout(() => {
       this.loadSettings(); 
      }, 1000);
    }
  }
}
