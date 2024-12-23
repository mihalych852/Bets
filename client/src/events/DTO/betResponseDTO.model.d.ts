import { betStatus } from "./betStatus";

export interface betResponseDTO{
    id: string;
    bettorId: string;
    amount: number;
    eventOutcomesId: string;
    state: betStatus;
    description?: string;
}