using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace EMSSystem.Functions
{
    class NormalFunctions
    {
        public string ChangeAccessDateFormatToMySqlDateFormat(string accessDate)
        {
            try
            {
                string date, month, year;
                string[] splitedDate = accessDate.Split('/');

                if (splitedDate[0].Length > 2)
                {
                    year = splitedDate[0];
                    date = splitedDate[2];
                }
                else
                {
                    year = splitedDate[2];
                    date = splitedDate[0];
                }

                month = splitedDate[1];

                return year + "-" + month + "-" + date;
            }
            catch
            {
                return "0000-00-00";
            }
        }

        #region Create DataTable

        //Create Usable DataTable
        public DataTable BuildDataTable(bool needBalance)
        {
            DataTable dtDetail;
            dtDetail = new DataTable();
            dtDetail.Columns.Add(new DataColumn("Date"));
            dtDetail.Columns.Add(new DataColumn("Category"));
            dtDetail.Columns.Add(new DataColumn("Detail"));
            dtDetail.Columns.Add(new DataColumn("Outgoings"));
            dtDetail.Columns.Add(new DataColumn("Income"));
            if (needBalance)
                dtDetail.Columns.Add(new DataColumn("Balance"));

            return dtDetail;
        }

        public DataTable BuildDataTableWithIDs()
        {
            DataTable dtDetail;
            dtDetail = new DataTable();
            dtDetail.Columns.Add(new DataColumn("DetailID"));
            dtDetail.Columns.Add(new DataColumn("MonthID"));
            dtDetail.Columns.Add(new DataColumn("Date"));
            dtDetail.Columns.Add(new DataColumn("Category"));
            dtDetail.Columns.Add(new DataColumn("Detail"));
            dtDetail.Columns.Add(new DataColumn("Outgoings"));
            dtDetail.Columns.Add(new DataColumn("Income"));

            return dtDetail;
        }

        public DataTable BuildDataTableForRepeating()
        {
            DataTable dtDetail;
            dtDetail = new DataTable();
            dtDetail.Columns.Add(new DataColumn("Category"));
            dtDetail.Columns.Add(new DataColumn("StartDate"));
            dtDetail.Columns.Add(new DataColumn("EndDate"));
            dtDetail.Columns.Add(new DataColumn("NextDate"));
            dtDetail.Columns.Add(new DataColumn("Detail"));
            dtDetail.Columns.Add(new DataColumn("Outgoings"));
            dtDetail.Columns.Add(new DataColumn("Income"));
            dtDetail.Columns.Add(new DataColumn("RepeatType"));

            return dtDetail;
        }

        public DataTable BuildDataTableForRepeatingWithIDs()
        {
            DataTable dtDetail;
            dtDetail = new DataTable();
            dtDetail.Columns.Add(new DataColumn("RepeatID"));
            dtDetail.Columns.Add(new DataColumn("Category"));
            dtDetail.Columns.Add(new DataColumn("StartDate"));
            dtDetail.Columns.Add(new DataColumn("EndDate"));
            dtDetail.Columns.Add(new DataColumn("NextDate"));
            dtDetail.Columns.Add(new DataColumn("Detail"));
            dtDetail.Columns.Add(new DataColumn("Outgoings"));
            dtDetail.Columns.Add(new DataColumn("Income"));
            dtDetail.Columns.Add(new DataColumn("RepeatType"));
            dtDetail.Columns.Add(new DataColumn("ReminderID"));
            dtDetail.Columns.Add(new DataColumn("DetailID"));

            return dtDetail;
        }

        public DataTable BuildDataTableForDetailInfo()
        {
            DataTable dtDetail;
            dtDetail = new DataTable();
            dtDetail.Columns.Add(new DataColumn("Category"));
            dtDetail.Columns.Add(new DataColumn("Monthly"));
            dtDetail.Columns.Add(new DataColumn("NeedCategory"));
            dtDetail.Columns.Add(new DataColumn("NeedBalance"));

            return dtDetail;
        }

        #endregion


        #region Date Format

        //Make Right Format of Date
        public string ChangeDateFormatForMySql(DateTime oldDateFormat)
        {
            try
            {
                return oldDateFormat.ToString("yyyy-MM-dd");
            }
            catch
            {
                return "";
                
            }
        }

        public string ChangeDateFormatForMySql(string oldDateFormat)
        {
            try
            {
                DateTime newDateFormat = DateTime.Parse(oldDateFormat);
                return newDateFormat.ToString("yyyy-MM-dd");
            }
            catch
            {
                return "";
            }
        }

        public string ChangeDateFormatForAussieType(string oldDateFormat)
        {
            try
            {
                DateTime newDateFormat = DateTime.Parse(oldDateFormat);
                return newDateFormat.ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }

        #endregion


        #region Change DateTime

        public string ReturnMonthAndYearOnlyWithMySqlFormat(DateTime day)
        {
            string newDay = day.ToString("yyyy-MM-dd");
            return newDay.Substring(0, 7);
        }

        public string ReturnMonthAndYearOnlyWithMySqlFormat(string day)
        {
            day = ChangeDateFormatForMySql(day);
            return day.Substring(0, 7);
        }

        public string IncreaseDateByYearOrMonthOrDate(string oldDate, int dayInterval, string type)
        {
            DateTime newDate = DateTime.Parse(oldDate);
            int year = newDate.Year;
            int month = newDate.Month;
            int date = newDate.Day;
            string newMonth, newDay;
            if (dayInterval == 0)
                dayInterval = 1;

            if (type.ToLower().IndexOf("year") > -1)
                year += dayInterval;
            else if (type.ToLower().IndexOf("month") > -1)
            {
                newDate = DateTime.Parse(GetLatterMonthByIntervalWithMySqlFormat(oldDate, dayInterval));
                year = newDate.Year;
                month = newDate.Month;
            }
            else if (type.ToLower().IndexOf("week") > -1)
            {
                newDate = newDate.AddDays(7 * dayInterval);

                year = newDate.Year;
                month = newDate.Month;
                date = newDate.Day;
            }
            else if (type.ToLower().IndexOf("day") > -1)
            {
                newDate = newDate.AddDays(dayInterval);

                year = newDate.Year;
                month = newDate.Month;
                date = newDate.Day;
            }
            else if (IsNumberic(type))
            {
                newDate = newDate.AddDays(7 * int.Parse(type));

                year = newDate.Year;
                month = newDate.Month;
                date = newDate.Day;
            }

            if (month.ToString().Length == 1)
                newMonth = "0" + month;
            else
                newMonth = month.ToString();

            if (date.ToString().Length == 1)
                newDay = "0" + date;
            else
                newDay = date.ToString();

            return year + "-" + newMonth + "-" + newDay;
        }

        public string IncreaseDateByYearOrMonthOrDate(string oldDate, string type)
        {
            return IncreaseDateByYearOrMonthOrDate(oldDate, 0, type);
        }

        public string DecreaseDateByYearOrMonthOrDate(string oldDate, string type)
        {
            DateTime newDate = DateTime.Parse(oldDate);
            int year = newDate.Year;
            int month = newDate.Month;
            int date = newDate.Day;
            string newMonth, newDay;

            if (type.ToLower().IndexOf("year") > -1)
                year -= 1;
            else if (type.ToLower().IndexOf("month") > -1)
            {
                newDate = DateTime.Parse(GetPreviousMonthByIntervalWithMySqlFormat(oldDate, 1));
                year = newDate.Year;
                month = newDate.Month;
            }
            else if (type.ToLower().IndexOf("week") > -1)
            {
                newDate = newDate.AddDays(-7);

                year = newDate.Year;
                month = newDate.Month;
                date = newDate.Day;
            }
            else if (type.ToLower().IndexOf("day") > -1)
            {
                newDate = newDate.AddDays(-1);

                year = newDate.Year;
                month = newDate.Month;
                date = newDate.Day;
            }
            else if (IsNumberic(type))
            {
                newDate = newDate.AddDays(-7 * int.Parse(type));

                year = newDate.Year;
                month = newDate.Month;
                date = newDate.Day;
            }

            if (month.ToString().Length == 1)
                newMonth = "0" + month;
            else
                newMonth = month.ToString();

            if (date.ToString().Length == 1)
                newDay = "0" + date;
            else
                newDay = date.ToString();

            return year + "-" + newMonth + "-" + newDay;
        }

        #endregion

        private string PutDateTogetherWithMySqlFormat(string year, string month, string day)
        {
            try
            {
                DateTime today = DateTime.Now;
                if (day == "")
                    day = "01";
                if (month == "")
                    month = today.Month.ToString();
                if (year == "")
                    year = today.Year.ToString();

                if (!IsNumberic(month))
                {
                    DateTime newDate = DateTime.Parse(year + "-" + month + "-" + day);
                    month = newDate.Month.ToString();
                }
                
                if (month.Length == 1)
                    month = "0" + month;

                if (day.Length == 1)
                    day = "0" + day;

                return year + "-" + month + "-" + day;
            }
            catch
            {
                return "0000-00-01";
            }
        }

        public string GetFirstDayOfMonthWithMySqlFormat(string monthly)
        {
            try
            {
                DateTime newMonth = DateTime.Parse(monthly);
                string year = newMonth.Year.ToString();
                string month = newMonth.Month.ToString();

                if (month.Length == 1)
                    month = "0" + month;

                return year + "-" + month + "-01";
            }
            catch
            {
                return "0000-00-01";
            }
        }

        public string GetLastDayofMonthWithMySqlFormat(string monthly)
        {
            try
            {
                DateTime newMonth = DateTime.Parse(monthly);
                string year = newMonth.Year.ToString();
                string month = newMonth.Month.ToString();
                int lastDay = GetLastDayofMonth(int.Parse(month));

                if (DateTime.IsLeapYear(int.Parse(year)) && int.Parse(month) == 2)
                    lastDay += 1;

                if (month.Length == 1)
                    month = "0" + month;

                return year + "-" + month + "-" + lastDay;
            }
            catch
            {
                return "0000-00-01";
            }
        }

        private int GetLastDayofMonth(int monthly)
        {
            int lastDay = 0;
            switch (monthly)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    lastDay = 31;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    lastDay = 30;
                    break;
                default: //February
                    lastDay = 28;
                    break;
            }

            return lastDay;
        }

        public string GetLastMonthWithMySqlFormat(string monthly)
        {
            DateTime lastMonth = DateTime.Parse(monthly);
            string year = lastMonth.Year.ToString();
            string month = (lastMonth.Month - 1).ToString();

            if (month == "0")
            {
                year = (lastMonth.Year - 1).ToString();
                month = "12";
            }

            if (month.Length == 1)
                month = "0" + month;

            return year + "-" + month + "-01";
        }

        public string GetPreviousMonthByIntervalWithMySqlFormat(string monthly, int monthInterval)
        {
            DateTime lastMonth = DateTime.Parse(monthly);
            string year = lastMonth.Year.ToString();
            string month = (lastMonth.Month - monthInterval).ToString();

            if (int.Parse(month) < 1)
            {
                int yearInterval = int.Parse(month) / 12;
                year = (lastMonth.Year + yearInterval).ToString();
                month = (int.Parse(month) + 12 * yearInterval).ToString();
            }

            if (month.Length == 1)
                month = "0" + month;

            return year + "-" + month + "-01";
        }

        public string GetNextMonthWithMySqlFormat(string monthly)
        {
            DateTime nextMonth = DateTime.Parse(monthly);
            string year = nextMonth.Year.ToString();
            string month = (nextMonth.Month + 1).ToString();

            if (month == "13")
            {
                year = (nextMonth.Year + 1).ToString();
                month = "01";
            }

            if (month.Length == 1)
                month = "0" + month;

            return year + "-" + month + "-01";
        }

        public string GetLatterMonthByIntervalWithMySqlFormat(string monthly, int monthInterval)
        {
            DateTime nextMonth = DateTime.Parse(monthly);
            string year = nextMonth.Year.ToString();
            string month = (nextMonth.Month + monthInterval).ToString();

            if (int.Parse(month) > 12)
            {
                int yearInterval = int.Parse(month) / 12;
                year = (nextMonth.Year + yearInterval).ToString();
                month = (int.Parse(month) - 12 * yearInterval).ToString();
            }

            if (month.Length == 1)
                month = "0" + month;

            return year + "-" + month + "-01";
        }

        public int GetWeekDayNumber(string weekDay)
        {
            int weekNum;

            switch (weekDay) 
            {
                case "Monday":
                    weekNum = 1;
                    break;
                case "Tuesday":
                    weekNum = 2;
                    break;
                case "Wednesday":
                    weekNum = 3;
                    break;
                case "Thursday":
                    weekNum = 4;
                    break;
                case "Friday":
                    weekNum = 5;
                    break;
                case "Saturday":
                    weekNum = 6;
                    break;
                default: //Sunday
                    weekNum = 0;
                    break;
            }

            return weekNum;
        }


        #region Check Data Format

        //Check Data Format
        public bool IsNumberic(string numberic)
        {
            try
            {
                double.Parse(numberic);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckInputDataIsRightOfFormat(string inputData)
        {
            if (inputData.Length == 0)
                return false;
            else if (inputData.IndexOf("!") > -1)
                return false;
            else
                return true;
        }

        public string CheckInputMoneyIsRightOfFormat(string inputNum)
        {
            if (inputNum.Length == 0)
                return "This data can't be empty.";
            else if (!IsNumberic(inputNum))
                return "The data format is wrong. It is number only.";
            else if (double.Parse(inputNum) < 0)
                return "The data can't less than 0.";
            else
                return "";
        }

        public string MakeInsertStringRight(string temp)
        {
            string anotherTemp = temp;
            while (temp.LastIndexOf("'") > -1)
            {
                anotherTemp = anotherTemp.Insert(temp.LastIndexOf("'"), @"\");
                temp = temp.Substring(0, temp.LastIndexOf("'"));
            }

            return anotherTemp;
        }

        #endregion


        public string InsertUserIntoEvent(string insertEvent, string user)
        {
            int dashIndex = insertEvent.IndexOf("-");
            int quotaIndex = insertEvent.IndexOf("(");

            if (insertEvent.IndexOf(user) == -1)
            {
                if (quotaIndex < dashIndex && quotaIndex > -1)
                    insertEvent = insertEvent.Insert(quotaIndex, "-" + user);
                else if (dashIndex < quotaIndex && dashIndex > -1)
                    insertEvent = insertEvent.Insert(dashIndex, "-" + user);
                else
                    insertEvent = insertEvent + "-" + user;
            }

            return insertEvent;
        }

        public string[,] MonthCalendar(string month, string year)
        {
            string firstDayofMonth = PutDateTogetherWithMySqlFormat("", month, year);
            string[,] calendar = new string[6, 7];
            string lastDayofMonth = GetLastDayofMonthWithMySqlFormat(firstDayofMonth);
            string firstWeekDay = DateTime.Parse(firstDayofMonth).DayOfWeek.ToString();
            int weekNum = GetWeekDayNumber(firstWeekDay);
            int lastDay = DateTime.Parse(lastDayofMonth).Day;
            int monthDay = 1;

            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    if (i == 0)
                    {
                        if (j < weekNum)
                        {
                            calendar[i,j] = "0";
                        }
                        else // if (j >= weekNum)
                        {
                            if (monthDay < 10)
                                calendar[i,j] = "0" + monthDay.ToString();
                            else
                                calendar[i, j] = monthDay.ToString();
                            monthDay++;
                        }
                    }
                    else // i != 0
                    {
                        if (monthDay <= lastDay)
                        {
                            if (monthDay < 10)
                                calendar[i, j] = "0" + monthDay.ToString();
                            else
                                calendar[i, j] = monthDay.ToString();
                            monthDay++;
                        }
                        else // monthDay > lastDay
                            calendar[i,j] = "0";
                    }
                }
            }

            return calendar;
        }

        public string WeekCalendar(string day, string month, string year)
        {
            string chosenDay = PutDateTogetherWithMySqlFormat(day, month, year);
            //string[] calendar = new string[7];
            string weekDay = DateTime.Parse(chosenDay).DayOfWeek.ToString();
            int weekNum = GetWeekDayNumber(weekDay);

            //if (weekNum != 1)
            chosenDay = ChangeDateFormatForMySql(DateTime.Parse(chosenDay).AddDays(-weekNum).ToString());

            return chosenDay;
        }
    }
}
