import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import { FlexLayoutModule} from '@angular/flex-layout'
import {RouterModule, Routes} from '@angular/router';
import { AppComponent } from './app.component';
import { ImageService } from './background/services/image.service';
import { BackgroundComponent } from './background/components/background.component';
import { CalendarComponent } from './calendar/components/calendar.component';
import { SharedModule } from './shared/shared.module';
import { CalendarService } from './calendar/services/calendar.service';
import { FormatDayPipe } from './calendar/pipes/format-day.pipe';
import { ClockComponent } from './clock/clock.component';
import { TodayComponent } from './today/today.component';
import { WeatherService } from './weather/services/weather.service';
import { WeatherComponent } from './weather/components/weather.component';
import { TodayForecastComponent } from './weather/components/today-forecast/today-forecast.component';
import { SharedWeatherService } from './weather/services/shared-weather.service';
import { ThermostatComponent } from './weather/components/thermostat/thermostat.component';
import { WeekForecastComponent } from './weather/components/week-forecast/week-forecast.component';
import { SettingsComponent } from './settings/component/settings.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { BoardComponent } from './board/board.component';
import { BoardGuard } from './guard/board.guard';
import { BoardService } from './shared/board.service';
import { CookieService } from 'ngx-cookie-service';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { SettingsService } from './settings/services/settings.service';

const appRoutes: Routes = [
  { path: 'board', component: BoardComponent, canActivate: [BoardGuard]},
  { path: 'settings', component: SettingsComponent },
  { path: '',   redirectTo: '/board', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    BackgroundComponent,
    CalendarComponent,
    ClockComponent,
    FormatDayPipe,
    TodayComponent,
    WeatherComponent,
    TodayForecastComponent,
    ThermostatComponent,
    WeekForecastComponent,
    BoardComponent,
    SettingsComponent,
    PageNotFoundComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes, {useHash: true}),
    BrowserModule,
    HttpClientModule,
    FlexLayoutModule,
    SharedModule
  ],
  providers: [ImageService, CalendarService, WeatherService, SharedWeatherService, BoardService, CookieService, SettingsService,
    {provide: LocationStrategy, useClass: HashLocationStrategy}],
  bootstrap: [AppComponent]
})
export class AppModule { }
