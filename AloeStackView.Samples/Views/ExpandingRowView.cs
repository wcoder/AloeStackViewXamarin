//
// ExpandingRowView.cs
//
// C# port created by Yauheni Pakala
// Copyright (c) 2019
//
// Original Swift version created by Marli Oshlack
// Copyright 2018 Airbnb, Inc.
//
using System;
using AloeStackView.Protocols;
using UIKit;
using CoreGraphics;
using Foundation;

namespace AloeStackView.Samples.Views
{
    public class ExpandingRowView : UIStackView, Tappable, Highlightable
    {
        private UILabel titleLabel = new UILabel();
        private UILabel showMoreLabel = new UILabel();
        private UILabel textLabel = new UILabel();

        private int nextLine = 1;

        public ExpandingRowView() : base(CGRect.Empty)
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
            setUpViews();
        }

        public ExpandingRowView(NSCoder coder) => throw new NotImplementedException("ctor(coder:) has not been implemented");

        private void setUpViews()
        {
            setUpSelf();
            setUpTitleLabel();
            setUpShowMoreLabel();
            setUpTextLabel();
        }

        private void setUpSelf()
        {
            Axis = UILayoutConstraintAxis.Vertical;
            Spacing = 4;
        }

        private void setUpTitleLabel()
        {
            titleLabel.Text = "Dynamically change row content";
            titleLabel.Font = UIFont.PreferredBody;
            AddArrangedSubview(titleLabel);
        }

        private void setUpShowMoreLabel()
        {
            showMoreLabel.Lines = 0;
            showMoreLabel.Text = "(Tap on this row to add more content!)\n";
            showMoreLabel.Font = UIFont.PreferredCaption2;
            showMoreLabel.TextColor = UIColor.Blue;
            AddArrangedSubview(showMoreLabel);
        }

        private void setUpTextLabel()
        {
            textLabel.Lines = 0;
            textLabel.Font = UIFont.PreferredCaption2;
            textLabel.Text = lines[0];
            AddArrangedSubview(textLabel);
        }

        public bool isHighlightable => HighlightableExtesions.isHighlightable;

        public void didTapView()
        {
            textLabel.Text = (textLabel.Text ?? "") + lines[nextLine];
            nextLine += 1;
            if (nextLine == lines.Length)
            {
                nextLine = 0;
            }
        }

        public void setIsHighlighted(bool isHighlighted) => HighlightableExtesions.setIsHighlighted(this, isHighlighted);

        private string[] lines =
        {
            "Two households, both alike in dignity,",
            "\nIn fair Verona, where we lay our scene,",
            "\nFrom ancient grudge break to new mutiny,",
            "\nWhere civil blood makes civil hands unclean.",
            "\nFrom forth the fatal loins of these two foes",
            "\nA pair of star-cross'd lovers take their life;",
            "\nWhose misadventured piteous overthrows",
            "\nDo with their death bury their parents' strife.",
            "\nThe fearful passage of their death-mark'd love,",
            "\nAnd the continuance of their parents' rage,",
            "\nWhich, but their children's end, nought could remove,",
            "\nIs now the two hours' traffic of our stage;",
            "\nThe which if you with patient ears attend,",
            "\nWhat here shall miss, our toil shall strive to mend."
        };
    }
}
