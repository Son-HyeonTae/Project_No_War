using UnityEngine;
using System;


/**

* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
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
