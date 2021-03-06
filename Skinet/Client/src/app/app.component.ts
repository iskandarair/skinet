import { Component, OnInit } from '@angular/core';
import { BasketService} from 'src/app/basket/basket.service';
import { AccountService } from './account/account.service';
import { IUser } from './shared/models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title: string = 'Skinet';

  constructor(private basketService: BasketService, private accountService: AccountService) {}

  ngOnInit(): void {
    this.loadBasket();
    this.loadToken();
  }
  
  loadToken() {
    
    const token = localStorage.getItem("token");
    if(token) {
      this.accountService.loadCurrentUser(token).subscribe(
        (user : IUser) => {
          console.log('loaded user');
          console.log(user);
        },
        error => {
          console.log(error);
        },
        () => {

        }
      )
    }
  }
  loadBasket() {    
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
