using Microsoft.Toolkit.Mvvm.ComponentModel;
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
    public partial class MateriaListPageViewModel : ObservableObject
    {
        public static List<MateriaModel> MateriasListForSearch { get; private set; } = new List<MateriaModel>();
        public ObservableCollection<MateriaModel> Materia { get; set; } = new ObservableCollection<MateriaModel>();

        private readonly IMateriaService _materiaService;
        public MateriaListPageViewModel(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }



        [ICommand]
        public async void GetMateriaList()
        {
            Materia.Clear();
            var materiaList = await _materiaService.GetMateriaList();
            if (materiaList?.Count > 0)
            {
                foreach (var materia in materiaList)
                {
                    Materia.Add(materia);
                }
                MateriasListForSearch.Clear();
                MateriasListForSearch.AddRange(materiaList);
            }
        }


        [ICommand]
        public async void AddUpdateMateria()
        {
            await AppShell.Current.GoToAsync(nameof(AddUpdateMateriaDetail));
        }


        [ICommand]
        public async void DisplayAction(MateriaModel materiaModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Seleccione", "Aceptar", null, "Editar", "Eliminar","Cancelar");
            if (response == "Editar")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("MateriaDetail", materiaModel);
                await AppShell.Current.GoToAsync(nameof(AddUpdateMateriaDetail), navParam);
            }
            else if (response == "Eliminar")
            {
                var delResponse1 = await AppShell.Current.DisplayActionSheet("¿Esta seguro de eliminar la materia? ", "Si", "No");
                if (delResponse1 == "Si")
                {
                    var delResponse = await _materiaService.DeleteMateria(materiaModel);
                    await Shell.Current.DisplayAlert("El registro fue eliminado", "Eliminado", "Aceptar");
                    GetMateriaList();
                }
                else if (delResponse1 == "No")
                {
                    GetMateriaList();
                }
            }
        }
    }
}
