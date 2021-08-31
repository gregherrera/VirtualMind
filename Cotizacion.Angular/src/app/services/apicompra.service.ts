import { Injectable, OnInit } from '@angular/core';
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
export class ApiCompraService implements OnInit {
  url: string = "";
  config: Config | undefined;

  constructor(private http: HttpClient, private appSetting: AppSettingsService) { }

  ngOnInit(): void {
  }

  getCompras(): Observable<Response> {
    this.config = this.appSetting.readConfig();
    this.url = this.config.webApiHost + "api/compra";

    return this.http.get<Response>(this.url);
  }

  Add(compra: CompraInsert): Observable<Response> {
    this.config = this.appSetting.readConfig();
    this.url = this.config.webApiHost + "api/compra";

    return this.http.post<Response>(this.url, compra, httpOptions);
  }
}
