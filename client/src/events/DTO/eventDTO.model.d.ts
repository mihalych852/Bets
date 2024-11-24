export interface eventDTO {
    id: number;
    description: string;
    eventOutcomes?: eventOutcomeDTO[];
    closingDate: Date;
}