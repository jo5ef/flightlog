import {Flight} from './flight';
import {Injectable} from 'angular2/core';
import {Http} from 'angular2/http';

@Injectable()
export class FlightlogService {

	private dummyData: Flight[];

	constructor(private http: Http) {
		this.dummyData = [{"departure_airport": "LOAV", "arrival_airport": "LOGF"}];
	}

	getFlights(): Promise<Flight[]> {
		return new Promise<Flight[]>(resolve => {
			this.http.get('http://localhost:2612/api/Flight')
				.subscribe(f => {
					console.log('subscribe!');
					resolve(f.json());
				});
		});
		/*	
		return new Promise<Flight[]>(resolve =>
			setTimeout(() => resolve(this.dummyData)));*/
	}
}
