using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helper {
    public class ToolStripService {

        public Tuple<string, DateTime, DateTime> GenerateLabel() {

            var day = ((int)System.DateTime.Now.DayOfWeek);
            var label = "THIS WEEK, WED - TUE ";
            int endDay = 2;
            int StartDay = 3;
            var startDate = new DateTime();
            var endDate = new DateTime();
        
            if(day <= endDay){
                if (day == 1) {
                    startDate   = DateTime.Now.AddDays(-5).Date;
                    endDate     = DateTime.Now.AddDays(day + 1).Date;
                    label += " " + DateTime.Now.AddDays(-5).Date.ToString("dd") + " - " + DateTime.Now.AddDays(day + 1).Date.ToString("dd");
                } else {
                    startDate   = DateTime.Now.AddDays(-6).Date;
                    endDate     = DateTime.Now.Date;
                    label += " " + DateTime.Now.AddDays(-6).Date.ToString("dd") + " - " + DateTime.Now.Date.ToString("dd");
                }
            } else {
                startDate   = DateTime.Now.AddDays(-(day - StartDay)).Date;
                endDate     = DateTime.Now.AddDays(+(9-day)).Date;
                label += "(" + System.DateTime.Now.ToString("MMMM").ToUpper() + " " + DateTime.Now.AddDays(-(day - StartDay)).Date.ToString("dd") + " - " + System.DateTime.Now.ToString("MMMM").ToUpper() + " " + DateTime.Now.AddDays(+(9-day)).Date.ToString("dd") + ")";
            }

            return new Tuple<string,DateTime,DateTime>(label,startDate, endDate);

        }

    }
}
