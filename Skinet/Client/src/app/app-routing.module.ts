import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home/home.component';
import { ShopModule } from './shop/shop.module';
import { BasketModule } from './basket/basket.module';
import { CheckoutModule } from './checkout/checkout.module';
import { AccountModule } from './account/account.module';
import { AccountRoutingModule } from './account/account-routing.module';
import { AuthGuard } from './core/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'home'}},
  {path: 'test-error', component: TestErrorComponent,data: {breadcrumb: 'test-error'}},
  {path: 'server-error', component: ServerErrorComponent, data: {breadcrumb: 'server-error'}},
  {path: 'not-found', component: NotFoundComponent, data: {breadcrumb: 'not-error'}},
  {path: 'shop', loadChildren: () => {return ShopModule;}, data: {breadcrumb: 'shop'}},
  {path: 'basket', loadChildren: () => {return BasketModule; }, data: {breadcrumb: 'basket'}},
  {path: 'checkout',
   loadChildren: () => {return CheckoutModule; },
   canActivate: [AuthGuard],
   data: {breadcrumb: 'checkout'}},
  {path: 'account', loadChildren: () => {return AccountModule; }, data: {skip: true}},
  {path: '**', redirectTo: 'not-found', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],

})
export class AppRoutingModule { }
