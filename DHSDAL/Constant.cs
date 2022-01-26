using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHSDAL
{
    public class Constant
    {
        public static readonly string MRAConnectionString = Properties.Settings.Default.MRANEWConnection;

        public static readonly string SuccessfulMessage = "Record saved successfully.";

        public static readonly string ErrorMessage = "There is an error while saving the record.";
    }
}
