import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Config } from '../interface/config';

const SETTINGS_LOCATION = "../assets/appsettings.json";

@Injectable({
    providedIn: 'root'
})
export class AppSettingsService {
    constructor(private http: HttpClient) {
    }

    getSettings() {
        return this.http.get<Config>(SETTINGS_LOCATION);
    }
}
