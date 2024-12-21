export interface MeteoWeather {
    currentTemp: number
    feelsLike: number
    humidity: number
    windSpeed: number
    windGusts: number
    windDirection: string
    sunrise: string
    sunset: string
    forecasts: Forecast[]
    hourly: Hourly[]
    date: string
    high: number
    low: number
    precipPercent: number
    weatherCode: string
  }
  
  export interface Forecast {
    date: string
    high: number
    low: number
    precipPercent: number
    weatherCode: string
  }
  
  export interface Hourly {
    hour: number
    temp: number
    feelsLike: number
    precipPercent: number
    weatherCode: string
  }
  