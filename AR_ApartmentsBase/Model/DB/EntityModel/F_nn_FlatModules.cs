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
    
    public partial class F_nn_FlatModules
    {
        public int ID_FLAT_MODULE { get; set; }
        public int ID_FLAT { get; set; }
        public int ID_MODULE { get; set; }
        public int REVISION { get; set; }
        public string LOCATION { get; set; }
        public string DIRECTION { get; set; }
        public Nullable<double> ANGLE { get; set; }
    
        public virtual F_R_Flats F_R_Flats { get; set; }
        public virtual F_R_Modules F_R_Modules { get; set; }
    }
}
