using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dinamic
{
    public partial class DocumentPage : ContentPage
    {
        public DocumentPage()
        {
            InitializeComponent();
            Browser.Source = "https://country.honeywellaidc.com/CatalogDocuments/scanpal-eda50-handheld-computer-data-sheet-pt-br.pdf";
//            Browser.Source = "scanpal-eda50-handheld-computer-data-sheet-pt-br.pdf";


        }
    }
}
