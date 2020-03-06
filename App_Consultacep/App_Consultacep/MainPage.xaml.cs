using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App_Consultacep.Servico.Modelo;
using App_Consultacep.Servico;

namespace App_Consultacep
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            cBotao.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EverntArgs args)
        {
            string cep = cCep.Text.Trim();

            if (isValidaCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnredecoVia(Cep);

                    if (end != null)
                    {
                        Cresultado.Text = string.format("Endereço:" + "{2} de {3} {0},{1}", end.localidade, end.uf, end.logradouro, end.bairro);
                    }

                    else
                    {
                        DisplayAlert("Erro", "O endereço não foi encontrado" + "para o CEP informado" + cep, "OK");
                    }
                }
                catch(Exception e)
                {
                    DisplayAlert("Erro Crítico", e.Message, "OK");
                }
            }
        }

        private bool isValidaCEP(string cep)
        {
            bool valido = true;

            if(Cep.length != 8)
            {
                DisplayAlert("Erro!", "Cep Inválido ou menor que 8 digitos", "OK");
                valido = false;
            }


            int NovoCEP = 0;
            if(!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("Erro!", "O cep deve ser composto apenas por números", "OK");
                valido = false;
            }

            return valido;
        }
    }   

}
