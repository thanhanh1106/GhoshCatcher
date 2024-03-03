
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PopupGDPR : UICanvas
{
    [SerializeField] private Text txtName;
    [SerializeField] private RawImage icon;

    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);
        // load đoạn chữ cảm ơn vì đã chơi game ... lên 
        if (IsShow)
        {
            txtName.text = string.Format("Thanks for playing\n{0}", RocketConfig.ProductName);
            //icon.texture = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Unknown)[0];
        }
    }

    public void OnClickAgree()
    {
        PlayerPrefs.SetInt("agreeGDPR", 1);

        Show(false);
    }

    public void OnClikNoAgree()
    {
        PlayerPrefs.SetInt("agreeGDPR", 2);

        Show(false);
    }

    public bool IsChecked()
    {
        // biến int ở đây thực chất đại diện cho biến bool
        if (PlayerPrefs.GetInt("showGDPR", 0) == 1)
        {
            return true;
        }
        else
        {
            // nếu chưa show popup này lần nào( tức là người chơi lần đầu bật game lên)
            PlayerPrefs.SetInt("showGDPR", 1);
            Show(true);
        }

        return false;
    }
}
