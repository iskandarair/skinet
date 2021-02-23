import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { NgxSpinnerModule } from 'ngx-spinner';
import { HomeModule } from './home/home.module';
import { ErroInterceptor } from './core/interceptors/error.interceptor';
import { LoadingInterceptors } from './core/interceptors/loading.interceptors';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CoreModule,
    HomeModule,
    NgxSpinnerModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErroInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptors, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
