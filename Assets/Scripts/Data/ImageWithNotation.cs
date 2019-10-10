using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ImageWithNotation
    {
        [SerializeField] private Sprite image;
        [SerializeField] private string notation;

        public ImageWithNotation(Sprite image, string notation)
        {
            this.image = image;
            this.notation = notation;
        }

        public Sprite Image => image;

        public string Notation => notation;
    }
}