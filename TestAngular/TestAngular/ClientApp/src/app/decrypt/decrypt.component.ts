import { Component } from '@angular/core';
import { UtilityService } from '../services/service.utility';

@Component({
  selector: 'app-decrypt-component',
  templateUrl: './decrypt.component.html'
})
export class DecryptComponent {

  // Variables
  public hasText = false;
  public isSuccessful = false;
  public inputText = "";
  public decryptedText = "";

  //asd
  //asddsa

  // Constructor
  constructor(private decryptService: UtilityService) { }

  // Methods
  public decryptText() {
    console.log("Hit decryptText function");
    console.log(this.inputText);

    var test = this.decryptService.getDecryptedText(this.inputText)
      .subscribe(result =>
      {
        console.log("decrypt subscribe result: " + result);
        this.decryptedText = result
      },
      error => {
        console.log("decrypt error result:" + error);
        this.isSuccessful = false;
      });

    this.isSuccessful = true;

    console.log("Decrypted Text: " + this.decryptedText);
    console.log("called decrypt service");
    console.log(test);

  }

  public clear() {
    this.isSuccessful = false;
    this.hasText = false;
    this.decryptedText = "";
    this.inputText = ""
  }
}


