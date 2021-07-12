/* Options:
Date: 2021-07-12 23:36:23
Version: 5.111
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: https://localhost:5001

//GlobalNamespace: 
//MakePropertiesOptional: False
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//AddDescriptionAsComments: True
//IncludeTypes: 
//ExcludeTypes: 
//DefaultImports: 
*/


export interface IReturn<T>
{
    createResponse(): T;
}

export interface IReturnVoid
{
    createResponse(): void;
}

export enum Priority
{
    Low = 1,
    Normal = 2,
    High = 3,
    Urgent = 4,
}

export enum XRayType
{
    UpperBody = 1,
    LowerBody = 2,
    Cardio = 3,
    Oral = 4,
    Lungs = 5,
}

export class Appointment
{
    public id: number;
    public dateofAppointment: string;
    public dateSent: string;
    public patientId: number;
    public priority: Priority;
    public reason: string;
    public xRayType: XRayType;
    public doctorId: number;

    public constructor(init?: Partial<Appointment>) { (Object as any).assign(this, init); }
}

export class HelloResponse
{
    public result: string;

    public constructor(init?: Partial<HelloResponse>) { (Object as any).assign(this, init); }
}

export class XRayResponse
{
    public success: boolean;
    public xRayAppointment: Appointment;

    public constructor(init?: Partial<XRayResponse>) { (Object as any).assign(this, init); }
}

export class DeleteAppointmentResponse
{
    public success: boolean;

    public constructor(init?: Partial<DeleteAppointmentResponse>) { (Object as any).assign(this, init); }
}

// @Route("/hello")
// @Route("/hello/{Name}")
export class Hello implements IReturn<HelloResponse>
{
    public name: string;

    public constructor(init?: Partial<Hello>) { (Object as any).assign(this, init); }
    public createResponse() { return new HelloResponse(); }
    public getTypeName() { return 'Hello'; }
}

// @Route("/XrayRequest")
export class XRayRequest implements IReturn<XRayResponse>
{
    public priority: Priority;
    public description: string;
    public dateSent: string;
    public recommendedDate: string;
    public xRayType: XRayType;

    public constructor(init?: Partial<XRayRequest>) { (Object as any).assign(this, init); }
    public createResponse() { return new XRayResponse(); }
    public getTypeName() { return 'XRayRequest'; }
}

// @Route("/DeleteXRay")
export class DeleteAppointmentRequest implements IReturn<DeleteAppointmentResponse>
{

    public constructor(init?: Partial<DeleteAppointmentRequest>) { (Object as any).assign(this, init); }
    public createResponse() { return new DeleteAppointmentResponse(); }
    public getTypeName() { return 'DeleteAppointmentRequest'; }
}

