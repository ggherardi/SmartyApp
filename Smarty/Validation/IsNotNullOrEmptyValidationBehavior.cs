using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Smarty.Validation
{
    public class IsNotNullOrEmptyValidationBehavior
    {
        public static readonly BindableProperty AttachBehaviorProperty = BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(IsNotNullOrEmptyValidationBehavior), false, propertyChanged: OnAttachBehaviorChanged);

        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
        {
            var entry = view as Entry;
            if (entry == null)
            {
                return;
            }

            bool attachBehavior = (bool)newValue;
            if (attachBehavior)
            {
                entry.TextChanged += OnEntryTextChanged;
            }
            else
            {
                entry.TextChanged -= OnEntryTextChanged;
            }
        }

        static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = args.NewTextValue != null && !string.IsNullOrWhiteSpace(args.NewTextValue);
            //((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
            ((Entry)sender).BackgroundColor = isValid ? Color.Default : Color.Red;
        }
    }
}
