export interface eventDTO {
    id: string;
    description: string;
    eventOutcomes?: eventOutcomeDTO[];
    betsEndTime: Date;
    eventStartTime: Date;
    status: eventsStatus;
}
