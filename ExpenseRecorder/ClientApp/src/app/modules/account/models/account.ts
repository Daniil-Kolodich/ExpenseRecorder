export interface SignIn {
	userName : string;
	password : string;
}

export interface SignInResponse {
	token : string;
	userName : string;
}

export interface SignUp {
	userName : string;
	email : string;
	password : string;
}

export interface SignUpResponse {
	userName: string;
	email: string;
}
