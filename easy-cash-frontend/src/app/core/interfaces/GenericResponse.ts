
export interface GenericResponse<Type> {
    statusCode: string;
    statusMessage: string;
    successful: boolean;
    responseObject: Type;
}
