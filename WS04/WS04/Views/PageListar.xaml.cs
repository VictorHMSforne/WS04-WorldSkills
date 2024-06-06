using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WS04.Models;
using WS04.Services;


namespace WS04.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListar : ContentPage
    {
        public PageListar()
        {
            InitializeComponent();
            ServicesDB dB = new ServicesDB(App.DbPath);
            ListaPacientes.ItemsSource = dB.Listar();
        }

        public void AtualizaLista()
        {
            string nome = "";
            if (txtNome.Text != null) nome = txtNome.Text;

            ServicesDB dB = new ServicesDB(App.DbPath);
            ListaPacientes.ItemsSource = dB.Localizar(nome);
        }

        private void btnLocalizar_Clicked(object sender, EventArgs e)
        {
            AtualizaLista();
        }

        private void ListaPacientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
                NavegarPaciente(e.SelectedItem as Paciente);
                
        }
        void NavegarPaciente(Paciente paciente)
        {
            PageCadastrar cadastrar = new PageCadastrar(paciente);
            cadastrar.paciente = paciente ;
            Navigation.PushAsync(cadastrar);
        }
    }
}