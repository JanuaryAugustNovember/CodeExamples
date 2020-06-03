import { Component } from '@angular/core';
import { UtilityService } from '../services/service.utility';

@Component({
  selector: 'app-elastic-search-component',
  templateUrl: './elastic-search.component.html'
})
export class ElasticSearchComponent {

  public isElasticRunning = false;

  constructor(private utilityService: UtilityService) {
    this.checkStatus();
  }

  checkStatus() {
    this.utilityService.getElasticStatus().subscribe(x => {
      this.isElasticRunning = x;
      console.log("ElasticSearchComponent Status: " + this.isElasticRunning);
    })
  }
}
