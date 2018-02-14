using System;
using System.Collections.Generic;
using System.Diagnostics;
using Dinamic.Models;
using Xamarin.Forms;

namespace Dinamic
{
    public class DinamicPage : ContentPage
    {

        private List<FieldView> oFields; 
        public DinamicPage(string cFormTitle, List<FieldView> _oFields)
        {

            oFields = _oFields;

            //preenche com os valores default
            if (oFields == null)
            {
                const string cGrupo01 = "Grupo 01";
                const string cGrupo02 = "Grupo 02";

                oFields = new List<FieldView>();

                oFields.Add(new FieldView().SetText(cGrupo01, "Titulo1", "detalhe1"));
                oFields.Add(new FieldView().SetNumber(cGrupo01, "Altura(cm)", "altura entre 10-20", 10, 20));
                oFields.Add(new FieldView().SetNumber(cGrupo01, "Largura(cm)", "largura entre 10-20", 10, 20));
                oFields.Add(new FieldView().SetNumber(cGrupo01, "Peso(Kg)", "peso entre 100-200", 100, 200));


                oFields.Add(new FieldView().SetText(cGrupo02, "Titulo2", "detalhe2"));
                oFields.Add(new FieldView().SetNumber(cGrupo02, "Altura(cm)", "altura entre 10-20", 10, 20));
                oFields.Add(new FieldView().SetNumber(cGrupo02, "Largura(cm)", "largura entre 10-20", 10, 20));
                oFields.Add(new FieldView().SetNumber(cGrupo02, "Peso(Kg)", "peso entre 100-200", 100, 200));

            };



            Title = cFormTitle;


            TableView oTableView = new TableView();
            oTableView.Intent = TableIntent.Form;
            //oTableView.RowHeight = 100;
            //oTableView.HasUnevenRows = true;

            TableRoot oTableRoot = new TableRoot();

            TableSection oTableSectionCurrent = new TableSection();

           

            string cGroupAux = "";
            foreach (var oField in oFields)
            {
                //se mudou o grupo
                if (!cGroupAux.Equals(oField.GroupTitle))
                {
                    //adiciona uma nova sessao
                    cGroupAux = oField.GroupTitle;
                    oTableSectionCurrent = new TableSection(oField.GroupTitle);
                    oTableRoot.Add(oTableSectionCurrent);
                }

                switch (oField.FieldType)
                {

                    case FieldTypeEnum.Image:
                        break;
                    case FieldTypeEnum.Text:


                        TextCell oTextCell = new TextCell
                        {
                            Text = oField.Title,
                            Detail = oField.Detail,
                        };

                        oTableSectionCurrent.Add(oTextCell);

                        break;
                    case FieldTypeEnum.Switch:
                        break;
                    case FieldTypeEnum.Entry:
                        
                        EntryCell oEntryCell = new EntryCell
                        {
                            Label = oField.Title,
                            Placeholder = oField.Detail,
                            Keyboard = Keyboard.Text,
                        };


                        oEntryCell.SetBinding(EntryCell.TextProperty, "Value");
                        oEntryCell.BindingContext = oField;


                        oTableSectionCurrent.Add(oEntryCell);    

                        break;
                    case FieldTypeEnum.DatePicker:
                        break;
                    case FieldTypeEnum.TimePicker:
                        break;
                    case FieldTypeEnum.Picker:
                        break;
                    case FieldTypeEnum.Slider:
                        break;
                    case FieldTypeEnum.Number:

                        EntryCell oNumberCell = new EntryCell
                        {
                            Label = oField.Title,
                            Placeholder = oField.Detail,
                            Keyboard = Keyboard.Numeric,
                        };


                        oNumberCell.SetBinding(EntryCell.TextProperty, "Value");
                        oNumberCell.BindingContext = oField;

                        oNumberCell.Completed += (sender, e) => {
                            oField.ValidatorCompletedCommand?.Execute(null);
                        };


                        oTableSectionCurrent.Add(oNumberCell);

                        break;
                }

            }

            oTableView.Root = oTableRoot;
            Content = oTableView;
            this.ToolbarItems.Add(new ToolbarItem() { Text = "Gravar", Order = ToolbarItemOrder.Primary, Command = new Command(ToolbarItemCommand), CommandParameter = "1" });


        }




        private async void ToolbarItemCommand()
        {

            string cMsg = "";
           //executa o processo de gravacao
            foreach (var oField in oFields)
            {

                //se existe validaçao executa a mesma
                oField.ValidatorCompletedCommand?.Execute(null);

                //se o campo nao for valido nao continua com o processo
                if (!oField.IsValid)
                    return;
                    
                cMsg += $"{oField.Title}={oField.Value}" +Environment.NewLine;

            }

            Application.Current.MainPage.DisplayAlert("Dados Gravados!", cMsg, "OK");


        }

    }
}

