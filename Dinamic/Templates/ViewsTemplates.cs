using System;
using Dinamic.Models;
using Xamarin.Forms;

namespace Dinamic.Templates
{
    public class ViewTemplate
    {
        public static Thickness DefaultMargin = new Thickness(5, 5, 5, 0); 
        public static Char DefaultCharDelimiterSplit = ';';


        public static StackLayout GetLoadingView(string cTitle1, string cTitle2)
        {
            var oStackLayout = new StackLayout()
            {
                //BackgroundColor = Color.LimeGreen,
                Margin = new Thickness(5, 20, 5, 20),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Vertical,
                Children = {
                    new Label(){
                        Text = cTitle1,
                        HorizontalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 20,
                        Margin = new Thickness(0, 0, 0, 20)
                    },

                    new ActivityIndicator(){
                        IsRunning = true,
                        IsVisible = true,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        WidthRequest = 100,
                        HeightRequest = 100,
                        Scale = 3,
                        Margin = new Thickness(0, 0, 0, 20)
                    },

                    new Label(){
                        Text = cTitle2,
                        HorizontalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 30,
                        Margin = new Thickness(0, 0, 0, 20)
                    },




                }
            };


            return oStackLayout;
        }

        public static ViewCell GetNumberCell(FieldView oField)
        {

            //Cria o primeiro label a ser exibido
            Label oLabel = new Label()
            {
                Text = oField.Title,
                VerticalTextAlignment = TextAlignment.Center,
            };

            //cria o componente de entrada de dados
            Entry oEntry = new Entry()
            {
                Placeholder = oField.Detail,
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            //faz o binding do campo Text com a propriedade Value do oField
            oEntry.SetBinding(Entry.TextProperty, "Value");
            oEntry.BindingContext = oField;

            //anexa os eventos
            oEntry.Completed += oField.OnCompletedEvent;
            oEntry.Unfocused +=  oField.OnUnfocusedEvent;

            //se for do tipo numero valida o numero
            if (oField.FieldType == FieldTypeEnum.Number)
            {
                //seta o valor default configurado
                Double nAux = 0;
                var lParseOk = Double.TryParse(oField.ValueDefault, out nAux);
                if (lParseOk)
                    oEntry.Text = nAux.ToString();

            }//se for do tipo text seta o texto default
            else if (oField.FieldType == FieldTypeEnum.Entry)
            {
                if (oField.ValueDefault != null)
                    oEntry.Text = oField.ValueDefault;

            }


            //Cria o ViewCell  com todos os componentes internamente
            ViewCell oViewCell = new ViewCell() { 
                
                
                View = new StackLayout()
                {
                    //define a orientaçao horizontal , para fica um do lado do outro
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    Margin = DefaultMargin,
                    //adiciona os filhos ao componente
                    Children = {
                        oLabel,
                        oEntry
                    }
                }
            
            };

            return oViewCell;

        }

        public static ViewCell GetEntryCell(FieldView oField)
        {
            return GetNumberCell(oField);
        }



        public static TextCell GetTextCell(FieldView oField)
        {
            TextCell oTextCell = new TextCell()
            {
                Text = oField.Title,
                Detail = oField.Detail,
            };

            return oTextCell;
        }



        public static ImageCell GetImageCell(FieldView oField)
        {
            ImageCell oImageCell = new ImageCell()
            {
                Text = oField.Title,
                Detail = oField.Detail
            };

            //verifica se é um link http
            if (oField.Source != null && oField.Source.ToUpper().Contains("HTTP"))
                oImageCell.ImageSource = ImageSource.FromUri(new Uri(oField.Source));
            else
                oImageCell.ImageSource = oField.Source;

            return oImageCell;

        }

        public static SwitchCell GetSwitchCell(FieldView oField)
        {
            SwitchCell oSwitchCell = new SwitchCell()
            {
                Text = oField.Title,

                //valores default aceitos como verdadeiro aceitos
                //TRUE
                //T
                //.T.
                //VERDADEIRO
                //V
                //ON
                //LIGADO
                //ATIVO
                On = (
                        oField.ValueDefault != null &&
                        (
                          oField.ValueDefault.ToUpper().Equals(true.ToString().ToUpper()) ||
                          oField.ValueDefault.ToUpper().Equals("T") ||
                          oField.ValueDefault.ToUpper().Equals(".T.") ||
                          oField.ValueDefault.ToUpper().Equals("ATIVO") ||
                          oField.ValueDefault.ToUpper().Equals("LIGADO") ||
                          oField.ValueDefault.ToUpper().Equals("V") ||
                          oField.ValueDefault.ToUpper().Equals("VERDADEIRO")
                         )
                     )

            };

            return oSwitchCell;
        }

        public static ViewCell GetDatePickerCell(FieldView oField)
        {
            //Cria o primeiro label a ser exibido
            Label oLabel = new Label()
            {
                Text = oField.Title,
                VerticalTextAlignment = TextAlignment.Center,
            };

            //cria o componente de data
            DatePicker oDatePicker = new DatePicker()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            DateTime dDate = new DateTime();

            var lParseOk = DateTime.TryParse(oField.ValueMinimum, out dDate);
            if (lParseOk)
                oDatePicker.MinimumDate = dDate;

            lParseOk = DateTime.TryParse(oField.ValueMaximum, out dDate);
            if (lParseOk)
                oDatePicker.MaximumDate = dDate;

            //faz o binding do campo Text com a propriedade Value do oField
            oDatePicker.DateSelected += (sender, e) => {
                oField.Value = oDatePicker.Date.ToString();
            };

            //anexa os eventos
            oDatePicker.Unfocused += oField.OnUnfocusedEvent;

            //seta o valor default
            lParseOk = DateTime.TryParse(oField.ValueDefault, out dDate);
            if (lParseOk)
                oDatePicker.Date = dDate;


            //Cria o ViewCell  com todos os componentes internamente
            ViewCell oViewCell = new ViewCell()
            {


                View = new StackLayout()
                {
                    //define a orientaçao horizontal , para fica um do lado do outro
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    Margin = DefaultMargin,
                    //adiciona os filhos ao componente
                    Children = {
                        oLabel,
                        oDatePicker
                    }
                }

            };

            return oViewCell;

        }

        public static ViewCell GetTimePickerCell(FieldView oField)
        {
            //Cria o primeiro label a ser exibido
            Label oLabel = new Label()
            {
                Text = oField.Title,
                VerticalTextAlignment = TextAlignment.Center,
            };

            //cria o componente de data
            TimePicker oTimePicker = new TimePicker()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center

            };
       
       
            //faz o binding do campo Text com a propriedade Value do oField
            oTimePicker.Unfocused += (sender, e) => {
                oField.Value = oTimePicker.Time.ToString();
            };

            //anexa os eventos
            oTimePicker.Unfocused += oField.OnUnfocusedEvent;

            //seta o valor default
            TimeSpan tTime = new TimeSpan();
            var lParseOk = TimeSpan.TryParse(oField.ValueDefault, out tTime);
            if (lParseOk)
                oTimePicker.Time = tTime;

            //Cria o ViewCell  com todos os componentes internamente
            ViewCell oViewCell = new ViewCell()
            {


                View = new StackLayout()
                {
                    //define a orientaçao horizontal , para fica um do lado do outro
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    Margin = DefaultMargin,
                    //adiciona os filhos ao componente
                    Children = {
                        oLabel,
                        oTimePicker
                    }
                }

            };

            return oViewCell;
        }

        public static ViewCell GetPickerCell(FieldView oField)
        {

            //Cria o primeiro label a ser exibido
            Label oLabel = new Label()
            {
                Text = oField.Title,
                VerticalTextAlignment = TextAlignment.Center,
            };

            //cria o componente de entrada de dados
            Picker oPicker = new Picker()
            {
                Title = oField.Detail,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            //cria a lista de opções
            if (oField.Source != null && !oField.Source.Trim().Equals(""))
            {
                oPicker.ItemsSource = oField.Source.Split(DefaultCharDelimiterSplit);
            }

            //faz o binding da propriedade selecionada
            oPicker.SelectedIndexChanged += (sender, e) => {
                if (oPicker.SelectedItem is string)
                    oField.Value = (string)oPicker.SelectedItem;
            };



            //Cria o ViewCell  com todos os componentes internamente
            ViewCell oViewCell = new ViewCell()
            {


                View = new StackLayout()
                {
                    //define a orientaçao horizontal , para fica um do lado do outro
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    Margin = DefaultMargin,
                    //adiciona os filhos ao componente
                    Children = {
                        oLabel,
                        oPicker
                    }
                }

            };

            return oViewCell;
        }

        public static ViewCell GetSliderCell(FieldView oField)
        {
            //Cria o primeiro label a ser exibido
            Label oLabel = new Label()
            {
                Text = oField.Title,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            Label oLabelValue = new Label()
            {
                Text = "",
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            //cria o componente de entrada de dados
            Slider oSlider = new Slider()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            double dAux = 0;
            var lParseOk = double.TryParse(oField.ValueMinimum, out dAux);
            if (lParseOk)
                oSlider.Minimum = dAux;

            lParseOk = double.TryParse(oField.ValueMaximum, out dAux);
            if (lParseOk)
                oSlider.Maximum = dAux;
            

            //faz o binding do campo Text com a propriedade Value do oField
            oSlider.ValueChanged += (sender, e) => {
                oLabelValue.Text = oSlider.Value.ToString("0");
                oField.Value = oLabelValue.Text;
            };


            lParseOk = double.TryParse(oField.ValueDefault, out dAux);
            if (lParseOk)
                oSlider.Value = dAux;

            //Cria o ViewCell  com todos os componentes internamente
            ViewCell oViewCell = new ViewCell()
            {
                Height = 100,
                View = new StackLayout()
                {
                    //define a orientaçao horizontal , para fica um do lado do outro
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    Margin = DefaultMargin,
                    //adiciona os filhos ao componente
                    Children = {
                        oLabel,
                        oSlider,
                        oLabelValue
                    }
                }

            };

            return oViewCell;
        }
    }
}
