
export interface CreateUpdateStadiumDto {
  displayName: string;
  location: string;
}

export interface StadiumDto {
  id?: string;
  displayName?: string;
  location?: string;
  timeZoneId?: string;
  timeZoneDstOffset: number;
  timeZoneRawOffset: number;
  creationTime?: string;
}

export interface StadiumFilter {
  displayName?: string;
  location?: string;
  timeZoneId?: string;
  timeZoneDstOffset?: number;
  timeZoneRawOffset?: number;
}
