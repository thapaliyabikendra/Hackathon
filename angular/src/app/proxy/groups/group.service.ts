import type { CreateUpdateGroupDto, GroupDto, GroupFilter } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  apiName = 'Default';
  

  create = (input: CreateUpdateGroupDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/group',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'DELETE',
      url: `/api/app/group/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, GroupDto>({
      method: 'GET',
      url: `/api/app/group/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getListByFilter = (input: PagedAndSortedResultRequestDto, filter: GroupFilter, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<GroupDto>>({
      method: 'GET',
      url: '/api/app/group/by-filter',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, tournamentId: filter.tournamentId, groupName: filter.groupName },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateGroupDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'PUT',
      url: `/api/app/group/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
