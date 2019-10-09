using System;
using System.Text.RegularExpressions;
using Enums;
using UnityEngine;

namespace Data
{
    public class CodexDataBit
    {
        private readonly CodexDataType _type;
        private readonly string _value;
        
        private const string TextVariablePattern = @"{[a-z_0-9]*}";

        public CodexDataBit(CodexDataType type, string value)
        {
            _type = type;
            _value = value;
        }

        public static CodexDataBit FromString(string input)
        {
            if (input == "")
            {
                return new CodexDataBit(CodexDataType.Newline, input);
            }
            
            var match = Regex.Match(input, TextVariablePattern);
            if (!match.Success)
            {
                return new CodexDataBit(CodexDataType.Text, input);
            }
            
            var result = match.Value.Substring(1, match.Length - 2);
            var split = result.Split('_');
            if (split.Length != 2)
            {
                Debug.LogError($"Could not parse text variable {result}");
            }
                
            return new CodexDataBit(GetTypeFromString(split[0]), split[1]);
        }

        private static CodexDataType GetTypeFromString(string input)
        {
            switch (input)
            {
                case "ref":
                    return CodexDataType.Reference;
                case "img":
                    return CodexDataType.Image;
                case "":
                    return CodexDataType.Newline;
                default:
                    return CodexDataType.Text;
            }
        }

        public CodexDataType Type => _type;

        public string Value => _value;
    }
}