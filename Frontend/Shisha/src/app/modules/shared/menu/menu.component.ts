import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  public user : any;

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.getUser();
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
    this.authService.getUser().subscribe(result => {
      this.user = result;
    });
  }

  logOut(): void{
    localStorage.setItem("Token", "");
    location.reload();
  }

}
