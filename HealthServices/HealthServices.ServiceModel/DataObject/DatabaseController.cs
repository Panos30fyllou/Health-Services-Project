using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HealthServices.ServiceModel.DataObject
{
    public static class DatabaseController
    {
        public static OrmLiteConnectionFactory dbFactory;

        public static bool Initialize(string connectionString)
        {
            dbFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);

            var db = dbFactory.OpenDbConnection();

            //Select
            //List<Appointment> appointments = db.Select<Appointment>();
            //Appointment app = db.Single<Appointment>(x => x.Id == 1);

            //Insert
            //db.Insert<Appointment>(new Appointment() { });

            //Delete
            //db.Delete<Appointment>(x => x.Reason == "malakia");

            //Update
            //Appointment ino = db.Single<Appointment>(x => x.Id == 2);
            //ino.Patient = new Patient();
            //db.Update(ino);

            //db.Close();
            return false;
        }
    }
}
