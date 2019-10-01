using System;
using Helpers;

namespace ExtensionMethods
{
    public static class EnumExtensions
    {
        public static string Label(this Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(EnumLabel), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((EnumLabel)attrs[0]).Text;
                }
            }
            return en.ToString();
        }
    }
}