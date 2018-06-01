using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Calculation
/// </summary>
public class Calculation
{
    public static CalculateAll CalculateTotalAmount(DataTable dtItems, DataTable dtSundri)
    {
        CalculateAll cal = new CalculateAll();
        if (dtItems != null)
        {
            DataTable dtGrdItems = (DataTable)(dtItems);

            foreach (DataRow item in dtGrdItems.Rows)
            {
                cal.IGST += CommonCls.ConvertDecimalZero(item["IGSTTaxAmt"].ToString());
                cal.SGST += CommonCls.ConvertDecimalZero(item["SGSTTaxAmt"].ToString());
                cal.CGST += CommonCls.ConvertDecimalZero(item["CGSTTaxAmt"].ToString());
                cal.CESS += CommonCls.ConvertDecimalZero(item["CESSTaxAmt"].ToString());
                cal.ItemAmount += CommonCls.ConvertDecimalZero(item["ItemAmount"].ToString());
                cal.ItemTaxable += CommonCls.ConvertDecimalZero(item["NetAmt"].ToString());                
                cal.ItemDiscount += CommonCls.ConvertDecimalZero(item["DiscountAmt"].ToString());
            }
        }

        decimal SundriAmt = 0;
        if (dtSundri != null)
        {
            foreach (DataRow item in dtSundri.Rows)
            {
                if (item["SundriInd"].ToString() == "Add") //For Sundri Amount Add
                    SundriAmt += CommonCls.ConvertDecimalZero(item["SundriAmt"].ToString());
                else if (item["SundriInd"].ToString() == "Less") //For Sundri Amount Less
                    SundriAmt -= CommonCls.ConvertDecimalZero(item["SundriAmt"].ToString());
            }
            cal.TotalSundriAddLess = SundriAmt;
        }
        //cal.TotalGross = cal.ItemTaxable;
        //cal.TotalAllNet = (cal.TotalTaxable + cal.ItemTaxable + cal.TotalSundriAddLess);
        cal.TotalGross = Math.Round((cal.ItemAmount - cal.ItemDiscount), 2);
        cal.TotalTaxable = Math.Round((cal.IGST + cal.SGST + cal.CGST + cal.CESS), 2);
        cal.TotalAllNet = Math.Round((cal.TotalGross + cal.TotalTaxable + cal.TotalSundriAddLess), 2);

        return cal;
    }

    public static StructItems CalculateRate(StructItems item)
    {
        item.DiscountValue = item.ItemDiscount;
        if (item.DiscountInPerc)
        {
            item.DiscountValue = (((item.ItemQty + item.ItemFree) * item.ItemRate) * item.ItemDiscount) / 100;
        }
        item.ItemTaxable = Math.Round((((item.ItemQty + item.ItemFree) * item.ItemRate) - item.DiscountValue), 2);
        item.ItemAmount = Math.Round((item.ItemQty * item.ItemRate), 2);
        item.DiscountValue = Math.Round(item.DiscountValue, 2);
        return item;
    }

    public static StructItems TaxCal(StructItems item)
    {
        decimal MaxAmt = item.ItemTaxable;

        int ValidCG = (item.ItemCGSTRate > 0 ? 1 : 0);
        int ValidSG = (item.ItemSGSTRate > 0 ? 1 : 0);
        int ValidIG = (item.ItemIGSTRate > 0 ? 1 : 0);
        int ValidCess = (item.ItemCESSRate > 0 ? 1 : 0);

        int devidedBy = ValidCG + ValidSG + ValidIG + ValidCess;

        if (ValidCG == 1)
        {
            item.ItemCGSTRate = item.ItemRate / devidedBy;
            item.ItemCGSTAmt = Math.Round(((MaxAmt * item.ItemCGSTRate) / 100), 2);
        }
        if (ValidIG == 1)
        {
            item.ItemIGSTRate = item.ItemRate / devidedBy;
            item.ItemIGSTAmt = Math.Round(((MaxAmt * item.ItemIGSTRate) / 100), 2);
        }
        if (ValidSG == 1)
        {
            item.ItemSGSTRate = item.ItemRate / devidedBy;
            item.ItemSGSTAmt = Math.Round(((MaxAmt * item.ItemSGSTRate) / 100), 2);
        }
        if (ValidCess == 1)
        {
            item.ItemCESSRate = item.ItemRate / devidedBy;
            item.ItemCESSAmt = Math.Round(((MaxAmt * item.ItemCESSRate) / 100), 2);
        }
        return item;
    }
}

public struct CalculateAll
{
    public decimal IGST { get; set; }
    public decimal SGST { get; set; }
    public decimal CGST { get; set; }
    public decimal CESS { get; set; }
    public decimal ItemAmount { get; set; }
    public decimal ItemDiscount { get; set; }
    public decimal ItemTaxable { get; set; }

    public decimal TotalGross { get; set; }
    public decimal TotalTaxable { get; set; }
    public decimal TotalSundriAddLess { get; set; }
    public decimal TotalAllNet { get; set; }

}

public struct StructItems
{
    public decimal ItemQty { get; set; }
    public decimal ItemFree { get; set; }
    public decimal ItemRate { get; set; }
    public decimal ItemAmount { get; set; }
    public decimal ItemDiscount { get; set; }
    public decimal ItemTaxable { get; set; }

    public decimal ItemIGSTRate { get; set; }
    public decimal ItemSGSTRate { get; set; }
    public decimal ItemCGSTRate { get; set; }
    public decimal ItemCESSRate { get; set; }

    public decimal ItemIGSTAmt { get; set; }
    public decimal ItemSGSTAmt { get; set; }
    public decimal ItemCGSTAmt { get; set; }
    public decimal ItemCESSAmt { get; set; }

    public decimal TotalGross { get; set; }
    public decimal TotalTaxable { get; set; }
    public decimal TotalSundriAddLess { get; set; }
    public decimal TotalAllNet { get; set; }

    public bool DiscountInPerc { get; set; }
    public decimal DiscountValue { get; set; }
    //public bool Rupees { get; set; }
}