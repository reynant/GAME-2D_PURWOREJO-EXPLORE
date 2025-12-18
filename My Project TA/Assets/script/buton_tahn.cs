using UnityEngine;
using UnityEngine.EventSystems;

public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { Left, Right }
    public ButtonType buttonType;
    public PlayerControllerMobile player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonType == ButtonType.Left)
            player.SetMoveDirection(-1);
        else if (buttonType == ButtonType.Right)
            player.SetMoveDirection(1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.SetMoveDirection(0);
    }
}
