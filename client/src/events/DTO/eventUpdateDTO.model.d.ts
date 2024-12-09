export interface eventUpdateDTO {
    id: string;
    description: string;
    //eventOutcomes?: eventOutcomeDTO[];
    eventStartTime: Date;
    betsEndTime: Date;
    modifyBy?: string;
    status: number;
}