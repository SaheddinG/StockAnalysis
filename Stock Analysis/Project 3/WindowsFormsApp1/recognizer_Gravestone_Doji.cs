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
/// purpose of the class is to check whether the candlestick(s) accessed has "Gravestone Doji" pattern
/// </summary>
public class recognizer_Gravestone_Doji : recognizer
{
    /// <summary>
    /// constructor of the class that specifies the pattern name and length
    /// </summary>
    public recognizer_Gravestone_Doji() : base("IsGravestoneDoji", 1) {}

    /// <summary>
    /// Checks whether the certain condition is met to be called "Gravestone Doji" pattern
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
            //Gravestone doji is a doji, but has an upper tail only,however, in this case I will add some tolerance to actually find on the chart
            bool r = smartCandleStick.BodyRange <= (smartCandleStick.Range * 0.20m) && smartCandleStick.LowerTail <= (smartCandleStick.BodyRange * 0.1m);
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
