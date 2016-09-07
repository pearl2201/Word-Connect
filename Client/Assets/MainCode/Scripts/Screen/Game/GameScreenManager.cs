using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameScreenManager : MonoBehaviour
{

    private string currUserAnswer;
    private int currIdQuestion;
    private string currQuestion;
    public WaitingPopup waitingPopup;
    public List<BtnQuestion> listBtnQuestion;
    public tk2dTextMesh txtCooldown;
    public tk2dTextMesh txtCurrUserAnswer;
    private List<BtnQuestion> listBtnQuestionSelect;
    public List<LineRenderer> listLineRenderer;
    public tk2dTextMesh txtResult;
    public tk2dTextMesh txtPlayerCountResult;
    [SerializeField]
    private GameObject bgResult;
    private bool isReadyCommit;
    [SerializeField]
    private tk2dSprite btnCommit;
    [SerializeField]
    private tk2dTextMesh txtScore;
    [SerializeField]
    private tk2dTextMesh txtRank;
    // Use this for initialization
    void Start()
    {
        listBtnQuestionSelect = new List<BtnQuestion>();
    }

    void OnEnable()
    {
        Config.gameState = GameState.waitnextQuestion;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ReceiveQuestion(int idQuestion, string question)
    {
        if (Config.gameState == GameState.waitnextQuestion)
        {
            waitingPopup.gameObject.SetActive(false);
            Config.gameState = GameState.ingame;
        }
        ShowQuestion(idQuestion, question);

    }

    public void ShowQuestion(int idQuestion, string question)
    {
        txtPlayerCountResult.gameObject.SetActive(false);
        txtResult.gameObject.SetActive(false);
        bgResult.SetActive(false);
        this.currIdQuestion = idQuestion;
        this.currQuestion = question;

        for (int i = 0; i < question.Length; i++)
        {
            listBtnQuestion[i].SetText(question[i].ToString());
            listBtnQuestion[i].gameObject.SetActive(true);
            listBtnQuestion[i].SetUnSelect();
        }

        for (int i = question.Length; i < listBtnQuestion.Count; i++)
        {
            listBtnQuestion[i].gameObject.SetActive(false);
        }
        float middleX = 0;
        float middleY = -3.5f;
        float distance = 1.35f;

        if (question.Length == 3)
        {
            listBtnQuestion[0].transform.localPosition = new Vector3(-2, -4.5f, 0);
            listBtnQuestion[1].transform.localPosition = new Vector3(0, -2.5f, 0);
            listBtnQuestion[2].transform.localPosition = new Vector3(2, -4.5f, 0);
        }
        else if (question.Length == 4)
        {
            listBtnQuestion[0].transform.localPosition = new Vector3(-2, -4.5f, 0);
            listBtnQuestion[1].transform.localPosition = new Vector3(-2, -2.5f, 0);
            listBtnQuestion[2].transform.localPosition = new Vector3(2, -4.5f, 0);
            listBtnQuestion[3].transform.localPosition = new Vector3(2, -2.5f, 0);
        }
        else if (question.Length == 5)
        {
            listBtnQuestion[0].transform.localPosition = new Vector3(-2, -4.5f, 0);
            listBtnQuestion[1].transform.localPosition = new Vector3(2, -4.5f, 0);
            listBtnQuestion[2].transform.localPosition = new Vector3(0, -3.5f, 0);
            listBtnQuestion[3].transform.localPosition = new Vector3(-2, -2.5f, 0);
            listBtnQuestion[4].transform.localPosition = new Vector3(2, -2.5f, 0);

        }
        else if (question.Length == 6)
        {


            listBtnQuestion[0].transform.localPosition = new Vector3(-2, -2.5f, 0);
            listBtnQuestion[1].transform.localPosition = new Vector3(0, -4.5f, 0);
            listBtnQuestion[2].transform.localPosition = new Vector3(2, -4.5f, 0);

            listBtnQuestion[3].transform.localPosition = new Vector3(-2, -4.5f, 0);
            listBtnQuestion[4].transform.localPosition = new Vector3(0, -2.5f, 0);
            listBtnQuestion[5].transform.localPosition = new Vector3(2, -2.5f, 0);

        }
        else if (question.Length == 7)
        {
            listBtnQuestion[0].transform.localPosition = new Vector3(-2, -1.92f, 0);
            listBtnQuestion[1].transform.localPosition = new Vector3(0, -1.92f, 0);
            listBtnQuestion[2].transform.localPosition = new Vector3(2, -1.92f, 0);

            listBtnQuestion[3].transform.localPosition = new Vector3(0, -3.42f, 0);

            listBtnQuestion[4].transform.localPosition = new Vector3(-2, -5f, 0);
            listBtnQuestion[5].transform.localPosition = new Vector3(0, -5f, 0);
            listBtnQuestion[6].transform.localPosition = new Vector3(2, -5f, 0);
        }
        else if (question.Length == 8)
        {
            listBtnQuestion[0].transform.localPosition = new Vector3(-2, -1.92f, 0);
            listBtnQuestion[1].transform.localPosition = new Vector3(0, -1.92f, 0);
            listBtnQuestion[2].transform.localPosition = new Vector3(2, -1.92f, 0);
            listBtnQuestion[3].transform.localPosition = new Vector3(-2, -3.42f, 0);
            listBtnQuestion[4].transform.localPosition = new Vector3(2, -3.42f, 0);
            listBtnQuestion[5].transform.localPosition = new Vector3(-2, -5f, 0);
            listBtnQuestion[6].transform.localPosition = new Vector3(0, -5f, 0);
            listBtnQuestion[7].transform.localPosition = new Vector3(2, -5f, 0);

        }
        else if (question.Length == 9)
        {
            listBtnQuestion[0].transform.localPosition = new Vector3(-2, -1.92f, 0);
            listBtnQuestion[1].transform.localPosition = new Vector3(0, -1.92f, 0);
            listBtnQuestion[2].transform.localPosition = new Vector3(2, -1.92f, 0);
            listBtnQuestion[3].transform.localPosition = new Vector3(-2, -3.42f, 0);
            listBtnQuestion[4].transform.localPosition = new Vector3(0, -3.42f, 0);
            listBtnQuestion[5].transform.localPosition = new Vector3(2, -3.42f, 0);
            listBtnQuestion[6].transform.localPosition = new Vector3(-2, -5f, 0);
            listBtnQuestion[7].transform.localPosition = new Vector3(0, -5f, 0);
            listBtnQuestion[8].transform.localPosition = new Vector3(2, -5f, 0);
        }
        listBtnQuestionSelect.Clear();
        SetTextCurrAnswer();
        for (int i = 0; i < listLineRenderer.Count; i++)
        {
            listLineRenderer[i].SetVertexCount(0);
        }
        StopCoroutine("cooldownTime");
        StartCoroutine(cooldownTime());

    }

    IEnumerator cooldownTime()
    {
        float timeCooldown = 17;
        txtCooldown.gameObject.SetActive(true);
        isReadyCommit = true;
        btnCommit.color = Color.white;
        while (timeCooldown > 0)
        {
            timeCooldown -= Time.deltaTime;
            if (timeCooldown <= 0)
            {
                timeCooldown = 0;
            }
            txtCooldown.text = timeCooldown.ToString("0.00");
            yield return null;
        }
        isReadyCommit = false;
        btnCommit.color = new Color(0.4f, 0.4f, 0.4f, 1);
    }

    public void ReceiveServerAnswer(string answer, int countAttend, int countCorrect, int countWrong, string leaderBoard)
    {
        txtCooldown.gameObject.SetActive(false);
        txtPlayerCountResult.gameObject.SetActive(true);
        txtResult.gameObject.SetActive(true);
        bgResult.SetActive(true);
        if (Config.gameState == GameState.waitnextQuestion)
        {

        }
        else
        {
            Debug.Log("receive answer: " + answer);
            txtResult.text = answer;
            txtPlayerCountResult.text = "Count player receive question: " + countAttend + "\nCount player correct: " + countCorrect + "\nCount player answer wrong: " + countWrong;
            //JSONObject js = new JSONObject(leaderBoard);

            //foreach (JSONObject j in js.list)
            //{
            //    if (j.GetField("userid").str.Equals(Config.GetAndroidID()))
            //    {
            //        int nextScore = int.Parse(j.GetField("score").str);
            //        Config.user.Rank = int.Parse(j.GetField("rank").str);
            //        txtRank.text = "" + Config.user.Rank;
            //        txtScore.text = "" + nextScore;
            //        if (nextScore > Config.user.Score)
            //        {
            //            // correct
            //            Debug.Log("correct");
            //        }
            //        else
            //        {
            //            Debug.Log("wrong");
            //            // wrong
            //        }
            //        Config.user.Score = nextScore;
            //    }
            //}
            Debug.Log("leaderboard: " + leaderBoard);
        }
    }
    public void ClickCommit()
    {
        if (isReadyCommit && currUserAnswer.Length == currQuestion.Length)
        {
            btnCommit.color = new Color(0.4f, 0.4f, 0.4f, 1);
            isReadyCommit = false;
            NetworkManager.Instance.SendAnser(currUserAnswer, currIdQuestion);
        }
    }



    public void CheckClickBtnQuestion(BtnQuestion btn)
    {
        if (!isReadyCommit)
            return;
        if (btn.isSelect)
        {
            int i = 0;
            for (; i < listBtnQuestionSelect.Count; i++)
            {
                if (listBtnQuestionSelect[i] == btn)
                {
                    break;
                }
            }

            // release tu i trong listBtnQuestionSelect
            for (int j = listBtnQuestionSelect.Count - 1; j >= i; j--)
            {
                listBtnQuestionSelect[j].SetUnSelect();
                listBtnQuestionSelect.RemoveAt(j);
                if (j > 0)
                {
                    listLineRenderer[j - 1].SetVertexCount(0);
                }
            }


            // release tu i-1 trong list line, neu i > 0
        }
        else
        {

            if (listBtnQuestionSelect.Count > 0)
            {
                listLineRenderer[listBtnQuestionSelect.Count - 1].SetVertexCount(2);
                listLineRenderer[listBtnQuestionSelect.Count - 1].SetPosition(0, listBtnQuestionSelect[listBtnQuestionSelect.Count - 1].transform.position + new Vector3(0, 0, 1f));
                listLineRenderer[listBtnQuestionSelect.Count - 1].SetPosition(1, btn.transform.position + new Vector3(0, 0, 1f));
            }
            btn.SetSelect();
            listBtnQuestionSelect.Add(btn);
        }
        SetTextCurrAnswer();
    }

    private void SetTextCurrAnswer()
    {
        currUserAnswer = "";
        for (int i = 0; i < listBtnQuestionSelect.Count; i++)
        {
            currUserAnswer += listBtnQuestionSelect[i].text;
        }
        txtCurrUserAnswer.text = currUserAnswer;
    }
}
