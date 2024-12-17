namespace Project_candlesticks
{
    partial class Form_Candlesticks
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button_pick_stock = new System.Windows.Forms.Button();
            this.openFileDialog_TickerChooser = new System.Windows.Forms.OpenFileDialog();
            this.datetimepicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.datetimepicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.chart_candlesticks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.candlestickBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button_UpdateDates = new System.Windows.Forms.Button();
            this.combobox_button = new System.Windows.Forms.ComboBox();
            this.combobox_header = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart_candlesticks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button_pick_stock
            // 
            this.button_pick_stock.Location = new System.Drawing.Point(1143, 157);
            this.button_pick_stock.Name = "button_pick_stock";
            this.button_pick_stock.Size = new System.Drawing.Size(244, 82);
            this.button_pick_stock.TabIndex = 0;
            this.button_pick_stock.Text = "Pick a Stock";
            this.button_pick_stock.UseVisualStyleBackColor = true;
            this.button_pick_stock.Click += new System.EventHandler(this.button_Stock_Picker);
            // 
            // openFileDialog_TickerChooser
            // 
            this.openFileDialog_TickerChooser.Filter = "TickerFile|*.csv| DayFile|*-Day.csv| WeeklyFile|*-Week.csv| MonthlyFile|*-Month.c" +
    "sv";
            this.openFileDialog_TickerChooser.Multiselect = true;
            this.openFileDialog_TickerChooser.FileOk += new System.ComponentModel.CancelEventHandler(this.fileHandler);
            // 
            // datetimepicker_startDate
            // 
            this.datetimepicker_startDate.Location = new System.Drawing.Point(1163, 385);
            this.datetimepicker_startDate.Name = "datetimepicker_startDate";
            this.datetimepicker_startDate.Size = new System.Drawing.Size(200, 20);
            this.datetimepicker_startDate.TabIndex = 3;
            this.datetimepicker_startDate.Value = new System.DateTime(2022, 12, 31, 9, 56, 0, 0);
            // 
            // datetimepicker_endDate
            // 
            this.datetimepicker_endDate.Location = new System.Drawing.Point(1163, 411);
            this.datetimepicker_endDate.Name = "datetimepicker_endDate";
            this.datetimepicker_endDate.Size = new System.Drawing.Size(200, 20);
            this.datetimepicker_endDate.TabIndex = 4;
            // 
            // chart_candlesticks
            // 
            chartArea3.Name = "ChartArea_OHLC";
            chartArea4.AlignWithChartArea = "ChartArea_OHLC";
            chartArea4.Name = "ChartArea_volume";
            this.chart_candlesticks.ChartAreas.Add(chartArea3);
            this.chart_candlesticks.ChartAreas.Add(chartArea4);
            this.chart_candlesticks.DataSource = this.candlestickBindingSource;
            this.chart_candlesticks.Location = new System.Drawing.Point(12, 25);
            this.chart_candlesticks.Name = "chart_candlesticks";
            series3.ChartArea = "ChartArea_OHLC";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series3.CustomProperties = "PriceDownColor=Red, PriceUpColor=Green";
            series3.IsXValueIndexed = true;
            series3.Name = "Series_OHLC";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series3.YValuesPerPoint = 4;
            series4.ChartArea = "ChartArea_volume";
            series4.IsXValueIndexed = true;
            series4.Name = "Series_volume";
            series4.XValueMember = "Date";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series4.YValueMembers = "Volume";
            this.chart_candlesticks.Series.Add(series3);
            this.chart_candlesticks.Series.Add(series4);
            this.chart_candlesticks.Size = new System.Drawing.Size(1091, 646);
            this.chart_candlesticks.TabIndex = 5;
            this.chart_candlesticks.Text = "chart_candlesticks";
            // 
            // candlestickBindingSource
            // 
            this.candlestickBindingSource.DataSource = typeof(Candlesticks.Candlestick);
            // 
            // button_UpdateDates
            // 
            this.button_UpdateDates.Location = new System.Drawing.Point(1236, 437);
            this.button_UpdateDates.Name = "button_UpdateDates";
            this.button_UpdateDates.Size = new System.Drawing.Size(75, 23);
            this.button_UpdateDates.TabIndex = 6;
            this.button_UpdateDates.Text = "Update";
            this.button_UpdateDates.UseVisualStyleBackColor = true;
            this.button_UpdateDates.Click += new System.EventHandler(this.button_update);
            // 
            // combobox_button
            // 
            this.combobox_button.FormattingEnabled = true;
            this.combobox_button.Location = new System.Drawing.Point(1143, 316);
            this.combobox_button.Name = "combobox_button";
            this.combobox_button.Size = new System.Drawing.Size(244, 21);
            this.combobox_button.TabIndex = 8;
            this.combobox_button.SelectedIndexChanged += new System.EventHandler(this.button_combobox);
            // 
            // combobox_header
            // 
            this.combobox_header.Location = new System.Drawing.Point(1181, 290);
            this.combobox_header.Name = "combobox_header";
            this.combobox_header.Size = new System.Drawing.Size(168, 20);
            this.combobox_header.TabIndex = 10;
            this.combobox_header.Text = "Choose the candlestick pattern:";
            // 
            // Form_Candlesticks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1438, 717);
            this.Controls.Add(this.combobox_header);
            this.Controls.Add(this.combobox_button);
            this.Controls.Add(this.button_UpdateDates);
            this.Controls.Add(this.chart_candlesticks);
            this.Controls.Add(this.datetimepicker_endDate);
            this.Controls.Add(this.datetimepicker_startDate);
            this.Controls.Add(this.button_pick_stock);
            this.Name = "Form_Candlesticks";
            this.Text = "Form_Candlesticks";
            ((System.ComponentModel.ISupportInitialize)(this.chart_candlesticks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_pick_stock;
        private System.Windows.Forms.OpenFileDialog openFileDialog_TickerChooser;
        private System.Windows.Forms.BindingSource candlestickBindingSource;
        private System.Windows.Forms.DateTimePicker datetimepicker_startDate;
        private System.Windows.Forms.DateTimePicker datetimepicker_endDate;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_candlesticks;
        private System.Windows.Forms.Button button_UpdateDates;
        private System.Windows.Forms.ComboBox combobox_button;
        private System.Windows.Forms.TextBox combobox_header;
    }
}

