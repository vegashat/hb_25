import { Component, OnInit, OnDestroy } from '@angular/core';
import { timer, Subject } from 'rxjs';
import { takeUntil, map } from 'rxjs/operators'
import { environment } from 'src/environments/environment';

import { ICalendar, IEntry } from '../models/calendar';
import { CalendarService } from '../services/calendar.service';
import { SettingsService } from 'src/app/settings/services/settings.service';
@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit, OnDestroy {

  public calendarDays: ICalendar[];

  weekSplits: number[] = [0, 1];
  daysToDisplay: number[][] = [[0, 1, 2, 3, 4], [5, 6, 7, 8, 9]];
  showFirstWeek: boolean;

  bottomCalendar: boolean = false;
  settingsLoaded: boolean = false;
  calendarLoaded: Promise<boolean>;

  private _unsubscribe: Subject<void> = new Subject();
  private _refresh_interval: number = (environment.intervals.calendar * 1000);
  private _switch_interval: number = (environment.intervals.calendar_switch * 1000);
  private _today: Date = new Date();

  constructor(private _calendarService: CalendarService, private _settingsService: SettingsService) {
  }

  ngOnInit() {
    this.loadSettings();
    this.setupIntervalSwitch();
  }

  loadSettings(): void {
    var setting = this._settingsService.getSetting("CalendarLocation", "full");
    this.bottomCalendar = setting[0] == "bottom";
    this.settingsLoaded = setting[1];
    if (!this.settingsLoaded) {
      setTimeout(() => {
        this.loadSettings();
      }, 1000);
    }else {
      this.getCalendar();
    }

  }
  getCalendar(): void {
    timer(0, this._refresh_interval).pipe(
      takeUntil(this._unsubscribe)
    )
      .subscribe(() => {
        this._calendarService.getCalendar()
          .pipe(
            map(result => {

              result = Array.from(result);
              var workingDate = new Date();
              workingDate.setHours(0,0,0,0);
              var today = new Date();

              if(this.bottomCalendar){
                var calendar = new Array<ICalendar>(environment.calendar_days);
                for (let day = 0; day < environment.calendar_days; day++) {
                  workingDate.setDate(today.getDate() + day);
                  var entry = result.find(day => {
                    var date = new Date(day.date);
                    if (workingDate.toDateString() === date.toDateString())
                    {
                      return day;
                    }
                  })
                  if (entry == null || entry == undefined) {
                    entry = <ICalendar>{};
                    entry.date = new Date(workingDate);
                    entry.entries = new Array<IEntry>(1);
                    calendar[day] = entry;
                  }else
                  {
                    calendar[day] = entry;
                  }
                }
              result = calendar;
              }else {
                if(result.length > 0)
                {
                  var count = result.length;
                  var half = Math.round((count/2) + .1);
                  this.daysToDisplay[0] = [];
                  this.daysToDisplay[1] = [];
                  for(let day = 0; day < count; day++){
                    if(day < half){
                      this.daysToDisplay[0].push(day);
                    }
                    else {
                      this.daysToDisplay[1].push(day);
                    }

                  }
                }
              }
              return result;
            })
          )
          .subscribe(result => {
            this.calendarDays = result;
            this.calendarLoaded = Promise.resolve(true);
          });
      });
  }

  setupIntervalSwitch() {
    timer(this._switch_interval, this._switch_interval).pipe(
      takeUntil(this._unsubscribe)
    )
      .subscribe(() => {
        this.showFirstWeek = !this.showFirstWeek;
      });
  }

  isFlintDay(entry: ICalendar): boolean {
    if (entry.entries === null) {
      return false;
    }
    var flintEntry = entry.entries.find(e => e.description === 'Flint');

    return flintEntry !== undefined;
  }

  isToday(date: Date): boolean {
    return new Date(date).getDate() === this._today.getDate();
  }

  filterOutFlintDays(entries: IEntry[]): IEntry[] {
    if (entries != null) {
      return entries.filter(e => e.description !== 'Flint');
    }
    return null;
  }

  ngOnDestroy() {
    this._unsubscribe.next();
    this._unsubscribe.complete();
  }
}
