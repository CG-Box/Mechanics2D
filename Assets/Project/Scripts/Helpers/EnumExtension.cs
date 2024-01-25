//using UnityEngine;
using System;

public static class EnumExtension
{
    public static bool CompareWithString<T>(this T enumType, string strValue ) where T : Enum
    {
        if(strValue == enumType.ToString())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}