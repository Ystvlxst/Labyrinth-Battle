using UnityEngine;

public class SignalizationTransition : Transition
{
    public void Signal()
    {
        NeedTransit = true;
    }
}
