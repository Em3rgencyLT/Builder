using System;
using UnityEngine;

namespace Data
{   
    [Serializable]
    public class Resource
    {
        public enum TypeName
        {
            BUILDING_MATERIAL,
            MONEY
        }

        public TypeName _type;
        public string _title;
        public Sprite _icon;
        public int _amount;

        public Resource(TypeName type, string title, Sprite icon, int amount = 0)
        {
            _type = type;
            _title = title;
            _icon = icon;
            _amount = amount;
        }
    }
}