
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class User
{
  
    private string mobileId;
    private string username;
    private int score;
    private int rank;



    public User( string mobileId, string username, int score, int rank)
    {

        this.mobileId = mobileId;
        this.username = username;
        this.score = score;
        this.rank = rank;
    }
  

    public string MobileId
    {
        get
        {
            return mobileId;
        }

        set
        {
            mobileId = value;
        }
    }

    public string Username
    {
        get
        {
            return username;
        }

        set
        {
            username = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int Rank
    {
        get
        {
            return rank;
        }

        set
        {
            rank = value;
        }
    }

    public override string ToString()
    {
        return "user: id - " + mobileId + " - username: - " + username + " - score: " + score + " - rank: " + rank;
    }
}

public class MiniUser
{

    private string mobileId;
    private string username;

    public MiniUser(string mobileId, string username)
    {
        this.mobileId = mobileId;
        this.username = username;
    }

    public string MobileId
    {
        get
        {
            return mobileId;
        }

        set
        {
            mobileId = value;
        }
    }

    public string Username
    {
        get
        {
            return username;
        }

        set
        {
            username = value;
        }
    }

    public static MiniUser GenFakeUser()
    {
        string r = Config.GetAndroidID();
        string idUser = "";
        for (var i = 'A'; i <= 'Z'; i++)
        {
            if (Random.Range(0, 3) == 0)
            {
                idUser += i;
            }
        }
        MiniUser user = new MiniUser(r, idUser);
        return user;
    }
}

