import { Gift } from "./gift.models";
import { User } from "./user.models";

export class Order {
    //כלל חשוב השמות במודל באותיות קטנות חשוב מאד
    orderId!:number;
    userId!:number;
    gigtId!:number;
    gift?:Gift;
    user?:User;

}