
    export class Forecast {
        date: Date;
        tempHigh: number;
        tempLow: number;
        conditionCode: string;
        conditionDescription: string;
    }

    export class Nest {
        ambientTemperature : number;
        humidity : number;
        targetTemperature:number;
        targetTemperatureHigh : number;
        targetTemperatureLow : number;
        hvacMode : string;
        hvacState : string;
        isAway : boolean;
    }

    export interface IWeather extends Forecast {
        currentTemp: number;
        feelsLike: number;
        humidity: number;
        windSpeed: number;
        windDirection: string;
        sunrise: Date;
        sunset: Date;
        forecasts: Forecast[];
    }

