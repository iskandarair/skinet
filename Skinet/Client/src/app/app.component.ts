import { Component, OnInit } from '@angular/core';
import { BasketService} from 'src/app/basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title: string = 'Skinet';

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    const basketId = localStorage.getItem('basket_id'); 
    console.log(basketId);
    if(basketId) {
      this.basketService.getBasket(basketId).subscribe( () => {
        console.log("initialized basket");
      },
      error => {
        console.log(Error);
      })
    }
  }
    
}
