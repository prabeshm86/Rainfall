import { Component, OnInit } from '@angular/core';
import { ProductService } from '../api/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.page.html',
  styleUrls: ['./product-list.page.scss'],
})
export class ProductListPage implements OnInit {

  constructor(private productService: ProductService) { }
  
  products: [];

  ngOnInit() {
    this.productService.getProducts(0, 0).subscribe((data: any)=> {
      this.products = data;
    });
  }

}
