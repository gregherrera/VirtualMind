import { APP_INITIALIZER, NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ListaCompraComponent } from './lista-compra/lista-compra.component';
import { CotizacionesComponent } from './cotizaciones/cotizaciones.component';
import { NgbdModalBasic } from './modal-basic/modal-basic';
import { AppSettingsService } from './services/appsettings.service';

export const configFactory = (appService: AppSettingsService) => {
  return () => appService.loadSettings();
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ListaCompraComponent,
    CotizacionesComponent,
    NgbdModalBasic
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    NgbModule,
    MatSnackBarModule
  ],
  providers: [ 
    {      
      provide: APP_INITIALIZER,
      useFactory: configFactory,
      deps: [AppSettingsService],
      multi: true      
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
