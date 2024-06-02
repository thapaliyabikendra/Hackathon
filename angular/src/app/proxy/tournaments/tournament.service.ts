import type { CreateUpdateTournamentDto, GenerateMatchDto, TournamentDto, TournamentFilter } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TournamentService {
  apiName = 'Default';
  

  create = (input: CreateUpdateTournamentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/tournament',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'DELETE',
      url: `/api/app/tournament/${id}`,
    },
    { apiName: this.apiName,...config });
  

  generateMatches = (input: GenerateMatchDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/tournament/generate-matches',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  generateRandomDateByStartDateAndEndDate = (startDate: string, endDate: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/tournament/generate-random-date',
      params: { startDate, endDate },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TournamentDto>({
      method: 'GET',
      url: `/api/app/tournament/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getListByFilter = (input: PagedAndSortedResultRequestDto, filter: TournamentFilter, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TournamentDto>>({
      method: 'GET',
      url: '/api/app/tournament/by-filter',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, displayName: filter.displayName, startDate: filter.startDate, endDate: filter.endDate },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateTournamentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'PUT',
      url: `/api/app/tournament/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
