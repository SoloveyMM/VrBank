using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MadamController : MonoBehaviour
{
    public Transform player;

    //Madam menu
    public GameObject madamMenu;
    public GameObject pressEText;
    public GameObject pressECloseText;
    public GameObject showFlatText;

    public GameObject menuController;

    public bool menuIsActive = false;
    public bool inZone = false;

    void Start()
    {
        showFlatText.SetActive(false);
        pressECloseText.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(player.position);
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (inZone)
            {
                if (!menuIsActive && !menuController.GetComponent<MenuController>().menuIsActive)
                {
                    showMadamMenu();
                }
                else if(menuIsActive && !menuController.GetComponent<MenuController>().menuIsActive)
                {
                    hideMadamMenu();
                }
                    
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            inZone = true;
            madamMenu.SetActive(true);
            GetComponent<Animator>().Play("hello");
            
        }

    }



    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            inZone = false;
            madamMenu.SetActive(false);
        }
    }

    void showMadamMenu()
    {
        pressEText.SetActive(false);
        pressECloseText.SetActive(true);
        menuController.GetComponent<MenuController>().pressM.SetActive(false);
        showFlatText.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menuIsActive = true;
    }

    void hideMadamMenu()
    {
        pressECloseText.SetActive(false);
        pressEText.SetActive(true);
        menuController.GetComponent<MenuController>().pressM.SetActive(true);
        showFlatText.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuIsActive = false;
    }

    public void showFlat()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
