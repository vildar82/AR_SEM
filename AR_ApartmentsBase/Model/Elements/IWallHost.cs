﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR_ApartmentBase.Model.DB.EntityModel;

namespace AR_ApartmentBase.Model.Elements
{
    /// <summary>
    /// Элемент принадлежащий стене (окно, дверь)
    /// </summary>
    public interface IWallHost : IElement
    {
        List<IElement> HostWall { get; set; }
        void SearchHostWallDwg (List<Element> elements);
        void SearchHostWallDB (F_R_Modules moduleEnt);
    }
}
