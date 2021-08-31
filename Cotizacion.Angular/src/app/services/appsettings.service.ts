import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Config } from '../interface/config';
import { Observable } from 'rxjs/observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import { retry } from 'rxjs/operators';

const SETTINGS_LOCATION = "../assets/appsettings.json";

@Injectable({
    providedIn: 'root'
})
export class AppSettingsService {
    config: Config | undefined;

    constructor(private http: HttpClient) {
    }

    loadSettings(): Promise<Config> {
        return this.http.get<Config>(SETTINGS_LOCATION).toPromise().then(con => this.config = con);
    }

    readConfig(): Config {
        return this.config as Config;
    }
}
