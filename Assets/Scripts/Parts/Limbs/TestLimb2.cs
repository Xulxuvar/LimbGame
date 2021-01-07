using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLimb2 : AbstractLimb
{
    private static bool[,] partGrid =
           { {false , true , true },
            {true , true , false }};

    public TestLimb2()
    : base(2, partGrid)
    {
        partName = "Test Limb2";
        partDescription = "Temp Limb 2 for Debugging Reasons";
    }
}

