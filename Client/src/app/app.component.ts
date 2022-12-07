
import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';
import { BusyService } from './core/services/busy.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
    
  title = 'Skinet';

  constructor(private basketService: BasketService,
    private accountService : AccountService) {}

  ngOnInit(): void {
   this.loadBasket(); 
   this.loadCurrentUSer();
  }

  loadCurrentUSer() {
    const token = localStorage.getItem('token'); 
      this.accountService.loadCurrentUser(token)
      .subscribe(
        () => console.log('user loaded') , 
        (error) => console.log(error)
      )
  }

  loadBasket() {
    const basketId = localStorage.getItem("basket_id");
    if(basketId)
      this.basketService.getBasket(basketId).subscribe(
        () => "Initialized", 
        (error) => console.log(error)
      ); 
  }

}
