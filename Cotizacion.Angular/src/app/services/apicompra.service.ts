import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../interface/response';
import { CompraInsert } from '../interface/comprainsert';
import { AppSettingsService } from './appsettings.service';
import { Config } from '../interface/config';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ApiCompraService {
  url: string = "";
  config: Config | undefined;

  constructor(private _http: HttpClient, private _setting: AppSettingsService) { }

  getCompras(): Observable<Response> {
    this._setting.getSettings().subscribe(data => { this.url = data.webApiHost + "api/compra"; });

    return this._http.get<Response>(this.url);
  }

  Add(compra: CompraInsert): Observable<Response> {
    this._setting.getSettings().subscribe(data => { this.url = data.webApiHost + "api/compra"; });

    return this._http.post<Response>(this.url, compra, httpOptions);
  }
}
