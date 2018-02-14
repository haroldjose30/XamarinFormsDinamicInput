using System;
using Dinamic.Models;
using Xamarin.Forms;

namespace Dinamic.Templates
{
    public class ViewTemplate
    {

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
            };

            //faz o binding do campo Text com a propriedade Value do oField
            oEntry.SetBinding(Entry.TextProperty, "Value");
            oEntry.BindingContext = oField;

            //anexa os eventos
            oEntry.Completed += oField.OnCompletedEvent;
            oEntry.Unfocused +=  oField.OnUnfocusedEvent;

            //Cria o ViewCell  com todos os componentes internamente
            ViewCell oViewCell = new ViewCell() { 
                
                
                View = new StackLayout()
                {
                    //define a orientaçao horizontal , para fica um do lado do outro
                    Orientation = StackOrientation.Horizontal,
                    //HorizontalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(5,5,5,0),
                    //adiciona os filhos ao componente
                    Children = {
                        oLabel,
                        oEntry
                    }
                }
            
            };

            return oViewCell;

        }


 
    }
}
