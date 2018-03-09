using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CalendarFilter
{
    class Program 
    {
        protected static void Main(string[] args)
        {
            var isCorrectYear = false;
            var isCorrectWeek = false;
            var isCorrectDay = false;
            IEnumerable<int> yearList = Enumerable.Range(1994, 101).ToList();
            int[] week = new int[] {0, 1, 2, 3, 4};
            String[] day = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            List<string> dayList = new List<string>(day);

            do //isCorrectYear
            {
                Console.WriteLine("Please enter a year range between 1994 - 2094");
                String yearInput = Console.ReadLine();
                if (yearList.Contains(Convert.ToInt32(yearInput)))
                {
                    isCorrectYear = true;
                    do //isCorrectWeek
                    {
                        Console.WriteLine("Please enter a week between 0 - 4 (0 being the last/5th week of the month)");
                        String weekInput = Console.ReadLine();
                        if (week.Contains(Convert.ToInt32(weekInput)))
                        {
                            if (weekInput == "0")
                            {
                                weekInput = "5";
                            }
                                isCorrectWeek = true;
                                do //isCorrectDay
                                {
                                    Console.WriteLine("Please enter a day Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday");
                                    String dayInput = Console.ReadLine();
                                    dayInput = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(dayInput.ToLower()); //Format input 1st Alphabet Caps the rest SmallCaps
                                    if (dayList.Contains(dayInput, StringComparer.CurrentCultureIgnoreCase))
                                    {
                                        isCorrectDay = true;
                                        for (int i = 1; i < 13; i++) //Loop entire year
                                        {
                                            //Filter out the dates based on the day that user inputs for that particular month  
                                            var dates = GetDatesBasedOnDay(Convert.ToInt32(yearInput), i, dayInput);
                                            DateTime dt = new DateTime(Convert.ToInt32(yearInput), i, 1);
                                            //int weeksInMonth = WeeksInMonth(dt);
                                            switch (Convert.ToInt32(weekInput))
                                            {
                                                case 1:
                                                    Console.WriteLine((dates.ElementAt(0).ToString("dd MMMM yyyy")));
                                                    break;
                                                case 2:
                                                    Console.WriteLine((dates.ElementAt(1).ToString("dd MMMM yyyy")));
                                                    break;
                                                case 3:
                                                    Console.WriteLine((dates.ElementAt(2).ToString("dd MMMM yyyy")));
                                                    break;
                                                case 4:
                                                    Console.WriteLine((dates.ElementAt(3).ToString("dd MMMM yyyy")));
                                                    break;
                                                case 5:
                                                    if (dates.Count() < 5)//Certain months only has 4 weeks
                                                    { 
                                                        Console.WriteLine((dates.ElementAt(3).ToString("dd MMMM yyyy")));
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine((dates.ElementAt(4).ToString("dd MMMM yyyy")));
                                                        break;
                                                    }
                                            }
                                        }
                                    } // end of if (dayList.Contains(dayInput, StringComparer.CurrentCultureIgnoreCase))
                                    else
                                    {
                                        Console.WriteLine("Please type a valid day of the week Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday");
                                        Console.WriteLine("");
                                    }
                                } while (!isCorrectDay);
                        } //end of if (week.Contains(Convert.ToInt32(weekInput)))
                        else
                        {                 
                            Console.WriteLine("Invalid week entered. Please enter a week number 0 - 4 only");
                            Console.WriteLine("");
                        }
                    } while (!isCorrectWeek);
                } //end of if (yearList.Contains(Convert.ToInt32(yearInput)))
                else
                {
                    Console.WriteLine("Invalid year entered. Please enter a year within 1994 - 2094");
                    Console.WriteLine("");
                }
            } while (!isCorrectYear);
        }
      
        //Display dates that are filtered by the day e.g Sun-Mon
        public static List<DateTime> GetDatesBasedOnDay(int year, int month, string day)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                .Where(d => new DateTime(year, month, d).ToString("dddd").Equals(day))
                .Select(d => new DateTime(year, month, d)).ToList();
        }

        //Calculate number of weeks in a month 
        public static int WeeksInMonth(DateTime thisMonth)
        {
            
            int month = thisMonth.Month;
            int year = thisMonth.Year;
            int daysThisMonth = DateTime.DaysInMonth(year, month);
            DateTime firstOfThisMonth = new DateTime(year, month, 1);
            //days of week starts by default as Sunday = 0
            int firstDayOfMonth = (int)firstOfThisMonth.DayOfWeek;
            int weeksInMonth = (int)Math.Ceiling((firstDayOfMonth + daysThisMonth) / 7.0);
            return weeksInMonth;
        }

        /*
     //Takes a date and get week of that month
     public static int GetWeekOfMonth(DateTime date)  
     {  
         DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);  

         while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)  
         date = date.AddDays(1);  

         return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays  / 7f) + 1;  
     } 
     */

    }
}
