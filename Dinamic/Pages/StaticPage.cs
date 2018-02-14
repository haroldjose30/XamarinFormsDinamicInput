using System;

using Xamarin.Forms;

namespace Dinamic
{
    public class StaticPage : ContentPage
    {
        public StaticPage()
        {
            Title = "INSPEÇÃO DE PROCESSO";

            Content = new TableView
            {
                Intent = TableIntent.Form,
                RowHeight = 100,
                HasUnevenRows = true,

                Root = new TableRoot("TABELA 001")
                                    {

                                        new TableSection("INSPEÇÃO NUM 00001")
                                        {
                                             new ImageCell
                                            {
                                                // Some differences with loading images in initial release.
                                                ImageSource = ImageSource.FromUri(new Uri("https://i.pinimg.com/originals/8e/ae/e9/8eaee9435b3332dc49b9163c8ec69d37.jpg")),
                                                Text = "Tanque REN000223",
                                                Detail = "Renaut",
                                            },


                                            new TextCell
                                            {
                                                Text = "Inspeçao da qualidade XYZ",
                                                Detail = "Realizar amostragem de 10 peças",
                                            },


                                            new SwitchCell
                                            {
                                                Text = "Aprovado?",
                                                On = false

                                            },

                                            new EntryCell
                                            {
                                                Label = "Largura:",
                                                Placeholder = "Informe a Largura em mm"
                                            },

                                            new ViewCell
                                            {
                                                View = new Label
                                                {
                                                    Text = "A largura do Item X deve estar entre 10mm e 20mm!"
                                                }
                                            },

                                            new ViewCell
                                            {
                                                View = new StackLayout{
                                            Orientation = StackOrientation.Horizontal,

                                                Children =
                                                {
                                                        new Label
                                                        {
                                                            Text = "Data de Algo:"
                                                        },

                                                        new DatePicker{

                                                                }
                                                        }
                                                }
                                            },

                                            new ViewCell
                                            {
                                                View = new StackLayout{
                                            Orientation = StackOrientation.Horizontal,

                                                Children ={
                                                        new Label
                                                        {
                                                            Text = "Hora de Algo:"
                                                        },

                                                        new TimePicker{

                                                                }
                                                        }
                                                }
                                            },



                                            new ViewCell
                                                    {
                                                        View = new StackLayout{
                                            Orientation = StackOrientation.Horizontal,

                                                                Children =
                                                                {
                                                                        new Label
                                                                        {
                                                                            Text = "Opções:"
                                                                        },

                                                                        new Picker{
                                                                                    ItemsSource = new string[]
                                                                                    { "Opçao 1","Opçao 2","Opçao 3", "Opçao 4"  }
                                                                                }
                                                                }
                                                        }
                                                    },


                                        new ViewCell
                                                    {
                                                        View = new StackLayout{
                                            Orientation = StackOrientation.Horizontal,

                                                                Children =
                                                                {
                                                                        new Label
                                                                        {
                                                                            Text = "Limite 0-100:"
                                                                        },

                                                                        new Slider {
                                                                            Minimum = 0,
                                                                            Maximum = 100,
                                                                            Value = 25
                                                                        }
                                                                }
                                                        }
                                                    },
                                        },


                                        new TableSection("INSPEÇÃO NUM 00002")
                                                {
                                                     new ImageCell
                                                    {
                                                        // Some differences with loading images in initial release.
                                                        ImageSource = ImageSource.FromUri(new Uri("https://i.pinimg.com/originals/8e/ae/e9/8eaee9435b3332dc49b9163c8ec69d37.jpg")),
                                                        Text = "Tanque HB0003",
                                                        Detail = "Hyundae",
                                                    },
                                        },


                                        new TableSection("INSPEÇÃO NUM 00003")
                                                {
                                                     new ImageCell
                                                    {
                                                        // Some differences with loading images in initial release.
                                                        ImageSource = ImageSource.FromUri(new Uri("https://i.pinimg.com/originals/8e/ae/e9/8eaee9435b3332dc49b9163c8ec69d37.jpg")),
                                                        Text = "Tanque TOY0052",
                                                        Detail = "Toyota",
                                                    },
                                        }



                                    }
            };

        
        }
    }
}

