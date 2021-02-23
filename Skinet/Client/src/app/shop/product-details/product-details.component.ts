import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity: number = 1;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, private bcService: BreadcrumbService, private basketService: BasketService) { 
    this.bcService.set("@productDetails",'');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  decrementQuantity() {
    if(this.quantity > 1) {
      this.quantity--;
    }
  }

  incrementQuantity() {
    this.quantity++;
  }

  addToBasket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  loadProduct(): void {
    var id = this.activatedRoute.snapshot.paramMap.get('id');
    if(id) {
        this.shopService.getProductById(+id).subscribe(product => {
          this.product = product;
          this.bcService.set("@productDetails", this.product.name);
        },error => {
          console.log(error);
        });
    }
  }
}
