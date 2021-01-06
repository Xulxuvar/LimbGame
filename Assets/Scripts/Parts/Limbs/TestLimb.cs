using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLimb : AbstractLimb
{

    private static bool[,] partGrid =
           { {true , true , true } };

    public TestLimb()
    :base(1,partGrid)
    {
        partName = "Test Limb";
        partDescription = "Temp Limb for Debugging Reasons";
    }
}
