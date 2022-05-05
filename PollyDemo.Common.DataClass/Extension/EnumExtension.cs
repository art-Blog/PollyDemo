using System;
using System.ComponentModel;

namespace PollyDemo.Common.DataClass.Extension
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(object value) => GetEnumDescription(value);
        public static string GetDescription(this Enum value) => GetEnumDescription(value);
        private static string GetEnumDescription(object value)
        {
            if (value == null)
                return (string) null;
            Type type = value.GetType();
            object[] objArray = type.IsEnum ? type.GetField(value.ToString()).GetCustomAttributes(typeof (DescriptionAttribute), false) : throw new ApplicationException("其值必須為列舉");
            return objArray.Length <= 1 ? (objArray[0] as DescriptionAttribute)?.Description : throw new ApplicationException("列舉類型「" + type.Name + "」有過多的Description屬性，相對應的列舉為「" + value + "」");
        }
    }
}