using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhyNsStreamWatcher
{
    abstract class Page
    {

        protected UI ui;

        protected string name = "No Name";
        public string Name
        {
            get
            {
                return name;
            }

        }

        public Page(UI ui)
        {
            this.ui = ui;

        }



        abstract public void ShowPage();
        abstract public void ClosePage();

    }
}
