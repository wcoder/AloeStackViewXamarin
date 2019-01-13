//
// AloeStackView.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo@gmail.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
using System;
using AloeStackView.Protocols;
using AloeStackView.Views;
using CoreGraphics;
using Foundation;
using UIKit;
using System.Linq;
using System.Collections.Generic;

namespace AloeStackView
{
    /*
     * A simple class for laying out a collection of views with a convenient API, while leveraging the
     * power of Auto Layout.
     */
    public class AloeStackView : UIScrollView
    {
        #region Lifecycle

        public AloeStackView() : base(CGRect.Empty)
        {
            setUpViews();
            setUpConstraints();
        }

        public AloeStackView(NSCoder coder)
        {
            throw new NotImplementedException("init(coder:) has not been implemented");
        }

        #endregion

        #region Public

        #region Adding and Removing Rows

        /// Adds a row to the end of the stack view.
        ///
        /// If `animated` is `true`, the insertion is animated.
        public void addRow(UIView row, bool animated = false)
        {
            insertCell(row, stackView.ArrangedSubviews.Count(), animated);
        }

        /// Adds multiple rows to the end of the stack view.
        ///
        /// If `animated` is `true`, the insertions are animated.
        public void addRows(UIView[] rows, bool animated = false)
        {
            foreach (var row in rows)
            {
                addRow(row, animated);
            }
        }

        /// Adds a row to the beginning of the stack view.
        ///
        /// If `animated` is `true`, the insertion is animated.
        public void prependRow(UIView row, bool animated = false)
        {
            insertCell(row, 0, animated);
        }

        /// Adds multiple rows to the beginning of the stack view.
        ///
        /// If `animated` is `true`, the insertions are animated.
        //open func prependRows(_ rows: [UIView], animated: Bool = false)
        //{
        //    rows.reversed().forEach { prependRow($0, animated: animated) }
        //}

        /// Inserts a row above the specified row in the stack view.
        ///
        /// If `animated` is `true`, the insertion is animated.
        public void insertRowBefore(UIView row, UIView beforeRow, bool animated = false)
        {
            if (beforeRow.Superview is StackViewCell cell)
            {
                // TODO:
                var index = Array.IndexOf(stackView.ArrangedSubviews, cell);

                insertCell(row, index, animated);
            }
        }

        /// Inserts multiple rows above the specified row in the stack view.
        ///
        /// If `animated` is `true`, the insertions are animated.
        public void insertRowsBefore(UIView[] rows, UIView beforeRow, bool animated)
        {
            foreach (var row in rows)
            {
                insertRowBefore(row, beforeRow, animated);
            }
        }

        /// Inserts a row below the specified row in the stack view.
        ///
        /// If `animated` is `true`, the insertion is animated.
        public void insertRowAfter(UIView row, UIView afterRow, bool animated = false)
        {
            if (afterRow.Superview is StackViewCell cell)
            {
                // TODO:
                var index = Array.IndexOf(stackView.ArrangedSubviews, cell);

                insertCell(row, index + 1, animated);
            }
        }

        /// Inserts multiple rows below the specified row in the stack view.
        ///
        /// If `animated` is `true`, the insertions are animated.
        //  open func insertRows(_ rows: [UIView], after afterRow: UIView, animated: Bool = false)
        //  {
        //      _ = rows.reduce(afterRow) {
        //          currentAfterRow, row in
        //insertRow(row, after: currentAfterRow, animated: animated)
        //      return row
        //    }
        //}

        /// Removes the given row from the stack view.
        ///
        /// If `animated` is `true`, the removal is animated.
        public void removeRow(UIView row, bool animated = false)
        {
            if (row.Superview is StackViewCell cell)
            {
                removeCell(cell, animated);
            }
        }

        /// Removes the given rows from the stack view.
        ///
        /// If `animated` is `true`, the removals are animated.
        public void removeRows(UIView[] rows, bool animated = false)
        {
            foreach (var row in rows)
            {
                removeRow(row, animated);
            }
        }

        /// Removes all the rows in the stack view.
        ///
        /// If `animated` is `true`, the removals are animated.
        public void removeAllRows(bool animated = false)
        {
            foreach (var view in stackView.ArrangedSubviews)
            {
                if (view is StackViewCell cell)
                {
                    removeRow(cell.contentView, animated);
                }
            }
        }

        #endregion

        #region Accessing Rows

        /// Returns an array containing of all the rows in the stack view.
        ///
        /// The rows in the returned array are in the order they appear visually in the stack view.
        public UIView[] getAllRows()
        {
            var rows = new List<UIView>();

            foreach (var cell in stackView.ArrangedSubviews)
            {
                if (cell is StackViewCell stackCell)
                {
                    rows.Add(stackCell.contentView);
                }
            }

            return rows.ToArray();
        }

        /// Returns `true` if the given row is present in the stack view, `false` otherwise.
        public bool containsRow(UIView row)
        {
            if (row.Superview is StackViewCell cell)
            {
                return stackView.ArrangedSubviews.Contains(cell);
            }
            return false;
        }

        #endregion

        #endregion

        #region Hiding and Showing Rows

        /// Hides the given row, making it invisible.
        ///
        /// If `animated` is `true`, the change is animated.
        public void hideRow(UIView row, bool animated = false)
        {
            setRowHidden(row, isHidden: true, animated: animated);
        }

        /// Hides the given rows, making them invisible.
        ///
        /// If `animated` is `true`, the changes are animated.
        public void hideRows(UIView[] rows, bool animated = false)
        {
            foreach (var row in rows)
            {
                hideRow(row, animated);
            }
        }

        /// Shows the given row, making it visible.
        ///
        /// If `animated` is `true`, the change is animated.
        public void showRow(UIView row, bool animated = false)
        {
            setRowHidden(row, isHidden: false, animated: animated);
        }

        /// Shows the given rows, making them visible.
        ///
        /// If `animated` is `true`, the changes are animated.
        public void showRows(UIView[] rows, bool animated = false)
        {
            foreach (var row in rows)
            {
                showRow(row, animated);
            }
        }

        /// Hides the given row if `isHidden` is `true`, or shows the given row if `isHidden` is `false`.
        ///
        /// If `animated` is `true`, the change is animated.
        public void setRowHidden(UIView row, bool isHidden, bool animated = false)
        {
            // TODO:
            if (row.Superview is StackViewCell cell && cell.Hidden != isHidden)
            {
                if (animated)
                {
                    UIView.Animate(0.3, () =>
                    {
                        cell.Hidden = isHidden;
                        cell.LayoutIfNeeded();
                    });
                }
                else
                {
                    cell.Hidden = isHidden;
                }
            }
        }

        /// Hides the given rows if `isHidden` is `true`, or shows the given rows if `isHidden` is
        /// `false`.
        ///
        /// If `animated` is `true`, the change are animated.
        public void setRowsHidden(UIView[] rows, bool isHidden, bool animated = false)
        {
            foreach (var row in rows)
            {
                setRowHidden(row, isHidden, animated);
            }
        }

        /// Returns `true` if the given row is hidden, `false` otherwise.
        public bool isRowHidden(UIView row)
        {
            return (row.Superview as StackViewCell)?.Hidden ?? false;
        }

        #endregion

        #region Handling User Interaction

        /// Sets a closure that will be called when the given row in the stack view is tapped by the user.
        ///
        /// The handler will be passed the row.
        /// TODO:
        //  open func setTapHandler<RowView: UIView>(forRow row: RowView, handler: ((RowView) -> Void)?) {
        //  guard let cell = row.superview as? StackViewCell else { return }

        //  if let handler = handler {
        //    cell.tapHandler = { contentView in
        //      guard let contentView = contentView as? RowView else { return }
        //      handler(contentView)
        //    }
        //  } else {
        //    cell.tapHandler = nil
        //  }
        //}


        #endregion

        #region Styling Rows

        /// The background color of rows in the stack view.
        ///
        /// This background color will be used for any new row that is added to the stack view.
        /// The default color is clear.
        public UIColor rowBackgroundColor = UIColor.Clear;

        /// The highlight background color of rows in the stack view.
        ///
        /// This highlight background color will be used for any new row that is added to the stack view.
        /// The default color is #D9D9D9 (RGB 217, 217, 217).
        public UIColor rowHighlightColor = AloeStackView.defaultRowHighlightColor;

        /// Sets the background color for the given row to the `UIColor` provided.
        public void setBackgroundColor(UIView row, UIColor color)
        {
            if (row.Superview is StackViewCell cell)
            {
                cell.rowBackgroundColor = color;
            }
        }

        /// Sets the background color for the given rows to the `UIColor` provided.
        public void setBackgroundColor(UIView[] rows, UIColor color)
        {
            foreach (var row in rows)
            {
                setBackgroundColor(row, color);
            }
        }

        /// Specifies the default inset of rows.
        ///
        /// This inset will be used for any new row that is added to the stack view.
        ///
        /// You can use this property to add space between a row and the left and right edges of the stack
        /// view and the rows above and below it. Positive inset values move the row inward and away
        /// from the stack view edges and away from rows above and below.
        ///
        /// The default inset is 15pt on each side and 12pt on the top and bottom.
        public UIEdgeInsets rowInset = new UIEdgeInsets(
          top: 12,
          left: AloeStackView.defaultSeparatorInset.Left,
          bottom: 12,
          // Intentional, to match the default spacing of UITableView's cell separators but balanced on
          // each side.
          right: AloeStackView.defaultSeparatorInset.Left);

        /// Sets the inset for the given row to the `UIEdgeInsets` provided.
        public void setInset(UIView row, UIEdgeInsets inset)
        {
            if (row.Superview is StackViewCell cell)
            {
                cell.rowInset = inset;
            }
        }

        /// Sets the inset for the given rows to the `UIEdgeInsets` provided.
        public void setInset(UIView[] rows, UIEdgeInsets inset)
        {
            foreach (var row in rows)
            {
                setInset(row, inset);
            }
        }

        #endregion

        #region Styling Separators

        /// The color of separators in the stack view.
        ///
        /// The default color matches the default color of separators in `UITableView`.
        private UIColor _separatorColor;
        public UIColor separatorColor
        {
            get => _separatorColor;
            set
            {
                _separatorColor = value;

                foreach (var cell in stackView.ArrangedSubviews)
                {
                    (cell as StackViewCell).separatorColor = separatorColor;
                }
            }
        }

        /// The height of separators in the stack view.
        ///
        /// The default height is 1px.
        private nfloat _separatorHeight = 1;
        public nfloat separatorHeight
        {
            get => _separatorHeight;
            set
            {
                _separatorHeight = value;

                foreach (var cell in stackView.ArrangedSubviews)
                {
                    (cell as StackViewCell).separatorHeight = separatorHeight;
                }
            }
        }

        /// Specifies the default inset of row separators.
        ///
        /// Only left and right insets are honored. This inset will be used for any new row that is added
        /// to the stack view. The default inset matches the default inset of cell separators in
        /// `UITableView`, which are 15pt on the left and 0pt on the right.
        public UIEdgeInsets separatorInset = AloeStackView.defaultSeparatorInset;

        /// Sets the separator inset for the given row to the `UIEdgeInsets` provided.
        ///
        /// Only left and right insets are honored.
        public void setSeperatorInset(UIView row, UIEdgeInsets inset)
        {
            if (row.Superview is StackViewCell cell)
            {
                cell.separatorInset = inset;
            }
        }

        /// Sets the separator inset for the given rows to the `UIEdgeInsets` provided.
        ///
        /// Only left and right insets are honored.
        public void setSeperatorInset(UIView[] rows, UIEdgeInsets inset)
        {
            foreach (var row in rows)
            {
                setSeperatorInset(row, inset: inset);
            }
        }

        #endregion

        #region Hiding and Showing Separators

        /// Specifies the default visibility of row separators.
        ///
        /// When `true`, separators will be hidden for any new rows added to the stack view.
        /// When `false, separators will be visible for any new rows added. Default is `false`, meaning
        /// separators are visible for any new rows that are added.
        public bool hidesSeparatorsByDefault = false;

        /// Hides the separator for the given row.
        public void hideSeparator(UIView row)
        {
            if (row.Superview is StackViewCell cell)
            {
                cell.shouldHideSeparator = true;
                updateSeparatorVisibility(cell);
            }
        }

        /// Hides separators for the given rows.
        public void hideSeparators(UIView[] rows)
        {
            foreach (var row in rows)
            {
                hideSeparator(row);
            }
        }

        /// Shows the separator for the given row.
        public void showSeparator(UIView row)
        {
            if (row.Superview is StackViewCell cell)
            {
                cell.shouldHideSeparator = false;
                updateSeparatorVisibility(cell);
            }
        }

        /// Shows separators for the given rows.
        public void showSeparators(UIView[] rows)
        {
            foreach (var row in rows)
            {
                showSeparator(row);
            }
        }

        /// Automatically hides the separator of the last cell in the stack view.
        ///
        /// Default is `false`.
        private bool _automaticallyHidesLastSeparator;
        public bool automaticallyHidesLastSeparator
        {
            get => _automaticallyHidesLastSeparator;
            set
            {
                _automaticallyHidesLastSeparator = value;

                if (stackView.ArrangedSubviews.Last() is StackViewCell cell)
                {
                    updateSeparatorVisibility(cell);
                }
            }
        }

        #endregion

        #region Modifying the Scroll Position

        /// Scrolls the given row onto screen so that it is fully visible.
        ///
        /// If `animated` is `true`, the scroll is animated. If the row is already fully visible, this
        /// method does nothing.
        public void scrollRowToVisible(UIView row, bool animated = true)
        {
            var superview = row.Superview;

            if (superview == null) return;

            ScrollRectToVisible(ConvertRectFromCoordinateSpace(row.Frame, superview), animated: animated);
        }

        #endregion

        #region Extending AloeStackView

        /// Returns the `StackViewCell` to be used for the given row.
        ///
        /// An instance of `StackViewCell` wraps every row in the stack view.
        ///
        /// Subclasses can override this method to return a custom `StackViewCell` subclass, for example
        /// to add custom behavior or functionality that is not provided by default.
        ///
        /// If you customize the values of some properties of `StackViewCell` in this method, these values
        /// may be overwritten by default values after the cell is returned. To customize the values of
        /// properties of the cell, override `configureCell(_:)` and perform the customization there,
        /// rather than on the cell returned from this method.
        public StackViewCell cellForRow(UIView row)
        {
            return new StackViewCell(row);
        }

        /// Allows subclasses to configure the properties of the given `StackViewCell`.
        ///
        /// This method is called for newly created cells after the default values of any properties of
        /// the cell have been set by the superclass.
        ///
        /// The default implementation of this method does nothing.
        public void configureCell(StackViewCell cell)
        { }

        #endregion

        #region Private

        private UIStackView stackView = new UIStackView();

        private void setUpViews()
        {
            setUpSelf();
            setUpStackView();
        }

        private void setUpSelf()
        {
            BackgroundColor = UIColor.White;
        }

        private void setUpStackView()
        {
            stackView.TranslatesAutoresizingMaskIntoConstraints = false;
            stackView.Axis = UILayoutConstraintAxis.Vertical;
            AddSubview(stackView);
        }

        private void setUpConstraints()
        {
            setUpStackViewConstraints();
        }

        private void setUpStackViewConstraints()
        {
            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[] {
              stackView.TopAnchor.ConstraintEqualTo(TopAnchor),
              stackView.BottomAnchor.ConstraintEqualTo(BottomAnchor),
              stackView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
              stackView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor),
              stackView.WidthAnchor.ConstraintEqualTo(WidthAnchor)
            });
        }

        private StackViewCell createCell(UIView contentView)
        {
            var cell = cellForRow(contentView);

            cell.rowBackgroundColor = rowBackgroundColor;
            //cell.rowHighlightColor = rowHighlightColor;
            cell.rowInset = rowInset;
            cell.separatorColor = separatorColor;
            cell.separatorHeight = separatorHeight;
            cell.separatorInset = separatorInset;
            cell.shouldHideSeparator = hidesSeparatorsByDefault;

            configureCell(cell);

            return cell;
        }

        private void insertCell(UIView contentView, nint index, bool animated)
        {
            var cellToRemove = containsRow(contentView) ? contentView.Superview : null;

            var cell = createCell(contentView);
            stackView.InsertArrangedSubview(cell, (nuint)index);

            if (cellToRemove is StackViewCell rcell)
            {
                removeCell(rcell, animated: false);
            }

            updateSeparatorVisibility(cell);

            // A cell can affect the visibility of the cell before it, e.g. if
            // `automaticallyHidesLastSeparator` is true and a new cell is added as the last cell, so update
            // the previous cell's separator visibility as well.

            var cellAboveV = cellAbove(cell);
            if (cellAboveV != null)
            {
                updateSeparatorVisibility(cellAboveV);
            }

            if (animated)
            {
                cell.Alpha = 0;
                LayoutIfNeeded();
                UIView.Animate(0.3, () =>
                {
                    cell.Alpha = 1;
                });
            }
        }

        private void removeCell(StackViewCell cell, bool animated)
        {
            var aboveCell = cellAbove(cell);

            //    let completion: (Bool)->Void = { [weak self] _ in
            //      guard let `self` = self else { return }
            //      cell.removeFromSuperview()

            //      // When removing a cell, the cell before the removed cell is the only cell whose separator
            //      // visibility could be affected, so we need to update its visibility.
            //      if let aboveCell = aboveCell {
            //        self.updateSeparatorVisibility(forCell: aboveCell)
            //      }
            //}
            Action<bool> completion = v =>
            {
                // TODO:
                if (v) return;

                cell.RemoveFromSuperview();

                // When removing a cell, the cell before the removed cell is the only cell whose separator
                // visibility could be affected, so we need to update its visibility.
                if (aboveCell != null)
                {
                    updateSeparatorVisibility(aboveCell);
                }
            };


            if (animated)
            {
                UIView.Animate(0.3,
                  () =>
                  {
                      cell.Hidden = true;
                  },
                  () => completion(false));
            }
            else
            {
                completion(true);
            }
        }

        private void updateSeparatorVisibility(StackViewCell cell)
        {
            var isLastCellAndHidingIsEnabled = automaticallyHidesLastSeparator &&
              cell == stackView.ArrangedSubviews.Last();
            var cellConformsToSeparatorHiding = cell.contentView is SeparatorHiding;

            cell.isSeparatorHidden =
              isLastCellAndHidingIsEnabled ||
              cellConformsToSeparatorHiding ||
              cell.shouldHideSeparator;
        }

        private StackViewCell cellAbove(StackViewCell cell)
        {
            // TODO:
            var index = Array.IndexOf(stackView.ArrangedSubviews, cell);

            if (index > 0)
            {
                return stackView.ArrangedSubviews[index - 1] as StackViewCell;
            }

            return null;
        }

        private static UIColor defaultRowHighlightColor = new UIColor(red: 217 / 255, green: 217 / 255, blue: 217 / 255, alpha: 1);
        private static UIColor defaultSeparatorColor = new UITableView().SeparatorColor ?? UIColor.Clear;
        private static UIEdgeInsets defaultSeparatorInset = new UITableView().SeparatorInset;

        #endregion
    }
}
