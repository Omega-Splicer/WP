using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace OmegaSplicer.Common
{
    class Notification
    {
        static public void UpdateTile(int nbr)
        {
            XmlDocument badgeDOM = new XmlDocument();
            badgeDOM.LoadXml(string.Format("<badge value='{0}'/>", nbr));
            BadgeNotification badge = new BadgeNotification(badgeDOM);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
        }
    }
}
