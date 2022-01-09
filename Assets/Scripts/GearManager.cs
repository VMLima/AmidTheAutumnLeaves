using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearManager : MonoBehaviour
{
    public static GearManager instance;
    public Gear gear = new Gear();
    void Awake()
    {
        instance = this;
    }

    public class Gear
    {

        private float heatInsulation = 0; // Divisor for temp gain over 80F.
        private float coldInsulation = 0; // Divisor for temp loss under 68F.

        private float waterRes = 0; // Level of weatherproofness of your gear.
        private float windRes = 0; // Resistance to the wind




        // The first one is the current value, the second is what comes from your gear.
        public float HeatInsulation
        {
            get { return heatInsulation; }
            set
            { heatInsulation = value; }
        }

        // The first one is the current value, the second is what comes from your gear.
        public float ColdInsulation
        {
            get { return coldInsulation; }
            set
            { coldInsulation = value; }
        }

        public float WaterRes
        {
            get { return waterRes; }
            set
            { waterRes = value; }
        }

        public float WindRes
        {
            get { return windRes; }
            set
            { windRes = value; }
        }

    }


   
}
