//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bioskop.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Karta
    {
        public int IdKarte { get; set; }
        public int Cijena { get; set; }
        public Nullable<int> PosjetilacIdPosjetioca { get; set; }
        public Nullable<int> PrikazujeIdPrikazivanja { get; set; }
    
        public virtual Posjetilac Posjetilac { get; set; }
        public virtual Prikazuje Prikazuje { get; set; }
    }
}
