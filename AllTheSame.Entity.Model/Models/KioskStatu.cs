using System;
using System.Collections.Generic;

namespace Accushield.Entity.Model.Models
{
    public partial class KioskStatu
    {
        public KioskStatu()
        {
            this.Kiosks = new List<Kiosk>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public virtual ICollection<Kiosk> Kiosks { get; set; }
    }
}
