using MvcNetCoreLinqToSqlInjection.Models;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctores();

        Task InsertDoctoresAsync(int idHospital, int idDoctor, string apellido, string especialidad, int salario);

        Task DeleteDoctorAsync(int idDoctor);

        Doctor GetDoctorPorId(int id);

        Task UpdateDoctorAsync(int idHospital, string apellido, string especialidad, int salario, int idDoctor);

        List<Doctor> GetDoctorPorEspecialidad(string especialidad);
    }
}
