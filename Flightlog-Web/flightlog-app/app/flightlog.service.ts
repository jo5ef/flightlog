import {Flight} from './flight';
import {Injectable} from 'angular2/core';
import {Http} from 'angular2/http';

@Injectable()
export class FlightlogService {


	constructor(private http: Http) {
	}

	getFlights(): Promise<Flight[]> {
		return new Promise<Flight[]>(resolve => {
			this.http.get('http://localhost:16525/api/Flight')
				.subscribe(f => {
					var flights = f.json();
					for(var i in flights) {
						this.fixFlight(flights[i]);
					}
					resolve(flights);
				});
		});
	}

	private fixFlight(f: any): void {
		f.DepartureTime = new Date(f.DepartureTime);
		f.ArrivalTime = new Date(f.ArrivalTime);
		f.FlightTime = this.parseFlightTime(f.FlightTime);
		f.PICTime = this.parseFlightTime(f.PICTime);
		f.DualTime = this.parseFlightTime(f.DualTime);
		f.NightTime = this.parseFlightTime(f.NightTime);
		f.IFRTime = this.parseFlightTime(f.IFRTime);
	}

	private parseFlightTime(s: string) {
		var parts = s.split(':');

		var totalMinutes = 0;
		totalMinutes += +parts[1];
		totalMinutes += 60 * +parts[0];

		return totalMinutes;
	}
}
