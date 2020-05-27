using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;

public class MenuController : MonoBehaviour
{
    //Press M to show/hide menu
    public GameObject pressM;
    public GameObject pressMClose;
    

    //Entermenu
    public GameObject enterMenu;
    public InputField logginField;
    public InputField passwordField;
    public Text errorText;

    //Menu
    public GameObject menu;

    //Personal Office
    public GameObject personalOffice;
    public Text firstName;
    public Text secondName;
    public Text balance;

    //MargageCalc
    public GameObject margageCalc;
    public InputField costField;
    public InputField firstPayField;
    public InputField creditPeriodField;
    public InputField creditPerField;
    public Text errorMargageText;
    public Text monthPayText;
    public Text overPayText;

    public GameObject madam;

    public bool authorize = false;
    public bool menuIsActive = false;
    private string path;
    private User user = new User();

    void Start()
    {
        path = Path.Combine(Application.dataPath, "User.json");
        user = JsonUtility.FromJson<User>(File.ReadAllText(path));
        errorText.gameObject.SetActive(false);
        errorMargageText.gameObject.SetActive(false);
        
        /*us.loggin = "123";
        us.password = "1234";
        us.firstName = "Алексей";
        us.secondName = "Ролич";
        File.WriteAllText(path, JsonUtility.ToJson(us));*/

    }

    void Update()
    {
        //show or hide menu
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!menuIsActive && !madam.GetComponent<MadamController>().menuIsActive)
                showMenu();
            else if(menuIsActive && !madam.GetComponent<MadamController>().menuIsActive)
                hideMenu();
        }
    }

    private void showMenu()
    {
        if (!authorize)
        {
            enterMenu.SetActive(true);

        }
        else
        {
            menu.SetActive(true);
            
        }
        if (madam.GetComponent<MadamController>().inZone)
        {
            madam.GetComponent<MadamController>().pressEText.SetActive(false);
        }
        pressM.SetActive(false);
        pressMClose.SetActive(true);
        menuIsActive = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void hideMenu()
    {
        if (madam.GetComponent<MadamController>().inZone)
        {
            madam.GetComponent<MadamController>().pressEText.SetActive(true);
        }

        pressM.SetActive(true);
        pressMClose.SetActive(false);
        errorText.gameObject.SetActive(false);
        errorMargageText.gameObject.SetActive(false);
        enterMenu.SetActive(false);
        menu.SetActive(false);
        personalOffice.SetActive(false);
        margageCalc.SetActive(false);
        menuIsActive = false;
        logginField.text = "";
        passwordField.text = "";
        resetMargageFields();
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public void logIn()
    {
 
        if (user.loggin == logginField.text && user.password == passwordField.text)
        {
            authorize = true;
            enterMenu.SetActive(false);
            menu.SetActive(true);
            errorText.gameObject.SetActive(false);
        }
        else
        {
            errorText.gameObject.SetActive(true);
        }
    }

    public void enterPersonalOffice()
    {
        personalOffice.SetActive(true);
        menu.SetActive(false);
        firstName.text = user.firstName;
        secondName.text = user.secondName;
        balance.text = Convert.ToString(user.balance);

    }

    public void cancelFromOffice()
    {
        personalOffice.SetActive(false);
        menu.SetActive(true);
    }

    public void cancelFromMargage()
    {
        errorMargageText.gameObject.SetActive(false);
        margageCalc.SetActive(false);
        personalOffice.SetActive(true);
        resetMargageFields();
    }

    public void margageCalcMenu()
    {
        margageCalc.SetActive(true);
        personalOffice.SetActive(false);
    }

    public void resetMargageFields()
    {
        costField.text = "";
        firstPayField.text = "";
        creditPeriodField.text = "";
        creditPerField.text = "";
        monthPayText.text = "";
        overPayText.text = "";
        
    }

    public void calculateMargage()
    {
        double cost, firstPay, creditPeriod,creditPercent;
        double monthPay, overPay;
        if(double.TryParse(costField.text, out cost) && 
            double.TryParse(firstPayField.text, out firstPay) &&
            double.TryParse(creditPerField.text, out creditPercent) &&
            double.TryParse(creditPeriodField.text, out creditPeriod))
        {
            creditPercent = creditPercent / 1200;
            creditPeriod = creditPeriod * 12;
            double num = Math.Pow((1 + creditPercent), creditPeriod);
            errorMargageText.gameObject.SetActive(false);
            if (cost - firstPay == 0)
            {
                monthPayText.text = "0";
                overPayText.text = "0";
            }
            else if (cost - firstPay < 0)
            {
                errorMargageText.gameObject.SetActive(true);
            }
            else
            {

                monthPay = (cost - firstPay) * ((creditPercent * num) / (num - 1));
                monthPay = Math.Round(monthPay, 2);
                overPay = monthPay * creditPeriod - (cost-firstPay);
                print(cost);
                print(creditPeriod);
                print(monthPay);
                overPay = Math.Round(overPay, 2);

                monthPayText.text = monthPay.ToString().Replace('.', ',');
                overPayText.text = overPay.ToString().Replace('.', ',');
            }
        }
        else
        {
            errorMargageText.gameObject.SetActive(true);
        }
        

    }


    public void exit()
    {
        Application.Quit();
    }

}
[Serializable]
public class User
{
    public string loggin;
    public string password;
    public string firstName;
    public string secondName;
    public float balance;
}