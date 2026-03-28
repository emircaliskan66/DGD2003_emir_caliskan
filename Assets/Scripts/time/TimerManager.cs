using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Header("Timer Settings")]
    public float startingTime = 60f;
    private float currentTime;
    private bool isTimerRunning = false;

    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public Image bloodOverlay;
    public float dangerTime = 15f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentTime = startingTime;
        isTimerRunning = true;

        if (bloodOverlay != null)
        {
            bloodOverlay.color = new Color(1, 0, 0, 0);
        }
    }

    void Update()
    {
        // Eðer sayaç çalýþmýyorsa (oyun kazanýldýysa veya bittiyse) aþaðýdakileri HÝÇ YAPMA
        if (!isTimerRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isTimerRunning = false;
            GameOver();
        }

        UpdateTimerUI();
        HandleDangerEffect();
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime <= 10f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }
    }

    void HandleDangerEffect()
    {
        if (bloodOverlay == null) return;

        // DÜZELTME BURADA: Sadece sayaç ÇALIÞIYORSA ve tehlike sýnýrýndaysa kýzar
        if (isTimerRunning && currentTime <= dangerTime && currentTime > 0)
        {
            float alpha = (Mathf.Sin(Time.time * 5f) + 1f) / 5f;
            bloodOverlay.color = new Color(1, 0, 0, alpha);
        }
        else
        {
            // Deðilse (yani tehlikede deðilsek veya oyunu kazandýysak) kýrmýzý ekraný sil
            bloodOverlay.color = new Color(1, 0, 0, 0);
        }
    }

    public void AddTime(float bonusTime)
    {
        // Sadece oyun devam ediyorsa süre eklenebilsin
        if (isTimerRunning && bonusTime > 0)
        {
            currentTime += bonusTime;
            Debug.Log("Zaman kazanildi: +" + bonusTime + " saniye! Yeni sure: " + currentTime);
        }
    }

    // OYUN KAZANILDIÐINDA ÇAÐRILAN FONKSÝYON
    public void StopTimer()
    {
        isTimerRunning = false; // Sayacý durdur

        // Kýrmýzý tehlike ekranýný anýnda temizle
        if (bloodOverlay != null)
        {
            bloodOverlay.color = new Color(1, 0, 0, 0);
        }

        // Sayacýn rengini tekrar beyaza çevir (eðer kýrmýzý kalmýþsa)
        if (timerText != null)
        {
            timerText.color = Color.white;
        }
    }

    void GameOver()
    {
        Debug.Log("Zaman Doldu! GAME OVER");
        timerText.text = "00:00";

        if (bloodOverlay != null) bloodOverlay.color = new Color(1, 0, 0, 0.6f);
    }
}