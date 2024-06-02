import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
	{ path: 'tournaments', loadChildren: () => import('./tournament/tournament.module').then(m => m.TournamentModule) },
	{ path: 'stadiums', loadChildren: () => import('./stadium/stadium.module').then(m => m.StadiumModule) },
	{ path: 'groups', loadChildren: () => import('./group/group.module').then(m => m.GroupModule) },
	{ path: 'matchs', loadChildren: () => import('./match/match.module').then(m => m.MatchModule) },
	{ path: 'teams', loadChildren: () => import('./team/team.module').then(m => m.TeamModule) },
];






@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
