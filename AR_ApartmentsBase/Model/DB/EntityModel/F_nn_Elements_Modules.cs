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
    
    public partial class F_nn_Elements_Modules
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public F_nn_Elements_Modules()
        {
            this.F_nn_ElementParam_Value = new HashSet<F_nn_ElementParam_Value>();
        }
    
        public int ID_ELEMENT_IN_MODULE { get; set; }
        public int ID_ELEMENT { get; set; }
        public int ID_MODULE { get; set; }
        public string LOCATION { get; set; }
        public string DIRECTION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<F_nn_ElementParam_Value> F_nn_ElementParam_Value { get; set; }
        public virtual F_R_Modules F_R_Modules { get; set; }
        public virtual F_S_Elements F_S_Elements { get; set; }
    }
}
