using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Dinamic.Models
{
    public class FieldView : INotifyPropertyChanged
    {
        

        public string Title { get; set; }

        public FieldTypeEnum FieldType { get; set; }
        public Boolean Required { get; set; }
        public string Detail { get; set; }
        public string Help { get; set; }
        public string Name { get; set; }

        private string _Value;
        public string Value
        {

            get
            {
                return _Value;
            }

            set
            {
                _Value = value;
                //se existe validaçao executa a mesma
                this.ValidatorChangedCommand?.Execute(null);
                NotifyPropertyChanged("Value");
            }
        }


        public int FieldOrder { get; set; }

        public string Source { get; set; }
        public double ValueMinimum { get; set; }
        public double ValueMaximum { get; set; }
        public string ValueDefault { get; set; }

        public FieldGroupEnum FieldGroup { get; set; }
        public string GroupTitle { get; set; }
        public int GroupOrder { get; set; }


        private Boolean _IsValid = true;
        public Boolean IsValid
        {

            get
            {
                return _IsValid;
            }

            set
            {
                _IsValid = value;
            }
        }

        public Command ValidatorChangedCommand { get; set; }
        public Command ValidatorCompletedCommand { get; set; }
       
        public FieldView()
        {
            //cria os validadores
            this.ValidatorChangedCommand = new Command(ValidatorChangedCommandExecute);
            this.ValidatorCompletedCommand = new Command(ValidatorCompletedCommandExecute);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }





        public FieldView SetText(string cGroupTitle, string cTitle, string cDetail)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Text;
            this.Title = cTitle;
            this.Detail = cDetail;

            return this;
        }

        public FieldView SetImage(string cGroupTitle,string cTitle, string cDetail, string cSource)
        {

            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Image; 
            this.Title = cTitle; Detail = cDetail; Source =cSource;
            return this;
        }

        public FieldView SetEntry(string cGroupTitle,string cTitle, string cPlaceholder)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Entry;
            this.Title = cTitle; 
            this.Detail = cPlaceholder;
            return this;
        }

        public FieldView SetNumber(string cGroupTitle,string cTitle, string cPlaceholder, double nMinimum, double nMaximum)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Number;
            this.Title = cTitle;
            this.Detail = cPlaceholder;
            this.ValueMinimum = nMinimum; 
            this.ValueMaximum = nMaximum;
            return this;
        }

     

        public FieldView SetSwitch(string cGroupTitle,string cTitle, Boolean lDefault)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Switch;
            this.Title = cTitle; 
            this.ValueDefault=lDefault.ToString();
            return this;
        }

        public FieldView SetDatePicker(string cGroupTitle,string cTitle)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.DatePicker;
            this.Title = cTitle;
            return this;
        }

        public FieldView SetTimePicker(string cGroupTitle,string cTitle)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.TimePicker; 
            this.Title = cTitle;
            return this;
        }

        public FieldView SetPicker(string cGroupTitle,string cTitle,string cSource)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Picker;
            this.Title = cTitle; 
            this.Source=cSource;
            return this;
        }

        public FieldView SetSlider(string cGroupTitle,string cTitle, double nMinimum, double nMaximum)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Picker;
            this.Title = cTitle;
            this.ValueMinimum = nMinimum; 
            this.ValueMaximum=nMaximum;
            return this;
        }      


        private async void ValidatorChangedCommandExecute()
        {


            if (this.FieldType == FieldTypeEnum.Number)
            {
                double cNumber = 0;
                if (this._Value != null && !this._Value.Trim().Equals("") && !Double.TryParse(this._Value, out cNumber))
                {
                    this._Value = "";
                    Application.Current.MainPage.DisplayAlert("Atenção", "Valor inválido", "OK");
                    return;
                }
            }
        }



        private async void ValidatorCompletedCommandExecute()
        {
            //verifica se o campo é obrigatorio
            if (this.Required && (this.Value == null || this.Value.Trim().Equals("")))
            {
                this._IsValid = false;
                Application.Current.MainPage.DisplayAlert("Atenção", $"{this.Title} obrigatório", "OK");
                return;
            }


            if (this.FieldType == FieldTypeEnum.Number)
            {

                double cNumber = 0;
                if (this._Value != null && !this._Value.Trim().Equals("") && !Double.TryParse(this._Value, out cNumber))
                {
                    this._IsValid = false;
                    this.Value = "";
                    Application.Current.MainPage.DisplayAlert("Atenção", "Valor inválido", "OK");
                    return;
                }


                if (this.ValueMinimum != null && cNumber < this.ValueMinimum)
                {
                    this._IsValid = false;
                    this.Value = "";
                    Application.Current.MainPage.DisplayAlert("Atenção", $"Valor deve ser maior que {this.ValueMinimum.ToString()}", "OK");
                    return;
                }

                if (this.ValueMaximum != null && cNumber > this.ValueMaximum)
                {
                    this._IsValid = false;
                    this.Value = "";
                    Application.Current.MainPage.DisplayAlert("Atenção", $"Valor deve ser menor que {this.ValueMaximum.ToString()}", "OK");
                    return;
                }
            }

            //se passou por todas as validacoes retorna como verdadeiro a validacao
            this._IsValid = true;
        }







    }

}
