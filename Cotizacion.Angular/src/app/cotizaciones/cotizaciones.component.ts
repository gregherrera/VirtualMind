import { Component } from '@angular/core';
import { Config } from '../interface/config';
import { ApiCotizacionService } from '../services/apicotizacion.service';
import { AppSettingsService } from '../services/appsettings.service';

@Component({
  selector: 'app-cotizaciones',
  templateUrl: './cotizaciones.component.html'
})
export class CotizacionesComponent {
  public lista: any[] = [];
  config: Config | undefined;
  cargar: boolean = false;

  constructor(private apiCotizacion: ApiCotizacionService) {
  }

  ngOnInit(): void {
    this.getCotizaciones();
  }

  getCotizaciones(): void {
    this.lista = [];
    this.cargar = true;
    this.apiCotizacion.getCotizaciones().subscribe(result => {
      this.lista = result.data;
      this.cargar = false;
    }, error => {
      console.error(error);
      this.cargar = false;
    });
  }
}
