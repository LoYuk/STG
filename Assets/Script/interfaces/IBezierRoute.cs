using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBezierRoute
{
    public enum Path 
    {
        Infinite,
        LoopFromFirstPoint,
        BackandForth,
        OneTime
    }
    public void followBezierCurve(Path path, int PathNum = 0);
}
