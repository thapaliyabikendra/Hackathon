import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StadiumComponent } from './stadium.component';

const routes: Routes = [{ path: '', component: StadiumComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StadiumRoutingModule { }
