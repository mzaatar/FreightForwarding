using FFSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FFSolution.Commons
{
    public class Helpers
    {
        private static string StringPadding(int number, int paddingSize)
        {
            if (paddingSize == 6)
            {
                if (number < 10)
                    return "00000" + number;
                else if (number < 100)
                    return "0000" + number;
                else if (number < 1000)
                    return "000" + number;
                else if (number < 10000)
                    return "00" + number;
                else if (number < 10000)
                    return "0" + number;
                else return number.ToString();
            }
            else if (paddingSize == 2)
            {
                if (number < 10)
                    return "0" + number;
                else
                    return number.ToString();
            }
            return number.ToString();
        }

        public static string CreateTranRefNumber()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(StringPadding(DateTime.Now.Day, 2));
            sb.Append(StringPadding(DateTime.Now.Month, 2));
            sb.Append(DateTime.Now.Year);
            using (FFAdminDBEntities db = new FFAdminDBEntities())
            {
                try
                {
                    sb.Append(StringPadding(db.Tran.Max(b => b.TranID) + 1, 6));
                }
                catch
                {
                    sb.Append(StringPadding(1, 6));
                }
            }

            return sb.ToString();
        }

        public static object SetValue(object inputObject, string propertyName, object propertyVal)
        {
            try
            {
                //find out the type
                Type type = inputObject.GetType();

                //get the property information based on the type
                System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);

                //find the property type
                Type propertyType = propertyInfo.PropertyType;

                //Convert.ChangeType does not handle conversion to nullable types
                //if the property type is nullable, we need to get the underlying type of the property
                var targetType = IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

                //Returns an System.Object with the specified System.Type and whose value is
                //equivalent to the specified object.
                propertyVal = Convert.ChangeType(propertyVal, targetType);

                //Set the value of the property
                propertyInfo.SetValue(inputObject, propertyVal, null);

                return inputObject;
            }
            catch
            {
                return inputObject;
            }

        }
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        public static T UpdateUserAndDate<T>(T obj)
        {
            string name = "FFSystem";
            try
            {
                name = HttpContext.Current.User.Identity.Name;
            }
            catch
            {

            }
            SetValue(obj, "Updator", name);
            SetValue(obj, "Updated", DateTime.Now.ToShortDateString());
            return obj;
        }

    }
}