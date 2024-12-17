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
/// purpose of the class is to check whether the candlestick(s) accessed has "Bullish Harami" pattern
/// </summary>
public class recognizer_Bullish_Harami : recognizer
{
    /// <summary>
    /// constructor of the class that specifies the pattern name and length
    /// </summary>
    public recognizer_Bullish_Harami() : base("IsBullishHarami", 2) { }

    /// <summary>
    /// Checks whether the certain condition is met to be called "Bullish Harami" pattern
    /// </summary>
    /// <param name="smart_candlesticks">list of smart candlesticks</param>
    /// <param name="index">the currently accessed candlestick</param>
    /// <returns></returns>
    public override bool Recognize(List<SmartCandlestick> smart_candlesticks, int index)
    {
        //makes sure the index(es) will be in the range
        if (index > 0 && index < smart_candlesticks.Count)
        {
            //access the previous candlestick
            SmartCandlestick previous = smart_candlesticks[index - 1];
            //access the current candlestick
            SmartCandlestick current = smart_candlesticks[index];

            //previous bearish candlestick engulfs the current bullish candlestick 
            bool r = (current.Close > current.Open) && (previous.Close < previous.Open) && (current.Open > previous.Close) && (current.Close < previous.Open);
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

