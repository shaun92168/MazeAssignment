using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI; 

namespace Console
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Argument { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        public void AddCommandToConsole()
        {
            DevConsole.AddCommandsToConsole(Command, this);
            DevConsole.AddStaticMessageToConsole(Command + ": " + Description);
        }

        public abstract void RunCommand();
        public abstract void RunCommandWithArg(string arg); 
    }
    public class DevConsole : MonoBehaviour
    {
        public static DevConsole Instance { get; private set; }
        public static Dictionary<string, ConsoleCommand> Commands { get; private set; }
        [Header("UI Components")]
        public Canvas consoleCanvas;
        public ScrollRect scrollRect;
        public Text consoleText;
        public Text inputText;
        public InputField consoleInput;
        public GameObject bg;
        public GameObject ball; 

        private void Awake()
        {
            if(Instance != null)
            {
                return;
            }

            Instance = this;
            Commands = new Dictionary<string, ConsoleCommand>();
        }

        private void Start()
        {
            consoleCanvas.gameObject.SetActive(false);
            CreateCommands(); 
        }

        private void CreateCommands()
        {
            AddMessageToConsole("Press F1 to close console"); 
            Quit commandQuit = Quit.CreateCommand();
            bgColour commandbgColour = bgColour.CreateCommand();
            ballColour commandballColour = ballColour.CreateCommand();
        }

        public static void AddCommandsToConsole(string _name, ConsoleCommand _command)
        {
            if(!Commands.ContainsKey(_name))
            {
                Commands.Add(_name, _command);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                consoleCanvas.gameObject.SetActive(true);
                consoleInput.Select();
                consoleInput.ActivateInputField();
                Time.timeScale = 0;
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                consoleCanvas.gameObject.SetActive(false);
                Time.timeScale = 1;
            }

            if (consoleCanvas.gameObject.activeInHierarchy)
            {
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    if(inputText.text != "")
                    {
                        AddMessageToConsole(inputText.text);
                        ParseInput(inputText.text);
                        consoleInput.text = "";
                        consoleInput.Select();
                        consoleInput.ActivateInputField();
                    }
                }
            }
        }

        private void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
            scrollRect.verticalNormalizedPosition = 0f; 
        }

        public static void AddStaticMessageToConsole(string msg)
        {
            DevConsole.Instance.consoleText.text += msg + "\n";
            DevConsole.Instance.scrollRect.verticalNormalizedPosition = 0f; 
        }

        private void ParseInput(string input)
        {
            string[] _input = input.Split(null);
            string[] colours = { "red", "blue", "green", "yellow" };

            if (_input.Length == 0 || _input == null)
            {
                AddMessageToConsole("Command not recognized.");
                return; 
            }

            if(!Commands.ContainsKey(_input[0]))
            {
                AddMessageToConsole("Command not recognized.");
            } else
            {
                if (_input.Length == 1)
                {
                    Commands[_input[0]].RunCommand();
                }
                else
                {
                    Commands[_input[0]].RunCommandWithArg(_input[1]);
                    _input[1] = _input[1].ToLower();

                    if (_input[0] == "bgcolour")
                    {
                        if (!colours.Any(_input[1].Contains))
                        {
                            AddMessageToConsole(_input[1] + " is not a supported colour");
                        }
                        else if (_input[1] == "red")
                        {
                            bg.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);

                        }
                        else if (_input[1] == "blue")
                        {
                            bg.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
                        }
                        else if (_input[1] == "green")
                        {
                            bg.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                        }
                        else if (_input[1] == "yellow")
                        {
                            bg.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);
                        }
                    }

                    if (_input[0] == "ballcolour")
                    {
                        if (!colours.Any(_input[1].Contains))
                        {
                            AddMessageToConsole(_input[1] + " is not a supported colour");
                        }
                        else if (_input[1] == "red")
                        {
                            ball.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);

                        }
                        else if (_input[1] == "blue")
                        {
                            ball.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
                        }
                        else if (_input[1] == "green")
                        {
                            ball.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                        }
                        else if (_input[1] == "yellow")
                        {
                            ball.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);
                        }
                    }
                }
            }
        }
    }

}