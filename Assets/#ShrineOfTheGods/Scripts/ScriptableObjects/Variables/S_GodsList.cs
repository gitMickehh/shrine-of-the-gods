﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New gods list", menuName = "Shrine Of The Gods/Lists/Gods")]
public class S_GodsList : S_AbstractList<S_God>
{
    [Tooltip("This will be the difference in power to make this god powerful or weak")]
    public int PowerGap = 3;
    public int leadOverpowerThreshold = 6;

    [SerializeField] List<S_God> strongestGods = new List<S_God>();
    [SerializeField] List<S_God> weakestGods = new List<S_God>();
    [SerializeField] List<S_God> neutralGods = new List<S_God>();
    public void ResetList()
    {
        //when disabling
        strongestGods.Clear();
        weakestGods.Clear();
    }

    public void ClearPowers()
    {
        foreach (S_God god in items)
        {
            god.currentPower.Value = 0;
        }
    }

    public void CalculateGap()
    {
        if (items.Count == 0)
            return;
        else if (items.Count == 1)
            return;

        //Debug.Log("Calculating Gap");

        strongestGods = new List<S_God>();
        weakestGods = new List<S_God>();

        for (int i = 0; i < items.Count; i++)
        {
            var god1 = items[i];

            for (int j = i+1; j < items.Count; j++)
            {
                var god2 = items[j];

                int powerDifference = god1.currentPower.Value - god2.currentPower.Value;
                CalculateDifference(powerDifference, god1, god2);
            }
        }

        strongestGods.OrderByDescending(x=>x.currentPower.Value);
        weakestGods.OrderByDescending(x=>x.currentPower.Value);
        neutralGods = items.Except(strongestGods.Union(weakestGods).ToList()).ToList();
    }

    private void CalculateDifference(int powerDifference, S_God god1, S_God god2)
    {
        if (Mathf.Abs(powerDifference) > PowerGap)
        {
            if (powerDifference > 0)
            {
                //god1 is strong, god2 is weak
                if (!strongestGods.Contains(god1))
                    strongestGods.Add(god1);

                if (!weakestGods.Contains(god2))
                    weakestGods.Add(god2);
            }
            else if (powerDifference < 0)
            {
                //god2 is strong, god1 is weak
                if (!strongestGods.Contains(god2))
                    strongestGods.Add(god2);

                if (!weakestGods.Contains(god1))
                    weakestGods.Add(god1);
            }
        }
    }

    public void ActivateGodEffects()
    {
        CalculateGap();


        if(strongestGods.Count != 0)
        {
            foreach (S_God god in strongestGods)
            {
                god.godStrong.Raise();
            }
        }

        if (weakestGods.Count != 0)
        {
            foreach (S_God god in weakestGods)
            {
                god.godWeak.Raise();
            }
        }

        if (neutralGods.Count != 0)
        {
            foreach (S_God god in neutralGods)
            {
                god.godDefault.Raise();
            }
        }


    }

    public int GetDifferenceBetweenFirstTwo()
    {
        if (strongestGods == null && weakestGods == null)
            return 0;
        else if (strongestGods.Count == 0 && weakestGods.Count == 0)
            return 0;

        var rankedList = items.OrderByDescending(x => x.currentPower.Value).ToList();

        int diff = rankedList[0].currentPower.Value - rankedList[1].currentPower.Value;

        return diff;
    }

    public int GetMaxDifference()
    {
        if (strongestGods == null && weakestGods == null)
            return 0;
        else if (strongestGods.Count == 0 && weakestGods.Count == 0)
            return 0;

        var rankedList = items.OrderByDescending(x => x.currentPower.Value).ToList();

        int maxDiff = 0;
        for (int i = 1; i < rankedList.Count; i++)
        {
            int diff = rankedList[i - 1].currentPower.Value - rankedList[i].currentPower.Value;
            if(diff > maxDiff)
            {
                maxDiff = diff;
            }
        }

        return maxDiff;
    }

    public string GetLeadGod()
    {
        if (strongestGods == null && weakestGods == null)
            return "";
        else if (strongestGods.Count == 0 && weakestGods.Count == 0)
            return "";

        var rankedList = items.OrderByDescending(x => x.currentPower.Value).ToList();
        return rankedList[0].name;
    }

    public int GetGodRanking(S_God god)
    {
        if (strongestGods == null && weakestGods == null)
            return 0;
        else if (strongestGods.Count == 0 && weakestGods.Count == 0)
            return 0;

        var rankedList = items.OrderByDescending(x => x.currentPower.Value).ToList();
        return rankedList.IndexOf(god);
    }

    //public S_God GetStrongestGod()
    //{
    //    if (items.Count == 0)
    //        return null;

    //    var maxPower = items.Max(y => y.currentPower.Value);
    //    var god = items.Find(x => x.currentPower.Value == maxPower);
    //    return god;
    //}

    //public S_God GetWeakestGod()
    //{
    //    if (items.Count == 0)
    //        return null;

    //    var minPower = items.Min(y => y.currentPower.Value);
    //    var god = items.Find(x => x.currentPower.Value == minPower);
    //    return god;
    //}
}
