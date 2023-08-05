import { Report } from "./Report";

export class Client {
    constructor(
        public userName: string = "",
        public email: string = "",
        public phone: string = "",
        public reports: Report[] = []
    ) {}
}