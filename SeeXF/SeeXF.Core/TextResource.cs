using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SeeXF.Core
{
    public class TextResource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Dictionary<string, string> _items;
        public Dictionary<string, string> Items
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;
                    this.OnPropertyChanged("Items");
                }
            }
        }

        /// <summary>
        /// 加载文字资源
        /// </summary>
        /// <param name="langName">语言名称</param>
        /// <returns></returns>
        public void LoadTextResource(Assembly assembly, string jsonInAssembly)
        {
            jsonInAssembly = jsonInAssembly.Replace("-", "_");//.net文件夹需要这样替换

            using (var stream = assembly.GetManifestResourceStream(jsonInAssembly)) // typeof(TextResource).GetTypeInfo().Assembly ($"AppDemo.Resources.lang.{langName}.json"))
            using (var sr = new System.IO.StreamReader(stream, Encoding.UTF8))
            {
                var json = sr.ReadToEnd();
                this.Items = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
        }
    }
}
