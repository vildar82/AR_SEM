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
    
    public partial class F_nn_ElementParam_Value
    {
        public int ID_ELEMENT_VALUE { get; set; }
        public int ID_ELEMENT { get; set; }
        public int ID_CAT_PARAMETER { get; set; }
        public string PARAMETER_VALUE { get; set; }
    
        public virtual F_nn_Category_Parameters F_nn_Category_Parameters { get; set; }
        public virtual F_S_Elements F_S_Elements { get; set; }
    }
}
