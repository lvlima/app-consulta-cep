using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using app_consulta_cep.Servico.Modelo;
using app_consulta_cep.Servico;

namespace app_consulta_cep
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnBuscar.Clicked += BuscarCep;

        }

        private void BuscarCep(Object sender, EventArgs args)
        {
            string cep = inputCEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco endereco = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (endereco !=null)
                    {
                        txtCEP.Text = string.Format("Endereço: {0} {1}, {2} - {3}/{4} - {5}", endereco.logradouro, endereco.complemento, endereco.bairro, endereco.localidade, endereco.uf, endereco.cep);    
                    }
                    else
                    {
                        txtCEP.Text = "Não foi encontrado um endereço para o CEP informado.";
                    }



                } catch(Exception e)
                {
                    DisplayAlert("Erro", "Erro na conexão com o webservice. " + e.Message + ".", "OK");    
                }
            }

        }

        private bool isValidCEP(string cep)
        {
            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "O CEP deve possuir 8 caracteres.", "OK");
                return false;    
            }

            int novoCEP = 0;
            if (!int.TryParse(cep, out novoCEP))
            {
                DisplayAlert("Erro", "O CEP deve possuir apenas caracteres numéricos.", "OK");
                return false;
            }

            return true;
        }
    }
}
