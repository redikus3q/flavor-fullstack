import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit, OnDestroy {

  public user : any;
  private subscription : Subscription | any;

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.getUser();
  }

  ngOnDestroy(): void { 
    this.subscription.unsubscribe();
  }

  goToFlavors(): void {
    this.router.navigate(['/flavors']);
  }

  goToAboutus(): void {
    this.router.navigate(['/aboutus']);
  }

  goToLogin(): void {
    this.router.navigate(['/auth/login']);
  }

  goToRegister(): void {
    this.router.navigate(['/auth/register']);
  }

  checkToken(): boolean {
    if(this.user == undefined){
      return false;
    }
    return true;
  }

  getUser(): void {
    this.subscription = this.authService.getUser().subscribe(result => {
      this.user = result;
    });
  }

  logOut(): void {
    localStorage.setItem("Token", "");
    location.reload();
  }

  goToProfile(): void {
    this.router.navigate(['/user/profile']);
  }

}
