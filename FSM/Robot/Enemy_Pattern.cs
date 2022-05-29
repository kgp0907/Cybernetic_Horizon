using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy_Pattern : MonoBehaviour
{
    Robot_Base robot;
    Robot_AI robotAi;
  
    public string[] pattern;
    public string[] patternCycle;
    List<string> patternStorage;

    private int currentPattern = 0;
    private float patternCount = 0f;
    private int patternIndex;
    private void Awake()
    {
        robot = this.transform.GetComponent<Robot_Base>();
        robotAi = robot.GetComponent<Robot_AI>();
        ListArrangeMent(robot);
    }
    private List<string> ParseCommands(string str)
    {
        List<string> list = new List<string>();
        string[] splits = str.Split(',');
        foreach (var split in splits)
            list.Add(split);
        return list;
    }

    public void ListArrangeMent(Robot_Base dragon)
    {
        List<string> command = ParseCommands(patternCycle[0]);
        patternCount = command.Count;
        patternStorage = command;
    }
    public void NextState(Robot_Base dragon)
    {
        if (pattern.Length == 0)
        {
            return;
        }      
        else if (currentPattern > patternCount-1)//{
        {
            currentPattern = 0;
        }

        List<string> currentpattern = ParseCommands(pattern[Convert.ToInt32(patternStorage[currentPattern])]);
        StartCoroutine(NextStateCoroutine(dragon, currentpattern));
        patternIndex += 1;
    }
    IEnumerator NextStateCoroutine(Robot_Base dragon, List<string> currentpattern)
    {
        SetState(dragon, currentpattern[patternIndex]);

        if (patternIndex >= currentpattern.Count - 1)// 1     2
        {
            yield return StaticCoroutine.Wait(0.1f);
            currentPattern += 1;
            patternIndex = 0;
        }
    }
    void SetState(Robot_Base boss, string command)
    {
        switch (command)
        {
            case "clap":
                robot.ChangeState(Robot_Base.RobotP1_State.P1_ATTACK_CLAP);
                break;
            case "takedown":
                robot.ChangeState(Robot_Base.RobotP1_State.P1_ATTACK_TAKEDOWN);
                break;
            case "dead":
                robot.ChangeState(Robot_Base.RobotP1_State.DEAD);
                break;
            case "punch":
                robot.ChangeState(Robot_Base.RobotP1_State.P2_ATTACK_PUNCH);
                break;
            case "smash":
                robot.ChangeState(Robot_Base.RobotP1_State.P2_ATTACK_SMASH);
                break;
            case "missile":
                robot.ChangeState(Robot_Base.RobotP1_State.P3_ATTACK_HOMING_MISSILE);
                break;
            case "earth":
                robot.ChangeState(Robot_Base.RobotP1_State.P3_ATTACK_EARTHQUAKE);
                break;
            case "claw":
                robot.ChangeState(Robot_Base.RobotP1_State.P3_ATTACK_CLAW_ATTACK);
                break;
            case "bomb":
                robot.ChangeState(Robot_Base.RobotP1_State.P3_ATTACK_BOMB);
                break;

        }
    }
}
