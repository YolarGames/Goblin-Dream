using Configs;

namespace GameCore.GameServices
{
	public interface IConfigService : IService
	{
		public AssetServiceConfig AssetServiceConfig { get; }
	}
}