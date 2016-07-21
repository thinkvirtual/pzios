using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SQLiteDatabase;
using HutongGames.PlayMaker;

public class WordWheelLogic : MonoBehaviour
{
    public Text debugm;
    public string solutionstring = "king";
    public InputField inp;
    public Text scoreTxt;
    public List<string> solutionletters = new List<string>();
    public List<string> alreadyguessed = new List<string>();
    public List<string> displaystring = new List<string>();
    public int correctcount = 0;
    public List<string> originalstring = new List<string>();
    public List<string> puzzlelist = new List<string>();
    public List<string> levellist = new List<string>();
    SQLiteDB db = SQLiteDB.Instance;
    public string currentpuzzle = "";
    public int currentpuzzleno = 0;
    public List<Text> currentTextSlots = new List<Text>();
    public List<Text> textspots = new List<Text>();
    public static bool issolving = false;
    public GameObject allTextsRoot;
    public static List<Button> disabledbuttons = new List<Button>();
    public GameObject keyboardroot;
    public Button[] allButtons;
        Dictionary<string, Button> keyboardbuttons = new Dictionary<string, Button>();
	public static AudioClip hoversound;
	bool updatesolvedit=false;
	public bool neednewpuzzle = false;

	public bool ispuzzlesolved=false;

    public AudioClip hsound;
    // Use this for initialization
    void Start()
    {
        //StartCoroutine("InitializePuzzleData");
        //debugm.text = filePath;
        //GameObject[] alltexts = GameObject.FindGameObjectsWithTag("letterspots");
        Transform[] alltexts = allTextsRoot.GetComponentsInChildren<Transform>();
		hoversound = hsound;
        for(int i = 1; i < alltexts.Length; i++)
        {
            Transform current = alltexts[i];
            if (current.GetComponent<Text>() != null)
            {
                textspots.Add(current.GetComponent<Text>());
                current.transform.parent.transform.gameObject.SetActive(false);

            }

        }

        allButtons = keyboardroot.GetComponentsInChildren<Button>();

        foreach(Button x in allButtons)
        {
            keyboardbuttons.Add(x.transform.name, x);
        }

		//loadfrompreviousgame
		currentpuzzleno=PlayerPrefs.GetInt ("lastpuzzleindx");
		FsmVariables.GlobalVariables.GetFsmInt ("playerpoints").Value=PlayerPrefs.GetInt ("savedplayerpoints");

        loadPuzzlesFromCsv();

       textspots.Reverse();

        //get keyboard letters

        //loadPuzzlesIntoMemory();
    }

    
    public string result = "";

    public void saveGame(){
		PlayerPrefs.SetInt ("lastpuzzleindx", currentpuzzleno);
		PlayerPrefs.SetInt ("savedplayerpoints", FsmVariables.GlobalVariables.GetFsmInt ("playerpoints").Value);
	}

	public void resetGame(){
		PlayerPrefs.SetInt ("lastpuzzleindx", 1);
		PlayerPrefs.SetInt ("savedplayerpoints", 0);
		FsmVariables.GlobalVariables.GetFsmInt ("playerpoints").Value = 0;
	}
    

    public void loadPuzzlesFromCsv()
    {

        TextAsset txt = (TextAsset)Resources.Load("prodwordsv1", typeof(TextAsset));
        string filecontent = txt.text;
       // filecontent.Trim();
        string[] allwords = filecontent.Split(',');

        foreach(string wrd in allwords)
        {
            puzzlelist.Add(wrd);
        }
      //  puzzlelist.Add("sand");
       // puzzlelist.Reverse();

        Debug.Log("number of puzzles" + puzzlelist.Count);

    }
    public void loadPuzzlesIntoMemory()
    {

        /*
        //db.DBLocation = Application.streamingAssetsPath;

        string tmp= getAssetPath();

        db.DBLocation = Application.persistentDataPath + "/db";

        db.ConnectToDefaultDatabase("wheelwords.db", false);


        Debug.Log(db.Exists);
        DBReader x = db.GetAllData("words");
        var fd = x.dataTable;

        while (x != null && x.Read())
        {
            string currentword = x.GetStringValue("word");

            puzzlelist.Add(currentword);
			puzzlelist.Reverse ();

        }
    

        Debug.Log("number of puzzles" + puzzlelist.Count);
        */
    }

    public void getnewpuzzle()
    {
		updatesolvedit = false;
		neednewpuzzle = false;
        foreach(Button b in allButtons)
        {
            b.interactable = true;
        }
        currentTextSlots.Clear();
        currentpuzzleno++;
        if (currentpuzzleno > puzzlelist.Count)
        {
            currentpuzzleno = 0;
        }
        solutionstring = puzzlelist[currentpuzzleno];
        solutionletters.Clear();
        alreadyguessed.Clear();
        displaystring.Clear();
        foreach (char x in solutionstring)
        {
            solutionletters.Add(x.ToString());
            displaystring.Add("_");
        }
        initLetterButtons();

       updateLetterButtons();
    }

    public void issolvingmode(bool l)
    {
        issolving = l;
       // if (issolving)
       // {
        //    originalstring = displaystring;
      //  }
    }

    public bool getFreeVowel()
    {

        string[] vowels = { "a", "e", "i", "o", "u" };
        foreach(string currentvowel in vowels)
        {
            if (!displaystring.Contains(currentvowel))
            {
                if (solutionletters.Contains(currentvowel))
                {
                    ProcessBonus(currentvowel);
					if (!displaystring.Contains ("_")) {
						ispuzzlesolved = true;
					}
                    return true;
                    break;
                }
            }


        }
        return false;
    }


    public void getFreeHint()
    {
        int cnt = 0;
        bool done = false;
        List<string> v = new List<string>();
        v.Add("a");
        v.Add("e");
        v.Add("i");
        v.Add("o");
        v.Add("u");

        string tmpvowel = "";


        foreach (string c in displaystring)
        {
            if (c.Equals("_"))
            {
                string candidate = solutionletters[cnt];
                //if (!v.Contains(candidate))
                //{
                    ProcessBonus(candidate);
					Debug.Log ("hintdone");
				if (!displaystring.Contains ("_")) {
					ispuzzlesolved = true;
				}
                    done = true;
                    break;
                //}
                //else
                //{
                  //  tmpvowel = c;
                //}
            }
            cnt++;
        }

       // if (!done)
        //{
         //   ProcessBonus(tmpvowel);
       // }
    }

    public void doGuess()
    {
        ProcessGuess(inp.text);
    }

    public void ProcessBonus(string guess)
    {
        if (guess != "")
        {
            if (issolving)
            {
                forSolve(guess);
            }
            else
            {
				if (keyboardbuttons.ContainsKey (guess)) {
					keyboardbuttons[guess].interactable = false;

				}


                guess = guess.ToLower();
                if (alreadyguessed.Contains(guess))
                {
                    Debug.Log("already guessed");
                    //PlayMakerFSM.BroadcastEvent("alreadyguessed");
                    //already guessed
                }
                else
                {
                    alreadyguessed.Add(guess);

                }
                if (solutionletters.Contains(guess))
                {
                    int c = 0;
                    foreach (string x in solutionletters)
                    {
                        if (x.Equals(guess))
                        {
                            correctcount++;
                            displaystring[c] = x;

                        }
                        c++;
                    }
                    Debug.Log("right guess");
                    //  PlayMakerFSM.BroadcastEvent("correctguess");

                    updateLetterButtons();
                    //update the letters
                }
                else
                {
                    //  PlayMakerFSM.BroadcastEvent("incorrectguess");

                    Debug.Log("incorrect");
                    //incorrect;
                }
            }

        }
    }



    public void ProcessGuess(string guess)
    {

        if (issolving)
        {
            forSolve(guess);
        }
        else
        {
            keyboardbuttons[guess].interactable = false;

        guess = guess.ToLower();
        if (alreadyguessed.Contains(guess))
        {
            Debug.Log("already guessed");
            PlayMakerFSM.BroadcastEvent("alreadyguessed");
            //already guessed
        }
        else
            {
                alreadyguessed.Add(guess);

            }
            if (solutionletters.Contains(guess))
        {
            int c = 0;
            foreach (string x in solutionletters)
            {
                if (x.Equals(guess))
                {
                    correctcount++;
                    displaystring[c] = x;

                }
                c++;
            }

                if (!displaystring.Contains("_"))
                {
                    string fullstring = "";
                    foreach (string d in displaystring)
                    {
                        fullstring += d;

                    }
                    if (fullstring.Equals(solutionstring))
                    {
                        originalstring.Clear();
                        updateLetterButtons();
                        ispuzzlesolved = true;
                        //PlayMakerFSM.BroadcastEvent("puzzlesolved");
                        issolving = false;

                        Debug.Log("Correct solved!");
                    }

                }
                else {

                Debug.Log("right guess");
                FsmVariables.GlobalVariables.GetFsmInt("playerpoints").Value += Example_Reward.pointwheelresult;
                    scoreTxt.text = FsmVariables.GlobalVariables.GetFsmInt("playerpoints").Value.ToString();
                FsmVariables.GlobalVariables.GetFsmString("finalearned").Value = Example_Reward.pointwheelresult.ToString() + " Points!";
                updateLetterButtons();

                PlayMakerFSM.BroadcastEvent("correctguess");
                }
                //update the letters
            }
        else
        {
            PlayMakerFSM.BroadcastEvent("incorrectguess");

            Debug.Log("incorrect");
            //incorrect;
        }
        }

    }


    public void forSolve(string solveletter)
    {
        if (originalstring.Count == 0)
        {
            foreach (string l in displaystring)
            {
                originalstring.Add(l);

            }
        }

        if (displaystring.Contains("_"))
        {
            int cl = 0;
            foreach (string d in displaystring)
            {

                if (d.Equals("_"))
                {
                    displaystring[cl] = solveletter;
                    updateLetterButtons();
                    break;
                }
                cl++;
            }

            if (!displaystring.Contains("_"))
            {
                string fullstring = "";
                foreach (string d in displaystring)
                {
                    fullstring += d;

                }
                if (fullstring.Equals(solutionstring))
                {
                    originalstring.Clear();
                    updateLetterButtons();
					ispuzzlesolved = true;
                    //PlayMakerFSM.BroadcastEvent("puzzlesolved");
                    issolving = false;

                    Debug.Log("Correct solved!");
                }
                else
                {

                    int p = 0;
                    foreach (string c in originalstring)
                    {
                        displaystring[p] = c;
                        p++;
                    }
                    originalstring.Clear();
                    issolving = false;

                    PlayMakerFSM.BroadcastEvent("wrongsolve");
                    Debug.Log("Incorrect");
                    //updateLetterButtons();

                }
            }

        }
        else
        {
            string fullstring = "";
            foreach (string d in displaystring)
            {
                fullstring += d;

            }
            if (fullstring.Equals(solutionstring))
            {
                originalstring.Clear();
				ispuzzlesolved = true;
                PlayMakerFSM.BroadcastEvent("puzzlesolved");
                issolving = false;

                Debug.Log("Correct solved!");
            }
            else
            {
                int p = 0;
                foreach (string c in originalstring)
                {
                    displaystring[p] = c;
                    p++;
                }
                originalstring.Clear();
                PlayMakerFSM.BroadcastEvent("wrongsolve");

                Debug.Log("Incorrect");
                updateLetterButtons();
            }

        }

        //updateLetterButtons();
    }


    void initLetterButtons()
    {

        //for every text element, assign it the display letter value
        Debug.Log(displaystring.ToString());

        int currentpuzzlesize = solutionletters.Count;
        int count = 0;

        foreach (Text lspot in textspots)
        {
            if (count < currentpuzzlesize)
            {
                lspot.transform.parent.transform.gameObject.SetActive(true);

                 lspot.text = displaystring[count];
                //lspot.text = count.ToString();
                currentTextSlots.Add(lspot);
            }
            else
            {
                lspot.transform.parent.transform.gameObject.SetActive(false);
            }
            count++;

        }
       // if (correctcount == solutionstring.Length)
      //  {
      //      Debug.Log("solved");
      //      PlayMakerFSM.BroadcastEvent("puzzlesolved");

            //puzzle solved
      //  }
        currentTextSlots.Reverse();
    }

    public void updateLetterButtons()
    {

        //for every text element, assign it the display letter value
        Debug.Log(displaystring.ToString());

        int currentpuzzlesize = solutionletters.Count;
        int count = 0;



        foreach (Text lspot in currentTextSlots)
        {
            if (count < currentpuzzlesize)
            {
                if (displaystring[count].Equals("_"))
                {
                    lspot.enabled = false;
                    lspot.text = displaystring[count];

                }
                else
                {
                    lspot.enabled = true;
                    lspot.text = displaystring[count];

                }


            }
            else
            {
                break;
            }

            count++;

        }
		/*

        if (!displaystring.Contains("_"))
        {
            PlayMakerFSM.BroadcastEvent("puzzlesolved");

            Debug.Log("solved from letter button");
        }
        */
       // else if (correctcount == solutionstring.Length)
       // {
        //    PlayMakerFSM.BroadcastEvent("puzzlesolved");
//
        //    Debug.Log("solved");
            //puzzle solved
      //  }
    }

    // Update is called once per frame
    void Update()
    {

		if (ispuzzlesolved) {
			ispuzzlesolved = false;
			PlayMakerFSM.BroadcastEvent ("puzzlesolved");
		}

		/*
		if (!updatesolvedit) {
			if (!displaystring.Contains ("_")) {

				string fullstring = "";
				foreach (string d in displaystring)
				{
					fullstring += d;

				}
				if (fullstring.Equals(solutionstring))
				{
					originalstring.Clear();
					updateLetterButtons();
					updatesolvedit = true;

					PlayMakerFSM.BroadcastEvent("puzzlesolved");
					issolving = false;

					Debug.Log("Correct solved!");
				}

			}
		}
		*/
    }
}
