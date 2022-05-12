using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Robot_P1_Pattern : MonoBehaviour
{
    Robot_P1 robotP1;
    Robot_AI robotAi;
  
    public string[] Pattern;
    public string[] PatternCycle;
    List<string> patternStorage;

    private int currentPattern = 0;
    private float patternCount = 0f;
    private int patternIndex;
    private void Awake()
    {
        robotP1 = this.transform.GetComponent<Robot_P1>();
        robotAi = robotP1.GetComponent<Robot_AI>();
        ListArrangeMent(robotP1);
    }
    private List<string> ParseCommands(string str)
    {
        List<string> list = new List<string>();
        string[] splits = str.Split(',');
        foreach (var split in splits)
            list.Add(split);
        return list;
    }

    public void ListArrangeMent(Robot_P1 dragon)
    {
        List<string> command = ParseCommands(PatternCycle[0]);
        patternCount = command.Count;
        patternStorage = command;
    }
    public void NextState(Robot_P1 dragon)
    {
        if (Pattern.Length == 0)
        {
            return;
        }
        else if (currentPattern > patternCount - 1)//{
        {
            currentPattern = 0;
        }

        List<string> currentpattern = ParseCommands(Pattern[Convert.ToInt32(patternStorage[currentPattern])]);
        StartCoroutine(NextStateCoroutine(dragon, currentpattern));
        patternIndex += 1;
    }
    IEnumerator NextStateCoroutine(Robot_P1 dragon, List<string> currentpattern)
    {
        SetState(dragon, currentpattern[patternIndex]);

        if (patternIndex >= currentpattern.Count - 1)// 1     2
        {
            yield return new WaitForSeconds(0.1f);
            currentPattern += 1;
            patternIndex = 0;
        }
    }
    void SetState(Robot_P1 boss, string command)
    {
        switch (command)
        {
            case "Clap":
                robotP1.ChangeState(Robot_P1.RobotP1_State.ATTACK_CLAP);
                break;
            case "Takedown":
                robotP1.ChangeState(Robot_P1.RobotP1_State.ATTACK_TAKEDOWN);
                break;
            case "Dead":
                robotP1.ChangeState(Robot_P1.RobotP1_State.DEAD);
                break;
            case "Punch":
                robotP1.ChangeState(Robot_P1.RobotP1_State.PUNCH);
                break;
            case "Smash":
                robotP1.ChangeState(Robot_P1.RobotP1_State.SMASH);
                break;
            case "missile":
                robotP1.ChangeState(Robot_P1.RobotP1_State.HOMING_MISSILE);
                break;
            case "earth":
                robotP1.ChangeState(Robot_P1.RobotP1_State.EARTHQUAKE);
                break;
            case "claw":
                robotP1.ChangeState(Robot_P1.RobotP1_State.CLAW_ATTACK);
                break;
            case "bomb":
                robotP1.ChangeState(Robot_P1.RobotP1_State.BOMB);
                break;

        }
    }
}
