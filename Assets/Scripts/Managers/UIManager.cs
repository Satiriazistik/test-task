using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Cached Links")]
    public Canvas EndGameCanvas;

    public Text HeaderText;

    public Text[] EnemyInfoTexts;

    [Header("End Game Settings")]

    public Color LoseTextColor;
    public Color WinTextColor;

    public string WinHeaderText,
                  LoseHeaderText;

    public string DetectedEnemyText,
                  UndetectedEnemyText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowEndGameInfo(bool isWin, List<Enemy> enemies)
    {
        var headerText = isWin
            ? WinHeaderText
            : LoseHeaderText;

        var headerColor = isWin
            ? WinTextColor
            : LoseTextColor;

        HeaderText.text = headerText;
        HeaderText.color = headerColor;

        if (EnemyInfoTexts.Length != enemies.Count)
            Debug.LogErrorFormat("Number of text fields with enemy info different from number of enemies.");

        for (int i = 0; i < enemies.Count; i++)
        {
            if (i >= EnemyInfoTexts.Length)
                break;

            var enemyDetectedText = enemies[i].DetectorTarget.FullDetected
                ? DetectedEnemyText
                : UndetectedEnemyText;

            var enemyDetectedTextColor = enemies[i].DetectorTarget.FullDetected
                ? WinTextColor
                : LoseTextColor;

            EnemyInfoTexts[i].text = enemies[i].name + enemyDetectedText;
            EnemyInfoTexts[i].color = enemyDetectedTextColor;
        }

        EndGameCanvas.gameObject.SetActive(true);
    }

}
