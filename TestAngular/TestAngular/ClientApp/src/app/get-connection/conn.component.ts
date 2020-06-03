import { Component } from '@angular/core';
import { TestService } from '../services/service.test';
import { UtilityService } from '../services/service.utility';

@Component({
  selector: 'app-conn-component',
  templateUrl: './conn.component.html',
  styleUrls: ['./conn.component.css']
})
export class ConnComponent {

  // Variables
  public hasText = false;
  public isSuccessful = false;
  public searchClicked = false;
  public hasResults = false;
  public loadingResults = false;
  public searchText = "";
  public connections: Connection[];

  // Constructor
  constructor(private connService: UtilityService) { }

  // Methods
  public getConnections() {
    console.log("Hit getConnections function");
    console.log(this.searchText);

    this.searchClicked = true;

    this.loadingResults = true;
    var connections = this.connService.getConnections(this.searchText)
      .subscribe(result => {
        this.isSuccessful = true;
        console.log("GetConnections subscribe result: " + result);

        this.connections = result;

        if (this.connections.length > 0) {
          this.hasResults = true;
        }

        this.loadingResults = false;
      },
      error => {
        console.log("Error getting connection strings: " + error);
        this.isSuccessful = false;
        this.loadingResults = false;
    })
  }

  public clear() {
    this.isSuccessful = false;
    this.hasText = false;
    this.searchText = "";
    this.searchClicked = false;
    this.loadingResults = false;
  }
}

interface Connection {
  dealerDataSourceId: number;
  hostAddress: string;
  hostPort: number;
  database: string;
  isEnabled: boolean;
  dealerNumber: number;
  dealerEnvironmentId: number;
  username: string;
  password: string;
  decryptedPassword: string;
}

