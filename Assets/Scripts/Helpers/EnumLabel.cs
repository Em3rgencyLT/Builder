using System;

namespace Helpers
{
    public class EnumLabel : Attribute
    {
        private string text;

        public EnumLabel(string Text)
        {
            text = Text;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}