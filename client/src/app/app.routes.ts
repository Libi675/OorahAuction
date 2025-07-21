import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { GiftListComponent } from './components/gift-list/gift-list.component';
import { DonorListComponent } from './components/donor-list/donor-list.component';
import { AddDonorComponent } from './components/add-donor/add-donor.component';
import { UpdateDonorComponent } from './components/update-donor/update-donor.component';
import { GiftToUserComponent } from './components/gift-to-user/gift-to-user.component';
import { CartComponent } from './components/cart/cart.component';
import { AddGiftComponent } from './components/add-gift/add-gift.component';
import { GiftEditComponent } from './components/gift-edit/gift-edit.component';
import { PaymentComponent } from './components/payment/payment.component';


export const routes: Routes = [
    { path: '', redirectTo: 'app-home', pathMatch: 'full' },

    {
        path: 'app-home', component: HomeComponent, children: [
            { path: '', redirectTo: 'app-home', pathMatch: 'full' },
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
            { path: 'gift', component: GiftListComponent },
            { path: 'donor', component: DonorListComponent },
            { path: 'giftUser/:parm', component: GiftToUserComponent },
            { path: 'donor/add', component: AddDonorComponent },
            { path: 'donor/update/:id', component: UpdateDonorComponent },
            { path: 'gift/add', component: AddGiftComponent },
            { path: 'gift/update/:id', component: GiftEditComponent },
            { path: 'cart', component: CartComponent },
            { path: 'payment', component: PaymentComponent },


        ]
    }

];


