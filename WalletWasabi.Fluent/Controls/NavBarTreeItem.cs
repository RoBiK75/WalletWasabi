﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;

namespace WalletWasabi.Fluent.Controls
{
	public class NavBarTreeItem : TreeViewItem
	{
		public static readonly StyledProperty<IconElement> IconProperty =
			AvaloniaProperty.Register<NavBarTreeItem, IconElement>(nameof(Icon));		

		public IconElement Icon
		{
			get => GetValue(IconProperty);
			set => SetValue(IconProperty, value);
		}

		protected override IItemContainerGenerator CreateItemContainerGenerator()
		{
			return new TreeItemContainerGenerator<NavBarTreeItem>(
				this,
				TreeViewItem.HeaderProperty,
				TreeViewItem.ItemTemplateProperty,
				TreeViewItem.ItemsProperty,
				TreeViewItem.IsExpandedProperty);
		}
	}
}
