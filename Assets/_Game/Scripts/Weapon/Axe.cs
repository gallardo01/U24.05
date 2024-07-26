using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    private void Update()
    {
        tf.Rotate(720f * Time.deltaTime * Vector3.up);
    }
}
