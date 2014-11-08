using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UI
{
    public abstract class IClickable
    {
        public abstract event OnClick clicked;

        public delegate void OnClick(object sender, EventArgs args);
    }
}
