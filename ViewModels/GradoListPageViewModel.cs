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
    public partial class GradoListPageViewModel : ObservableObject
    {
        public static List<GradoModel> GradosListForSearch { get; private set; } = new List<GradoModel>();
        public ObservableCollection<GradoModel> Grado { get; set; } = new ObservableCollection<GradoModel>();

        private readonly IGradoService _gradoService;
        public GradoListPageViewModel(IGradoService gradoService)
        {
            _gradoService = gradoService;
        }



        [ICommand]
        public async void GetGradoList()
        {
            Grado.Clear();
            var gradoList = await _gradoService.GetGradoList();
            if (gradoList?.Count > 0)
            {
                foreach (var grado in gradoList)
                {
                    Grado.Add(grado);
                }
                GradosListForSearch.Clear();
                GradosListForSearch.AddRange(gradoList);
            }
        }


        [ICommand]
        public async void AddUpdateGrado()
        {
            await AppShell.Current.GoToAsync(nameof(AddUpdateGradoDetail));
        }


        [ICommand]
        public async void DisplayAction(GradoModel gradoModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Seleccione", "Aceptar", null, "Editar", "Eliminar","Cancelar");
            if (response == "Editar")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("GradoDetail", gradoModel);
                await AppShell.Current.GoToAsync(nameof(AddUpdateGradoDetail), navParam);
            }
            else if (response == "Eliminar")
                
            {
                // var delResponse = await _gradoService.DeleteGrado(gradoModel);
                var delResponse1 = await AppShell.Current.DisplayActionSheet("¿Esta seguro de eliminar el grado? ", "Si", "No");
                if (delResponse1 == "Si")
                {
                     var delResponse = await _gradoService.DeleteGrado(gradoModel);
                    await Shell.Current.DisplayAlert("El registro fue eliminado", "Eliminado", "Aceptar");
                    GetGradoList();
                }
                else if (delResponse1 == "No")
                {
                    GetGradoList();
                }
            }
        }
    }
}
