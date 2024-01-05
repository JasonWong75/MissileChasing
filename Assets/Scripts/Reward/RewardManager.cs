using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour {
    private Transform m_Transform;
    private GameObject reward;
    private int rewardCount; // Count the current number of rewards
    private int maxRewardCount = 3; // max number of rewards

    public int RewardCount
    {
        get { return rewardCount; }
        set { rewardCount = value; }
    }
	// Use this for initialization
	void Start () {
        reward = Resources.Load<GameObject>("reward");
        m_Transform = gameObject.GetComponent<Transform>();

        InvokeRepeating("RewardGenerate", 2, 5);   // Generate rewards

	}
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// Generate Rewards
    /// </summary>
    private void RewardGenerate()
    {
        if (rewardCount < maxRewardCount)
        {
            Vector3 pos = new Vector3(Random.Range(-1134,719),-7,Random.Range(-1270,319));
            GameObject.Instantiate(reward,pos,Quaternion.identity,m_Transform);
            rewardCount++;
            Debug.Log("Current Rewards: " + rewardCount);
        }
    }

    public void StopRewardGenerate()
    {
        CancelInvoke();
    }

    public void RewardCountDown()
    {
        rewardCount--;
        Debug.Log("Current Rewards: " + rewardCount);
    }
}
