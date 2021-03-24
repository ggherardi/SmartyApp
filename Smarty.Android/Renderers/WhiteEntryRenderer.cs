using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Smarty.Controls;
using Smarty.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WhiteEntry), typeof(WhiteEntryRenderer))]
namespace Smarty.Droid.Renderers
{
    public class WhiteEntryRenderer : EntryRenderer
    {
        public WhiteEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(Control == null && e.NewElement == null)
            {
                return;
            }
            Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
            //Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
        }
    }
}