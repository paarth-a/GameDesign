using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Player
{
    public GameObject projectile;

    public override void Attack()
    {
        Vector3 shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        //instantiate and give it a velocity in this direction
    }
}
