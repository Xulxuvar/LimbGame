using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHeart : AbstractHeart
{
    private static bool[,] heartGrid = 
            { {false, false, true, true, true, false,false },
            { false,true, true,true, true, true,false },
            { true,true, true,true, true, true,true },
            { true,true, true,true, true, true,true },
            { true,true, true,true, true, true,true },
            { false,true, true,true, true, true,false },
            { false,false, true, true, true, false,false } };

    private static bool[,] partGrid =
            { { false, true, false },
            { true, true, true },
            { false, true, false } };


    public TestHeart()
    :base(0,partGrid,heartGrid)
    {

    }
}
