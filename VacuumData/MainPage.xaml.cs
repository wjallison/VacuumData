using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
//using System;
//using System.Threading;
using System.Diagnostics;
using Windows.Devices.Gpio;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VacuumData
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Define minimums and maximums
        //public double min950CurrentFull = 0, max950CurrentFull = 20,
        //    min950CurrentHalf = 0, max950CurrentHalf = 
        //{minCurrentFull,maxCurrentFull, minCHalf, maxCHalf, minCClosed, maxCClosed,
        // minPressureFull, maxPressureFull, ...
        // minAirflowFull, maxAirflowFull, ... *DOESN'T INCLUDE CLOSED*
        public double[] values950 = { 0, 20, 0, 20, 0, 20,
                                        0, 100, 0, 100, 0, 100,
                                        0, 1000, 0, 1000, 0, 0 };
        public double[] values950HEPA = { 0, 20, 0, 20, 0, 20,
                                        0, 100, 0, 100, 0, 100,
                                        0, 1000, 0, 1000, 0, 0 };
        public double[] values1250 = { 0, 20, 0, 20, 0, 20,
                                        0, 100, 0, 100, 0, 100,
                                        0, 1000, 0, 1000, 0, 0 };
        public double[] values1250HEPA = { 0, 20, 0, 20, 0, 20,
                                        0, 100, 0, 100, 0, 100,
                                        0, 1000, 0, 1000, 0, 0 };
        public double[] values1550 = { 0, 20, 0, 20, 0, 20,
                                        0, 100, 0, 100, 0, 100,
                                        0, 1000, 0, 1000, 0, 0 };
        public double[] values1550HEPA = { 0, 20, 0, 20, 0, 20,
                                        0, 100, 0, 100, 0, 100,
                                        0, 1000, 0, 1000, 0, 0 };
        public double[] valuesGasVac = { 0, 0, 0, 0, 0, 0,
                                        0, 100, 0, 100, 0, 100,
                                        0, 1000, 0, 1000, 0, 0 };

        public Stopwatch watch;

        public List<double> openCurrentList = new List<double>();
        public List<double> halfCurrentList = new List<double>();
        public List<double> closedCurrentList = new List<double>();

        public List<double> openPressureList = new List<double>();
        public List<double> halfPressureList = new List<double>();
        public List<double> closedPressureList = new List<double>();

        public List<double> openFlowList = new List<double>();
        public List<double> halfFlowList = new List<double>();

        public Test t = new Test();

        //var gpio = GpioController.GetDefault();
        GpioPin pin;

        //public Timer t;

        public MainPage()
        {
            this.InitializeComponent();
            //var autoEvent = new AutoResetEvent(false);
            //var statusChecker = new StatusChecker(10);
            vTypeSelectionBox.Items.Add("950");
            vTypeSelectionBox.Items.Add("950 HEPA");
            vTypeSelectionBox.Items.Add("1250");
            vTypeSelectionBox.Items.Add("1250 HEPA");
            vTypeSelectionBox.Items.Add("1550");
            vTypeSelectionBox.Items.Add("1550 HEPA");
            vTypeSelectionBox.Items.Add("Gas Vac");
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();
            if(gpio == null)
            {
                pin = null;
                return;
            }
            //pin = gpio.OpenPin()
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        //NO LONGER USED
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if(flipper.SelectedIndex == 0) { serialNumber.Text = snEntryTextBox.Text; }

            try
            {
                flipper.SelectedIndex++;
            }
            catch { }
        }
        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                flipper.SelectedIndex--;
            }
            catch { }
        }

        private void openFeedStartButton_Click(object sender, RoutedEventArgs e)
        {
            //Start Recording
            watch = Stopwatch.StartNew();
            flipper.SelectedIndex++;
            while(watch.ElapsedMilliseconds < 15000)
            {
                //add data to currentList
                //add data to pressureList
                //add data to flowList
                
            }

            t.values[0] = t.averageData(openCurrentList);
            t.values[3] = t.averageData(openPressureList);
            t.values[6] = t.averageData(openFlowList);

            //Stop Recording
            flipper.SelectedIndex++;
        }

        private void flipper_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (flipper.SelectedIndex )
        }

        private void zeroNextButton_Click(object sender, RoutedEventArgs e)
        {
            serialNumber.Text = snEntryTextBox.Text;
            t.serialNumber = snEntryTextBox.Text;
            t.vacType = vTypeSelectionBox.SelectedItem.ToString();
            flipper.SelectedIndex++;
        }

        private void oneNextButton_Click(object sender, RoutedEventArgs e)
        {
            flipper.SelectedIndex++;
        }

        private void twoNextButton_Click(object sender, RoutedEventArgs e)
        {
            flipper.SelectedIndex++;
        }

        private void halfFeedStartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void closedFeedStartButton_Click(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
