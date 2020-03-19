using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{

    public override void Attack()
    {
        Vector3 position = player.transform.position;
        Debug.Log("Attack");
    }
}
