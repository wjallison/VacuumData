using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacuumData
{
    public class ReportAssembly
    {
        public string vacType;
        public StringBuilder report;
        public double[] values = { 0, 0, 0,
                                   0, 0, 0,
                                   0, 0 };

        //public ReportAssembly(string type, string serial, 
        //    double[] allowables,
        //    double currentFull, double currentHalf, double currentClosed,
        //    double pressureFull, double pressureHalf, double pressureClosed,
        //    double flowFull, double flowHalf)
        //{
        //    report.Append("REPORT: S/N ");
        //    report.AppendLine(serial);
        //    report.AppendLine();
        //    report.Append("EVALUATION: ");
            
        //    //EVALUATE
        //    if(currentFull < allowables[0] || currentFull > allowables[1]|| 
        //        currentHalf <allowables[2] || currentHalf > allowables[3]||
        //        currentClosed < allowables[4] || currentClosed > allowables[5]||
        //        pressureFull < allowables[6] || pressureFull > allowables[7]||
        //        pressureHalf < allowables[8] || pressureHalf > allowables[9]||
        //        pressureClosed < allowables[10] || pressureClosed > allowables[11]||
        //        flowFull < allowables[12] || flowFull > allowables[13]||
        //        flowHalf < allowables[14] || flowHalf > allowables[15])
        //    {
        //        report.AppendLine("FAILED");
        //    }
        //    else { report.AppendLine("PASSED"); }

        //    //TODO: Implement warnings
        //    report.AppendLine("WARNINGS:");

        //    report.AppendLine();


        //    report.AppendLine("CURRENT:");

        //    report.Append("Full Airflow:");
        //    report.Append('\t');
        //    report.Append('\t');
        //    report.AppendLine(currentFull.ToString());
        //    report.Append("Max:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[0].ToString());
        //    report.Append("Min:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[1].ToString());

        //    report.Append("Halved Airflow:");
        //    report.Append('\t');
        //    report.Append('\t');
        //    report.AppendLine(currentHalf.ToString());
        //    report.Append("Max:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[2].ToString());
        //    report.Append("Min:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[3].ToString());

        //    report.Append("No Airflow:");
        //    report.Append('\t');
        //    report.Append('\t');
        //    report.AppendLine(currentClosed.ToString());
        //    report.Append("Max:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[4].ToString());
        //    report.Append("Min:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[5].ToString());


        //    report.AppendLine("PRESSURE:");

        //    report.Append("Full Airflow:");
        //    report.Append('\t');
        //    report.Append('\t');
        //    report.AppendLine(pressureFull.ToString());
        //    report.Append("Max:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[6].ToString());
        //    report.Append("Min:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[7].ToString());

        //    report.Append("Halved Airflow:");
        //    report.Append('\t');
        //    report.Append('\t');
        //    report.AppendLine(pressureHalf.ToString());
        //    report.Append("Max:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[8].ToString());
        //    report.Append("Min:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[9].ToString());

        //    report.Append("No Airflow:");
        //    report.Append('\t');
        //    report.Append('\t');
        //    report.AppendLine(currentClosed.ToString());
        //    report.Append("Max:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[10].ToString());
        //    report.Append("Min:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[11].ToString());


        //    report.AppendLine("AIRFLOW:");

        //    report.Append("Full Airflow:");
        //    report.Append('\t');
        //    report.Append('\t');
        //    report.AppendLine(flowFull.ToString());
        //    report.Append("Max:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[12].ToString());
        //    report.Append("Min:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[13].ToString());

        //    report.Append("Halved Airflow:");
        //    report.Append('\t');
        //    report.Append('\t');
        //    report.AppendLine(flowHalf.ToString());
        //    report.Append("Max:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[14].ToString());
        //    report.Append("Min:");
        //    report.Append('\t');
        //    report.AppendLine(allowables[15].ToString());
        //}

        public ReportAssembly(Test t)
        {
            report.Append("REPORT: S/N ");
            report.AppendLine(t.serialNumber);
            report.AppendLine();
            report.Append("EVALUATION: ");
            if (t.failTest.Contains(true)) { report.AppendLine("FAILED"); }
            else { report.AppendLine("Passed"); }

            //TODO: Implement warnings
            report.AppendLine("WARNINGS:");

            report.AppendLine();


            report.AppendLine("CURRENT:");

            report.Append("Full Airflow:");
            report.Append('\t');
            report.Append('\t');
            report.AppendLine(t.values[0].ToString());
            report.Append("Max:");
            report.Append('\t');
            report.AppendLine(t.allowables[0].ToString());
            report.Append("Min:");
            report.Append('\t');
            report.AppendLine(t.allowables[1].ToString());

            report.Append("Halved Airflow:");
            report.Append('\t');
            report.Append('\t');
            report.AppendLine(t.values[1].ToString());
            report.Append("Max:");
            report.Append('\t');
            report.AppendLine(t.allowables[2].ToString());
            report.Append("Min:");
            report.Append('\t');
            report.AppendLine(t.allowables[3].ToString());

            report.Append("No Airflow:");
            report.Append('\t');
            report.Append('\t');
            report.AppendLine(t.values[2].ToString());
            report.Append("Max:");
            report.Append('\t');
            report.AppendLine(t.allowables[4].ToString());
            report.Append("Min:");
            report.Append('\t');
            report.AppendLine(t.allowables[5].ToString());


            report.AppendLine("PRESSURE:");

            report.Append("Full Airflow:");
            report.Append('\t');
            report.Append('\t');
            report.AppendLine(t.values[3].ToString());
            report.Append("Max:");
            report.Append('\t');
            report.AppendLine(t.allowables[6].ToString());
            report.Append("Min:");
            report.Append('\t');
            report.AppendLine(t.allowables[7].ToString());

            report.Append("Halved Airflow:");
            report.Append('\t');
            report.Append('\t');
            report.AppendLine(t.values[4].ToString());
            report.Append("Max:");
            report.Append('\t');
            report.AppendLine(t.allowables[8].ToString());
            report.Append("Min:");
            report.Append('\t');
            report.AppendLine(t.allowables[9].ToString());

            report.Append("No Airflow:");
            report.Append('\t');
            report.Append('\t');
            report.AppendLine(t.values[5].ToString());
            report.Append("Max:");
            report.Append('\t');
            report.AppendLine(t.allowables[10].ToString());
            report.Append("Min:");
            report.Append('\t');
            report.AppendLine(t.allowables[11].ToString());


            report.AppendLine("AIRFLOW:");

            report.Append("Full Airflow:");
            report.Append('\t');
            report.Append('\t');
            report.AppendLine(t.values[6].ToString());
            report.Append("Max:");
            report.Append('\t');
            report.AppendLine(t.allowables[12].ToString());
            report.Append("Min:");
            report.Append('\t');
            report.AppendLine(t.allowables[13].ToString());

            report.Append("Halved Airflow:");
            report.Append('\t');
            report.Append('\t');
            report.AppendLine(t.values[7].ToString());
            report.Append("Max:");
            report.Append('\t');
            report.AppendLine(t.allowables[14].ToString());
            report.Append("Min:");
            report.Append('\t');
            report.AppendLine(t.allowables[15].ToString());


        }


    }

    public class Test
    {
        public bool pass = true;

        public string serialNumber;
        public string vacType;
        
        public double[] values = { 0, 0, 0,
                                   0, 0, 0,
                                   0, 0 };
        public double[] allowables;
        public bool[] failTest = {false,false,false,
                                    false,false,false,
                                    false,false};
        public int[] warning = { 0, 0, 0,
                                   0, 0, 0,
                                   0, 0 };

        public Test()
        {

        }

        public Test(string type, string serial,
            double[] allowed,
            double currentFull, double currentHalf, double currentClosed,
            double pressureFull, double pressureHalf, double pressureClosed,
            double flowFull, double flowHalf)
        {
            serialNumber = serial;
            vacType = type;
            allowables = allowed;

            values[0] = currentFull;
            values[1] = currentHalf;
            values[2] = currentClosed;

            values[3] = pressureFull;
            values[4] = pressureHalf;
            values[5] = pressureClosed;

            values[6] = flowFull;
            values[7] = flowHalf;

            //Check for errors
            if(values[0] < allowed[0]) { failTest[0] = true; }
            if(values[0] > allowed[1]) { failTest[0] = true; }
            if(values[1] < allowed[2]) { failTest[1] = true; }
            if(values[1] > allowed[3]) { failTest[1] = true; }
            if(values[2] < allowed[4] || values[2] > allowed[5]) { failTest[2] = true; }

            if (values[3] < allowed[6] || values[3] > allowed[7]) { failTest[3] = true; }
            if (values[4] < allowed[8] || values[4] > allowed[9]) { failTest[4] = true; }
            if (values[5] < allowed[10] || values[5] > allowed[11]) { failTest[5] = true; }

            if (values[6] < allowed[12] || values[6] > allowed[13]) { failTest[6] = true; }
            if (values[7] < allowed[14] || values[7] > allowed[15]) { failTest[7] = true; }

            //Check for warnings

        }

        public double averageData(List<double> vals)
        {
            double total = 0;
            for (int i = 0; i < vals.Count(); i++)
            {
                total += vals[i];
            }
            return total / vals.Count();
        }

    }
}
