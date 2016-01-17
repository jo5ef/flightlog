import {Pipe} from 'angular2/core';

@Pipe({name: 'fltime'})
export class FlightTimePipe {

	transform(value:number, args:string[]) : any {

		var s = this.twodigits(Math.floor(value / 60));
		s += ':';
		s += this.twodigits(value % 60);
		return s;
	}

	private twodigits(n:number): string {
		return (n < 10 ? '0' : '') + n;
	}
}
