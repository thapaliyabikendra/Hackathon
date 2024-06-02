import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { OAuthService } from 'angular-oauth2-oidc';
import * as signalR from "@aspnet/signalr";
import { Toaster, ToasterService } from '@abp/ng.theme.shared';

import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
    <abp-internet-status></abp-internet-status>
  `,
})
export class AppComponent  implements OnInit {
  private hubConnection: signalR.HubConnection;
  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }
  get accessToken(): string {
    return this.oAuthService.getAccessToken();
  }
  get toasterOptions(): Partial<Toaster.ToastOptions> {
    return {
      life: 5000,
      tapToDismiss: true,
    }
  };
  alarm: any = null;
  constructor(
    private oAuthService: OAuthService,
    private toaster: ToasterService,
    private datePipe: DatePipe,
    ){
      this.alarm = new Audio();
      this.alarm.src = `${environment.oAuthConfig.redirectUri}/assets/audio/alarm.wav`;
      this.alarm.load();
  }
  ngOnInit() {
    if (this.hasLoggedIn) {
      this.initSignalR();
    }
  }
  
  initSignalR(){
    this.hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${environment.apis.default.url}/signalr-hubs/chat`, {
      accessTokenFactory: () => this.accessToken
    })
    .build();
  this.hubConnection.on("ReceiveMessage", (message) => {
    const datePipeString = this.datePipe.transform(Date.now(),'MM-dd hh:mm aa');
    this.toaster.info(message, `New Message @ ${datePipeString}`, this.toasterOptions);
    this.playAlarm();
  });
  this.hubConnection
    .start()
    .then(() => {
      console.log('Connection started')
    })
    .catch(err => console.log('Error while starting connection: ' + err))
  }
  playAlarm() {
    this.alarm.play();
  }
  stopAlarm() {
    this.alarm.pause();
    this.alarm.currentTime = 0;
  }
}
