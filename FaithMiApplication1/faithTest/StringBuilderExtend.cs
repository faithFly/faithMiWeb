using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faithTest
{
    internal static class StringBuilderExtend
    {
        public static StringBuilder AppendFormatWithSafe(this StringBuilder a, string format, object arg0, StringBuilder sb)
        {
            sb.AppendFormat(format,
                ((string)arg0)
                .ToLower()
                .Replace("update", "")
                .Replace("delete", "")
                .Replace("select", "")
                .Replace("insert", "")
                .Replace("from", "")
                .Replace("or", "")
                .Replace("'", "")
                .Replace("@", "")
                .Trim()
             );
            return sb;
        }
    }
}
