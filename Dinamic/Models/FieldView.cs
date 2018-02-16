using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Dinamic.Models
{
    /// <summary>
    /// Teste de commit via visual studio
    /// </summary>
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
                this.OnChangedEvent?.Invoke(null, null);

                NotifyPropertyChanged("Value");
            }
        }


        public int FieldOrder { get; set; }

        public string Source { get; set; }
        public string ValueMinimum { get; set; }
        public string ValueMaximum { get; set; }
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

        public EventHandler OnChangedEvent { get; set; }
        public EventHandler OnCompletedEvent { get; set; }
        public EventHandler<FocusEventArgs> OnUnfocusedEvent { get; set; }


       
        public FieldView()
        {
            //Cria os eventos necessários de validacao dos campos
            this.OnChangedEvent += OnChanged;
            this.OnCompletedEvent += OnCompleted;
            this.OnUnfocusedEvent += OnUnfocused;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }





        public FieldView SetTextField(string cGroupTitle, string cTitle, string cDetail)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Text;
            this.Title = cTitle;
            this.Detail = cDetail;

            return this;
        }

        public FieldView SetImageField(string cGroupTitle,string cTitle, string cDetail, string cSource)
        {

            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Image; 
            this.Title = cTitle; Detail = cDetail; Source =cSource;
            return this;
        }

        public FieldView SetEntryField(string cGroupTitle,string cTitle, string cPlaceholder)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Entry;
            this.Title = cTitle; 
            this.Detail = cPlaceholder;
            return this;
        }

        public FieldView SetNumberField(string cGroupTitle,string cTitle, string cPlaceholder, double? nMinimum = null, double? nMaximum = null)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Number;
            this.Title = cTitle;
            this.Detail = cPlaceholder;
            this.ValueMinimum = nMinimum?.ToString(); 
            this.ValueMaximum = nMaximum?.ToString();
            return this;
        }

     

        public FieldView SetSwitchField(string cGroupTitle,string cTitle, Boolean lDefault = false)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Switch;
            this.Title = cTitle; 
            this.ValueDefault=lDefault.ToString();
            return this;
        }

        public FieldView SetDatePickerField(string cGroupTitle,string cTitle,DateTime? dMinimum = null, DateTime? dMaximum = null)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.DatePicker;
            this.Title = cTitle;
            this.ValueMinimum = dMinimum?.ToString();
            this.ValueMaximum = dMaximum?.ToString();
            return this;
        }

        public FieldView SetTimePickerField(string cGroupTitle,string cTitle)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.TimePicker; 
            this.Title = cTitle;
            return this;
        }

        public FieldView SetPickerField(string cGroupTitle,string cTitle,string cTitlePicker,string cSource)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Picker;
            this.Title = cTitle;
            this.Detail = cTitlePicker;
            this.Source=cSource;
            return this;
        }

        public FieldView SetSliderField(string cGroupTitle,string cTitle, double? nMinimum = null, double? nMaximum = null)
        {
            this.GroupTitle = cGroupTitle;
            this.FieldType = FieldTypeEnum.Slider;
            this.Title = cTitle;
            this.ValueMinimum = nMinimum?.ToString(); 
            this.ValueMaximum=nMaximum?.ToString();
            return this;
        }      


        private async void OnChanged(object sender, EventArgs e)
        {


            if (this.Value != null && this.Value.Trim().Equals(""))
                return;



            if (this.FieldType == FieldTypeEnum.Number)
            {

                double cNumber = 0;
                if (this._Value != null && !this._Value.Trim().Equals("") && !Double.TryParse(this._Value, out cNumber))
                {
                    this._IsValid = false;
                    this.Value = "";
                    //no onchanged nao precisa exibir a informacao
                    //Application.Current.MainPage.DisplayAlert("Atenção", "Valor inválido", "OK");
                    return;
                }


            }
               
        }
       

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            if (this.Value != null && this.Value.Trim().Equals(""))
                return;


            OnCompleted(sender, e);
        }


        private async void OnCompleted(object sender, EventArgs e)
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

                double nNumber = 0;
                if (this._Value != null && !this._Value.Trim().Equals("") && !Double.TryParse(this._Value, out nNumber))
                {
                    this._IsValid = false;
                    this.Value = "";
                    Application.Current.MainPage.DisplayAlert("Atenção", "Valor inválido", "OK");
                    return;
                }


                //valida o valor minimo
                double nAuxMin = 0;
                var lParseMinOk = Double.TryParse(this.ValueMinimum, out nAuxMin);

                //valida o valor maximo
                double nAuxMax = 0;
                var lParseMaxOk = Double.TryParse(this.ValueMaximum, out nAuxMax);


                if (lParseMinOk && lParseMaxOk
                   )
                {
                    if (nNumber < nAuxMin || nNumber > nAuxMax)
                    {
                        this._IsValid = false;
                        this.Value = "";
                        Application.Current.MainPage.DisplayAlert("Atenção", $"Valor deve ser entre {this.ValueMinimum} e {this.ValueMaximum}", "OK");
                        return;
                    }
           
                }
           


                if (lParseMinOk)
                {
                    if (nNumber < nAuxMin)
                    {
                        this._IsValid = false;
                        this.Value = "";
                        Application.Current.MainPage.DisplayAlert("Atenção", $"Valor deve ser maior que {this.ValueMinimum}", "OK");
                        return;
                    }
           
                }
           
                if (lParseMaxOk)
                {
                    if (nNumber > nAuxMax)
                    {
                        this._IsValid = false;
                        this.Value = "";
                        Application.Current.MainPage.DisplayAlert("Atenção", $"Valor deve ser menor que {this.ValueMaximum}", "OK");
                        return;
                    }

                    
                }



               
            }

            //se passou por todas as validacoes retorna como verdadeiro a validacao
            this._IsValid = true;
        }




     
    }

}
