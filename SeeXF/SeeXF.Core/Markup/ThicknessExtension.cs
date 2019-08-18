using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeeXF.Core.Markup
{
    [ContentProperty(nameof(Value))]
    public class ThicknessExtension : IMarkupExtension
    {
        /// <summary>
        /// xmal输入的值,跟特性联系
        /// </summary>
        public string Value { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Value))
            {
                return null;
            }
            return GetThickness(Value);
        }
        /// <summary>
        /// 只能填1，2，4个
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Thickness GetThickness(string value)
        {
            if (value.Contains("-"))
            {
                string[] arr = value.Split('-');
                var scale = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width / AppSetting.DesignWidth;
                if (arr.Length==2)
                {
                    return new Thickness(Convert.ToDouble(arr[0]), Convert.ToDouble(arr[1]));
                }
                 return new Thickness(Convert.ToDouble(arr[0]) * scale, Convert.ToDouble(arr[1]) * scale, Convert.ToDouble(arr[2]) * scale, Convert.ToDouble(arr[3]) * scale);
            }
            else
            {
                double result = Convert.ToDouble(value);
                result *= Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width / AppSetting.DesignWidth;
                return new Thickness(result);
            }
        }
    }

}
