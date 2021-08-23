import { Component } from '@angular/core';
import { ApiCotizacionService } from '../services/apicotizacion.service';

@Component({
  selector: 'app-cotizaciones',
  templateUrl: './cotizaciones.component.html'
})
export class CotizacionesComponent {
  public lista: any[] = [];
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
