using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Bird : Bird_red
{
    public override void useSkill()
    {
        base.useSkill();
        rg.velocity *= 2;
    }
}
