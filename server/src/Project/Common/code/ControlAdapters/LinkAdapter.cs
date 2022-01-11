using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Sitecore.Shell.Applications.ContentEditor;

namespace Project.Common.ControlAdapters
{
    /// <summary>
    /// Control adapter that adds a "Go to target" link to link fields in the content editor.
    /// This link can be used, even if the item is locked/not editable.
    /// </summary>
    public class LinkAdapter :  ControlAdapter
    {
        protected override void Render(HtmlTextWriter writer)
        {
            bool lMakeLink = false;

            using (HtmlAnchor lLink = new HtmlAnchor())
            {
                lLink.HRef = "#";
                if (this.Control is Link)
                {
                    lLink.Attributes.Add("onclick", Sitecore.Context.ClientPage.GetClientEvent(string.Format("contentlink:follow(id={0})", this.Control.ClientID)));
                    lMakeLink = true;
                }
                else if (this.Control is InternalLink)
                {
                    string lValuePath = ((InternalLink)this.Control).Value;
                    if (!string.IsNullOrEmpty(lValuePath))
                    {
                        Sitecore.Data.Items.Item lLinkedItem = Sitecore.Context.Database.GetItem(lValuePath);
                        if (lLinkedItem != null)
                        {
                            lLink.Attributes.Add("onclick", Sitecore.Context.ClientPage.GetClientEvent(string.Format("item:load(id={0})", lLinkedItem.ID)));
                            lMakeLink = true;
                        }
                    }
                }
                else
                {
                    // don't ask me why i occured. although don't need to use elseif above
                }
                lLink.Controls.Add(new LiteralControl("Go to target"));

                base.Render(writer);
                if (lMakeLink)
                {
                    lLink.RenderControl(writer);
                }
            }
                
        }
    }
}
