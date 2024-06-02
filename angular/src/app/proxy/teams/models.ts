
export interface CreateUpdateTeamDto {
  displayName: string;
}

export interface TeamDto {
  id?: string;
  displayName?: string;
  creationTime?: string;
}

export interface TeamFilter {
  displayName?: string;
}
