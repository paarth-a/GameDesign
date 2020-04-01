using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : Enemy
{
    public override void Attack()
    {
        float step = 1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Player.S.transform.position, step);
    }
}
