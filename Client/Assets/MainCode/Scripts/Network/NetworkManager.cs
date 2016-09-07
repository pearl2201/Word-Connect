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

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _instance;

    public static NetworkManager Instance
    {
        get
        {

            return _instance;
        }
    }

    void Awake()
    {

        _instance = this;


    }
    [SerializeField]
    private ScreenManager screenManager;

    [SerializeField]
    private SocketIOComponent socket;

    private string currQuestion;
    private string currAnswer;
    private string correctAnwer;
    private int countPlayerAttend;
    private int countPlayerCorrect;
    private int countPlayerWrong;
    private int idQuestion;
    void Start()
    {
        Config.GetAndroidID();
        Config.gameState = GameState.connect;

        Invoke("Connect", 0.5f);
    }

    public void Connect()
    {
        socket.Connect();
        socket.On("open", TestOpen);
        socket.On("infouser", ReceiveInfoUser);
        socket.On("boop", TestBoop);
        socket.On("error", TestError);
        socket.On("close", TestClose);
        socket.On("answer", ShowAnswer);
        socket.On("question", ShowQuestion);
        socket.On("requestregis", CallRequestRegis);
        socket.On("leaderboard", ReceiveLeaderboard);
    }

    public void CallRequestRegis(SocketIOEvent e)
    {
        Debug.Log("Call request regis");
        Config.gameState = GameState.regis;
        screenManager.OpenRegisUserScreen();
    }


    public void Login()
    {
        Config.gameState = GameState.login;
        JSONObject js = new JSONObject();
        js.AddField("userid", Config.GetAndroidID());
        Debug.Log("login");
        socket.Emit("login", js);
    }




    public void Regis(string usernameRegis)
    {

        MiniUser miniUser = new MiniUser(Config.GetAndroidID(), usernameRegis);

        JSONObject js = new JSONObject();
        js.AddField("userid", miniUser.MobileId);
        js.AddField("username", miniUser.Username);
        socket.Emit("regisaccount", js);

    }

    public void TestOpen(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
        if (!Config.isLogin)
        {
            Config.isLogin = true;
            Login();
        }


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

            "Answer: " + e.data.GetField("answer").str

        );
        currQuestion = e.data.GetField("question").str;
        idQuestion = int.Parse(e.data.GetField("idAnswer").str);
        currAnswer = e.data.GetField("answer").str;

        socket.Emit("receiveAnswer");
        screenManager.gameScreenManager.ReceiveQuestion(idQuestion, currQuestion);
    }

    public void ShowAnswer(SocketIOEvent e)
    {

        if (e.data == null) { return; }
        correctAnwer = e.data.GetField("answer").str;
        countPlayerAttend = int.Parse(e.data.GetField("attend").str);
        countPlayerCorrect = int.Parse(e.data.GetField("correct").str);
        countPlayerWrong = int.Parse(e.data.GetField("wrong").str);

        string leaderBoard = e.data.GetField("leaderboard").str;
        screenManager.gameScreenManager.ReceiveServerAnswer(correctAnwer, countPlayerAttend, countPlayerCorrect, countPlayerWrong, leaderBoard);

    }

    public void ReceiveInfoUser(SocketIOEvent e)
    {
       
        if (e.data == null)
        {
            Debug.Log("data null");
            return;
        }
        string username = e.data.GetField("username").str;
     
        int score = int.Parse(e.data.GetField("score").str);
       
        int rank = int.Parse(e.data.GetField("rank").str);
       
        Config.user = new User(Config.GetAndroidID(), username, score, rank);
      
        screenManager.OpenWelcomeScreen();
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

    }

    public void TestClose(SocketIOEvent e)
    {
        Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }

   

    public void Close()
    {
        socket.Emit("exit");
        socket.Close();
    }
    void OnApplicationQuit()
    {

        
    }


    public void RequestLeaderboard()
    {
        socket.Emit("leaderboard");
    }

    public void ReceiveLeaderboard(SocketIOEvent e)
    {
     
        if (Config.gameState == GameState.start)
        {
          
            screenManager.OpenLeaderboard(e.data.GetField("leaderboard").str);
        }
        else
        {
            Debug.Log("tatadadsa");
        }
    }
}


