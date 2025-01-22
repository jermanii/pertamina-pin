using Pertamina.IRIS.Utility.Wording.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Utility.Wording.Implement
{
    public class UpdatedWordingUtility : IUpdatedWordingUtility
    {
        public UpdatedWordingUtility()
        {

        }

        private string Difference(DateTime CalculateDate)
        {
            string result = string.Empty;

            TimeSpan DateDiff = DateTime.Now - CalculateDate;

            result = DateDiff.TotalHours < 24 ? $"{(int)DateDiff.TotalHours} hours ago" : DateDiff.TotalDays <= 7 ? $"{(int)DateDiff.TotalDays} days ago" : (int)(DateDiff.TotalDays / 7) <= 4 ? $"{(int)(DateDiff.TotalDays / 7)} weeks ago" : (DateTime.Now.Year - CalculateDate.Year) >= 1 ? $"{DateTime.Now.Year - CalculateDate.Year} years ago" : $"{((DateTime.Now.Year - CalculateDate.Year) * 12 + (DateTime.Now.Month - CalculateDate.Month))} months ago";

            return result;
        }

        public string GetUpdatedWording(DateTime? CreatedDate, DateTime? UpdatedDate)
        {
            string result = "Updated ";

            if (UpdatedDate == null)
            {
                result += Difference(CreatedDate.Value);
            }
            else
            {
                result += Difference(UpdatedDate.Value);
            }

            return result;
        }
    }
}
