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
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using System.Xml.Linq;

/// <summary>
/// Will be the base recognizer class for all operations
/// </summary>
public abstract class recognizer
{
    //two important properties for the recognizer:
    //The pattern's name like IsBullish
    string pattern_name;
    //How many candlestick needed to decide if the particular pattern is satisfied
    int pattern_length;

    /// <summary>
    /// constructor for the class
    /// </summary>
    /// <param name="pn">the string pn that will be read will be put onto the pattern name</param>
    /// <param name="pl">the integer pl that will be read will be put onto the pattern length</param>
    public recognizer(string pn, int pl)
    {
        //the string pn that will be read will be put onto the pattern name
        pattern_name = pn;
        //the integer pl that will be read will be put onto the pattern length
        pattern_length = pl;
    }

    /// <summary>
    /// Base recognize function(not to be mistaken with the recognize() on recognizer_xxx)
    /// </summary>
    /// <param name="smart_candlesticks">list of small candlesticks</param>
    /// <param name="index">the index of the candlestick asked from the list</param>
    /// <returns></returns>
    public abstract bool Recognize(List<SmartCandlestick> smart_candlesticks, int index);

    /// <summary>
    /// Applies the recognize() function to all of the candlesticks to find its corresponding pattern(s)
    /// </summary>
    /// <param name="smart_candlesticks">list of smart candlesticks</param>
    public void recognizeAll(List<SmartCandlestick> smart_candlesticks)
    {
        //for loop which iterates through all elements of smart_candlesticks
        for (int index = 0; index < smart_candlesticks.Count; index++)
        {
            //smart_candlestick list->the particular candlestick based on index->recognize whether the asked pattern exists or not by calling recognize()
            smart_candlesticks[index].candlestick_pattern[pattern_name] = Recognize(smart_candlesticks, index);
        }
    }

    /// <summary>
    /// Bacially, arrows will be drawn using this function, with overarching if cases based on the number of elements the pattern has
    /// </summary>
    /// <param name="smart_candlesticks">list of smart candlesticks</param>
    /// <param name="index">the candlestick currently accessed</param>a
    /// <param name="chart_candleSticks">candlesticks visualized on the chart</param>
    public void point_to_candlesticks(BindingList<SmartCandlestick> smart_candlesticks, int index, Chart chart_candleSticks)
    {
        //makes sure that given index is in the range
        if (index >= 0 && index < smart_candlesticks.Count)
        {
            //access the current candlestick we have
            SmartCandlestick smartCandleStick = smart_candlesticks[index];
            //check whether smart_candlestick pattern_name is accessable
            if (smartCandleStick.candlestick_pattern.TryGetValue(pattern_name, out bool v))
            {
                //On first layer, if the candlestick has at least 1 element on its pattern, you add the first arrow at the index
                if (pattern_length >= 1 && index >=0)
                {
                    //creates a new arrow
                    ArrowAnnotation pointer = new ArrowAnnotation();
                    //sets the anchor point of the arrow
                    pointer.AnchorDataPoint = chart_candleSticks.Series[0].Points[index];
                    //sets the properties of the arrow itself
                    pointer.ArrowSize = 1;
                    pointer.AnchorOffsetY = 4;
                    pointer.Height = 3.5;
                    pointer.Width = 1.5;
                    //after everything is done arrow is added to the chart of candlesticks
                    chart_candleSticks.Annotations.Add(pointer);
                }
                //On second layer, if the candlestick has at least 2 element on its pattern, you add the second arrow at the previous candlestick
                if (pattern_length >= 2 && index >=1)
                {
                    //creates a new arrow
                    ArrowAnnotation pointer = new ArrowAnnotation();
                    //sets the anchor point of the arrow
                    pointer.AnchorDataPoint = chart_candleSticks.Series[0].Points[index - 1];
                    //sets the properties of the arrow itself
                    pointer.ArrowSize = 1;
                    pointer.AnchorOffsetY = 4;
                    pointer.Height = 3.5;
                    pointer.Width = 1.5;
                    //after everything is done arrow is added to the chart of candlesticks
                    chart_candleSticks.Annotations.Add(pointer);
                }
                //On the third layer, if the candlestick has at least 3 element on its pattern, you add the third arrow at the next candlestick
                //(which will be the only one code is going to find since the largest candlestick pattern we have has 3 elements)
                if (pattern_length >= 3 && index >= 1 && index < smart_candlesticks.Count-1)
                {
                    //creates a new arrow
                    ArrowAnnotation pointer = new ArrowAnnotation();
                    //sets the anchor point of the arrow
                    pointer.AnchorDataPoint = chart_candleSticks.Series[0].Points[index + 1];
                    //sets the properties of the arrow itself
                    pointer.ArrowSize = 1;
                    pointer.AnchorOffsetY = 4;
                    pointer.Height = 3.5;
                    pointer.Width = 1.5;
                    //after everything is done arrow is added to the chart of candlesticks
                    chart_candleSticks.Annotations.Add(pointer);
                }
            }
        }
    }


}