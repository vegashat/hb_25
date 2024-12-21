import { Component, OnInit, OnDestroy } from '@angular/core';
import { timer, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { SettingsService } from '../settings/services/settings.service';

@Component({
  selector: 'app-clock',
  templateUrl: './clock.component.html',
  styleUrls: ['./clock.component.css']
})
export class ClockComponent implements OnInit, OnDestroy {

  constructor(private _settingsService : SettingsService) { }

  currentDateTime : Date;
  private _unsubscribe: Subject<void> = new Subject();
  militaryTime: boolean = false;
  settingsLoaded: boolean = false;

  ngOnInit() {
    this.getTime();
    this.loadSettings();
  }

  loadSettings(): void {
    var setting = this._settingsService.getSetting("UseMilitaryTime", "false");
    this.militaryTime = setting[0] == "true";
    this.settingsLoaded = setting[1];
    if (!this.settingsLoaded) {
      setTimeout(() => {
        this.loadSettings();
      }, 1000);
    }
  }

  getTime() {
    timer(0,1000).pipe(
      takeUntil(this._unsubscribe)
    ).subscribe(() => {
      this.currentDateTime = new Date();
    });
  }

  ngOnDestroy() {
    this._unsubscribe.next();
    this._unsubscribe.complete();
  }
}
