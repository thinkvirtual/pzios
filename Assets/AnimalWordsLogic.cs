using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

using SQLiteDatabase;


public class AnimalWordsLogic : MonoBehaviour
{
    SQLiteDB db = SQLiteDB.Instance;
    public List<SinglePuzzle> puzzlelist = new List<SinglePuzzle>();
    public static List<string> solutions = new List<string>();
    public List<string> solutions2 = new List<string>();
    public static string[] puzzlesolutions;
    public int currentWordSize = 4;
    public static GameObject[] gridBlocks;
    public string currentLevel = "intro";
    public List<string> previousCorrect = new List<string>();
    public TextMesh firstword;
    public TextMesh secondword;
    static SinglePuzzle currentPuzzle;
    public string firstwrd;
    public string secondwrd;

    public List<string> levellist;

    public List<SinglePuzzle> currentlevelpuzzlelist = new List<SinglePuzzle>();


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadPuzzlesIntoMemory()
    {
        db.DBLocation = Application.streamingAssetsPath;
        db.ConnectToDefaultDatabase("puzzleislandwords.db", false);
        Debug.Log(db.Exists);
        DBReader x = db.GetAllData("word");
        var fd = x.dataTable;

        while (x != null && x.Read())
        {

            SinglePuzzle c = new SinglePuzzle(x.GetStringValue("scramble").ToCharArray(), x.GetStringValue("words"), x.GetIntValue("width"), x.GetStringValue("levelname"));
            if (!levellist.Contains(x.GetStringValue("levelname")))
            {
                levellist.Add(x.GetStringValue("levelname"));
            }
            Debug.Log("width" + x.GetIntValue("width"));
            puzzlelist.Add(c);

        }



        currentlevelpuzzlelist = puzzlelist.FindAll(s => s.LevelName.Contains(currentLevel));
        //List<SinglePuzzle> withinrange = puzzlelist.FindAll(s => s.LevelName.Contains(currentLevel));

        currentPuzzle = currentlevelpuzzlelist[getRandomIndexFromRange(currentlevelpuzzlelist.Count)];

        Debug.Log("number of puzzles" + puzzlelist.Count);
    }


    public class SinglePuzzle
    {
        public char[] scramble;
        public string words;
        public int width;
        public string levelname;


        public SinglePuzzle(char[] scramble1, string words1, int width1, string levelname1)
        {
            scramble = scramble1;
            words = words1;
            levelname = levelname1;
            width = width1;

        }
        public char[] Scramble
        {
            get { return scramble; }
            set { scramble = value; }
        }

        public string Words
        {
            get { return words; }
            set { words = value; }
        }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public string LevelName
        {
            get { return levelname; }
            set { levelname = value; }
        }



    }

    public int getRandomIndexFromRange(int elementCount)
    {
        return Random.Range(0, elementCount);
    }

    public List<SinglePuzzle> getPuzzlesForLevel(string levelname)
    {
        return puzzlelist.FindAll(s => s.LevelName.Contains(levelname));


    }

    public string GenerateNewPuzzleBoard(int wordsize)
    {

        // take the if and else out if this breaks something

        //if (isLevelCompleted ()) {
        //		advanceLevel ();

        //} else {

        //newpuzzleok = false;
        wordsize = 4;
        previousCorrect.Clear();

        currentWordSize = wordsize;
        solutions.Clear();
        solutions2.Clear();
        Debug.Log("current puzzle size:" + currentlevelpuzzlelist.Count);

        //List<SinglePuzzle> withinrange = puzzlelist.FindAll(s => s.LevelName.Contains(currentLevel));
        if (currentlevelpuzzlelist.Count > 0)
        {
            currentPuzzle = currentlevelpuzzlelist[getRandomIndexFromRange(currentlevelpuzzlelist.Count)];


            string currentPuzzleScramble = new string(currentPuzzle.Scramble);
            string board = currentPuzzleScramble;
            string puzzlewords = currentPuzzle.Words;
            Debug.Log(board);


            string[] splitPuzzleWords = puzzlewords.Split(',');
            //var solAry = (PlayMakerArrayListProxy)FsmVariables.GlobalVariables.GetFsmGameObject("CurrentSolutions").Value.GetComponent<PlayMakerArrayListProxy>();

            // FsmVariables.GlobalVariables.GetFsmArray().
            foreach (string currentPuzzleWord in splitPuzzleWords)
            {

                //solutionary.Values().a
                solutions.Add(currentPuzzleWord);
                solutions2.Add(currentPuzzleWord);
            }

            //Parse first second;

            if (solutions.Count > 1)
            {
                secondwrd = solutions[1];
                firstwrd = solutions[0];


            }
            else
            {
                secondwrd = "";
                firstwrd = solutions[0];
            }

            drawBlanks(firstwrd.Length, secondwrd.Length);


            puzzlesolutions = solutions.ToArray();
            // FsmVariables.GlobalVariables.GetFsmArray("")
            return UpdatePuzzleGrid(board);
            //}
        }

        return "";

        //		return "";
        //return board;
    }

    public void drawBlanks(int firstwordlength, int secondwordlength)
    {
        firstword.text = "";
        secondword.text = "";

        for (int i = 0; i < firstwordlength; i++)
        {
            firstword.text += "_ ";
        }

        for (int i = 0; i < secondwordlength; i++)
        {
            secondword.text += "_ ";
        }
    }

    public bool isLevelCompleted()
    {
        if (currentlevelpuzzlelist.Count == 1)
        {

            currentlevelpuzzlelist.Remove(currentPuzzle);
            return true;

        }
        else
        {
            currentlevelpuzzlelist.Remove(currentPuzzle);
            return false;


        }

    }


    public void advanceLevel()
    {
        Debug.Log("level completed!");

        if (levellist.Count == 1)
        {
            Debug.Log("you won game over");


        }
        else if (levellist.Count > 1)
        {

            levellist.Remove(currentLevel);

            currentLevel = levellist[0];
            currentlevelpuzzlelist = getPuzzlesForLevel(currentLevel);
            if (currentlevelpuzzlelist.Count != 0)
            {
            }

        }
        else
        {
            Debug.Log("you won game over");

        }
        //trigger a fanfare or playmaker action for this
        //check in level array to see which level is next in the sequence 
    }

    public int getTargetWordBoardSize(int wrdsize)
    {
        return 0;
    }

    public bool checkIfInSolutions(string word)
    {
        bool lpres = solutions.Contains(word);
        Debug.Log(word + " " + lpres);


        if (firstwrd.Equals(word))
        {
            firstword.text = word;
            if (!secondword.text.Contains("_"))
            {
                Debug.Log("puzzcomplete");
                return false;
            }
            else
            {
                return true;
            }

        }
        else if (secondwrd.Equals(word))
        {
            secondword.text = word;

            if (!firstword.text.Contains("_"))
            {

            }
            else
            {
                return true;
            }
        }

        //if (!firstwrd.Contains ("_") && !secondwrd.Contains ("_")) {
        //GetComponent<PlayMakerFSM> ().SendEvent ("levelcomplete");
        //	return true;
        //}


        return false;
    }

    //add method to check if it's the end of the level

    public void setDifficulty(int wordsize)
    {
    }

    

    public string UpdatePuzzleGrid(string board)
    {

        char[] boardLetters = board.ToCharArray();
        List<string> boardltrs = new List<string>();
        if (boardLetters.Length <= gridBlocks.Length)
        {
            for (int i = 0; i < boardLetters.Length; i++)
            {
                GameObject currentGridBlock = gridBlocks[i];
                string t = i.ToString();
                if (currentGridBlock.GetComponent<TextMesh>() != null)
                {
                    currentGridBlock.GetComponent<TextMesh>().text = "" + boardLetters[i];
                    //currentGridBlock.GetComponent<TextMesh>().text = ""+ t;
                    currentGridBlock.transform.name = "" + boardLetters[i];
                    boardltrs.Add("" + boardLetters[i]);
                    currentGridBlock.SetActive(true);
                }
            }

        }
        return board;
    }




}
