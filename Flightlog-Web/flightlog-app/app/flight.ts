export interface Flight {
	Id: number,
	Tailnumber: string,
	Type: string,
	Crew: string,
	DepartureAirport: string,
	DestinationAirport: string,
	DepartureTime: Date,
	ArrivalTime: Date,
	DayLandings: number,
	NightLandings: number,
	FlightTime: number,
	PICTime: number,
	DualTime: number,
	NightTime: number,
	IFRTime: number,
	Remarks: string
}
