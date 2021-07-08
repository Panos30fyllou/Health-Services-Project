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
(function (factory) {
    if (typeof module === "object" && typeof module.exports === "object") {
        var v = factory(require, exports);
        if (v !== undefined) module.exports = v;
    }
    else if (typeof define === "function" && define.amd) {
        define(["require", "exports"], factory);
    }
})(function (require, exports) {
    "use strict";
    exports.__esModule = true;
    exports.XRayRequest = exports.Hello = exports.XRayResponse = exports.HelloResponse = exports.Appointment = exports.Doctor = exports.Patient = exports.Gender = exports.XRayType = exports.Priority = void 0;
    var Priority;
    (function (Priority) {
        Priority[Priority["Low"] = 1] = "Low";
        Priority[Priority["Normal"] = 2] = "Normal";
        Priority[Priority["High"] = 3] = "High";
        Priority[Priority["Urgent"] = 4] = "Urgent";
    })(Priority = exports.Priority || (exports.Priority = {}));
    var XRayType;
    (function (XRayType) {
        XRayType[XRayType["UpperBody"] = 1] = "UpperBody";
        XRayType[XRayType["LowerBody"] = 2] = "LowerBody";
        XRayType[XRayType["Cardio"] = 3] = "Cardio";
        XRayType[XRayType["Oral"] = 4] = "Oral";
        XRayType[XRayType["Lungs"] = 5] = "Lungs";
    })(XRayType = exports.XRayType || (exports.XRayType = {}));
    var Gender;
    (function (Gender) {
        Gender[Gender["Man"] = 1] = "Man";
        Gender[Gender["Woman"] = 2] = "Woman";
    })(Gender = exports.Gender || (exports.Gender = {}));
    var Patient = /** @class */ (function () {
        function Patient(init) {
            Object.assign(this, init);
        }
        return Patient;
    }());
    exports.Patient = Patient;
    var Doctor = /** @class */ (function () {
        function Doctor(init) {
            Object.assign(this, init);
        }
        return Doctor;
    }());
    exports.Doctor = Doctor;
    var Appointment = /** @class */ (function () {
        function Appointment(init) {
            Object.assign(this, init);
        }
        return Appointment;
    }());
    exports.Appointment = Appointment;
    var HelloResponse = /** @class */ (function () {
        function HelloResponse(init) {
            Object.assign(this, init);
        }
        return HelloResponse;
    }());
    exports.HelloResponse = HelloResponse;
    var XRayResponse = /** @class */ (function () {
        function XRayResponse(init) {
            Object.assign(this, init);
        }
        return XRayResponse;
    }());
    exports.XRayResponse = XRayResponse;
    // @Route("/hello")
    // @Route("/hello/{Name}")
    var Hello = /** @class */ (function () {
        function Hello(init) {
            Object.assign(this, init);
        }
        Hello.prototype.createResponse = function () { return new HelloResponse(); };
        Hello.prototype.getTypeName = function () { return 'Hello'; };
        return Hello;
    }());
    exports.Hello = Hello;
    var XRayRequest = /** @class */ (function () {
        function XRayRequest(init) {
            Object.assign(this, init);
        }
        XRayRequest.prototype.createResponse = function () { return new XRayResponse(); };
        XRayRequest.prototype.getTypeName = function () { return 'XRayRequest'; };
        return XRayRequest;
    }());
    exports.XRayRequest = XRayRequest;
});
