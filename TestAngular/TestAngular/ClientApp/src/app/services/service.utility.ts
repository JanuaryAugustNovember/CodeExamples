import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'text/json' })
};

@Injectable({
  providedIn: 'root',
})
export class UtilityService {
  constructor(private http: HttpClient) { }

  baseUrl: string = 'https://localhost:44304/api';
  //baseUrl: string =  'https://localhost:44304/api/Utility/Decrypt?inputText=Test';
  //baseUrl: string = 'http://localhost:44304/api/Utility/Ping';
  //baseUrl: string = 'http://localhost:44304/api/SampleData/WeatherForecasts';

  getDecryptedText(inputText) {
    console.log("inside serveice.decrypt.ts input text is: " + inputText);

    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

    var httpOptions = {
      headers: new HttpHeaders({
        'Accept': 'text/html, application/xhtml+xml, */*',
        'Content-Type': 'application/x-www-form-urlencoded'
      }),
      responeType: 'text'
    }

    // headers: { "Accept": "charset=utf-8", "Accept-Charset": "charset=utf-8" }
    //var response = this.http.get<string>(this.baseUrl + "/Utility/Decrypt?inputText=" + inputText, { responseType: 'text', headers: headers });
    var response = this.http.get(this.baseUrl + "/Utility/Decrypt?inputText=" + inputText, { responseType: 'text' });
    //var response = this.http.get<string>(this.baseUrl + "/Utility/Decrypt?inputText=" + inputText, { headers: headers });
    //var response = this.http.get<string>(this.baseUrl + "/Utility/Decrypt?inputText=" + inputText, { responseType: 'json' }); // remove the whole repsonseType param and it defaults to json
    
    console.log(response);

    return response;
  }

  getElasticStatus() {
    var status = false;

    console.log("Pinging Elastic...")
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    var response = this.http.get<boolean>(this.baseUrl + "/Utility/GetElasticStatus", { responseType: 'json', headers: headers });
    console.log("Reached Elastic repo");

    //response.subscribe(x => {
    //  status = x;
    //});

    return response;
  }

  getConnections(searchText) {
    console.log("Inside service.utility.ts input text is: " + searchText);

    var httpOptions = {
      headers: new HttpHeaders({
        'Accept': 'text/html, application/xhtml+xml, */*',
        'Content-Type': 'application/x-www-form-urlencoded'
      }),
      responeType: 'json'
    }
    var response = this.http.get<Connection[]>(this.baseUrl + "/Utility/GetConnections?searchText=" + searchText, { responseType: 'json' });

    console.log(response);

    return response;
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


