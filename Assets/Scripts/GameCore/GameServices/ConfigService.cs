﻿using System.Threading.Tasks;
using Configs;
using UnityEngine;

namespace GameCore.GameServices
{
	public sealed class ConfigService : IConfigService
	{
		public AssetServiceConfig AssetServiceConfig { get; private set; }

		public async Task Init()
		{
			AssetServiceConfig = await LoadConfig<AssetServiceConfig>();

			await Task.CompletedTask;
		}

		private async Task<TConfig> LoadConfig<TConfig>() where TConfig : Object, IConfig
		{
			ResourceRequest request = Resources.LoadAsync<TConfig>(GetConfigPath<TConfig>());

			while (!request.isDone)
				await Task.Yield();

			return request.asset as TConfig;
		}

		private static string GetConfigPath<TConfig>() =>
			typeof(TConfig).ToString().Replace('.', '/');
	}
}