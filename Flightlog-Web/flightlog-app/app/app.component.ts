import {Component, OnInit} from 'angular2/core';
import {Flight} from './flight';
import {FlightlogService} from './flightlog.service';

@Component({
	selector: 'flightlog-app',
	template: `<h1>flights</h1>
		<ul>
		<li *ngFor="#flight of flights">
			{{flight.DepartureAirport}} -> {{flight.DestinationAirport}}
		</li>
		</ul>`,
	providers: [FlightlogService]
})

export class AppComponent implements OnInit {
	private flights: Flight[];

	constructor(private _flightlogService: FlightlogService) {
	}

	ngOnInit() {
		this._flightlogService.getFlights().then(flights => this.flights = flights);
	}
}
