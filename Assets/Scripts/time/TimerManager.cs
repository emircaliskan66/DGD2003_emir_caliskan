using UnityEngine;
using TMPro;
using UnityEngine.UI; // Image bileþeni için gerekli

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Header("Timer Settings")]
    public float startingTime = 60f; // Baþlangýç süresi (saniye)
    private float currentTime;
    private bool isTimerRunning = false;

    [Header("UI References")]
    public TextMeshProUGUI timerText; // Ekranda süreyi gösterecek yazý
    public Image bloodOverlay; // Zaman azalýnca yanýp sönecek kýrmýzý ekran
    public float dangerTime = 15f; // Son kaç saniyede ekran kýzarsýn?

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentTime = startingTime;
        isTimerRunning = true;

        // Baþlangýçta kýrmýzý ekraný tamamen görünmez yap
        if (bloodOverlay != null)
        {
            bloodOverlay.color = new Color(1, 0, 0, 0);
        }
    }

    void Update()
    {
        if (!isTimerRunning) return;

        currentTime -= Time.deltaTime; // Süreyi geriye say

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

        // Süreyi "Dakika:Saniye" (Örn: 01:30) formatýna çevir
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Son 10 saniyede yazýnýn rengini kýrmýzý yap
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

        // Eðer süre tehlike sýnýrýna girdiyse
        if (currentTime <= dangerTime && currentTime > 0)
        {
            // Mathf.Sin kullanarak nefes alýp veren (pulsing) bir saydamlýk oluþtur (0 ile 0.4 arasý)
            float alpha = (Mathf.Sin(Time.time * 5f) + 1f) / 5f;
            bloodOverlay.color = new Color(1, 0, 0, alpha);
        }
        else if (currentTime > dangerTime)
        {
            bloodOverlay.color = new Color(1, 0, 0, 0);
        }
    }

    // Eþya toplandýðýnda çaðrýlacak fonksiyon
    public void AddTime(float bonusTime)
    {
        if (bonusTime > 0)
        {
            currentTime += bonusTime;
            Debug.Log("Zaman kazanildi: +" + bonusTime + " saniye! Yeni sure: " + currentTime);
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    void GameOver()
    {
        Debug.Log("Zaman Doldu! GAME OVER");
        timerText.text = "00:00";

        // Ekraný tam kýrmýzý yap
        if (bloodOverlay != null) bloodOverlay.color = new Color(1, 0, 0, 0.6f);

        // TODO: Karakterin hareketini kilitleme veya Yeniden Baþla menüsü açma kodlarý buraya gelecek.
    }
}