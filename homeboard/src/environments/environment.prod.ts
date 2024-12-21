export const environment = {
  production: true,
  urls : {
    photo : "https://api.vegashat.com/api/photo",
    calendar : "https://api.vegashat.com/api/calendar/",
    nest : "https://api.vegashat.com/api/nest/",
    weather : "https://weather.vegashat.com/api/",
    settings : "https://api.vegashat.com/api/settings"
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
