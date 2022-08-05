using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVP.Presenters.Common
{
   public class ModelDataValdation
    {
        public void Validate(object model)
        {
            string errorMessage = "";
            List<ValidationResult> result = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, context, result, true);
            if(isValid==false)
            {
                foreach (var item in result)
                    errorMessage += ". " + item.ErrorMessage + "\n";
                throw new Exception(errorMessage);
            }
        }
    }
}
