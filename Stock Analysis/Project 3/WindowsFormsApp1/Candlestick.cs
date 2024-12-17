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

//Namespace decleration of the Candlesticks
namespace Candlesticks
{
    /// <summary>
    /// This class is about the parameters you can have regarding the candlesticks
    /// </summary>
    public partial class Candlestick
    {
        /// <summary>
        /// gets and sets the opening price.
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// Gets and sets the highest price.
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// Gets and sets the lowest price.
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// gets and sets the closing price.
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// gets and sets adjusted closing price.
        /// </summary>
        public decimal AdjClose { get; set; }

        /// <summary>
        /// gets and sets volume
        /// </summary>
        public ulong Volume { get; set; }

        /// <summary>
        /// Gets and sets date(s)
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// This class is for reading the line and dividing the line into according sub-strings/sections
        /// </summary>
        /// <param name="rowOfData">string that contains the candlestick data in the according format</param>
        
        ///
        public Candlestick(string rowOfData)
        {
            //seperates the line into substrings based on encountering of these strings
            char[] separators = new char[] { ',', ' ', '"' };
            //splits the line into substrings based on separators
            string[] subs = rowOfData.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //attempts to parse the first(starting 0) substring to the date
            string dateString = subs[0];
            //pasrses the date
            Date = DateTime.Parse(dateString);

            //temporary variable to hold a decimal type value
            decimal temp;

            //attempts to parse the second substring to the opening number
            bool success = decimal.TryParse(subs[1], out temp);
            if (success) Open = temp;

            //attempts to parse the third substring to high(est)
            success = decimal.TryParse(subs[2], out temp);
            if (success) High = temp;

            //attempts to parse the fourth substring to low(est)
            success = decimal.TryParse(subs[3], out temp);
            if (success) Low = temp;

            //attempts to parse the fifth substring to the closing number
            success = decimal.TryParse(subs[4], out temp);
            if (success) Close = temp;

            //attempts to parse the sixth substring to the adjusted close
            success = decimal.TryParse(subs[5], out temp);
            if (success) AdjClose = temp;

            //since volume is potentially a very large number, it is temporarily stored using long type
            ulong tempVolume;
            //attempts to parse the first substring to the volume
            success = ulong.TryParse(subs[6], out tempVolume);
            if (success) Volume = tempVolume;
        }
    }

    /// <summary>
    /// This is the smart candlestick class derived from the candlestick class from Project 1. It has properties like Range, BodyRange, TopPrie, BottomPrice, Uppertail, Lowertail. Methods(booleans) determine the type of candlestick in particular: Bullis, Bearish,Neutral, Marubozu, Hammer, Doji, DragonflyDoji, and lastly the Gravestone Doji.
    /// </summary>
    public class SmartCandlestick : Candlestick
    {
        // Properties
        // Range is the difference between highest and the lowest price
        public decimal Range => High - Low;
        //Body Range is the difference between opening and closing prices
        public decimal BodyRange => Math.Abs(Open - Close);
        //Top Price is the larger one of the opening/closing
        public decimal TopPrice => Math.Max(Open, Close);
        //Bottom Price is the lower one of the opening/closing
        public decimal BottomPrice => Math.Min(Open, Close);
        //Basically like an extension of the body on the upperbound, found by the difference of high and top price
        public decimal UpperTail => High - TopPrice;
        //BAsically like an extension of the body on the lowerbound, found by the difference of low and bottom price
        public decimal LowerTail => BottomPrice - Low;

        /// <summary>
        /// Gets and sets the dictionary(patterns)
        /// </summary>
        public Dictionary<string, bool> candlestick_pattern { get;  set;}

        /// <summary>
        /// Default constructor of the SmartCandlestick class, creates a dictionary and computes all patterns for the dictionary
        /// </summary>
        /// <param name="rowOfData">rowofdata refers to the line given to the code in excel, waiting to be processed</param>
        public SmartCandlestick(string rowOfData) : base(rowOfData) 
        {
            //creates a new dictionary that has string and boolean elements
            candlestick_pattern = new Dictionary<string, bool>();
        }
    }
}