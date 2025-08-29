using System.ComponentModel.DataAnnotations;

namespace DeptSample.Data.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dt)
            {
                return dt > DateTime.Now;
            }
            return false;
        }
    }
}
