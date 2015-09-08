using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MultiplicationTable
{
    class MultiButton : Button
    {
        public int Number { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public MultiButton(int number)
        {
            Number = number;
            //base.Content = Number.ToString();
            base.FontSize = 24;
        }
    }
}
