import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'text/json' })
};

@Injectable({
  providedIn: 'root',
})
export class TestService {
  constructor(private http: HttpClient) { }

  baseUrl: string = 'http://localhost:44304/api/SampleData/WeatherForecasts';

  post() {

  }

  getWeatherForecasts() {
    return this.http.get<WeatherForecast[]>(this.baseUrl);
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

