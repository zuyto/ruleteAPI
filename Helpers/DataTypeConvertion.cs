using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace rulete.Helpers
{
    public static class DataTypeConvertion
    {
        public static int getFieldInt(object obj, string prop)
        {
            Type tipo = obj.GetType();
            return (int)tipo.GetProperty(prop).GetValue(obj, null);
        }
        public static string getFieldString(object obj, string prop)
        {
            Type tipo = obj.GetType();
            return (string)tipo.GetProperty(prop).GetValue(obj, null);
        }
        public static bool getFieldBool(object obj, string prop)
        {
            Type tipo = obj.GetType();
            return (bool)tipo.GetProperty(prop).GetValue(obj, null);
        }
        public static int? getRowInt(object objeto)
        {
            if (objeto != null && !string.IsNullOrEmpty(objeto.ToString()))
            {
                return Convert.ToInt32(objeto.ToString());
            }
            else
            {
                return (int?)null;
            }
        }
        public static long? getRowLong(object objeto)
        {
            if (objeto != null && !string.IsNullOrEmpty(objeto.ToString()))
            {
                return Convert.ToInt64(objeto.ToString());
            }
            else
            {
                return (long?)null;
            }
        }
        public static DateTime? getRowDateTime(object objeto)
        {
            if (objeto != null && !string.IsNullOrEmpty(objeto.ToString()))
            {
                return Convert.ToDateTime(objeto.ToString());
            }
            else
            {
                return (DateTime?)null;
            }
        }
        public static Double? getRowDouble(object objeto)
        {
            if (objeto != null && !string.IsNullOrEmpty(objeto.ToString()))
            {
                return Convert.ToDouble(objeto.ToString());
            }
            else
            {
                return (Double?)null;
            }
        }
        public static Decimal? getRowDecimal(object objeto)
        {
            if (objeto != null && !string.IsNullOrEmpty(objeto.ToString()))
            {
                return Convert.ToDecimal(objeto.ToString());
            }
            else
            {
                return (Decimal?)null;
            }
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T ConvertDataTableObject<T>(DataTable dt)
        {
            T data = (T)Activator.CreateInstance(typeof(T));
            foreach (DataRow row in dt.Rows)
            {
                data = GetItem<T>(row);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {

                var prop = (from propiedades in temp.GetProperties()
                            where propiedades.Name.ToUpper() == column.ColumnName
                            select new
                            {
                                tipoProp = propiedades.PropertyType,
                                pro = propiedades
                            }).ToList();

                Type tipoDr = dr[column.ColumnName].GetType();

                var valorColumna = dr[column.ColumnName].ToString();

                if (valorColumna != null && valorColumna != "null" && !string.IsNullOrEmpty(dr[column.ColumnName].ToString()))
                    if (prop.Count() != 0)
                        if ((prop[0].tipoProp != tipoDr))
                        {
                            switch (prop[0].tipoProp.ToString().Replace("System.", ""))
                            {
                                case "String":
                                    string valorstr = dr[column.ColumnName].ToString();
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valorstr), (object[])null);
                                    break;
                                case "Nullable`1[Int64]":
                                    long? valor64 = getRowLong(dr[column.ColumnName]);
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valor64), (object[])null);
                                    break;
                                case "Int64":
                                    long valor64N = (long)getRowLong(dr[column.ColumnName]);
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valor64N), (object[])null);
                                    break;
                                case "Nullable`1[Int32]":
                                    int? valor32 = getRowInt(dr[column.ColumnName]);
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valor32), (object[])null);
                                    break;
                                case "Int32":
                                    int valor32N = (int)getRowInt(dr[column.ColumnName]);
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valor32N), (object[])null);
                                    break;
                                case "Nullable`1[Decimal]":
                                    decimal? valorDecimal = getRowDecimal(dr[column.ColumnName]);
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valorDecimal), (object[])null);
                                    break;
                                case "Decimal":
                                    decimal valorDecimalN = (Decimal)getRowDecimal(dr[column.ColumnName]);
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valorDecimalN), (object[])null);
                                    break;
                                case "Boolean":
                                    bool valorBoolean = Convert.ToBoolean(dr[column.ColumnName]);
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valorBoolean), (object[])null);
                                    break;
                                case "Nullable":
                                    string valorNull = string.Empty;
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valorNull), (object[])null);
                                    break;
                                case "Nullable`1[DateTime]":
                                    DateTime? valorDateTime = getRowDateTime(dr[column.ColumnName]);
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valorDateTime), (object[])null);
                                    break;
                                case "DateTime":
                                    DateTime valor = (DateTime)getRowDateTime(dr[column.ColumnName]);
                                    prop[0].pro.SetValue((object)obj, RuntimeHelpers.GetObjectValue(valor), (object[])null);
                                    break;

                            }
                        }
                        else
                        {
                            prop[0].pro.SetValue(obj, dr[column.ColumnName], null);
                        }
            }
            return obj;
        }
    }
}
