using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;
using System.Threading.Tasks;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        private IRepositoryDoctores repo;

        public DoctoresController(IRepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            await this.repo.InsertDoctoresAsync(doctor.IdHospital, doctor.IdDoctor, doctor.Apellido, doctor.Especialidad, doctor.Salario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.DeleteDoctorAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Doctor doctor = this.repo.GetDoctorPorId(id);
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Doctor doctor)
        {
            await this.repo.UpdateDoctorAsync(doctor.IdHospital, doctor.Apellido, doctor.Especialidad, doctor.Salario, doctor.IdDoctor);
            return RedirectToAction("Index");
        }
        public IActionResult DoctoresEspecialidad()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        [HttpPost]
        public async Task<IActionResult> DoctoresEspecialidad(string especialidad)
        {
            List<Doctor> doctores = this.repo.GetDoctorPorEspecialidad(especialidad);
            return View(doctores);
        }
    }
}
