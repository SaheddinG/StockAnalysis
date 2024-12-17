using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using Candlesticks;

/// <summary>
/// purpose of the class is to check whether the candlestick(s) accessed has "Hammer" pattern
/// </summary>
public class recognizer_Hammer : recognizer
{
    /// <summary>
    /// constructor of the class that specifies the pattern name and length
    /// </summary>
    public recognizer_Hammer() : base("IsHammer", 1) {}

    /// <summary>
    /// Checks whether the certain condition is met to be called "Hammer" pattern
    /// </summary>
    /// <param name="smart_candlesticks">list of smart candlesticks</param>
    /// <param name="index">the currently accessed candlestick</param>
    /// <returns></returns>
    public override bool Recognize(List<SmartCandlestick> smart_candlesticks, int index)
	{
        //makes sure the index(es) will be in the range
        if (index >= 0 && index < smart_candlesticks.Count)
		{
            //access the current candlestick
            SmartCandlestick smartCandleStick = smart_candlesticks[index];
            //Hammer is a body with a single tail, for the code to find more Hammers there is a 3% tolerance rate that accepts candlesticks very similar to hammers with minimal difference.
            //This time around I also added the case for a reverse hammer to be displayed in the chart
            bool r = (smartCandleStick.LowerTail >= (2 * smartCandleStick.BodyRange * 0.97m) && smartCandleStick.UpperTail < (smartCandleStick.BodyRange * 0.97m)) || (smartCandleStick.UpperTail >= (2 * smartCandleStick.BodyRange * 0.97m) && smartCandleStick.LowerTail < (smartCandleStick.BodyRange * 0.97m));
            //return whether the condition is met or not
            return r;
		}

		else
		{
            //if the index is outside of range finding that pattern is impossible
            return false;
        }
		
	}
}
