using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS04.Models; //Adicionado
using WS04.Services; //Adicionado

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WS04.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastrar : ContentPage
    {
        public Paciente paciente;
        public PageCadastrar()
        {
            InitializeComponent();
        }

        public PageCadastrar(Paciente paciente)
        {
            InitializeComponent();
            btnSalvar.Text = "ATUALIZAR";
            txtId.IsVisible = true;
            btnExcluir.IsVisible = true;
            btnExcluir.IsEnabled = true;
            txtId.Text = paciente.Id.ToString();
            txtNomePaciente.Text = paciente.Nome_Paciente;
            txtNomeDoutor.Text = paciente.Nome_Doutor;
            pckTipoQuarto.SelectedItem = paciente.Tipo_quarto; 
            txtQuarto.Text = paciente.Quarto.ToString();
        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Paciente paciente = new Paciente();
                paciente.Nome_Paciente = txtNomePaciente.Text;
                paciente.Nome_Doutor = txtNomeDoutor.Text;
                paciente.Tipo_quarto = pckTipoQuarto.SelectedItem.ToString();
                paciente.Quarto = Convert.ToInt32(txtQuarto.Text);
                ServicesDB dB = new ServicesDB(App.DbPath);
                if (btnSalvar.Text == "Salvar")
                {
                    dB.Inserir(paciente);
                    DisplayAlert("Cadastro!", dB.StatusMessage, "OK");
                    Navigation.PushAsync(new PageListar());
                }
                else
                {
                    paciente.Id = Convert.ToInt32(txtId.Text);
                    dB.Editar(paciente);
                    DisplayAlert("Edição!", dB.StatusMessage, "OK");
                    Navigation.PushAsync(new PageListar());
                }
                
            }
            catch (Exception erro)
            {
                DisplayAlert("Erro",erro.Message, "OK");
                
            }
        }

        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Exclusão", "Deseja Excluir este cadastro?", "Sim", "Não");
            if (resp == true)
            {
                ServicesDB db = new ServicesDB(App.DbPath);
                int Id = Convert.ToInt32(txtId.Text);
                db.Excluir(Id);
                await DisplayAlert("Exlcusão", db.StatusMessage, "Ok");
            }
           await Navigation.PushAsync(new PageHome());

        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageHome());
        }
    }
}