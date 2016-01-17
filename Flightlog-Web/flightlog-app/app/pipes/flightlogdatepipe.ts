import {Pipe} from 'angular2/core';

@Pipe({name: 'fldate'})
export class FlightlogDatePipe {

	transform(value:Date, args:string[]) : any {

		var s = value.getFullYear() + '';
		s += '-';
		s += this.twodigits(value.getMonth() + 1);
		s += '-';
		s += this.twodigits(value.getDate());
		return s;
	}

	private twodigits(n:number): string {
		return (n < 10 ? '0' : '') + n;
	}
}


