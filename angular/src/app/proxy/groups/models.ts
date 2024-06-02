
export interface CreateUpdateGroupDto {
  tournamentId: string;
  groupName: string;
}

export interface GroupDto {
  id?: string;
  tournamentId?: string;
  groupName?: string;
  creationTime?: string;
}

export interface GroupFilter {
  tournamentId?: string;
  groupName?: string;
}
