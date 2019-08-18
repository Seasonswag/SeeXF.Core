using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeeXF.Core.Markup
{
    [ContentProperty(nameof(Text))]
    public class TextExtension: IMarkupExtension
    {
        /// <summary>
        /// xmal输入的值,跟ContentProperty特性联系
        /// </summary>
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return null;
            }
            try
            {
                return AppSetting.TextResource.Items[Text];
            }
            catch (Exception)
            {
                return Text;
            }
        }
    }
}
