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
    [QueryProperty(nameof(GradoDetail), "GradoDetail")]
    public partial class AddUpdateGradoDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        private GradoModel _gradoDetail = new GradoModel();

        private readonly IGradoService _gradoService;
        public AddUpdateGradoDetailViewModel(IGradoService gradoService)
        {
            _gradoService = gradoService;
        }

        [ICommand]
        public async void AddUpdateGrado()
        {
            int response = -1;
            if (GradoDetail.Descripcion==null)
            {
                await Shell.Current.DisplayAlert("No agrego Ningun valor ", "Vuelva a digitar la informacion", "Aceptar");
            }
            //else if (GradoDetail.Descripcion == GradoDetail.Descripcion)
            //{
              //  await Shell.Current.DisplayAlert("El grado ya existe", "Vuelva a digitar la informacion", "Aceptar");
           // }
            else if (GradoDetail.GradoID > 0)
            {
                response = await _gradoService.UpdateGrado(GradoDetail);
            }
            else
            {
                response = await _gradoService.AddGrado(new Models.GradoModel
                {

                    Descripcion = GradoDetail.Descripcion,
                    
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
