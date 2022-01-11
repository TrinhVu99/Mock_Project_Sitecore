using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
namespace MockProject.Foundation.SitecoreExtensions.Comparers
{
    public class ItemEqualityComparer : IEqualityComparer<Item>
    {
        public bool Equals(Item x, Item y)
        {
            return x.ID.Guid == y.ID.Guid;
        }

        public int GetHashCode(Item obj)
        {
            return obj.ID.Guid.GetHashCode();
        }
    }
}