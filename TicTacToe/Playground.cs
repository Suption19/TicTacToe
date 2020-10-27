using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe
{
    public class Playground
    {
        private List<Button> _fields = new List<Button>();

        public Playground(List<Button> fields)
        {
            _fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }

        public List<Button> GetFields()
        {
            return _fields;
        }
    }
}