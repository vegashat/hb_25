import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiUrlAppenderService } from 'src/app/shared/api-url-appender.service';
import { ISetting } from '../models/settings';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { IsAfterPipe } from 'ngx-moment';
import { until } from 'protractor';
import { settings } from 'cluster';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  private _settingsUrl: string = environment.urls.settings;
  private _settings : ISetting[] = null;

  constructor(private _http : HttpClient, private _apiAppenderService : ApiUrlAppenderService) {
    this.loadSettings();
   }

  loadSettings() : void {
    this.getSettings()
      .pipe(take(1))
      .subscribe(settings => this._settings = settings);
  }

  getSettings() : Observable<ISetting[]> {
    const url = this._apiAppenderService.appendBoardId(this._settingsUrl);
    return this._http.get<ISetting[]>(url)
  }

  getSetting(settingName : string, defaultValue : string) : [string, boolean] {
    if(this._settings === null){
      return [defaultValue, false];
    }
    else {
      var setting = this._settings.find(s => s.key == settingName);
      if(setting === undefined){
        return [defaultValue, true];
      }

      return [setting.value, true];
    }
  }

}
