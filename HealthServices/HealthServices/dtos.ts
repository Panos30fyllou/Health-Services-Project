/* Options:
Date: 2021-07-08 13:59:02
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

export enum Gender
{
    Man = 1,
    Woman = 2,
}

export class Patient
{
    public id: number;
    public name: string;
    public surname: string;
    public fathersName: string;
    public mothersName: string;
    public healthID: string;
    public gender: Gender;
    public dateOfBirth: string;
    public street: string;
    public number: number;
    public postalCode: number;
    public numbersOfContact: string[];

    public constructor(init?: Partial<Patient>) { (Object as any).assign(this, init); }
}

export class Doctor
{
    public id: string;
    public name: string;
    public surname: string;
    public department: string;

    public constructor(init?: Partial<Doctor>) { (Object as any).assign(this, init); }
}

export class Appointment
{
    public id: number;
    public dateofAppointment: string;
    public setDate: string;
    public patient: Patient;
    public priority: Priority;
    public reason: string;
    public xRayType: XRayType;
    public doctor: Doctor;

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
    public appointment: Appointment;

    public constructor(init?: Partial<XRayResponse>) { (Object as any).assign(this, init); }
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

export class XRayRequest implements IReturn<XRayResponse>
{
    public priority: Priority;
    public description: string;
    public setDate: string;
    public recomendedDate: string;
    public xRayType: XRayType;

    public constructor(init?: Partial<XRayRequest>) { (Object as any).assign(this, init); }
    public createResponse() { return new XRayResponse(); }
    public getTypeName() { return 'XRayRequest'; }
}

