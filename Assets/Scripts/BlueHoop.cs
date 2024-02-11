using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueHoop : HoopClass
{
    // This is Polymorphism from the HoopClass script to change it's score
    public override void HoopScore(int pointValue)
    {
        base.HoopScore(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
