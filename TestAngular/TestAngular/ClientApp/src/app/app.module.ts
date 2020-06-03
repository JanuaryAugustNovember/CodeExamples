import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { TestComponent } from './test-component/test.component';
import { DecryptComponent } from './decrypt/decrypt.component';
import { ElasticSearchComponent } from './elastic-search/elastic-search.component';
import { ConnComponent } from './get-connection/conn.component';

import { TestService } from './services/service.test';
import { UtilityService } from './services/service.utility';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TestComponent,
    DecryptComponent,
    ElasticSearchComponent,
    ConnComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'test', component: TestComponent },
      { path: 'decrypt', component: DecryptComponent },
      { path: 'elastic-search', component: ElasticSearchComponent },
      { path: 'get-connections', component: ConnComponent }
    ])
  ],
  providers: [TestService, UtilityService],
  bootstrap: [AppComponent]
})
export class AppModule { }
