using System;
using System.Collections.Generic;
using System.Diagnostics;
using Dinamic.Models;
using Dinamic.Templates;
using Xamarin.Forms;

namespace Dinamic
{
    public class DinamicPage : ContentPage
    {

        private List<FieldView> oFields; 
        public DinamicPage(string cFormTitle, List<FieldView> _oFields)
        {
            oFields = _oFields;
            Title = cFormTitle;
            //monta a tela de loading
            Content = ViewTemplate.GetLoadingView("Carregando","Por favor aguarde...");

           
           
        }

        protected override void OnAppearing()
        {
            if (oFields == null)
                return;

          TableView oTableView = new TableView();
          oTableView.Intent = TableIntent.Form;
          oTableView.RowHeight = 50;
          oTableView.HasUnevenRows = true;

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

                  case FieldTypeEnum.Image:oTableSectionCurrent.Add(ViewTemplate.GetImageCell(oField));
                      break;
                  case FieldTypeEnum.Text: oTableSectionCurrent.Add(ViewTemplate.GetTextCell(oField));
                      break;
                  case FieldTypeEnum.Switch: oTableSectionCurrent.Add(ViewTemplate.GetSwitchCell(oField));
                      break;
                  case FieldTypeEnum.Entry: oTableSectionCurrent.Add(ViewTemplate.GetEntryCell(oField));
                      break;
                  case FieldTypeEnum.DatePicker: oTableSectionCurrent.Add(ViewTemplate.GetDatePickerCell(oField));
                      break;
                  case FieldTypeEnum.TimePicker:  oTableSectionCurrent.Add(ViewTemplate.GetTimePickerCell(oField));
                      break;
                  case FieldTypeEnum.Picker: oTableSectionCurrent.Add(ViewTemplate.GetPickerCell(oField));
                      break;
                  case FieldTypeEnum.Slider:oTableSectionCurrent.Add(ViewTemplate.GetSliderCell(oField));
                      break;
                  case FieldTypeEnum.Number: oTableSectionCurrent.Add(ViewTemplate.GetNumberCell(oField));
                      break;
              }

          }

          oTableView.Root = oTableRoot;
          Content = oTableView;
          this.ToolbarItems.Add(new ToolbarItem() { Text = "Gravar", Order = ToolbarItemOrder.Primary, Command = new Command(ToolbarItemCommand), CommandParameter = "1" });

          base.OnAppearing();
        }
      

        private async void ToolbarItemCommand()
        {

            string cMsg = "";
           //executa o processo de gravacao
            foreach (var oField in oFields)
            {

                //se existe validaçao executa a mesma
                oField.OnCompletedEvent?.Invoke(null,null);

                //se o campo nao for valido nao continua com o processo
                if (!oField.IsValid)
                    return;
                    
                cMsg += $"{oField.Title}={oField.Value}" +Environment.NewLine;

            }

            Application.Current.MainPage.DisplayAlert("Dados Gravados!", cMsg, "OK");


        }

    }
}

