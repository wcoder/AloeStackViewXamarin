//
// Highlightable.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo@gmail.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
using AloeStackView.Views;
using UIKit;

namespace AloeStackView.Protocols
{
    public interface Highlightable
    {
        /// Checked when the user touches down on a row to determine if the row should be highlighted.
        ///
        /// The default implementation of this method always returns `true`.
        bool isHighlightable { get; }

        /// Called when the highlighted state of the row changes.
        /// Override this method to provide custom highlighting behavior for the row.
        ///
        /// The default implementation of this method changes the background color of the row to the `rowHighlightColor`.
        void setIsHighlighted(bool isHighlighted);
    }

    public static class HighlightableExtesions
    {
        // TODO:
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
