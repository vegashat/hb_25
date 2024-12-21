import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { environment } from 'src/environments/environment';
import { ICalendar, IEntry } from '../models/calendar';
import { BoardService } from 'src/app/shared/board.service';
import { ApiUrlAppenderService } from 'src/app/shared/api-url-appender.service';
import { of } from 'rxjs/internal/observable/of';

@Injectable()
export class CalendarService {

    private _calendarUrl : string = environment.urls.calendar;
    private cals: ICalendar[];

    constructor(private _http : HttpClient, private _apiAppenderService : ApiUrlAppenderService) { }

    getCalendar() : Observable<ICalendar[]> {
      const url = this._apiAppenderService.appendBoardId(this._calendarUrl);
      return this._http.get<ICalendar[]>(url);
    }
  }