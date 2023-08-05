export class RegisterDTO{
    constructor(
        public mobile:string,
        public name:string ,
        public email:string ,
        public gender:string="f",
        public password:string ,
        public confirmPassword: string,
        ){}

        
  }

