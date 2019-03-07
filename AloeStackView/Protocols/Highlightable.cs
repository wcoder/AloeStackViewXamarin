//
// Highlightable.cs
//
// C# port created by Yauheni Pakala
// Copyright (c) 2019
//
// Original Swift version created by Marli Oshlack
// Copyright 2018 Airbnb, Inc.
//
using AloeStackView.Views;
using UIKit;

namespace AloeStackView.Protocols
{
    /// <summary>
    /// Indicates that a row in an `AloeStackView` should be highlighted when the user touches it.
    /// 
    /// Rows that are added to an `AloeStackView` can conform to this protocol to have their
    /// background color automatically change to a highlighted color (or some other custom behavior defined by the row)
    /// when the user is pressing down on them.
    /// </summary>
    public interface Highlightable
    {
        /// <summary>
        /// Checked when the user touches down on a row to determine if the row should be highlighted.
        /// </summary>
        /// <value>The default implementation of this method always returns `true`.
        /// (use HighlightableExtesions)</value>
        bool isHighlightable { get; }

        /// <summary>
        /// Called when the highlighted state of the row changes.
        /// Override this method to provide custom highlighting behavior for the row.
        /// </summary>
        /// <value>The default implementation of this method changes the background color of the row to the `rowHighlightColor`.
        /// (use HighlightableExtesions)</value>
        void setIsHighlighted(bool isHighlighted);
    }

    public static class HighlightableExtesions
    {
        public static bool isHighlightable => true;

        public static void setIsHighlighted(this UIView view, bool isHighlighted)
        {
            if (view.Superview is StackViewCell cell)
            {
                cell.BackgroundColor = isHighlighted ? cell.rowHighlightColor : cell.rowBackgroundColor;
            }
        }
    }
}
