using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MockProject.Foundation.DependencyInjection
{
	public enum Lifetime
	{
		Transient,
		Scoped,
		Singleton
	}
}