import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StadiumRoutingModule } from './stadium-routing.module';
import { StadiumComponent } from './stadium.component';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    StadiumComponent
  ],
  imports: [
    CommonModule,
    StadiumRoutingModule,
    SharedModule,
    HttpClientModule
  ]
})
export class StadiumModule { }
