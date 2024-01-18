﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using System;
using Avalonia.Input;

namespace UVtools.UI.Controls;

public class SplitButtonWithIcon : SplitButton
{
    protected override Type StyleKeyOverride => typeof(SplitButton);
    
    public static readonly StyledProperty<bool> OpenFlyoutWithRightClickProperty =
        AvaloniaProperty.Register<SplitButtonWithIcon, bool>(nameof(OpenFlyoutWithRightClick), defaultValue: true);

    public bool OpenFlyoutWithRightClick
    {
        get => GetValue(OpenFlyoutWithRightClickProperty);
        set => SetValue(OpenFlyoutWithRightClickProperty, value);
    }

    public static readonly StyledProperty<string?> TextProperty =
        ButtonWithIcon.TextProperty.AddOwner<SplitButtonWithIcon>();

    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly StyledProperty<ButtonWithIcon.IconPlacementType> IconPlacementProperty =
        ButtonWithIcon.IconPlacementProperty.AddOwner<SplitButtonWithIcon>();

    public ButtonWithIcon.IconPlacementType IconPlacement
    {
        get => GetValue(IconPlacementProperty);
        set => SetValue(IconPlacementProperty, value);
    }

    public static readonly StyledProperty<string?> IconProperty =
        ButtonWithIcon.IconProperty.AddOwner<SplitButtonWithIcon>();

    public string? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<double> SpacingProperty =
        ButtonWithIcon.SpacingProperty.AddOwner<SplitButtonWithIcon>();

    public double Spacing
    {
        get => GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public SplitButtonWithIcon()
    {
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (ReferenceEquals(e.Property, TextProperty)
            || ReferenceEquals(e.Property, IconProperty)
            || ReferenceEquals(e.Property, IconPlacementProperty))
        {
            RebuildContent();
        }
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        if (e.InitialPressMouseButton == MouseButton.Right && OpenFlyoutWithRightClick)
        {
            OpenFlyout();
        }
    }

    public Projektanker.Icons.Avalonia.Icon MakeIcon()
    {
        return new Projektanker.Icons.Avalonia.Icon { Value = Icon };
    }

    private void RebuildContent()
    {
        if (string.IsNullOrWhiteSpace(Icon))
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                Content = Text;
            }
            return;
        }

        if (string.IsNullOrWhiteSpace(Text))
        {
            if (!string.IsNullOrWhiteSpace(Icon))
            {
                Content = MakeIcon();
            }

            return;
        }

        var panel = new StackPanel
        {
            Spacing = Spacing,
            VerticalAlignment = VerticalAlignment.Stretch,
            Orientation = IconPlacement is ButtonWithIcon.IconPlacementType.Left or ButtonWithIcon.IconPlacementType.Right
                ? Orientation.Horizontal
                : Orientation.Vertical
        };

        if (IconPlacement is ButtonWithIcon.IconPlacementType.Left or ButtonWithIcon.IconPlacementType.Top) panel.Children.Add(MakeIcon());
        panel.Children.Add(new TextBlock { VerticalAlignment = VerticalAlignment.Center, Text = Text });
        if (IconPlacement is ButtonWithIcon.IconPlacementType.Right or ButtonWithIcon.IconPlacementType.Bottom) panel.Children.Add(MakeIcon());

        Content = panel;
    }
}