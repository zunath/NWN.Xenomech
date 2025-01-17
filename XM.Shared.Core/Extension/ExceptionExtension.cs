﻿using System;
using System.Text;

namespace XM.Shared.Core.Extension
{
    public static class ExceptionExtension
    {
        public static string ToMessageAndCompleteStacktrace(this Exception exception)
        {
            var e = exception;
            var s = new StringBuilder();
            while (e != null)
            {
                s.AppendLine("Exception type: " + e.GetType().FullName);
                s.AppendLine("Message       : " + (e.Message ?? string.Empty));
                s.AppendLine("Stacktrace:");
                s.AppendLine(e.StackTrace);

                s.AppendLine();
                e = e.InnerException;
            }

            return s.ToString();
        }
    }
}
