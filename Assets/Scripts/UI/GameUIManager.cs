using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {

    private GameObject m_GamePanel;
    private GameObject m_OverPanel;

    private PlaneFly m_Planefly;

    private UILabel label_Score;
    private UILabel label_Time;

    private GameObject leftAndRightButtons;
    private GameObject resetButton;

    private int time;

    public int Time
    {
        get { return time; }
        set 
        { 
            time = value;
            Debug.Log(time);
            UpdateTimeLabel(time); //携程每次更新，都更新text上时间状态
        }
    }


    //OverPanelInfo
    private UILabel finalScore;
    private UILabel starNumber;
    private UILabel timeSpan;

	void Start () {
        m_GamePanel = GameObject.Find("GamePanel");
        m_OverPanel = GameObject.Find("OverPanel");
        m_Planefly = GameObject.Find("Ship_5").GetComponent<PlaneFly>();
        resetButton = GameObject.Find("Reset");

        label_Score = GameObject.Find("starnumber").GetComponent<UILabel>();
        label_Score.text = "0";

        label_Time = GameObject.Find("Time").GetComponent<UILabel>();
        label_Time.text = "0:0";

        StartCoroutine("AddTime");

        finalScore = GameObject.Find("yourscore/yourscorenumber").GetComponent<UILabel>();
        starNumber = GameObject.Find("star/starscore").GetComponent<UILabel>();
        timeSpan = GameObject.Find("time/timescore").GetComponent<UILabel>();


        leftAndRightButtons = GameObject.Find("ButtonControl");
        m_OverPanel.SetActive(false);



        UIEventListener.Get(resetButton).onClick = ResetButtonClick;
	}

    void Update()
    {
        //UpdateTimeLabel(time); 
    }


    /// <summary>
    /// update starscore everytime got star
    /// </summary>
    public void UpdateLabel(int rewardScore)
    {
        label_Score.text = rewardScore.ToString();
    }

    /// <summary>
    /// update time according to AddTime()->Time->label_Time.text
    /// </summary>
    /// <param name="time"></param>
    private void UpdateTimeLabel(int t)
    {
        if (t < 60)
        {
            label_Time.text = "0:" + t;
        }
        else
        {
            label_Time.text = (t / 60) + ":" + (t % 60);
        }
    }

    public void ShowOverPanelAndHideLeftRightControlButton()
    {
        m_OverPanel.SetActive(true);
        leftAndRightButtons.SetActive(false);
        SetOverPanelInfo();
        StopAddTime();
    }

    private void ResetButtonClick(GameObject go)
    {
        SceneManager.LoadScene("StartUI");
    }

    /// <summary>
    /// 通过携程累加时间, excellent method!
    /// </summary>
    IEnumerator AddTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); //每次等待一秒
            Time++; //Time就增加1
        }
    }

    /// <summary>
    /// 停止调用携程
    /// </summary>
    private void StopAddTime()
    {
        StopCoroutine("AddTime");
    }

    /// <summary>
    /// 从GamePanel给结束面板赋值
    /// </summary>
    private void SetOverPanelInfo()
    {
        int t = int.Parse(label_Score.text);
        int score = t * 10 + time;
        starNumber.text = "+" + t*10;  //一个star值10分
        timeSpan.text = "+" + time.ToString();
        finalScore.text = (score).ToString();


        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Gold", t*10);
    }
}
