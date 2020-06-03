import { Component } from '@angular/core';
import { TestService } from '../services/service.test';

@Component({
  selector: 'app-test-component',
  templateUrl: './test.component.html'
})
export class TestComponent {

  // Variables
  public testCount = 0;
  public isEnabled = false;
  public forecasts: WeatherForecast[];

  // Constructor
  constructor(private testService: TestService) {  }

  // Methods
  public incrementTestCounter() {
    this.testCount++;

    if (this.testCount == 10)
      this.isEnabled = true;
    else
      this.isEnabled = false;
  }

  public decrementTestCounter() {
    this.testCount--;

    if (this.testCount == 10)
      this.isEnabled = true;
    else
      this.isEnabled = false;
  }

  public getWeatherForecasts() {
    console.log("Hit the getWeatherForecasts function");

    var test1 = this.testService.getWeatherForecasts()
      .subscribe(data =>
        { this.forecasts = data },
        error => console.error(error));

    console.log(test1);

    console.log(this.forecasts);

    console.log("called the testSErvice.getWeatherForecasts service");
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

