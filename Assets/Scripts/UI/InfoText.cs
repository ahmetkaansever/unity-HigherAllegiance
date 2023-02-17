using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InfoText : MonoBehaviour
{
    TMP_Text scoreText;
    int totalLight;
    int totalMedium;
    int totalHeavy;
    int totalStatic;
    
    int shotLight;
    int shotMedium;
    int shotHeavy;
    int shotTank;
    int shotStatic;

    int lightScore;
    int mediumScore;
    int heavyScore;
    int tankScore;
    int staticScore;
    int hitScore;
    int totalScore;

    static int lightWorth = 1000;
    static int mediumWorth = 2000;
    static int heavyWorth = 3000;
    static int tankWorth = 1500;
    static int staticWorth = 2500;

    int prevSceneIndex;
    void Start()
    {
        prevSceneIndex = LevelFinished.getSceneIndex();
        
        totalLight = LevelFinished.enemyLightNo;
        totalMedium = LevelFinished.enemyMediumNo;
        totalHeavy = LevelFinished.enemyHeavyNo;
        totalStatic = LevelFinished.enemyStaticNo;

        shotLight = LevelFinished.getLight();
        shotMedium = LevelFinished.getMedium();
        shotHeavy = LevelFinished.getHeavy();
        shotTank = LevelFinished.getTank();
        shotStatic = LevelFinished.getStatic();
        

        lightScore = shotLight * lightWorth;
        mediumScore = shotMedium * mediumWorth;
        heavyScore = shotHeavy * heavyWorth;
        tankScore = shotTank * tankWorth;
        staticScore = shotStatic * staticWorth;
        totalScore = LevelFinished.getScore();
        hitScore = totalScore - lightScore - mediumScore - heavyScore - tankScore - staticScore;
        

        scoreText = GetComponent<TMP_Text>();
        scoreText.text = string.Format("{0,-30}       {1, -15}  {2,20}\n", $"Fighter plane destroyed:", $"{shotLight} / {totalLight}", $"Score: {lightScore}") +
                         string.Format("{0,-30}       {1, -15}  {2,20}\n", $"Attacker plane destroyed:", $"{shotMedium} / {totalMedium}", $"Score: {mediumScore}") +
                         string.Format("{0,-30}       {1, -15}  {2,20}\n", $"Bomber plane destroyed:", $"{shotHeavy} / {totalHeavy}", $"Score: {heavyScore}") +
                         string.Format("{0,-30}       {1, -15}  {2,20}\n", $"Buildings destroyed:", $"{shotStatic} / {totalStatic}", $"Score: {staticScore}") +
                         string.Format("{0,-30}       {1, -15}  {2,20}\n", $"Tank destroyed:", $"{shotTank}", $"Score: {tankScore}") +
                         string.Format("{0, 73}\n", $"Hit Score: {hitScore}")+
                         string.Format("{0, 73}", $"Total Score: {totalScore}");


        LevelFinished.reset();
    }

    
}
