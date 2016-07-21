using UnityEngine;
using System.Collections;

public class MenuFlow : MonoBehaviour {
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject difficultyMenu;
    public GameObject statsMenu;
	public GameObject menuRoot;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!mainMenu.activeSelf)
            {
                toggleGivenMenu(pauseMenu);
            }
        }

    }

    void resumeGame()
    {

    }
    void pauseGame()
    {

    }

    void startOver()
    {

    }

    public void quitGame()
    {
        Application.Quit();
    }

    void toggleGivenMenu(GameObject menu)
    {
        if (menu.activeSelf)
        {
			menuRoot.SetActive (false);
            menu.SetActive(false);
        }
        else
        {
			menuRoot.SetActive (true);

            menu.SetActive(true);
        }

    }

    


}
