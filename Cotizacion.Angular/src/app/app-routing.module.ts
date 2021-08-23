import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CotizacionesComponent } from './cotizaciones/cotizaciones.component';
import { HomeComponent } from './home/home.component';
import { ListaCompraComponent } from './lista-compra/lista-compra.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'cotizaciones', component: CotizacionesComponent },
  { path: 'lista-compra', component: ListaCompraComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
