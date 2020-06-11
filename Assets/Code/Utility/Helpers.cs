using System;
using UnityEngine;

namespace Code.Utility
{
    public static class Helpers
    {
        public static string GetJsonText(string fileName)
        {
            var jsonText = Resources.Load<TextAsset>(fileName);
            
            if (jsonText != null) return jsonText.text;
            throw new NullReferenceException($"there is no file {fileName} in Resources folder", null);;
        }
    }
}
