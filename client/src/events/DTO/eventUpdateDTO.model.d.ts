export interface eventUpdateDTO {
    id: string;
    description: string;
    eventStartTime: Date;
    betsEndTime: Date;
    modifiedBy?: string;
    status: number;
}