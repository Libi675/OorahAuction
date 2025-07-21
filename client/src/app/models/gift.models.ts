import { Categorya } from "./category.models";
import { Donor } from "./donor.models";

export class Gift {
    //כלל חשוב השמות במודל באותיות קטנות חשוב מאד
    giftId!: number;
    giftName: string = '';
    ticketPrice!: number;
    donorNameGift?: string;
    categoryName?: string;
    donorId?: number;
    categoryaId?:number;
    image?:string;
    donorsGift!:Donor;
    categorya!:Categorya

}
