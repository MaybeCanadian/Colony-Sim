using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PathingEvalStates
{
    UNEVALUATED,
    OPEN,
    FRONTIER,
    CLOSED
}
