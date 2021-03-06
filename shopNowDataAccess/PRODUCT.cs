//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace shopNowDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.ORDER_DETAIL = new HashSet<ORDER_DETAIL>();
        }
    
        public int productId { get; set; }
        public string productName { get; set; }
        public string productImage { get; set; }
        public Nullable<int> catId { get; set; }
        public Nullable<decimal> productPrice { get; set; }
        public Nullable<bool> isAvailable { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public string productDescription { get; set; }
    
        public virtual CATEGORY CATEGORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_DETAIL> ORDER_DETAIL { get; set; }
    }
}
