import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Flavor } from 'src/app/interfaces/flavor';
import { FlavorsService } from 'src/app/services/flavors.service';

@Component({
  selector: 'app-flavor',
  templateUrl: './flavor.component.html',
  styleUrls: ['./flavor.component.scss']
})
export class FlavorComponent implements OnInit, OnDestroy {

  public flavor: Flavor | undefined;
  public sub: any;
  public id: number | undefined;

  constructor(
    private route: ActivatedRoute,
    private flavorsService: FlavorsService
    ) {
  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id'];
      if(this.id){
        this.getFlavorDetails();
      }
    });
  }

  public getFlavorDetails(): void {
    this.flavorsService.getFlavorById(this.id).subscribe( (result) => {
      this.flavor = result;
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe(); 
  }

}
