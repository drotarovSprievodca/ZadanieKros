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
    
    public partial class hierarchia
    {
        public int kod { get; set; }
        public string nazov { get; set; }
        public Nullable<int> patriDo { get; set; }
        public Nullable<int> veduci { get; set; }
    
        public virtual zamestnanci zamestnanci { get; set; }
    }
}