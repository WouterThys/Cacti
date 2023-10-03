﻿using System;
using System.Drawing;
using Newtonsoft.Json;

namespace CactiClient.Model
{
    public class CactusView : IEquatable<CactusView>
    {
        [JsonIgnore]
        private static readonly Image DefaultImage = Image.FromFile("cactus-icon-vector.jpg");

        [JsonIgnore]
        private static int NextInsertId = -100;


        public long Id { get; set; } = NextInsertId--;
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public string Barcodes { get; set; } = "";

        [JsonIgnore]
        public Image Image { get; set; } = DefaultImage;

        [JsonIgnore]
        public bool ImageLoading { get; set; }


        #region Equals


        public override bool Equals(object? obj)
        {
            return Equals(obj as CactusView);
        }

        public virtual bool Equals(CactusView? other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        #endregion

    }
}
