import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../interface/response';
import { AppSettingsService } from './appsettings.service';

@Injectable({
  providedIn: 'root'
})
export class ApiMonedaService {
  url: string = "";

  constructor(private _http: HttpClient, private _setting: AppSettingsService) { }

  getMonedas(): Observable<Response> {
    this._setting.getSettings().subscribe(data => { this.url = data.webApiHost + "api/moneda"; });

    return this._http.get<Response>(this.url);
  }
}
