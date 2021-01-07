using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLimb3 : AbstractLimb
{
    private static bool[,] partGrid =
           { {true , true , true },
            {false,false,true }};

    public TestLimb3()
    : base(3, partGrid)
    {
        partName = "Test Limb2";
        partDescription = "Temp Limb 2 for Debugging Reasons";
    }
}


