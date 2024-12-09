export interface eventOutcomeDTO{
    id: string;
    description: string;
    eventId: string;
    createdBy: string;
    betsClosed: boolean;
    isHappened?: boolean;
    currentOdd: number;
}