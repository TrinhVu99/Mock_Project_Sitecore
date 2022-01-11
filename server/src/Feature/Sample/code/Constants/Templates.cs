using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockProject.Feature.Sample.Constants
{
    public class Templates
    {
		public static class Advertisement
		{
			public const string TemplateId = "{A7965570-0D13-45B9-8EB2-02581041E97A}";
			public static class Fields
			{
				public const string Items = "{D649033A-D715-4A14-9179-C44EA17EF6B3}";
				public const string Time = "{EEE0E408-4FDD-4E0F-A1B5-9ADF7D3C2822}";
			}

		}
		public static class AdvertisementImage
		{
			public const string TemplateId = "{67EB969F-9865-4E12-9AE6-726D8DD7F0A2}";
			public static class Fields
			{
				public const string Image = "{00DA2E82-BC53-4F41-B633-F9E5A2F1F48A}";
				public const string Link = "{AA235530-DF44-45A9-87F7-FB6154126EA6}";
			}

		}
	}
}