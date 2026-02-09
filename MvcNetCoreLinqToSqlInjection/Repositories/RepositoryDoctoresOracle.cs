using Microsoft.AspNetCore.Http.HttpResults;
using MvcNetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static Azure.Core.HttpHeader;

#region PROCEDURE
//create procedure SP_DELETE_DOCTOR
//(p_iddoctor DOCTOR.DOCTOR_NO%TYPE)
//AS
//BEGIN
//    DELETE FROM DOCTOR WHERE DOCTOR_NO = p_iddoctor;
//COMMIT;
//END;

//EXECUTE SP_DELETE_DOCTOR(121);

//create procedure SP_UPDATE_DOCTOR
//(p_idhospital DOCTOR.HOSPITAL_COD%TYPE, p_apellido DOCTOR.APELLIDO%TYPE, p_especialidad DOCTOR.ESPECIALIDAD%TYPE,
//p_salario DOCTOR.SALARIO%TYPE, p_iddoctor DOCTOR.DOCTOR_NO%TYPE)
//AS
//BEGIN
//    update DOCTOR set HOSPITAL_COD=p_idhospital, APELLIDO = p_apellido, ESPECIALIDAD = p_especialidad,
//    SALARIO = p_salario where DOCTOR_NO=p_iddoctor;
//COMMIT;
//END;

//EXECUTE SP_UPDATE_DOCTOR(22, 'Alonso', 'Pediatria', 1, 120);
#endregion

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresOracle: IRepositoryDoctores
    {
        private DataTable tablaDoctor;
        private OracleConnection cn;
        private OracleCommand com;

        public RepositoryDoctoresOracle()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/FREEPDB1; Persist Security Info=true;User Id=SYSTEM;Password=oracle";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            this.tablaDoctor = new DataTable();
            string sql = "select * from DOCTOR";
            OracleDataAdapter ad = new OracleDataAdapter(sql, this.cn);
            ad.Fill(this.tablaDoctor);
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable() select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doctor = new Doctor();
                doctor.IdDoctor = row.Field<int>("DOCTOR_NO");
                doctor.Apellido = row.Field<string>("APELLIDO");
                doctor.Especialidad = row.Field<string>("ESPECIALIDAD");
                doctor.Salario = row.Field<int>("SALARIO");
                doctor.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctores.Add(doctor);
            }
            return doctores;
        }

        public async Task InsertDoctoresAsync(int idHospital, int idDoctor, string apellido, string especialidad, int salario)
        {
            string sql = "insert into DOCTOR values (:idHospital, :id, :apellido, :especialidad, :salario)";
            
            OracleParameter pamIdHospital = new OracleParameter(":idHospital", idHospital);
            OracleParameter pamIdDoctor = new OracleParameter(":id", idDoctor);
            OracleParameter pamApellido = new OracleParameter(":apellido", apellido);
            OracleParameter pamEspecialidad = new OracleParameter(":especialidad", especialidad);
            OracleParameter pamSalario = new OracleParameter(":salario", salario);

            this.com.Parameters.Add(pamIdHospital);
            this.com.Parameters.Add(pamIdDoctor);
            this.com.Parameters.Add(pamApellido);
            this.com.Parameters.Add(pamEspecialidad);
            this.com.Parameters.Add(pamSalario);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeleteDoctorAsync(int idDoctor)
        {
            string sql = "SP_DELETE_DOCTOR";
            OracleParameter pamIdDoctor = new OracleParameter(":p_iddoctor", idDoctor);
            this.com.Parameters.Add(pamIdDoctor);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public Doctor GetDoctorPorId(int id)
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable() where datos.Field<int>("DOCTOR_NO") == id select datos;

            var row = consulta.First();
            Doctor doctor = new Doctor();
            doctor.IdHospital = row.Field<int>("HOSPITAL_COD");
            doctor.IdDoctor = row.Field<int>("DOCTOR_NO");
            doctor.Apellido = row.Field<string>("APELLIDO");
            doctor.Especialidad = row.Field<string>("ESPECIALIDAD");
            doctor.Salario = row.Field<int>("SALARIO");
            return doctor;
            
        }

        public async Task UpdateDoctorAsync(int idHospital, string apellido, string especialidad, int salario, int idDoctor)
        {
            string sql = "SP_UPDATE_DOCTOR";

            this.com.Parameters.Add(":p_idhospital", idHospital);
            this.com.Parameters.Add(":p_apellido", apellido);
            this.com.Parameters.Add(":p_especialidad", especialidad);
            this.com.Parameters.Add(":p_salario", salario);
            this.com.Parameters.Add(":p_iddoctor", idDoctor);

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public List<Doctor> GetDoctorPorEspecialidad(string especialidad)
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable() where (datos.Field<string>("ESPECIALIDAD")).ToUpper().StartsWith(especialidad.ToUpper()) select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doctor = new Doctor();
                doctor.IdDoctor = row.Field<int>("DOCTOR_NO");
                doctor.Apellido = row.Field<string>("APELLIDO");
                doctor.Especialidad = row.Field<string>("ESPECIALIDAD");
                doctor.Salario = row.Field<int>("SALARIO");
                doctor.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctores.Add(doctor);
            }
            return doctores;
        }
    }
}
