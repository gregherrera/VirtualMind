import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { CompraInsert } from '../interface/comprainsert';
import { ListaCompraComponent } from '../lista-compra/lista-compra.component';
import { ApiCompraService } from '../services/apicompra.service';
import { ApiMonedaService } from '../services/apimoneda.service';

@Component({
    selector: 'ngbd-modal-basic',
    templateUrl: './modal-basic.html',
    styleUrls: ['./modal-basic.scss']
})
export class NgbdModalBasic implements OnInit {
    @Input() compra: CompraInsert = new CompraInsert();
    lista: any[] = [];

    closeResult = '';

    constructor(private modalService: NgbModal,
        private apiMoneda: ApiMonedaService,
        private apiCompra: ApiCompraService,
        private snackBar: MatSnackBar,
        private listaCompra: ListaCompraComponent
    ) { }

    ngOnInit(): void {
        this.getMonedas();
    }

    getMonedas(): void {
        this.apiMoneda.getMonedas().subscribe(result => {
            this.lista = result.data;
        }, error => console.error(error));
    }

    open(content: any) {
        this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', keyboard: false, backdrop: 'static' }).result.then((result) => {
            this.apiCompra.Add(this.compra).subscribe(result => {
                if (result.success === 1) {
                    this.snackBar.open("Money exchange realized successfully!", "", {
                        duration: 2000,
                        verticalPosition: 'top',
                        panelClass: ['mat-accent']
                    });

                    this.listaCompra.getCompras();

                    this.compra.idusuario = 0;
                    this.compra.idmoneda = 1;
                    this.compra.monto = 0;
                }
                else {
                    this.snackBar.open(result.message, "", {
                        duration: 2000,
                        verticalPosition: 'top',
                        panelClass: ['mat-warning']
                    });
                }
            }, error => {
                this.snackBar.open(error.error.message, "", {
                    duration: 2000,
                    verticalPosition: 'top',
                    panelClass: ['mat-warning']
                });
            });
        }, (reason) => {
            this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        });
    }

    private getDismissReason(reason: any): string {
        if (reason === ModalDismissReasons.ESC) {
            return 'by pressing ESC';
        } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
            return 'by clicking on a backdrop';
        } else {
            return `with: ${reason}`;
        }
    }
}