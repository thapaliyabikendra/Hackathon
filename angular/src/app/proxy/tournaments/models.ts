
export interface CreateUpdateTournamentDto {
  displayName?: string;
  startDate?: string;
  endDate?: string;
}

export interface GenerateMatchDto {
  tournamentId: string;
  teamIds: string[];
  stadiumIds: string[];
}

export interface TournamentDto {
  id?: string;
  displayName?: string;
  startDate?: string;
  endDate?: string;
  creationTime?: string;
}

export interface TournamentFilter {
  displayName?: string;
  startDate?: string;
  endDate?: string;
}
