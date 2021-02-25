using System;
using System.Linq;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using WalletWasabi.Wallets;

namespace WalletWasabi.Fluent.Converters
{
	public static class WalletIconConverter
	{
		public static readonly IMultiValueConverter TypesToImage =
			new FuncMultiValueConverter<WalletType, Bitmap>(parts =>
			{
				var inputs = parts.ToArray();
				var type = inputs[0] == WalletType.Unknown ? inputs[1] : inputs[0];
				return GetBitmap(type);
			});

		public static readonly IValueConverter WalletTypeToImage =
			new FuncValueConverter<WalletType, Bitmap>(GetBitmap);

		public static readonly IValueConverter StringToImage =
			new FuncValueConverter<string?, Bitmap>(icon =>
			{
				var type = GetWalletType(icon);
				return GetBitmap(type);
			});

		public static readonly IValueConverter BoolToType =
			new FuncValueConverter<bool, WalletType>(x => x ? WalletType.Hardware : WalletType.Normal);

		public static readonly IValueConverter StringToType =
			new FuncValueConverter<string?, WalletType>(x => x is { } ? GetWalletType(x) : WalletType.Unknown);

		private static WalletType GetWalletType(string? icon)
		{
			return Enum.TryParse(typeof(WalletType), icon, true, out var typ) && typ is { }
				? (WalletType) typ
				: WalletType.Normal;
		}

		private static Bitmap GetBitmap(WalletType type)
		{
			Uri uri = new("avares://WalletWasabi.Fluent/Assets/HardwareIcons/generic.png");

			switch (type)
			{
				case WalletType.Coldcard:
					uri = new("avares://WalletWasabi.Fluent/Assets/HardwareIcons/coldcard.png");
					break;

				case WalletType.Trezor:
					uri = new("avares://WalletWasabi.Fluent/Assets/HardwareIcons/trezor.png");
					break;

				case WalletType.Ledger:
					uri = new("avares://WalletWasabi.Fluent/Assets/HardwareIcons/ledger.png");
					break;

				case WalletType.Normal:
				case WalletType.Unknown:
					uri = new("avares://WalletWasabi.Fluent/Assets/HardwareIcons/normal.png");
					break;
			}

			var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

			using var image = assets.Open(uri);
			return new Bitmap(image);
		}
	}
}