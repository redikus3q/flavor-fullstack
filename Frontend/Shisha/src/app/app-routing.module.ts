import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('src/app/modules/flavors/flavors.module').then(m => m.FlavorsModule),
  },
  {
    path: 'aboutus',
    //canActivate: [AuthGuard],
    loadChildren: () => import('src/app/modules/aboutus/aboutus.module').then(m => m.AboutusModule)
  },
  {
    path: 'auth',
    loadChildren: () => import('src/app/modules/auth/auth.module').then(m => m.AuthModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
