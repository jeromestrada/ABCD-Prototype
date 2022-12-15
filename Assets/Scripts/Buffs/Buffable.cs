using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Buffable
{
    public void ApplyBuff(string buffName);

    public void RemoveBuff(string buffName);

}
