using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSplicer.Models
{
    class MenuDrawerItem
    {
        string mIcon;
        string mName;

        public MenuDrawerItem(string Icon, string Name)
        {
            this.mIcon = Icon;
            this.mName = Name;
        }

        public string Icon
        {
            get { return mIcon;  }
            set { this.mIcon = value;  }
        }

        public string Name
        {
            get { return mName; }
            set { this.mName = value; }
        }
    }
}
