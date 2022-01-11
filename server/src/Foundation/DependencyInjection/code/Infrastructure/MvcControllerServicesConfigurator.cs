namespace MockProject.Foundation.DependencyInjection.Infrastructure
{
	using Microsoft.Extensions.DependencyInjection;
	using Sitecore.DependencyInjection;

	public class MvcControllerServicesConfigurator : IServicesConfigurator
	{
		private const string HelixProjectPrefix = "MockProject";

		public void Configure(IServiceCollection serviceCollection)
		{
			serviceCollection.AddControllers($"{HelixProjectPrefix}.Feature.*");
			serviceCollection.AddClassesWithServiceAttribute($"{HelixProjectPrefix}.Feature.*");
			serviceCollection.AddClassesWithServiceAttribute($"{HelixProjectPrefix}.Foundation.*");
		}
	}
}