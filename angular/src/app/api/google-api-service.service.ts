import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GoogleApiServiceService {
  private apiUrl = 'https://maps.googleapis.com/maps/api/';
  private googleApiKey = "AIzaSyC4dWwTioCBvxcWYtgf98TpqbSP3f6cahM"
  constructor(private http: HttpClient) { }

  // Method to get places based on input
  getPlaces(input: string) {
    const url = `${this.apiUrl}place/autocomplete/json?input=${input}&key=${this.googleApiKey}`;
    return this.http.get(url);
  }
}
