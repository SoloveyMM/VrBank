using UnityEngine;


public class flat_MenuController : MonoBehaviour
{
    public GameObject pressM;
    public GameObject pressMClose;

    public GameObject menu;

    public bool menuIsActive = false;

    void Start()
    {
        pressM.SetActive(true);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!menuIsActive)
            {
                showMenu();
            }
            else
            {
                hideMenu();
            }
        }
    }

    private void showMenu()
    {
        menu.SetActive(true);
        pressM.SetActive(false);
        pressMClose.SetActive(true);
        menuIsActive = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void hideMenu()
    {
        menu.SetActive(false);
        pressM.SetActive(true);
        pressMClose.SetActive(false);
        menuIsActive = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }


    public void exit()
    {
        Application.Quit();
    }

}
