using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Username
    string gamingName;

    //Game states
    int level;
    enum Screen {HomeScreen, MainMenuScreen, GamePlayScreen, WinScreen, TryAgainScreen};
    Screen currentScreen;

    //Passwords and Response Messages
    string[][] passwords = new string[2][];
    string[] passwordsForLevels = new string [2];
    string[] scrambledWords = new string [2];
    string[] congratulationsMsg = { ", You just hacked into the Library's Network. I see why you did that. You have fine written all over you.", ", You just hacked into the Police Station's Network. I see guilty people." };
    string[] tryAgainMsg = { "I think you are cool, but this is not working.", "It doesn't work...... why?" };

    // Start is called before the first frame update
    void Start() {
        passwords[0] = new string[6] { "Thesarus", "Bibliography", "Citation", "Glossary", "Archives", "Encyclopedia" };
        passwords[1] = new string[6] { "Jurisprudential", "Marshal", "Trooper", "Gendarme", "Tithingmn", "Proctor" };
        
        for(int i = 0; i<2; i++)
        {
            int randIndex = UnityEngine.Random.Range(0, 6);
            passwordsForLevels[i] = passwords[i][randIndex];
            print("PasswordsForLevels[i] = " + passwordsForLevels[i]);
            scrambledWords[i] = ScrambleWord(passwordsForLevels[i]);
            print("scrambledWords[i] = " + scrambledWords[i]);
        }

        //Show HomeScreen
        ShowHomeScreen();            
    }

    //Taking User Input
    void OnUserInput(string input) {
        print("The user has entered " + input + " as input");

        if (currentScreen == Screen.HomeScreen)
        {
            input = input.Trim(' ');
            if (input != null && input != "")
            {
                gamingName = input;
                ShowMainMenuScreen();
            }
            else
            {
                Terminal.WriteLine("Entered username cannot be blank");
            }
        }
        else if (currentScreen == Screen.MainMenuScreen)
        {
            if (input == "1" || input == "2")
            {
                level = Int32.Parse(input);
                ShowGamePlayScreen();
            }
            else if (input == "3")
            {
                ShowHomeScreen();
            }
            else
            {
                Terminal.WriteLine("Entered level does not exist. Please enter a valid level.");
            }
        }
        else if (currentScreen == Screen.GamePlayScreen)
        {
            CheckPassword(input);
        }
        else if (currentScreen == Screen.WinScreen)
        {
            ShowMainMenuScreen();
        } 
        else if(currentScreen == Screen.TryAgainScreen)
        {
            if(input == "1")
            {
                ShowGamePlayScreen();
            }
            else if(input == "2")
            {
                ShowMainMenuScreen();
            } 
            else
            {
                Terminal.WriteLine("Entered Input is Invalid.");
            }
        }
    }

    //Methods to compute
    void CheckPassword(string enteredPassword) 
    {        

        if (enteredPassword == passwordsForLevels[level-1])
        {
            ShowWinScreen();
        } else
        {
            ShowTryAgainScreen();         
        }
    }

    string ScrambleWord(string password)
    {
        char[] chars = new char[password.Length];
        int index = 0;

        while (password.Length > 0)
        {
            int next = UnityEngine.Random.Range(0, password.Length - 1);

            chars[index] = password[next];
            password = password.Substring(0, next) + password.Substring(next + 1);
            index++;
        }
        print(chars);
        return new String(chars);
    }



    //Screens 
    //Home Screen 
    void ShowHomeScreen() 
    {
        Terminal.ClearScreen();
        currentScreen = Screen.HomeScreen;
        Terminal.WriteLine("*****Welcome to Terminal Hacker*****");
        Terminal.WriteLine("Please enter you username. Please make sure that there are no blank spaces in \nyour username");
    }

    //MainMenu Screen
    void ShowMainMenuScreen() 
    {
        Terminal.ClearScreen();
        currentScreen = Screen.MainMenuScreen;
        Terminal.WriteLine("*****Welcome to Terminal Hacker*****");
        Terminal.WriteLine("Welcome " + gamingName);
        Terminal.WriteLine("What would like to hack into?");
        Terminal.WriteLine("Press 1 for the Local Library");
        Terminal.WriteLine("Press 2 for the Police Station");
        Terminal.WriteLine("Press 3 to go back to Home Screen.");
        Terminal.WriteLine("Enter you selection");
    }    

    //GamePlay Screen
    void ShowGamePlayScreen() 
    {
        Terminal.ClearScreen();
        currentScreen = Screen.GamePlayScreen;
        Terminal.WriteLine("*****Welcome to Terminal Hacker*****");
        Terminal.WriteLine("Hey " + gamingName + ", Welcome to level " + level);
        Terminal.WriteLine("Here is your clue......\n");
        Terminal.WriteLine(scrambledWords[level-1]);
        Terminal.WriteLine("\nPlease enter the Password......");
    }

    //WinScreen
    void ShowWinScreen()
    {
        Terminal.ClearScreen();
        currentScreen = Screen.WinScreen;
        Terminal.WriteLine("*****Welcome to Terminal Hacker*****");
        Terminal.WriteLine("Congratulations " + gamingName + congratulationsMsg[level - 1]);
        Terminal.WriteLine("\n\nDo you wanna hack something else?... Please any key to go to the Menu");
    }

    //TryAgainScreen
    void ShowTryAgainScreen()
    {
        Terminal.ClearScreen();
        currentScreen = Screen.TryAgainScreen;
        Terminal.WriteLine("*****Welcome to Terminal Hacker*****");
        Terminal.WriteLine(tryAgainMsg[level - 1]);
        Terminal.WriteLine("\n\nDo you wanna try again... Press 1 to try again.");
        Terminal.WriteLine("Press 2 for going to the Menu");
    }

}
