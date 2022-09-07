﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SQLiteDemo.Models;
using SQLiteDemo.Services;
using SQLiteDemo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.ViewModels
{
    public partial class StudentListPageViewModel : ObservableObject
    {
        public static List<StudentModel> StudentsListForSearch { get; private set; } = new List<StudentModel>();
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();

        private readonly IStudentService _studentService;
        public StudentListPageViewModel(IStudentService studentService)
        {
            _studentService = studentService;
        }



        [ICommand]
        public async void GetStudentList()
        {
            Students.Clear();
            var studentList = await _studentService.GetStudentList();
            if (studentList?.Count > 0)
            {
                studentList = studentList.OrderBy(f => f.FullName).ToList();
                foreach (var student in studentList)
                {
                    Students.Add(student);
                }
                StudentsListForSearch.Clear();
                StudentsListForSearch.AddRange(studentList);
            }
        }


        [ICommand]
        public async void AddUpdateStudent()
        {
            await AppShell.Current.GoToAsync(nameof(AddUpdateStudentDetail));
        }


        [ICommand]
        public async void DisplayAction(StudentModel studentModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Seleccione", "Aceptar", null, "Editar", "Eliminar","Cancelar");
            if (response == "Editar")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("StudentDetail", studentModel);
                await AppShell.Current.GoToAsync(nameof(AddUpdateStudentDetail), navParam);
            }
            else if (response == "Eliminar")
            {
                var delResponse1 = await AppShell.Current.DisplayActionSheet("¿Esta seguro de eliminar el estudiante ? ", "Si", "No");
                if (delResponse1 == "Si")
                {
                    var delResponse = await _studentService.DeleteStudent(studentModel);
                    await Shell.Current.DisplayAlert("El registro fue eliminado", "Eliminado", "Aceptar");
                    GetStudentList();
                }
                else if (delResponse1 == "No")
                {
                    GetStudentList();
                }
            }
        }
    }
}
