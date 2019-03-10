using System;
using System.Collections.Generic;
using System.Drawing;

namespace MES.Utils
{
    public class ValuePainter
    {
        public byte Alpha = 0xff;
        private List<Color> ColorsOfMap = new List<Color>()
        {
            Color.Blue,
            Color.Cyan,
            Color.Green,
            Color.GreenYellow,
            Color.Yellow,
            Color.Orange,
            Color.OrangeRed,
            Color.Red,
            Color.Firebrick,
            Color.DarkRed
        };

        public Color GetColorForValue(double val, double maxVal)
        {
            double valPerc = val / (maxVal + 1);// value%
            double colorPerc = 1d / (ColorsOfMap.Count - 1);// % of each block of color. the last is the "100% Color"
            double blockOfColor = valPerc / colorPerc;// the integer part repersents how many block to skip
            int blockIdx = (int)Math.Truncate(blockOfColor);// Idx of 
            double valPercResidual = valPerc - (blockIdx * colorPerc);//remove the part represented of block 
            double percOfColor = valPercResidual / colorPerc;// % of color of this block that will be filled

            if (blockIdx < 0) blockIdx = 0;
            Color cTarget = ColorsOfMap[blockIdx];
            Color cNext = cNext = ColorsOfMap[blockIdx + 1];

            var deltaR = cNext.R - cTarget.R;
            var deltaG = cNext.G - cTarget.G;
            var deltaB = cNext.B - cTarget.B;

            var R = cTarget.R + (deltaR * percOfColor);
            var G = cTarget.G + (deltaG * percOfColor);
            var B = cTarget.B + (deltaB * percOfColor);
           
            return Color.FromArgb(0xff, (byte)R, (byte)G, (byte)B);
        }
    }
}
