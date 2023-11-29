using Configs;

namespace GameCore.Services
{
	public interface IConfigService : IService
	{
		public AssetServiceConfig AssetServiceConfig { get; }
	}
}