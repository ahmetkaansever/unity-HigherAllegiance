using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelFinished : MonoBehaviour
{
    [SerializeField] public static int enemyLightNo;
    [SerializeField] public static int enemyMediumNo;
    [SerializeField] public static int enemyHeavyNo;
    [SerializeField] public static int enemyStaticNo;
    private static int shotEnemyLightNo;
    private static int shotEnemyMediumNo;
    private static int shotEnemyHeavyNo;
    private static int shotTankNo;
    private static int shotStaticNo;
    private static int totalScore;

    private static int sceneID;

    private void Start()
    {
        
    }
    public void ProcessLevelEnd()
    {
        totalScore = ScoreBoard.score;
        sceneID = SceneManager.GetActiveScene().buildIndex;
        Debug.Log($"Light Enemy Shot: {shotEnemyLightNo}/{enemyLightNo}");
        Debug.Log($"Medium Enemy Shot: {shotEnemyMediumNo}/{enemyMediumNo}");
        Debug.Log($"Heavy Enemy Shot: {shotEnemyHeavyNo}/{enemyHeavyNo}");
        Debug.Log($"Tank Shot: {shotTankNo}");
        Debug.Log($"Static Shot: {shotStaticNo}");
        Invoke("LoadEndLevelScene", 2f);
    }

    private void LoadEndLevelScene()
    {
        SceneManager.LoadScene(1);
    }
    public static void reset(){
        enemyLightNo = 0;
        enemyMediumNo = 0;
        enemyHeavyNo = 0;
        enemyStaticNo = 0;
        shotEnemyHeavyNo = 0;
        shotEnemyLightNo = 0;
        shotEnemyMediumNo = 0;
        shotTankNo = 0;
        shotStaticNo = 0;
    }

    public static int getSceneIndex(){
        return sceneID;
    }

    public void incrementLight(){
        shotEnemyLightNo++;
    }

    public void incrementMedium(){
        shotEnemyMediumNo++;
    }

    public void incrementHeavy(){
        shotEnemyHeavyNo++;
    }

    public void incrementTank(){
        shotTankNo++;
    }

    public void incrementStatic(){
        shotStaticNo++;
    }

    //Getters
    public static int getLight(){
        return shotEnemyLightNo;
    }

    public static int getMedium(){
        return shotEnemyMediumNo;
    }

    public static int getHeavy(){
        return shotEnemyHeavyNo;
    }

    public static int getTank(){
        return shotTankNo;
    }

    public static int getStatic(){
        return shotStaticNo;
    }

    public static int getScore(){
        return totalScore;
    }
}
