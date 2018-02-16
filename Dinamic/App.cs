using System;
using System.Collections.Generic;
using Dinamic.Models;
using Xamarin.Forms;

namespace Dinamic
{
    public class App : Application
    {
        public App()
        {

            List<FieldView> oFields = new List<FieldView>();


            const string cGrupo04 = "Imagem";
            oFields.Add(new FieldView().SetImageField(cGrupo04, "Rsac Soluções", "www.rsacsolucoes.com.br", "http://www.rsacsolucoes.com.br/images/banners/banner1.png"));


            const string cGrupo01 = "Texto e Número";
            oFields.Add(new FieldView().SetTextField(cGrupo01, "Titulo qualquer", "informativo qualquer"));
            oFields.Add(new FieldView().SetEntryField(cGrupo01, "Serial", "numeros e letras"));
            oFields.Add(new FieldView().SetNumberField(cGrupo01, "Peso(Kg)", "numeros s/ limites"));
            oFields.Add(new FieldView().SetNumberField(cGrupo01, "Largura(cm)", "Entre 10 e 20", 10, 20));
            oFields.Add(new FieldView().SetSliderField(cGrupo01, "Conf(%0-100)", 0, 100));

            const string cGrupo05 = "Opções/Combo";
            oFields.Add(new FieldView().SetPickerField(cGrupo05, "Estados", "Informe o estado", "AC;AL;AP;AM;BA;CE;DF;ES;GO;MA;MT;MS;MG;PA;PB;PR;PE;PI;RJ;RN;RS;RO;RR;SC;SP;SE;TO"));

            const string cGrupo02 = "Data e Hora";
            oFields.Add(new FieldView().SetDatePickerField(cGrupo02, "Data"));
            oFields.Add(new FieldView().SetDatePickerField(cGrupo02, "Data(Lim:2018)", new DateTime(2018, 1, 1), new DateTime(2018, 12, 31)));
            oFields.Add(new FieldView().SetTimePickerField(cGrupo02, "Hora"));

            const string cGrupo03 = "Ligado/Desligado";
            oFields.Add(new FieldView().SetSwitchField(cGrupo03, "Ligado?"));
            oFields.Add(new FieldView().SetSwitchField(cGrupo03, "Default On", true));

            MainPage = new NavigationPage(new DinamicPage("Exemplo de Formulário", oFields));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
