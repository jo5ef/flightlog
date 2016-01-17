import {Component, OnInit} from 'angular2/core';
import {Flight} from './flight';
import {FlightlogService} from './flightlog.service';
import {FlightlogDatePipe} from './pipes/flightlogdatepipe';
import {FlightTimePipe} from './pipes/flighttimepipe';

@Component({
	selector: 'flightlog-app',
	templateUrl: './app/templates/app.html',
	providers: [FlightlogService],
	pipes: [FlightlogDatePipe, FlightTimePipe]
})

export class AppComponent implements OnInit {
	private flights: Flight[];

	constructor(private _flightlogService: FlightlogService) {
	}

	ngOnInit() {
		this._flightlogService.getFlights().then(flights => this.flights = flights);
	}
}
