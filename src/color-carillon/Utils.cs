using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemColor = System.Drawing.Color;
using MyColor = color_carillon.Enums.Color;

namespace color_carillon
{
    class Utils
    {
        public static MyColor Classify(int red, int green, int blue)
        {
            SystemColor color = SystemColor.FromArgb(red, green, blue);
            
            float hue = color.GetHue();
            float sat = color.GetSaturation();
            float lgt = color.GetBrightness();

            MyColor ans = MyColor.None;

            if (lgt < 0.2) ans = MyColor.Black;
            else if (lgt > 0.8) ans = MyColor.White;

            else if (sat < 0.25) ans = MyColor.Gray;

            else if (hue < 30) ans = MyColor.Red;
            else if (hue < 90) ans = MyColor.Yellow;
            else if (hue < 150) ans = MyColor.Green;
            else if (hue < 210) ans = MyColor.Cyan;
            else if (hue < 270) ans = MyColor.Blue;
            else if (hue < 330) ans = MyColor.Magenta;
            else ans = MyColor.Red;

            return ans;
        }
    }
}
