using System;
using System.Collections.Generic;
using System.Text;

namespace DBMSWeb.Model
{
    public static class TypesValidator
    {
        public static bool IsValidValue(string customTypeName, string value)
        {
            switch (customTypeName)
            {
                case "Integer":
                    return validateInteger(value);

                case "Real":
                    return validateReal(value);

                case "Char":
                    return validateChar(value);

                case "String":
                    return validateString(value);

                case "Time":
                    return validateTime(value);

                case "TimeInvl":
                    return validateTimeInvl(value);


                default:
                    return false;
            }
        }

        private static bool validateChar(string value)
        {
            char buf;
            if (char.TryParse(value, out buf))
            {
                return true;
            }
            return false;
        }

        private static bool validateInteger(string value)
        {
            int buf;
            if (int.TryParse(value, out buf))
            {
                return true;
            }
            return false;
        }

        private static bool validateReal(string value)
        {
            double buf;
            if (double.TryParse(value, out buf))
            {
                return true;
            }
            return false;
        }


        private static bool validateString(string value)
        {
            return true;
        }

        private static bool validateTime(string value)
        {
            try
            {
                if (value.Length > 5 || value.Length < 4) return false;
                if (value[value.Length - 3] != ':') return false;
                string hh = value.Length == 4 ? value.Substring(0, 1) : value.Substring(0, 2);
                string mm = value.Substring(value.IndexOf(':') + 1);
                int h, m;
                if (!int.TryParse(hh, out h) || !int.TryParse(mm, out m)) return false;
                if (h > 23 || m > 59) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool validateTimeInvl(string value)
        {
            int f, t;
            string[] q = value.Split('-');
            if (q.Length != 2) return false;
            string start, end;
            start = q[0].Trim();
            end = q[1].Trim();

            if (!validateTime(start) || !validateTime(end)) return false;
            if (!compare(start, end)) return false;

            return true;
        }
        private static bool compare(string start, string end)
        {
            string h1, m1, h2, m2;
            h1 = start.Length == 4 ? start.Substring(0, 1) : start.Substring(0, 2);
            m1 = start.Substring(start.IndexOf(':') + 1);
            h2 = end.Length == 4 ? end.Substring(0, 1) : end.Substring(0, 2);
            m2 = end.Substring(end.IndexOf(':') + 1);

            int hh1, mm1, hh2, mm2;
            int.TryParse(h1, out hh1);
            int.TryParse(h2, out hh2);
            int.TryParse(m1, out mm1);
            int.TryParse(m2, out mm2);

            if (hh1 > hh2) return false;
            else if (hh1 == hh2)
            {
                if (mm1 > mm2) return false;
            }

            return true;
        }
    }
}
