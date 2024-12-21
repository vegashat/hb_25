// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.


export const environment = {
  production: false,
  urls : {
    /*
    pic : "http://localhost:8080/images/",
    photo : "http://localhost:1777/api/photo/",
    calendar : "http://localhost:1777/api/calendar/",
    weather : "http://localhost:1777/api/weather/",
    nest : "http://localhost:1777/api/nest",
    settings : "http://localhost:1777/api/settings"
    */
    pic : "http://localhost:8080/images",
    photo : "http://localhost:5400/api/photo/",
    calendar : "http://localhost:5400/api/calendar/",
    weather : "http://localhost:5400/api/weather/",
    nest : "http://localhost:5400/api/nest",
    settings : "http://localhost:5400/api/settings"
  },
  calendar_days : 14,
  intervals: {
    image : 20,
    calendar : 300,
    calendar_switch : 7,
    feels_switch : 5,
    weather : 300
  },
  boards :
    [
     {name : "Albrecht", id: 1},
     {name : "Cloer", id: 2},
     {name : "Trentham", id: 3},
     {name : "Alvis", id: 4}
    ]
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
