//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FirmaModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class zamestnanci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public zamestnanci()
        {
            this.hierarchia = new HashSet<hierarchia>();
        }
    
        public int id { get; set; }
        public string titul { get; set; }
        public string meno { get; set; }
        public string priezvisko { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hierarchia> hierarchia { get; set; }
    }
}