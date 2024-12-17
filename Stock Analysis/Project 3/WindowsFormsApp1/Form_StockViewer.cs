using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using Candlesticks;

//Namespace decleration of the Project 3
namespace Project_candlesticks
{
    /// <summary>
    /// Defines Form_Candlesticks as a Windows Form
    /// </summary>
    public partial class Form_Candlesticks : Form
    {
        //Declares a list to store all candlesticks
        List<SmartCandlestick> candlesticks = new List<SmartCandlestick>(1024);

        //Declares a binding list to bind all candlesticks
        BindingList<SmartCandlestick> boundCandlesticks = new BindingList<SmartCandlestick>();
        Dictionary<string, recognizer> recognizer_xxx = new Dictionary<string, recognizer>();
        /// <summary>
        /// Initializes the new instance of the Form_Candlesticks class defined at line 16
        /// </summary>
        public Form_Candlesticks()
        {
            //Method to initialize the form components
            InitializeComponent();
            //populate the combobox with pattern names
            recognizer_xxx = populate_and_verify();
            //filter the combobox
            combobox_filteration();
        }

        public Form_Candlesticks(string stockPath, DateTime start, DateTime end)
        {
            //Method to initialize the form components
            InitializeComponent();

            DateTime startDate = datetimepicker_startDate.Value;
            //Retrieves the end date from the datetimepicker button at the bottom on Windows Forms
            DateTime endDate = datetimepicker_endDate.Value;
            
            candlesticks = readCandlesticksFromFile(stockPath);

            //calling method to filter the candlesticks(to a specific range based on new* start and end date)
            filterCandlesticks();
            //normalize the candlesticks based on the new filters(only show the relevant part of the Y-Axis)
            normalizechart();
            //display the candlesticks based on new filteration
            displayCandlesticks();
            //populate the combobox with pattern names
            recognizer_xxx = populate_and_verify();
            //filter the combobox
            combobox_filteration();
        }


        /// <summary>
        /// Adds the functionality for "Pick a Stock" button to open the file explorer and chose the stock
        /// </summary>
        /// <param name="sender">Source of event</param>
        /// <param name="e">Contains the event data</param>
        private void button_Stock_Picker(object sender, EventArgs e)
        {
            //opens the file dialog to choose a ticker file
            openFileDialog_TickerChooser.ShowDialog();
        }

        /// <summary>
        /// NOTE: TA has noticed us that some functions that already use void can be exempt from having two versions(Update function will be the exception here as it will only have one version)
        /// Adds the functionality for "Update" button to update the datagriedview and visualizations according to the new date input
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">contains event data</param>
        private void button_update(object sender, EventArgs e)
        {
            //Retrieves the start date from the datetimepicker button at the top on Windows Forms
            DateTime startDate = datetimepicker_startDate.Value;
            //Retrieves the end date from the datetimepicker button at the bottom on Windows Forms
            DateTime endDate = datetimepicker_endDate.Value;

            //calling method to filter the candlesticks(to a specific range based on new* start and end date)
            filterCandlesticks();
            //normalize the candlesticks based on the new filters(only show the relevant part of the Y-Axis)
            normalizechart();
            //display the candlesticks based on new filteration
            displayCandlesticks();
            //points at the candlestick using arrow annotation
            point_to_candlestick();
        }


        /// <summary>
        /// After the file is chosen, this functions reads, filters, normalizes, and displays the candlesticks
        /// Now it does it for Multiple functions
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">contains event data</param>
        private void fileHandler(object sender, CancelEventArgs e)
        {
            chart_candlesticks.Annotations.Clear();
            //n is the number of windows opened(or number of files opened in other words)
            int n = openFileDialog_TickerChooser.FileNames.Count();

            //yyyy here is a prior check to know if the code will be dealing with multiple file openings or a single one
            int yyyy = 0;
            foreach (string filepath in openFileDialog_TickerChooser.FileNames)
            {
                yyyy++;
            }

            //if single file is opened we will proccees similarly to Project 1
            if(yyyy==1)
            {
                //if we are on the default file, we procceed the same way as Project 1
                readCandlesticksFromFile();
                //calling method to filter the candlesticks(to a specific range based on new* start and end date)
                filterCandlesticks();
                //normalize the candlesticks based on the new filters(only show the relevant part of the Y-Axis)
                normalizechart();
                //display the candlesticks based on new filteration
                displayCandlesticks();

                //points at the candlestick using arrow annotation
                point_to_candlestick();
            }

            //if there are multiple file situation, we will deal with it like project 2
            else
            { 
            //we will keep the count to sperate parent windows from the child windows
            int count = 0;

            //this for loop iterates through all files
            foreach (string filepath in openFileDialog_TickerChooser.FileNames)
            {
                //we define the stockviewever first
                Form_Candlesticks stockViewer;
                if (count==0)
                {
                    //if we are on the default file, we procceed the same way as Project 1
                    readCandlesticksFromFile();

                    //filter the combobox
                    //combobox_filteration();
                }

                else
                {
                    //For the rest of the, they have to be initialized seperately, using the constructor
                    stockViewer = new Form_Candlesticks(filepath, datetimepicker_startDate.Value, datetimepicker_endDate.Value);
                    //To distinguish child window from parent window, and accidentally closing all windows, all child windows will have the word "Child" next to the file path
                    stockViewer.Text = filepath + "    Child";
                    //reads the candlesticks
                    readCandlesticksFromFile();

                    //and shows it on the graph
                    stockViewer.Show();

                    //calling method to filter the candlesticks(to a specific range based on new* start and end date)
                    filterCandlesticks();
                    //normalize the candlesticks based on the new filters(only show the relevant part of the Y-Axis)
                    normalizechart();
                    //display the candlesticks based on new filteration
                    displayCandlesticks();

                    //points at the candlestick using arrow annotation
                    point_to_candlestick();
                }
                //increment count(although it is probably not needed as long as it not zero, since for loop is not dependent on count itself)
                count++;
            }
            }
        }


        //Candlestick Reader Version 1 and Version 2
        /// <summary>
        /// Reads the candleticks from the file and adds it to the list of candlesticks(Version 1)
        /// </summary>
        /// <param name="filename">filename refers to name of the file that was opened with "pick a stock" button</param>
        /// <returns>returns the final list of the candlesticks</returns>
        private List<SmartCandlestick> readCandlesticksFromFile(string filename)
        {
            //string to validate the file format does match
            const string referenceString = "Date,Open,High,Low,Close,Adj Close,Volume";
            //start reading the file:
            using (StreamReader sr = new StreamReader(filename))
            {
                //clean the previous leftovers from the list
                candlesticks.Clear();

                //reads the first line which are liekly the headers
                string line = sr.ReadLine();

                //checks if the file format is not wrong or the file is not empty
                if (line != null && line == referenceString)
                {
                    //if not the while loop is initiated and reads all the lines
                    while ((line = sr.ReadLine()) != null)
                    {
                        //if the line that is being read is not an empty line, that line is added to the list of candlesticks
                        if (!string.IsNullOrEmpty(line))
                        {
                            //stores the particyular line as candlestick standard
                            SmartCandlestick cs = new SmartCandlestick(line);
                            //adds the single candlestick to the list of candlesticks
                            candlesticks.Add(cs);
                        }
                    }
                }

                //in case there is something abnormal with the file and the while loop is not initiated, the code will tell there is something wrong with the file
                else
                {
                    Text = "badfile";
                }

                //after everything is finished, function returns the complete list of candlesticks
                return candlesticks;
            }
        }

        /// <summary>
        /// Void for the function of same name, reads the filename and passes it to the first function above(This is Version 2)
        /// </summary>
        private void readCandlesticksFromFile()
        {
            //reads the name of the file
            string filename = openFileDialog_TickerChooser.FileName;
            //shows the filename at the top of the window to know which file was opened
            this.Text = filename;
            //calls the function of same name by giving the name of the file as the parameter
            readCandlesticksFromFile(filename);
        }

        //Filter Candlesticks Version 1 and Version 2
        /// <summary>
        /// Filters the candlestick to a specific range only(Version 1)
        /// </summary>
        /// <param name="unfilteredList">first the function is given the raw, unfiltered list of candlesticks</param>
        /// <param name="startDate">Then function is given the startdate</param>
        /// <param name="endDate">And then function is given the enddate</param>
        /// <returns>function returns the unfileteredlist (now filtered) where the date of the line is between start and end date</returns>
        private List<SmartCandlestick> filterCandlesticks(List<SmartCandlestick> unfilteredList, DateTime startDate, DateTime endDate)
        {
            //Professor has asked an additional explanation for how LINQ works if we use this version of the filter function:
            //This line applies the filetering process to the uniflitered list by utilizing LINQ's 'where' method to iterate over each element.
            //During the interation, each candlestick object, which is referred to as 'c' in the lambda expression, is evaluated to determine if the date property is in the specified range.
            //The comparison c.Date >= startDate && c.Date <= endDate ensures that only candlesticks in this range is returned as part of the unfiltered(now filtered list)
            //One weakness of this algorithm is that it does not get any advantage from pre-sorted tables as the loop still goes on even if the endDate hasbeen reached,
            //Which should have been the end -> as we know from (ascending)sorted list that all the numbers that come after endDate will be bigger, thus none would meet the requirement to be in the list(c.Date <= endDate)
            //This method is prefferered due to its simpler and understandable writing approach (Occam's Razor if you like philosophy)
            return unfilteredList.Where(c => c.Date >= startDate && c.Date <= endDate).ToList();

        }

        /// <summary>
        /// The function of the same name with void type(Version 2). The startdate and enddate is received from the button on Windows Forms
        /// </summary>
        private void filterCandlesticks()
        {
            //Retrieves the start date from the datetimepicker button at the top on Windows Forms
            DateTime startDate = datetimepicker_startDate.Value;
            //Retrieves the end date from the datetimepicker button at the bottom on Windows Forms
            DateTime endDate = datetimepicker_endDate.Value;

            //the new binding list is set to be the returned value based on the function of the same name with the given parameters(Version 1) 
            boundCandlesticks = new BindingList<SmartCandlestick>(filterCandlesticks(candlesticks, datetimepicker_startDate.Value, datetimepicker_endDate.Value));
        }



        //NormalizeChart Version 1 and Version 2
        /// <summary>
        /// Since there are two values two return, tuple data type was the preference for the Version-1. The first parts finds out the max and the minimum of the filtered candlesticks and returns those values accordingly.
        /// </summary>
        /// <param name="min">minimum value there is on filtered candlesticks</param>
        /// <param name="max">maximum value there is on filtered candlesticks</param>
        /// <returns>returns the minimum and the maximum values</returns>
        private Tuple<Decimal, Decimal> normalizechart(Decimal min, Decimal max)
        {
            //goes through all candlesticks
            for (int i = 0; i < boundCandlesticks.Count; ++i)
            {
                //chooses one candlestick at a time
                Candlestick cs = boundCandlesticks[i];

                //if the candlestick's low value is lower than the current minimum vlaue, the new lowest(min) is set to be that
                if (cs.Low < min)
                {
                    min = cs.Low;
                }
                //if the candlestick's max value is higher than the current maximum value, the new highest(max) is set to be that
                if (cs.High > max)
                {
                    max = cs.High;
                }
            }

            //returns the minimum and maximum values after going through all the candlesticks
            return Tuple.Create(min, max);
        }

        /// <summary>
        /// Void versipon of the same-name function(Version 2). Buffer, min, max is defined and the min-max is set to the Version-1 of the same function to be updated to the new values. Based on the new updated values maximum and minimum of the Y-Axis will be set.
        /// </summary>
        private void normalizechart()
        {
            //buffer is currently set to have the Y-Axis 2% below the lowest and 2% above the highest to have a more readable design
            double buffer = 0.02;
            //min is set to be a very high number to ensure tha, the min will be getting updated when the version-1 of same-name function that is being called
            Decimal min = 100000000;
            //max is set to be a very low number to ensure that the max will be getting updated when the version-1 of same-name function that is being called
            Decimal max = 0;

            //updates the min/max accordingly
            (min, max) = normalizechart(min, max);

            //the minimum is set to be the 98%(100-2) of the lowest for better visibility
            chart_candlesticks.ChartAreas["ChartArea_OHLC"].AxisY.Minimum = (double)Math.Floor(min) * (1 - buffer);
            //the maximum is set to be the 102%(100+2) of the lowest for better visibility
            chart_candlesticks.ChartAreas["ChartArea_OHLC"].AxisY.Maximum = (double)Math.Ceiling(max) * (1 + buffer);

            /*
            CURRENTLY NOT IN USE
            //uses boundcandlesticks for the source of the data to be used
            chart_candlesticks.DataSource = boundCandlesticks;
            //binds the gathered data to the chart
            chart_candlesticks.DataBind();
            */
        }

        //Display Version 1 and Version 2
        /// <summary>
        /// Displays the final version of the candlesticks(Version 1)
        /// </summary>
        /// <param name="boundCandlesticks">refers to the binding list of the candlesticks</param>
        /// <returns>returns the candlesticks</returns>
        private BindingList<SmartCandlestick> displayCandlesticks(BindingList<SmartCandlestick> boundCandlesticks)
        {
            //The parameter that will be displayed on X-Axis is set to be the date(s)
            chart_candlesticks.Series[0].XValueMember = "Date";
            //The parameter that will be displayed on Y-Axis is set to be the OHLC values(the order here is High, Low, Open, Close becaue the code would become unstable in that order)
            chart_candlesticks.Series[0].YValueMembers = "High, Low, Open, Close";

            //returns the binding list of candlesticks
            return boundCandlesticks;
        }

        /// <summary>
        /// Void version of the function of same name(version-2). Displays the candlesticks on the chart and datagridview.
        /// </summary>
        private void displayCandlesticks()
        {
            //shows the candlesticks on the datagridview
            //dataGridView_candlesticks.DataSource = boundCandlesticks;
            //uses boundcandlesticks for the source of the data to be used
            chart_candlesticks.DataSource = displayCandlesticks(boundCandlesticks);
            //binds the gathered data to the chart
            chart_candlesticks.DataBind();
        }

        /// <summary>
        /// base function for the combobox button, calls the arrow function(point_to_candlestick)
        /// </summary>
        /// <param name="sender">source of event</param>
        /// <param name="e">contains event data</param>
        private void button_combobox(object sender, EventArgs e)
        {
            //points at the candlestick using arrow annotation
            point_to_candlestick();
        }

        /// <summary>
        /// Populates the combobox with the methods starting with "Is" in particular
        /// </summary>
        private void combobox_filteration()
        {
            //create list of strings that will store the method names
            List<string> items_combobox = new List<string>();
            //retrieves all methods from smartcandlestick that start with Is(Isbearish, IsMarubozu etc.) and stores them into the methods[] array
            MethodInfo[] methods = typeof(SmartCandlestick).GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(method => method.Name.StartsWith("Is")).ToArray();

            //We loop through each method and find the pattern_name, and recognizer duos
            foreach (KeyValuePair<string, recognizer> method in recognizer_xxx)
            {
                //adds the name of each method in particular to the items array
                items_combobox.Add(method.Key);
            }

            //bindingsource object gets created, that will be used for data binding
            var bindingsource_combobox_filteration = new BindingSource();
            //sets the data source of bindingsource to the items array
            bindingsource_combobox_filteration.DataSource = items_combobox;
            //We bind the combobox button to the list of method names
            combobox_button.DataSource = bindingsource_combobox_filteration;
        }

        /// <summary>
        /// this will be the dictionary we will use for Project 3
        /// Instead of boolean, we now use recognizer class as the method of verifying(which is also returning bool in a way)
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, recognizer> populate_and_verify()
        {
            //Have candlestick available to check Bullish pattern on combobox
            recognizer_xxx.Add("IsBullish", new recognizer_Bullish());

            //Have candlestick available to check Bearish pattern on combobox
            recognizer_xxx.Add("IsBearish", new recognizer_Bearish());

            //Have candlestick available to check Neutral pattern on combobox
            recognizer_xxx.Add("IsNeutral", new recognizer_Neutral());

            //Have candlestick available to check Marubozu pattern on combobox
            recognizer_xxx.Add("IsMarubozu", new recognizer_Marubozu());

            //Have candlestick available to check Hammer pattern on combobox
            recognizer_xxx.Add("IsHammer", new recognizer_Hammer());

            //Have candlestick available to check Doji pattern on combobox
            recognizer_xxx.Add("IsDoji", new recognizer_Doji());

            //Have candlestick available to check Dragonfly Doji pattern on combobox
            recognizer_xxx.Add("IsDragonflyDoji", new recognizer_Dragonfly_Doji());

            //Have candlestick available to check Gravestone Doji pattern on combobox
            recognizer_xxx.Add("IsGravestoneDoji", new recognizer_Gravestone_Doji());

            //Have candlestick available to check Bullish Harami pattern on combobox
            recognizer_xxx.Add("IsBullishHarami", new recognizer_Bullish_Harami());

            //Have candlestick available to check Bearish Harami pattern on combobox
            recognizer_xxx.Add("IsBearishHarami", new recognizer_Bearish_Harami());

            //Have candlestick available to check Bullish Engulfing pattern on combobox
            recognizer_xxx.Add("IsBullishEngulfing", new recognizer_Bullish_Engulfing());

            //Have candlestick available to check Bearish Engulfing pattern on combobox
            recognizer_xxx.Add("IsBearishEngulfing", new recognizer_Bearish_Engulfing());

            //Have candlestick available to check Peak pattern on combobox
            recognizer_xxx.Add("IsPeak", new recognizer_Peak());

            //Have candlestick available to check Valley pattern on combobox
            recognizer_xxx.Add("IsValley", new recognizer_Valley());

            //return the dictionary with all of the pattern types and the way of identifying it 
            return recognizer_xxx;
        }

        /// <summary>
        /// Uses arrow annotation to "point" into the candlestick that has the desired property
        /// </summary>
        private void point_to_candlestick()
        {
            //clear everything from the previous annotations to have a clean setup
            chart_candlesticks.Annotations.Clear();
            //retrieves the item from combobox in string format
            if (combobox_button.SelectedItem != null)
            {
                //get the pattern_name chosen on combobox
                string selectedMethodName = combobox_button.SelectedItem.ToString();

                //check for the pattern on all of the candlesticks
                recognizer_xxx[selectedMethodName].recognizeAll(candlesticks);
                //loops through every smartcandlestick
                foreach (SmartCandlestick smart_candlestick in boundCandlesticks)
                {
                    //check whether the "checked" candlestick's pattern is true or not
                    bool result = smart_candlestick.candlestick_pattern[selectedMethodName];

                    //if "checked" candlestick is in fact true:
                    if (result == true)
                    {
                        //call the arrow pointer function to have and arrow(s) accordingly based on the type of pattern
                        recognizer_xxx[selectedMethodName].point_to_candlesticks(boundCandlesticks, boundCandlesticks.IndexOf(smart_candlestick), chart_candlesticks);
                    }
                }
            }
        }




    }
}