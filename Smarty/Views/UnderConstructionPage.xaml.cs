﻿using Smarty.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smarty.Views
{
    public partial class UnderConstructionPage : ContentPage
    {
        public UnderConstructionPage()
        {
            InitializeComponent();
            this.BindingContext = new UnderConstructionViewModel();
        }
    }
}