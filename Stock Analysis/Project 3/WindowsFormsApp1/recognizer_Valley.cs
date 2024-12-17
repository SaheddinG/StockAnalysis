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
/// purpose of the class is to check whether the candlestick(s) accessed has "Valley" pattern
/// </summary>
public class recognizer_Valley : recognizer
{
    /// <summary>
    /// constructor of the class that specifies the pattern name and length
    /// </summary>
    public recognizer_Valley() : base("IsValley", 3) { }

    /// <summary>
    /// Checks whether the certain condition is met to be called Valley pattern
    /// </summary>
    /// <param name="smart_candlesticks">list of smart candlesticks</param>
    /// <param name="index">the currently accessed candlestick</param>
    /// <returns></returns>
    public override bool Recognize(List<SmartCandlestick> smart_candlesticks, int index)
    {
        //makes sure the index(es) will be in the range
        if (index > 0 && index < smart_candlesticks.Count - 1)
        {
            //access the previous candlestick
            SmartCandlestick previous = smart_candlesticks[index - 1];
            //access the current candlestick
            SmartCandlestick current = smart_candlesticks[index];
            //access the next candlestick
            SmartCandlestick next = smart_candlesticks[index + 1];

            //valley is wehn the current candlestick is highest of them all
            bool r = current.Low < previous.Low && current.Low < next.Low;
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

