using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Console
{
    public class bgColour : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Argument { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public bgColour()
        {
            Name = "bgColour";
            Command = "bgcolour";
            Description = "Change the background colour";
            Argument = "A colour";
            Help = "Use this command with a single argument";

            AddCommandToConsole();
        }

        public override void RunCommand()
        {
            DevConsole.AddStaticMessageToConsole(Help);
        }

        public override void RunCommandWithArg(string arg)
        {
            Debug.Log("Changed background colour to " + arg);
        }

        public static bgColour CreateCommand()
        {
            return new bgColour();
        }
    }
}