using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace hw1.DataTypeAttributes
{
    public class 驗證手機格式Attribute : DataTypeAttribute
    {
        public string[] PhoneFormat { get; set; }
        public 驗證手機格式Attribute() : base(DataType.Text)
        {
            ErrorMessage = "手機格式不正確";
        }
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if (value is String)
                return Regex.IsMatch(value.ToString(), "\\d{4}-\\d{6}");
            else
                return true;
        }
    }
}