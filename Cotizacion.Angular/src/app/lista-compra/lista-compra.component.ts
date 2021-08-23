import { Component } from '@angular/core';
import { ApiCompraService } from '../services/apicompra.service';

@Component({
  selector: 'app-lista-compra',
  templateUrl: './lista-compra.component.html'
})
export class ListaCompraComponent {
  public lista: any[] = [];
  cargar: boolean = false;

  constructor(private apiCompra: ApiCompraService) {
  }

  ngOnInit(): void {
    this.getCompras();
  }

  getCompras() {
    this.cargar = true;
    this.apiCompra.getCompras().subscribe(result => {
      this.lista = result.data;
      this.cargar = false;
    }, error => {
      console.error(error);
      this.cargar = false;
    });
  }
}
