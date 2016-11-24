﻿using System;
using System.Linq;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using AcadLib.Errors;
using System.Drawing;
using Autodesk.AutoCAD.Geometry;
using AR_ApartmentBase.Model;
using AR_ApartmentBase.Model.DB.EntityModel;

namespace AR_ApartmentBase_AutoCAD
{
    public class ParameterAC : Parameter
    {
        // Константные атрибуты в блоках
        public static Dictionary<ObjectId, List<Parameter>> BlocksConstantAtrs = new Dictionary<ObjectId, List<Parameter>>();

        public ParameterAC (string name, object value) : base(name, value)
        {            

        }

        public static List<Parameter> GetParameters(BlockReference blRef, string blName, Matrix3d transToModel)
        {
            List<Parameter> parameters = new List<Parameter>();

            // считывание дин параметров
            defineDynParams(blRef, parameters, blName, transToModel);

            // считывание атрибутов
            defineAttributesParam(blRef, parameters, blName, transToModel);

            // Сортировка параметров по имени
            parameters.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));

            return parameters;
        }

        private static void defineDynParams(BlockReference blRef, List<Parameter> parameters, string blName, Matrix3d transToModel)
        {
            if (blRef.IsDynamicBlock)
            {
                foreach (DynamicBlockReferenceProperty prop in blRef.DynamicBlockReferencePropertyCollection)
                {
                    Error errHasParam = new Error($"Дублирование параметра {prop.PropertyName} в блоке {blName}.",
                       blRef, transToModel ,icon: SystemIcons.Error);
                    addParam(parameters, prop.PropertyName, prop.Value, errHasParam);
                }
            }
        }

        private static void defineAttributesParam(BlockReference blRef, List<Parameter> parameters, string blName, Matrix3d transToModel)
        {
            if (blRef.AttributeCollection != null)
            {
                foreach (ObjectId idAtrRef in blRef.AttributeCollection)
                {
                    var atrRef = idAtrRef.GetObject(OpenMode.ForRead, false, true) as AttributeReference;

                    if (atrRef != null)
                    {
                        Error errHasParam = new Error($"Дублирование параметра {atrRef.Tag} в блоке {blName}.",
                           blRef, transToModel, icon: SystemIcons.Error);
                        addParam(parameters, atrRef.Tag, atrRef.TextString, errHasParam);
                    }
                }
            }
            // Добавка константных атрибутов
            parameters.AddRange(getConstAtrParameters(blRef));
        }

        private static List<Parameter> getConstAtrParameters(BlockReference blRef)
        {
            List<Parameter> constAtrParameters;
            ObjectId idBtr = blRef.DynamicBlockTableRecord;
            if (!BlocksConstantAtrs.TryGetValue(idBtr, out constAtrParameters))
            {
                constAtrParameters = new List<Parameter>();
                var btr = idBtr.GetObject(OpenMode.ForRead) as BlockTableRecord;
                foreach (var idEnt in btr)
                {
                    var atr = idEnt.GetObject(OpenMode.ForRead, false, true) as AttributeDefinition;
                    if (atr == null || !atr.Constant) continue;
                    ParameterAC constAtrParam = new ParameterAC(atr.Tag.Trim(), atr.TextString.Trim());
                    constAtrParameters.Add(constAtrParam);
                }
                BlocksConstantAtrs.Add(idBtr, constAtrParameters);
            }
            return constAtrParameters;
        }

        private static void addParam(List<Parameter> parameters, string name, object value, Error errorHasParam)
        {
            if (hasParamName(parameters, name))
            {
                Inspector.Errors.Add(errorHasParam);
            }
            else
            {
                if (!OptionsAC.Instance.IgnoreParamNames.Contains(name, StringComparer.OrdinalIgnoreCase))
                {
                    ParameterAC param = new ParameterAC(name, value);
                    parameters.Add(param);
                }
            }
        }

        /// <summary>
        /// Оставить только нужные для базы параметры
        /// </summary>      
        public static List<Parameter> ExceptOnlyRequiredParameters(List<Parameter> parameters, string category)
        {
            var paramsCategory = BaseApartments.GetBaseCategoryParameters().SingleOrDefault(c => c.Key.Equals(category)).Value;
            List<Parameter> resVal = new List<Parameter>();

            if (paramsCategory != null)
            {
                foreach (var param in parameters)
                {
                    var paramDb = paramsCategory.FirstOrDefault(p => p.NAME_PARAMETER.Equals(param.Name, StringComparison.OrdinalIgnoreCase));
                    if (paramDb != null)
                    {
                        ((ParameterAC)param).ConvertValueToDbType(paramDb.TYPE_PARAMETER);                        
                        resVal.Add(param);
                    }
                }
                if (paramsCategory.Exists(p => p.NAME_PARAMETER.Equals("FuckUp", StringComparison.OrdinalIgnoreCase)))
                {
                    if (!resVal.Exists(p => p.Name.Equals("FuckUp", StringComparison.OrdinalIgnoreCase)))
                    {
                        resVal.Add(new ParameterAC("FuckUp", ""));
                    }
                }
            }
            return resVal;
        }

        /// <summary>
        /// Приведение значения параметра в соответствие с типом значения нужным для базы
        /// </summary>      
        public void ConvertValueToDbType(string tYPE_PARAMETER)
        {
            switch (tYPE_PARAMETER)
            {
                case "Double":
                    Value = Convert.ToDouble(ObjectValue).ToString("F4");
                    break;
                case "Int":
                    Value = Convert.ToInt32(ObjectValue).ToString();
                    break;
                case "Point":
                    Value = TypeConverter.Point(ObjectValue);
                    break;
                default:
                    Value = ObjectValue.ToString();
                    break;
            }
        }

        private static bool hasParamName(List<Parameter> parameters, string name)
        {
            return parameters.Exists(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        //public bool Equals(ParameterAC other)
        //{
        //    return this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase) &&
        //       this.Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        //}

        ///// <summary>
        ///// проверка списков параметров.
        ///// Все элементы из второго списка обязательно должны соответствовать первому списку, 
        ///// второй список может содержать лишние параметры
        ///// </summary>      
        //public static bool Equal(List<ParameterAC> params1, List<ParameterAC> params2)
        //{
        //    foreach (var p1 in params1)
        //    {
        //        if (!params2.Contains(p1))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
    }
}