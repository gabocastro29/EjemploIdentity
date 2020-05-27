using System;
using System.ComponentModel.DataAnnotations;

namespace EjemploIdentity
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class BithDateRangeAttribute : ValidationAttribute
    {
        public DateTime Minimum = DateTime.Now.AddYears(-110);
        public DateTime Maximum = DateTime.Now.AddYears(-18);

        public BithDateRangeAttribute() {}
        
        public override bool IsValid(object value)
        {
            if (value == null) //0 null
                return true;

            var s = value as string;
            if (string.IsNullOrEmpty(s)) //0 null
                return true;

            var min = (IComparable)Minimum;
            var max = (IComparable)Maximum;
            return (min.CompareTo(value) <= 0) && (max.CompareTo(value) >= 0);
        }

        public override string FormatErrorMessage(string name) {
            var msg = string.Format("La {0} debe estar entre {1:dd/MM/yyyy} y {2:dd/MM/yyyy}", name, Minimum, Maximum);
            return msg;
        }
    }
}