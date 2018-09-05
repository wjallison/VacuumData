using System;
using System.Threading.Tasks;
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
using Windows.Devices.I2c;
using Windows.Devices.Enumeration;

using ADC.Devices.I2c.ADS1115;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VacuumData
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Preset minimums and maximums
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
        #endregion

        public Stopwatch watch;

        #region Data Storage
        public List<double> openCurrentList = new List<double>();
        public List<double> halfCurrentList = new List<double>();
        public List<double> closedCurrentList = new List<double>();

        public List<double> openPressureList = new List<double>();
        public List<double> halfPressureList = new List<double>();
        public List<double> closedPressureList = new List<double>();

        public List<double> openFlowList = new List<double>();
        public List<double> halfFlowList = new List<double>();

        public Test t = new Test();
        public ReportAssembly r;

        #endregion

        //var gpio = GpioController.GetDefault();
        GpioPin pin;

        private I2cDevice sensorPressure;
        private ADS1115Sensor adc;
        //private I2cDevice analogConverter;
        private DispatcherTimer timer;


        //public Timer t;

        public MainPage()
        {
            this.InitializeComponent();
            
            //string s = 
            

            #region Add types of vacuums
            vTypeSelectionBox.Items.Add("950");
            vTypeSelectionBox.Items.Add("950 HEPA");
            vTypeSelectionBox.Items.Add("1250");
            vTypeSelectionBox.Items.Add("1250 HEPA");
            vTypeSelectionBox.Items.Add("1550");
            vTypeSelectionBox.Items.Add("1550 HEPA");
            vTypeSelectionBox.Items.Add("Gas Vac");
            #endregion
        }

        #region i2c work
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //base.OnNavigatedFrom(e);
            StopScenario();
        }

        private async Task StartScenarioAsync()
        {
            string i2cDeviceSelector = I2cDevice.GetDeviceSelector();
            IReadOnlyList<DeviceInformation> devices = await DeviceInformation.FindAllAsync(i2cDeviceSelector);

            //TODO: Find proper settings value.
            var pressureDevice_settings = new I2cConnectionSettings(0x40);
            //var analogConverter_settings = new I2cConnectionSettings(0x40);

            sensorPressure = await I2cDevice.FromIdAsync(devices[0].Id, pressureDevice_settings);
            //analogConverter = await I2cDevice.FromIdAsync(devices[1].Id, analogConverter_settings);

            timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(500) };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        void StopScenario()
        {
            if(timer != null)
            {
                timer.Tick -= Timer_Tick;
                timer.Stop();
                timer = null;
            }

            if(sensorPressure != null)
            {
                sensorPressure.Dispose();
                sensorPressure = null;
            }
            //if(analogConverter != null)
            //{
            //    analogConverter.Dispose();
            //    analogConverter = null;
            //}
        }

        async void StartStopScenario()
        {
            if(timer != null)
            {
                StopScenario();
            }
            else
            {
                await StartScenarioAsync();
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            //throw new NotImplementedException();
            var command = new byte[1];
            var PressureData = new byte[2];
            var PDiffData = new byte[2];
            var CurrentData = new byte[2];

            //TODO: Find proper commands
            command[0] = 0xE5;
            sensorPressure.WriteRead(command, PressureData);

            command[0] = 0xE3;
            //analogConverter.WriteRead()

            if(adc != null && adc.IsInitialized)
            {
                try
                {
                    var rawAnalogValue = adc.readContinuous();

                }
                catch (Exception ex)
                {

                }
            }
        }

        private async void InitializeAnalogConvert()
        {
            try
            {
                adc = new ADS1115Sensor(AdcAddress.GND);
                await adc.InitializeAsync();
            }
            catch(Exception ex)
            {

            }
        }

        #endregion

        #region No Longer Used Event Handlers
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
        #endregion

        #region Page flipping buttons
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
        #endregion

        #region Convert Data to usable units

        public double ConvertToInH2O(byte[] inData)
        {
            double result = 0;

            return result;
        }

        public double ConvertToCurrent(byte[] inData)
        {
            double result = 0;

            return result;
        }

        public double ConvertToMetersPerSecond(byte[] inData)
        {
            double result = 0;

            return result;
        }

        #endregion

        private void saveAndQuitButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime current = new DateTime();
            //System.IO.Stream s = new System.IO.Stream()
            //using (System.IO.StreamWriter file = new StreamWriter(
            //    Directory.GetCurrentDirectory() +@"\" + t.serialNumber + 
            //    current.Year.ToString() + current.Month.ToString() + current.Day.ToString() + current.TimeOfDay.ToString()
            //    + ".txt"))
            //{

            //}
            System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + @"\" + t.serialNumber +
                current.Year.ToString() + current.Month.ToString() + current.Day.ToString() + current.TimeOfDay.ToString()
                + ".txt",
                r.report.ToString());
        }
    }
}
