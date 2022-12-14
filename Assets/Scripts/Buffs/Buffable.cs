using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Buffable
{
    public void ApplyBuff(Buff buff);

    public void RemoveBuff(Buff buff);

}
