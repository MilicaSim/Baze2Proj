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
    
    public partial class Prikazuje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prikazuje()
        {
            this.Kartas = new HashSet<Karta>();
        }
    
        public System.DateTime Termin { get; set; }
        public int IdPrikazivanja { get; set; }
        public Nullable<int> SadrziSjedisteIdSjedista { get; set; }
        public int SadrziSalaIdSale { get; set; }
        public int FilmIdFilma { get; set; }
    
        public virtual Sadrzi Sadrzi { get; set; }
        public virtual Film Film { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Karta> Kartas { get; set; }
    }
}
