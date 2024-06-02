import type { CreateUpdateStadiumDto, StadiumDto, StadiumFilter } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StadiumService {
  apiName = 'Default';
  

  create = (input: CreateUpdateStadiumDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/stadium',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'DELETE',
      url: `/api/app/stadium/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, StadiumDto>({
      method: 'GET',
      url: `/api/app/stadium/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getListByFilter = (input: PagedAndSortedResultRequestDto, filter: StadiumFilter, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<StadiumDto>>({
      method: 'GET',
      url: '/api/app/stadium/by-filter',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, displayName: filter.displayName, location: filter.location, timeZoneId: filter.timeZoneId, timeZoneDstOffset: filter.timeZoneDstOffset, timeZoneRawOffset: filter.timeZoneRawOffset },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateStadiumDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'PUT',
      url: `/api/app/stadium/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
