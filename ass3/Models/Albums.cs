//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ass3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Albums
    {
        public Albums()
        {
            this.Users = new HashSet<Users>();
        }
    
        public int albumID { get; set; }
        public string albumName { get; set; }
        public int albumYear { get; set; }
    
        public virtual ICollection<Users> Users { get; set; }
    }
}