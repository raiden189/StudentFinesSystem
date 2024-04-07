using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.Service
{
    public class CustomPopupService : ICustomPopupService
    {
        private Page page;
        private Popup popup;
        private bool isShow = false;

        public bool IsShow()
        { 
            return isShow;
        }
        public void Init(Page page)
        {
            this.page = page;
            this.popup = new Popup();
        }

        public void Init(Page page, Popup popup)
        {
            this.page = page;
            this.popup = popup;
        }

        public void Show(Popup popup)
        {
            isShow = true;
            if (page == null)
                page = App.Current?.MainPage ?? throw new NullReferenceException();

            page.ShowPopup(popup);
        }

        public void Show()
        {
            isShow = true;
            if (page == null)
                page = App.Current?.MainPage ?? throw new NullReferenceException();

            page.ShowPopup(this.popup);
        }

        public void Close(Popup popup)
        {
            isShow = false;
            popup.Close();
        }

        public void Close()
        {
            isShow = false;
            popup.Close();
        }
    }
}
