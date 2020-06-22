using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodCourtSystem.CustomValidation
{
    public class VietNamMoney: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                int amount = (int)value;
                bool isValid = true;
                if (amount <= 0)
                {
                    ErrorMessage = "Số tiền nạp thêm phải >0";
                    isValid = false;
                }
                else if (amount % 1000 != 0 || amount < 50000)
                {
                    ErrorMessage = "Số tiền phả là bội số của 1000 và tối thiểu là 50000";
                    isValid = false;
                }
                return isValid;
            }
            catch(InvalidCastException)
            {
                ErrorMessage = "Số tiền nhập phải là số";
                return false;
            }
        }
    }
}