import { Component, OnInit } from '@angular/core';
import { ApiService } from './services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Fizikal-client';
  allCustomers: any;
  allCities: any;

  constructor(private apiService: ApiService){}

  ngOnInit(): void {
    this.apiService.GetData().subscribe((response) => {
      this.allCustomers = response.customersLst;
      this.allCities = response.citiesLst;
    });
  }

  getCityName(cityId: any){
    for(let city of this.allCities){
      if(city.id == cityId){
        return city.name;
      }
    }
  }
}
