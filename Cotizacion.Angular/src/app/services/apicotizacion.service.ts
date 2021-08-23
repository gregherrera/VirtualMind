import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../interface/response';
import { AppSettingsService } from './appsettings.service';

@Injectable({
  providedIn: 'root'
})
export class ApiCotizacionService {
  url: string = "";

  constructor(private _http: HttpClient, private _setting: AppSettingsService) { }

  getCotizaciones(): Observable<Response> {
    this._setting.getSettings().subscribe(data => { this.url = data.webApiHost + "api/cotizacion"; });

    return this._http.get<Response>(this.url);
  }
}
