using System;
using System.Collections.Generic;
using System.Text;

namespace SeeXF.Core
{
    public static class AppSetting
    {
        public static double DesignWidth { set; get; } = 1024;
        public static TextResource TextResource { set; get; } = new TextResource();
    }
}
