using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhyNsStreamWatcher
{
    class UI
    {



        // Notification Fields

        private List<NotificationForm> ActiveNotifications = new List<NotificationForm>();
                
        
        // Notification Methods

        public List<NotificationForm> NewActiveNotification(NotificationForm Notification)
        {

            ActiveNotifications.Add(Notification);

            //MessageBox.Show("New Active Notification");

            return ActiveNotifications;

        }

        public void RemoveActiveNotification(NotificationForm Notification)
        {

            ActiveNotifications.Remove(Notification);                   
        
        }

        // Page Fields

        private Page MainPage = null;
        private Page ActivePage = null;
        private Page LastActivePage = null;

        // Page Methods

        public void SetMainPage(Page MainPage)
        {

            this.MainPage = MainPage;

        }

        public void ShowPage(Page pageToShow)
        {

            if (ActivePage != pageToShow)
            {

                if (ActivePage != null)
                {

                    ActivePage.ClosePage();
                    LastActivePage = ActivePage;

                }

                pageToShow.ShowPage();
                ActivePage = pageToShow;

            }

        }

        public void ClosePage(Page pageToClose)
        {

            if (MainPage != null)
            {

                MainPage.ShowPage();
                ActivePage = MainPage;

            }
            else
            {

                if (LastActivePage != null)
                {

                    LastActivePage.ShowPage();
                    ActivePage = LastActivePage;
                }


            }

            pageToClose.ClosePage();
            LastActivePage = pageToClose;
        }


    }
}
