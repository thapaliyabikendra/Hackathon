import type { CreateUpdateTeamDto, TeamDto, TeamFilter } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TeamService {
  apiName = 'Default';
  

  create = (input: CreateUpdateTeamDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/team',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'DELETE',
      url: `/api/app/team/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TeamDto>({
      method: 'GET',
      url: `/api/app/team/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getListByFilter = (input: PagedAndSortedResultRequestDto, filter: TeamFilter, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TeamDto>>({
      method: 'GET',
      url: '/api/app/team/by-filter',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, displayName: filter.displayName },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateTeamDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'PUT',
      url: `/api/app/team/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
