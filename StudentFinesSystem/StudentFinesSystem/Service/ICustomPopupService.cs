using CommunityToolkit.Maui.Views;

namespace StudentFinesSystem.Service
{
    public interface ICustomPopupService
    {
        void Init(Page page);
        void Init(Page page, Popup popup);
        void Show(Popup popup);
        void Show();
        void Close(Popup popup);
        void Close();

        bool IsShow();
    }
}