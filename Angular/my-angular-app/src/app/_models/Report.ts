export class Report {
    constructor(
        public id: number = 0,
        public location: string = "",
        public img: string = "",
        public date: Date = new Date(),
        public userId: string = "",
        public speciesId: number = 0,
        public speciesName: string=""
    ) {}
}
