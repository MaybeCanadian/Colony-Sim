using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain
{
    public AIAgent agent = null;
    public AINeedsValues needs = null;
    public AIMemory memory = null;

    #region Init Functions
    public AIBrain(AIAgent connectedAgent)
    {
        agent = connectedAgent;

        SetUpNeeds();
        SetUpMemory();
    }
    private void SetUpNeeds()
    {
        needs = new AINeedsValues();
    }
    private void SetUpMemory()
    {
        memory = new AIMemory();
    }
    #endregion

    #region Update Functions
    public void BrainUpdate(float deltaTime)
    {
        List<NeedsTypes> lowestNeeds = needs.GetNeedsBelowPercentValue(0.2f);
    }
    public void BrainFixedUpdate(float fixedDeltaTime)
    {

    }
    public void BrainLateUpdate(float deltaTime)
    {

    }
    #endregion
}
