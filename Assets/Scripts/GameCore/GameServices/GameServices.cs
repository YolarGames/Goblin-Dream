namespace GameCore.GameServices
{
	public class GameServices
	{
		public static IConfigService ConfigService;
		public static IAssetService AssetService;
		public static IFactoryService FactoryService;

		public GameServices()
		{
			CreateServices();
		}

		private async void CreateServices()
		{
			ConfigService = new ConfigService();
			await ConfigService.Init();

			AssetService = new AssetService(ConfigService);
			await ConfigService.Init();

			FactoryService = new FactoryService(AssetService);
			await ConfigService.Init();
		}
	}
}