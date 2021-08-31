import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Response } from '../interface/response';
import { AppSettingsService } from './appsettings.service';
import { Config } from '../interface/config';

@Injectable({
  providedIn: 'root'
})
export class ApiMonedaService implements OnInit {
  url: string = "";
  config: Config | undefined;

  constructor(private http: HttpClient, private appSetting: AppSettingsService) { }

  ngOnInit(): void {
  }

  getMonedas(): Observable<Response> {
    this.config = this.appSetting.readConfig();
    this.url = this.config.webApiHost + "api/moneda";

    return this.http.get<Response>(this.url);
  }
}
