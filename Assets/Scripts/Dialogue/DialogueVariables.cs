using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        // create the story
        Story globalVariablesStory = new Story(loadGlobalsJSON.text);

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public void StartListening(Story story)
    {
        // it's important that VariablesToStory is before assigning the listener!
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        // only maintain variables that were initialized from the globals ink file
        if (variables.ContainsKey(name))
        {
            CalculatePoints(value.ToString());
            variables[name] = value;
            PrintVariables();
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    private void CalculatePoints(string choice)
    {
        switch (choice)
        {
            case "Charmander":
                if (variables["vicky"] is Ink.Runtime.IntValue vickyInt)
                {
                    variables["vicky"] = new Ink.Runtime.IntValue(vickyInt.value + 1);
                }
                break;
            case "Squirtle":
                if (variables["vivian"] is Ink.Runtime.IntValue vivInt)
                {
                    variables["vivian"] = new Ink.Runtime.IntValue(vivInt.value + 1);
                }
                break;
            case "Bulbasaur":
                if (variables["benny"] is Ink.Runtime.IntValue bennyInt)
                {
                    variables["benny"] = new Ink.Runtime.IntValue(bennyInt.value + 1);
                }
                break;
            default:
                break;
        }
    }

    // for debugging
    public void PrintVariables()
    {
        foreach (var variable in variables)
        {
            Debug.Log(variable.Key + " = " + variable.Value);
        }
    }

}