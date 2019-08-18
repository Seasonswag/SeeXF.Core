using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeeXF.Core.Markup
{
    [ContentProperty(nameof(Value))]
    public class SizeExtension : IMarkupExtension
    {
        /// <summary>
        /// xmal输入的值
        /// </summary>
        public string Value { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Value))
            {
                return null;
            }
            var value = GetSize(Value);
            try
            {
                var target = serviceProvider.GetService<IProvideValueTarget>();

                Type proType;
                if (target.TargetProperty is System.Reflection.PropertyInfo)
                {
                    var property = (PropertyInfo)target.TargetProperty;
                    proType = property.PropertyType;

                }
                else
                {
                    BindableProperty pro = ((BindableProperty)target.TargetProperty);
                    proType = pro.ReturnType;

                }
                //根据绑定目标的类型，需要做一些转换
                if (proType == typeof(GridLength))//gridlength类型可以直接转
                    return new GridLength(value);
                else if (proType != typeof(double))//如果不是double，转换类型,只能是正常数值类型
                    return Convert.ChangeType(value, proType);
                else
                    return value;
            }
            catch (Exception)
            {
                return value;
            }
        }
        //按宽比例计算size值
        public static double GetSize(string value)
        {
            var designValue = Convert.ToDouble(value);
            return (designValue * Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width) / AppSetting.DesignWidth;
        }
    }
}
