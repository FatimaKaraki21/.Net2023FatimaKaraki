namespace ShoppingCart.Models;
using System;
using System.Runtime.Serialization;

[DataContract]
public class DataPoint
{
    public DataPoint(int x, decimal y)
    {
        this.x = x;
        this.Y = y;
    }

    public DataPoint(string label, double sold)
    {
        this.Label = label;
        this.Sold = sold;
    }


    [DataMember(Name = "x")]
    public Nullable<int> x = null;


    [DataMember(Name = "y")]
    public Nullable<decimal> Y = null;


    [DataMember(Name = "label")]
    public string Label = "";


    [DataMember(Name = "sold")]
    public Nullable<double> Sold = null;
} 