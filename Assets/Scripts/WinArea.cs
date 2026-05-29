using UnityEngine;

public class WinArea : MonoBehaviour
{
    [SerializeField] private PanelManager panelManager;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("OYUN KAZANILDI!");

            if (panelManager != null)
            {
                panelManager.OpenWinPanel();
            }
        }
    }
}