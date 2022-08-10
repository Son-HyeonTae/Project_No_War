using UnityEngine;
using System;


//Generic Data class
public class DATA<T> 
{
    private T v;
    public T Value
    {
        get { return this.v; }
        set
        {
            this.v = value;
            this.onChange?.Invoke(value);
        }
    }
    //callback
    public Action<T> onChange;
}
