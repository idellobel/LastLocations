using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace B4.PE3.DellobelI.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            Xamarin.FormsMaps.Init("oX4W3w39inKHLCarhcBC ~ kuJRGh2dakan68Ueb7xopA ~ Ah95VcIq0mTPUKdBX9bcrfa2f4b2Ft1d7DqCXBqUEEjl9XzW1AORVFSVqyruF3Xv ");
            this.InitializeComponent();
           
            LoadApplication(new B4.PE3.DellobelI.App());
           
        }
    }
}
