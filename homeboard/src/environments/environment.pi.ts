export const environment = {
  production: true,
  urls : {
    photo : "http://192.168.1.122:5001/api/photo",
    calendar : "http://192.168.1.122:5001/api/calendar/",
    nest : "http://192.168.1.122:5001/api/nest/",
    weather : "https://cors.vegashat.com/https://weather.vegashat.com/api/",
    settings : "http://192.168.1.122:5001/api/settings"
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
