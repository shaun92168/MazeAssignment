using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class Quit : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Argument { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public Quit()
        {
            Name = "Quit";
            Command = "quit";
            Description = "Quits the application";
            Argument = "None";
            Help = "Use this command with no arguments to force Unity to quit";

            AddCommandToConsole();
        }

        public override void RunCommand()
        {
            if(Application.isEditor)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            } else
            {
                Application.Quit(); 
            }
        }

        public override void RunCommandWithArg(string arg)
        {
            DevConsole.AddStaticMessageToConsole(Help); 
            
        }

        public static Quit CreateCommand()
        {
            return new Quit();
        }
    }

}

