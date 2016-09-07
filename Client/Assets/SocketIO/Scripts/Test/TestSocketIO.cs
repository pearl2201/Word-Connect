#region License
/*
 * TestSocketIO.cs
 *
 * The MIT License
 *
 * Copyright (c) 2014 Fabio Panettieri
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

using System.Collections;
using UnityEngine;
using SocketIO;

public class TestSocketIO : MonoBehaviour
{
    private SocketIOComponent socket;
    private string currQuestion;
    private string currAnswer;
    private string correctAnwer;
    private int countPlayerAttend;
    private int countPlayerCorrect;
    private int countPlayerWrong;
    private int idQuestion;
    public void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On("open", TestOpen);
        socket.On("infouser", ReceiveInfoUser);
        socket.On("boop", TestBoop);
        socket.On("error", TestError);
        socket.On("close", TestClose);
        socket.On("answer", ShowAnswer);
        socket.On("question", ShowQuestion);
       
        StartCoroutine("BeepBoop");
    }

    private IEnumerator BeepBoop()
    {
        //Debug.Log("beep boop");
        // wait 1 seconds and continue
        yield return new WaitForSeconds(1);
        Regis();
        //JSONObject jsObject = new JSONObject();
        //jsObject.AddField("room", "wordconnected");
        //jsObject.AddField("id", idQuestion);
        //socket.Emit("join", jsObject);

        //// wait 3 seconds and continue
        //yield return new WaitForSeconds(3);

        //socket.Emit("beep");

        //// wait 2 seconds and continue
        //yield return new WaitForSeconds(2);

        //socket.Emit("beep");

        //// wait ONE FRAME and continue
        //yield return null;

        //socket.Emit("beep");
        //socket.Emit("beep");
    }

    public void Login()
    {

    }

    public void Regis()
    {
        string r = Random.Range(1, 10000).ToString();
        string idUser = "";
        for (var i = 'A'; i<= 'Z'; i++)
        {
            if (Random.Range(0,3) == 0)
            {
                idUser += i;
            }
        }
        JSONObject js = new JSONObject();
        js.AddField("userid", r);
        js.AddField("username", idUser);
        socket.Emit("regisaccount", js);
        Debug.Log("regis");
    }

    public void TestOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    public void TestBoop(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);

        if (e.data == null) { return; }

        Debug.Log(
            "#####################################################" +
            "THIS: " + e.data.GetField("this").str +
            "#####################################################"
        );
    }

    public void ShowQuestion(SocketIOEvent e)
    {
        if (e.data == null) { return; }

        Debug.Log(

            "Question: " + e.data.GetField("question").str

        );
        currQuestion = e.data.GetField("question").str;
        idQuestion = int.Parse(e.data.GetField("idAnswer").str);
        currAnswer = e.data.GetField("answer").str;
        //Debug.Log("idQuestion: " + idQuestion);
        socket.Emit("receiveAnswer");
        SendAnser(currAnswer, idQuestion);
    }

    public void ShowAnswer(SocketIOEvent e)
    {
      
        if (e.data == null) { return; }

        
        correctAnwer = e.data.GetField("answer").str;
        

        countPlayerAttend = int.Parse(e.data.GetField("attend").str);
       
        countPlayerCorrect = int.Parse(e.data.GetField("correct").str);
        countPlayerWrong = int.Parse(e.data.GetField("wrong").str);
       
        Debug.Log("show answer: " + correctAnwer + " - attend:" + countPlayerAttend + " - correct:" + countPlayerCorrect + " -wrong: " + countPlayerWrong );
        string leaderBoard = e.data.GetField("leaderboard").str;
        //Debug.Log("leaderboard: " + leaderBoard);
    }

    public void ReceiveInfoUser(SocketIOEvent e)
    {
        Debug.Log("receive info");
        if (e.data == null) { return; }
        string username = e.data.GetField("username").str;
        int score = int.Parse(e.data.GetField("score").str);
        int rank  = int.Parse(e.data.GetField("rank").str);
        Debug.Log("username: " + username + " -score: " + score);
    }

    public void SendAnser(string currAnswer, int idQuestion)
    {
        JSONObject jsObject = new JSONObject();
        jsObject.AddField("answer", currAnswer);
        jsObject.AddField("id", idQuestion);
        socket.Emit("answer", jsObject);
    }
    public void TestError(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
        //socket.Close();
        //Application.Quit();
    }

    public void TestClose(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }

    void OnApplicationQuit()
    {
        socket.Close();
    }
}
