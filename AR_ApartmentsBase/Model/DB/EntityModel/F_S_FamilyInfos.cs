//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AR_ApartmentBase.Model.DB.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class F_S_FamilyInfos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public F_S_FamilyInfos()
        {
            this.F_S_Elements = new HashSet<F_S_Elements>();
        }
    
        public int ID_FAMILY_INFO { get; set; }
        public string FAMILY_NAME { get; set; }
        public string FAMILY_SYMBOL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<F_S_Elements> F_S_Elements { get; set; }
    }
}