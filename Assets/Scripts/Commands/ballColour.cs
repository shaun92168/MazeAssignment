using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Console
{
    public class ballColour : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Argument { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public ballColour()
        {
            Name = "ballColour";
            Command = "ballcolour";
            Description = "Change the ball colour";
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
            Debug.Log("Changed ball colour to " + arg);
        }

        public static ballColour CreateCommand()
        {
            return new ballColour();
        }
    }
}