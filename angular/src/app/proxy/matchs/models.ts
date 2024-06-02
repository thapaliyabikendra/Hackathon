
export interface CreateUpdateMatchDto {
  tournamentId: string;
  groupId?: string;
  teamAId: string;
  teamBId: string;
  matchDate: string;
  teamAScore: number;
  teamBScore: number;
}

export interface MatchDto {
  id?: string;
  tournamentId?: string;
  groupId?: string;
  teamAId?: string;
  teamBId?: string;
  matchDate?: string;
  teamAScore?: number;
  teamBScore?: number;
  creationTime?: string;
}

export interface MatchFilter {
  tournamentId?: string;
  groupId?: string;
  teamAId?: string;
  teamBId?: string;
  matchDate?: string;
  teamAScore?: number;
  teamBScore?: number;
}
