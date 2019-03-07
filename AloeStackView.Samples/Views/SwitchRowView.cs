//
// SwitchRowView.cs
//
// C# port created by Yauheni Pakala
// Copyright (c) 2019
//
// Original Swift version created by Marli Oshlack
// Copyright 2018 Airbnb, Inc.
//
using UIKit;
using CoreGraphics;
using System;
using Foundation;

namespace AloeStackView.Samples.Views
{
    public class SwitchRowView : UIView
    {
        private UILabel label = new UILabel();
        private UISwitch switchView = new UISwitch(CGRect.Empty);

        public SwitchRowView() : base(CGRect.Empty)
        {
            setUpViews();
            setUpConstraints();
        }

        public SwitchRowView(NSCoder coder) => throw new NotImplementedException("ctor(coder:) has not been implemented");

        public event EventHandler<bool> SwitchChanged;

        public string Text
        { 
            get => label.Text;
            set => label.Text = value;
        }

        private void setUpViews()
        {
            setUpLabel();
            setUpSwitchView();
        }

        private void setUpLabel()
        {
            label.TranslatesAutoresizingMaskIntoConstraints = false;
            label.Font = UIFont.PreferredBody;
            AddSubview(label);
        }

        private void setUpSwitchView()
        {
            switchView.TranslatesAutoresizingMaskIntoConstraints = false;
            switchView.ValueChanged += (sender, e) =>
            {
                SwitchChanged?.Invoke(sender, switchView.On);
            };
            AddSubview(switchView);
        }

        private void setUpConstraints()
        {
            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[]
            { 
                label.TopAnchor.ConstraintEqualTo(TopAnchor),
                label.BottomAnchor.ConstraintEqualTo(BottomAnchor),
                label.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
                switchView.LeadingAnchor.ConstraintEqualTo(label.TrailingAnchor, 8),
                switchView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor, 8),
                switchView.CenterYAnchor.ConstraintEqualTo(CenterYAnchor)
            });
        }
    }
}
