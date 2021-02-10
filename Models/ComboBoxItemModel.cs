using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStoc
{
    class ComboBoxItemModel
    {

        private string text { get; set; }
        private object value2 { get; set; }

        public string Text { get => text; set => text = value; }
        public object Value { get => value2; set => value2 = value; }

        public ComboBoxItemModel()
        {

        }

        public override string ToString()
        {
            return text;
        }




    }
}
