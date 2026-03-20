public interface IInteractable
{
    void OnFocus();      // Oyuncu objeye baktýðýnda (Glow açmak için)
    void OnLoseFocus();  // Oyuncu bakmayý býraktýðýnda (Glow kapatmak için)
    void Interact();     // Oyuncu 'E' tuþuna bastýðýnda (Toplamak için)
}