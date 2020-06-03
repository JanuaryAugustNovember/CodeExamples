import { Component } from '@angular/core';
import { UtilityService } from '../services/service.utility';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  isElasticRunning = false;


  constructor(private utilityService: UtilityService) {
    this.checkStatus();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  checkStatus() {

    // https://stackoverflow.com/questions/47605737/how-to-make-a-synchronous-call-in-angular-5
    // An observable should be returned from the service and we subscribe here because it is async.
    // If we returned just a boolean from the service it would return before it assigns the bool resulting in false or undefined.
    this.utilityService.getElasticStatus().subscribe(x => {
      this.isElasticRunning = x;
      console.log("new way is elastic running: " + this.isElasticRunning);
    })
  }
}
