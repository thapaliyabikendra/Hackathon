import type { CreateUpdateMatchDto, MatchDto, MatchFilter } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MatchService {
  apiName = 'Default';
  

  create = (input: CreateUpdateMatchDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/match',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'DELETE',
      url: `/api/app/match/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MatchDto>({
      method: 'GET',
      url: `/api/app/match/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getListByFilter = (input: PagedAndSortedResultRequestDto, filter: MatchFilter, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<MatchDto>>({
      method: 'GET',
      url: '/api/app/match/by-filter',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, tournamentId: filter.tournamentId, groupId: filter.groupId, teamAId: filter.teamAId, teamBId: filter.teamBId, matchDate: filter.matchDate, teamAScore: filter.teamAScore, teamBScore: filter.teamBScore },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateMatchDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'PUT',
      url: `/api/app/match/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
