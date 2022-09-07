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
    public partial class SedeListPageViewModel : ObservableObject
    {
        public static List<SedeModel> SedesListForSearch { get; private set; } = new List<SedeModel>();
        public ObservableCollection<SedeModel> Sede { get; set; } = new ObservableCollection<SedeModel>();

        private readonly ISedeService _sedeService;
        public SedeListPageViewModel(ISedeService sedeService)
        {
            _sedeService = sedeService;
        }



        [ICommand]
        public async void GetSedeList()
        {
            Sede.Clear();
            var sedeList = await _sedeService.GetSedeList();
            if (sedeList?.Count > 0)
            {
                foreach (var sede in sedeList)
                {
                    Sede.Add(sede);
                }
                SedesListForSearch.Clear();
                SedesListForSearch.AddRange(sedeList);
            }
        }


        [ICommand]
        public async void AddUpdateSede()
        {
            await AppShell.Current.GoToAsync(nameof(AddUpdateSedeDetail));
        }


        [ICommand]
        public async void DisplayAction(SedeModel sedeModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Seleccione", "Aceptar", null, "Editar", "Eliminar","Cancelar");
            if (response == "Editar")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("SedeDetail", sedeModel);
                await AppShell.Current.GoToAsync(nameof(AddUpdateSedeDetail), navParam);
            }
            else if (response == "Eliminar")
            {
                var delResponse1 = await AppShell.Current.DisplayActionSheet("¿Esta seguro de eliminar la sede? ", "Si", "No");
                if (delResponse1 == "Si")
                {
                    var delResponse = await _sedeService.DeleteSede(sedeModel);
                    await Shell.Current.DisplayAlert("El registro fue eliminado", "Eliminado", "Aceptar");
                    GetSedeList();
                }
                else if (delResponse1 == "No")
                {
                    GetSedeList();
                }
            }
        }
    }
}
