using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhyNsStreamWatcher
{
    class myComboBoxItem
    {

        public string Text { get; set; }
        public object Value { get; set; }


        public myComboBoxItem() { }

        public myComboBoxItem(string Text, object Value) {

            this.Text = Text;
            this.Value = Value;

        }



        public override string ToString()
        {
            return Text;
        }
        
    }
}
