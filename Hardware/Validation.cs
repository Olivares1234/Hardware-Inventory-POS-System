using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hardware_MS
{
    class Validation
    {
        public bool Is_Blank(string str)
        {
            return str.Trim() == "" || str[0] == ' ' || str[str.Length - 1] == ' ';
        }
        public bool HasLength(string str, int length)
        {
            return str.Length == length;
        }
        public bool HasLengthGreaterThan(string str, int minLength)
        {
            return str.Length > minLength;
        }
        public bool HasLengthLessThan(string str, int maxLength)
        {
            return str.Length < maxLength;
        }
        public bool HasLengthBetween(string str, int min, int max)
        {
            return HasLengthGreaterThan(str, min - 1) && HasLengthLessThan(str, max + 1);
        }
        public bool IsNegativeValue(decimal value)
        {
            return value < 0;
        }
        public bool IsNegativeValue(double value)
        {
            return value < 0;
        }
        public bool IsNegativeValue(int value)
        {
            return value < 0;
        }
        public bool IsGreaterThanZero(double value)
        {
            return value > 0;
        }
        public bool IsGreaterThanZero(decimal value)
        {
            return value > 0;
        }
        public bool IsValidPrice(decimal value)
        {
            return value > 0;
        }
        public bool IsValidDateSchedule(DateTime date)
        {
            return date.Date >= DateTime.Now.Date;
        }
        public bool IsValidTimeSchedule(DateTime dateTime)
        {
            return dateTime >= DateTime.Now.AddMinutes(-1);
        }
    }
}
