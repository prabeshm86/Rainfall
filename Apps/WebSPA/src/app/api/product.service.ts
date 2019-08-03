import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../models/product';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseUrl: string = "https://localhost:2001/api/v1/product";
  constructor(private httpClient: HttpClient) { }

  getProducts(size: number, start: number): Observable<Product[]>  {
    return this.httpClient.get<Product[]>(`${this.baseUrl}`);
  }
}
