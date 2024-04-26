using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public static class StringBuilderScaler
{
    public static StringBuilder builder = new StringBuilder();

    public static void ApplyText(TMP_Text tmpText, string format, params object[] args)
    {
        builder.Clear();
        builder.AppendFormat(format, args);
        tmpText.SetText(builder);
    }

    public static void ApplyText(TMP_Text tmpText, object arg)
    {
        builder.Clear();
        builder.AppendFormat("{0}", arg);
        tmpText.SetText(builder);
    }

    public static string GetScaledText(string format, params object[] args)
    {
        builder.Clear();
        builder.AppendFormat(format, args);
        return builder.ToString();
    }

    public static string GetText(object args)
    {
        builder.Clear();
        builder.AppendFormat("{0}", args);
        return builder.ToString();
    }
}
