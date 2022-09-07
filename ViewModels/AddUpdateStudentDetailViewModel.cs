using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SQLiteDemo.Models;
using SQLiteDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.ViewModels
{
    [QueryProperty(nameof(StudentDetail), "StudentDetail")]
    public partial class AddUpdateStudentDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private StudentModel _studentDetail = new StudentModel();

        private readonly IStudentService _studentService;
        public AddUpdateStudentDetailViewModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [ICommand]
        public async void AddUpdateStudent()
        {
            int response = -1;
           // if (StudentDetail.Document == null)
           // {
            //    await Shell.Current.DisplayAlert("Documento del estudiante esta vacio ", "Digite la informacion correspondiente ", "Aceptar");
            //}
            if (StudentDetail.FirstName == null)
            {
                await Shell.Current.DisplayAlert("Nombre del estudiante esta vacio ", "Digite la informacion correspondiente ", "Aceptar");
            }
            else if (StudentDetail.LastName == null)
            {
                await Shell.Current.DisplayAlert("Apellidos del estudiante esta vacio ", "Digite la informacion correspondiente ", "Aceptar");
            }
            else if (StudentDetail.Email == null)
            {
                await Shell.Current.DisplayAlert("Email del estudiante esta vacio ", "Digite la informacion correspondiente ", "Aceptar");
            }
            if (StudentDetail.Phone == null)
            {
                await Shell.Current.DisplayAlert("Teléfono del estudiante esta vacio ", "Digite la informacion correspondiente ", "Aceptar");
            }
            else if (StudentDetail.StudentID > 0)
            {
                response = await _studentService.UpdateStudent(StudentDetail);
            }
            else
            {
                response = await _studentService.AddStudent(new Models.StudentModel
                {
                    Email = StudentDetail.Email,
                    Document = StudentDetail.Document,
                    FirstName = StudentDetail.FirstName,
                    LastName = StudentDetail.LastName,
                    Phone = StudentDetail.Phone,
                });
            }

        

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Registro Guardado Exitosamente", "Guardado", "Aceptar");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", "El registro no fue guardado", "Aceptar");
            }
        }

    }
}
