//
// AloeStackView.cs
//
// C# port created by Yauheni Pakala
// Copyright (c) 2019
//
// Original Swift version created by Marli Oshlack
// Copyright 2018 Airbnb, Inc.
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
    /// <summary>
    /// A simple class for laying out a collection of views with a convenient API, while leveraging the
    /// power of Auto Layout.
    /// </summary>
    public class AloeStackView : UIScrollView
    {
        #region Lifecycle

        public AloeStackView() : base(CGRect.Empty)
        {
            SetUpViews();
            SetUpConstraints();
        }

        public AloeStackView(NSCoder coder) => throw new NotImplementedException("ctor(coder:) has not been implemented");

        #endregion

        #region Public

        #region Adding and Removing Rows

        /// <summary>
        /// Adds a row to the end of the stack view.
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="animated">If `animated` is `true`, the insertion is animated.</param>
        public void AddRow(UIView row, bool animated = false)
        {
            InsertCell(row, stackView.ArrangedSubviews.Count(), animated);
        }

        /// <summary>
        /// Adds multiple rows to the end of the stack view.
        /// </summary>
        /// <param name="rows">Rows.</param>
        /// <param name="animated">If `animated` is `true`, the insertions are animated.</param>
        public void AddRows(UIView[] rows, bool animated = false)
        {
            foreach (var row in rows)
            {
                AddRow(row, animated);
            }
        }

        /// <summary>
        /// Adds a row to the beginning of the stack view.
        /// </summary>
        /// <param name="row">Row.</param>
        /// <param name="animated">If `animated` is `true`, the insertion is animated.</param>
        public void PrependRow(UIView row, bool animated = false)
        {
            InsertCell(row, 0, animated);
        }

        // TODO: port
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
        public void InsertRowBefore(UIView row, UIView beforeRow, bool animated = false)
        {
            if (beforeRow.Superview is StackViewCell cell)
            {
                var index = Array.IndexOf(stackView.ArrangedSubviews, cell);

                if (index < 0) return;

                InsertCell(row, index, animated);
            }
        }

        /// Inserts multiple rows above the specified row in the stack view.
        ///
        /// If `animated` is `true`, the insertions are animated.
        public void InsertRowsBefore(UIView[] rows, UIView beforeRow, bool animated)
        {
            foreach (var row in rows)
            {
                InsertRowBefore(row, beforeRow, animated);
            }
        }

        /// Inserts a row below the specified row in the stack view.
        ///
        /// If `animated` is `true`, the insertion is animated.
        public void InsertRowAfter(UIView row, UIView afterRow, bool animated = false)
        {
            if (afterRow.Superview is StackViewCell cell)
            {
                var index = Array.IndexOf(stackView.ArrangedSubviews, cell);

                if (index < 0) return;

                InsertCell(row, index + 1, animated);
            }
        }

        // TODO: port
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
        public void RemoveRow(UIView row, bool animated = false)
        {
            if (row.Superview is StackViewCell cell)
            {
                RemoveCell(cell, animated);
            }
        }

        /// Removes the given rows from the stack view.
        ///
        /// If `animated` is `true`, the removals are animated.
        public void RemoveRows(UIView[] rows, bool animated = false)
        {
            foreach (var row in rows)
            {
                RemoveRow(row, animated);
            }
        }

        /// Removes all the rows in the stack view.
        ///
        /// If `animated` is `true`, the removals are animated.
        public void RemoveAllRows(bool animated = false)
        {
            foreach (var view in stackView.ArrangedSubviews)
            {
                if (view is StackViewCell cell)
                {
                    RemoveRow(cell.contentView, animated);
                }
            }
        }

        #endregion

        #region Accessing Rows

        /// Returns an array containing of all the rows in the stack view.
        ///
        /// The rows in the returned array are in the order they appear visually in the stack view.
        public UIView[] GetAllRows()
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

        /// <summary>
        /// Containses the row.
        /// </summary>
        /// <returns>Returns `true` if the given row is present in the stack view, `false` otherwise.</returns>
        /// <param name="row">Row.</param>
        public bool ContainsRow(UIView row)
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
        public void HideRow(UIView row, bool animated = false)
        {
            SetRowHidden(row, isHidden: true, animated: animated);
        }

        /// Hides the given rows, making them invisible.
        ///
        /// If `animated` is `true`, the changes are animated.
        public void HideRows(UIView[] rows, bool animated = false)
        {
            foreach (var row in rows)
            {
                HideRow(row, animated);
            }
        }

        /// Shows the given row, making it visible.
        ///
        /// If `animated` is `true`, the change is animated.
        public void ShowRow(UIView row, bool animated = false)
        {
            SetRowHidden(row, isHidden: false, animated: animated);
        }

        /// Shows the given rows, making them visible.
        ///
        /// If `animated` is `true`, the changes are animated.
        public void ShowRows(UIView[] rows, bool animated = false)
        {
            foreach (var row in rows)
            {
                ShowRow(row, animated);
            }
        }

        /// Hides the given row if `isHidden` is `true`, or shows the given row if `isHidden` is `false`.
        ///
        /// If `animated` is `true`, the change is animated.
        public void SetRowHidden(UIView row, bool isHidden, bool animated = false)
        {
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
        public void SetRowsHidden(UIView[] rows, bool isHidden, bool animated = false)
        {
            foreach (var row in rows)
            {
                SetRowHidden(row, isHidden, animated);
            }
        }

        /// <summary>
        /// Ises the row hidden.
        /// </summary>
        /// <returns>Returns <c>true</c> if the given row is hidden, <c>false</c> otherwise.</returns>
        /// <param name="row">Row.</param>
        public bool IsRowHidden(UIView row)
        {
            return (row.Superview as StackViewCell)?.Hidden ?? false;
        }

        #endregion

        #region Handling User Interaction

        public void SetTapHandler<TRowView>(TRowView row, Action<TRowView> handler)
            where TRowView : UIView
        {
            if (row.Superview is StackViewCell cell)
            {
                if (handler != null)
                {
                    cell.TapHandler = (contentView) =>
                    {
                        if (contentView is TRowView view)
                        {
                            handler(view);
                        }
                    };
                }
                else
                {
                    cell.TapHandler = null;
                }
            }
        }

        #endregion

        #region Styling Rows

        /// <summary>
        /// The background color of rows in the stack view.
        ///
        /// This background color will be used for any new row that is added to the stack view.
        /// The default color is clear.
        /// </summary>
        public UIColor RowBackgroundColor = UIColor.Clear;

        /// <summary>
        /// The highlight background color of rows in the stack view.
        ///
        /// This highlight background color will be used for any new row that is added to the stack view.
        /// The default color is #D9D9D9 (RGB 217, 217, 217).
        /// </summary>
        public UIColor RowHighlightColor = AloeStackView.defaultRowHighlightColor;

        /// <summary>
        /// Sets the background color for the given row to the `UIColor` provided.
        /// </summary>
        /// <param name="row">Row.</param>
        /// <param name="color">Color.</param>
        public void SetBackgroundColor(UIView row, UIColor color)
        {
            if (row.Superview is StackViewCell cell)
            {
                cell.RowBackgroundColor = color;
            }
        }

        /// <summary>
        /// Sets the background color for the given rows to the `UIColor` provided.
        /// </summary>
        /// <param name="rows">Rows.</param>
        /// <param name="color">Color.</param>
        public void SetBackgroundColor(UIView[] rows, UIColor color)
        {
            foreach (var row in rows)
            {
                SetBackgroundColor(row, color);
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
        public UIEdgeInsets RowInset = new UIEdgeInsets(
            top: 12,
            left: AloeStackView.defaultSeparatorInset.Left,
            bottom: 12,
            // Intentional, to match the default spacing of UITableView's cell separators but balanced on
            // each side.
            right: AloeStackView.defaultSeparatorInset.Left);

        /// <summary>
        /// Sets the inset for the given row to the `UIEdgeInsets` provided.
        /// </summary>
        /// <param name="forRow">Row.</param>
        /// <param name="inset">Inset.</param>
        public void SetInset(UIView forRow, UIEdgeInsets inset)
        {
            if (forRow.Superview is StackViewCell cell)
            {
                cell.RowInset = inset;
            }
        }

        /// <summary>
        /// Sets the inset for the given rows to the `UIEdgeInsets` provided.
        /// </summary>
        /// <param name="rows">Rows.</param>
        /// <param name="inset">Inset.</param>
        public void SetInset(UIView[] rows, UIEdgeInsets inset)
        {
            foreach (var row in rows)
            {
                SetInset(row, inset);
            }
        }

        #endregion

        #region Styling Separators

        private UIColor _separatorColor = AloeStackView.defaultSeparatorColor;

        /// <summary>
        /// The color of separators in the stack view.
        /// </summary>
        /// <value>The default color matches the default color of separators in `UITableView`.</value>
        public UIColor SeparatorColor
        {
            get => _separatorColor;
            set
            {
                _separatorColor = value;

                foreach (var cell in stackView.ArrangedSubviews)
                {
                    if (cell is StackViewCell stackViewCell)
                    {
                        stackViewCell.SeparatorColor = SeparatorColor;
                    }
                }
            }
        }

        private nfloat _separatorHeight = 1f / UIScreen.MainScreen.Scale;

        /// <summary>
        /// The height of separators in the stack view.
        /// </summary>
        /// <value>The default height is 1px.</value>
        public nfloat SeparatorHeight
        {
            get => _separatorHeight;
            set
            {
                _separatorHeight = value;

                foreach (var cell in stackView.ArrangedSubviews)
                {
                    if (cell is StackViewCell stackViewCell)
                    {
                        stackViewCell.SeparatorHeight = SeparatorHeight;
                    }
                }
            }
        }

        /// <summary>
        /// Specifies the default inset of row separators.
        ///
        /// Only left and right insets are honored. This inset will be used for any new row that is added
        /// to the stack view. The default inset matches the default inset of cell separators in
        /// `UITableView`, which are 15pt on the left and 0pt on the right.
        /// </summary>
        public UIEdgeInsets SeparatorInset = AloeStackView.defaultSeparatorInset;

        /// <summary>
        /// Sets the separator inset for the given row to the `UIEdgeInsets` provided.
        ///
        /// Only left and right insets are honored.
        /// </summary>
        /// <param name="row">Row.</param>
        /// <param name="inset">Inset.</param>
        public void SetSeperatorInset(UIView row, UIEdgeInsets inset)
        {
            if (row.Superview is StackViewCell cell)
            {
                cell.SeparatorInset = inset;
            }
        }

        /// <summary>
        /// Sets the separator inset for the given rows to the `UIEdgeInsets` provided.
        ///
        /// Only left and right insets are honored.
        /// </summary>
        /// <param name="rows">Rows.</param>
        /// <param name="inset">Inset.</param>
        public void SetSeperatorInset(UIView[] rows, UIEdgeInsets inset)
        {
            foreach (var row in rows)
            {
                SetSeperatorInset(row, inset: inset);
            }
        }

        #endregion

        #region Hiding and Showing Separators

        /// Specifies the default visibility of row separators.
        ///
        /// When `true`, separators will be hidden for any new rows added to the stack view.
        /// When `false, separators will be visible for any new rows added. Default is `false`, meaning
        /// separators are visible for any new rows that are added.
        public bool HidesSeparatorsByDefault;

        /// Hides the separator for the given row.
        public void HideSeparator(UIView forRow)
        {
            if (forRow.Superview is StackViewCell cell)
            {
                cell.ShouldHideSeparator = true;
                UpdateSeparatorVisibility(cell);
            }
        }

        /// Hides separators for the given rows.
        public void HideSeparators(UIView[] rows)
        {
            foreach (var row in rows)
            {
                HideSeparator(row);
            }
        }

        /// Shows the separator for the given row.
        public void ShowSeparator(UIView row)
        {
            if (row.Superview is StackViewCell cell)
            {
                cell.ShouldHideSeparator = false;
                UpdateSeparatorVisibility(cell);
            }
        }

        /// Shows separators for the given rows.
        public void ShowSeparators(UIView[] rows)
        {
            foreach (var row in rows)
            {
                ShowSeparator(row);
            }
        }

        /// Automatically hides the separator of the last cell in the stack view.
        ///
        /// Default is `false`.
        private bool _automaticallyHidesLastSeparator;
        public bool AutomaticallyHidesLastSeparator
        {
            get => _automaticallyHidesLastSeparator;
            set
            {
                _automaticallyHidesLastSeparator = value;

                if (stackView.ArrangedSubviews.LastOrDefault() is StackViewCell cell)
                {
                    UpdateSeparatorVisibility(cell);
                }
            }
        }

        #endregion

        #region Modifying the Scroll Position

        /// Scrolls the given row onto screen so that it is fully visible.
        ///
        /// If `animated` is `true`, the scroll is animated. If the row is already fully visible, this
        /// method does nothing.
        public void ScrollRowToVisible(UIView row, bool animated = true)
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
        public StackViewCell CellForRow(UIView row)
        {
            return new StackViewCell(row);
        }

        /// Allows subclasses to configure the properties of the given `StackViewCell`.
        ///
        /// This method is called for newly created cells after the default values of any properties of
        /// the cell have been set by the superclass.
        ///
        /// The default implementation of this method does nothing.
        public void ConfigureCell(StackViewCell cell)
        { }

        #endregion

        #region Private

        private UIStackView stackView = new UIStackView();

        private void SetUpViews()
        {
            SetUpSelf();
            SetUpStackView();
        }

        private void SetUpSelf()
        {
            BackgroundColor = UIColor.White;
        }

        private void SetUpStackView()
        {
            stackView.TranslatesAutoresizingMaskIntoConstraints = false;
            stackView.Axis = UILayoutConstraintAxis.Vertical;
            AddSubview(stackView);
        }

        private void SetUpConstraints()
        {
            SetUpStackViewConstraints();
        }

        private void SetUpStackViewConstraints()
        {
            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[] {
              stackView.TopAnchor.ConstraintEqualTo(TopAnchor),
              stackView.BottomAnchor.ConstraintEqualTo(BottomAnchor),
              stackView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
              stackView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor),
              stackView.WidthAnchor.ConstraintEqualTo(WidthAnchor)
            });
        }

        private StackViewCell CreateCell(UIView contentView)
        {
            var cell = CellForRow(contentView);

            cell.RowBackgroundColor = RowBackgroundColor;
            cell.RowHighlightColor = RowHighlightColor;
            cell.RowInset = RowInset;
            cell.SeparatorColor = SeparatorColor;
            cell.SeparatorHeight = SeparatorHeight;
            cell.SeparatorInset = SeparatorInset;
            cell.ShouldHideSeparator = HidesSeparatorsByDefault;

            ConfigureCell(cell);

            return cell;
        }

        private void InsertCell(UIView contentView, nint index, bool animated)
        {
            var cellToRemove = ContainsRow(contentView) ? contentView.Superview : null;

            var cell = CreateCell(contentView);
            stackView.InsertArrangedSubview(cell, (nuint)index);

            if (cellToRemove is StackViewCell rcell)
            {
                RemoveCell(rcell, animated: false);
            }

            UpdateSeparatorVisibility(cell);

            // A cell can affect the visibility of the cell before it, e.g. if
            // `automaticallyHidesLastSeparator` is true and a new cell is added as the last cell, so update
            // the previous cell's separator visibility as well.

            var cellAboveV = CellAbove(cell);
            if (cellAboveV != null)
            {
                UpdateSeparatorVisibility(cellAboveV);
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

        private void RemoveCell(StackViewCell cell, bool animated)
        {
            var aboveCell = CellAbove(cell);

            void completion(bool _)
            {
                cell.RemoveFromSuperview();

                // When removing a cell, the cell before the removed cell is the only cell whose separator
                // visibility could be affected, so we need to update its visibility.
                if (aboveCell != null)
                {
                    UpdateSeparatorVisibility(aboveCell);
                }
            }

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

        private void UpdateSeparatorVisibility(StackViewCell cell)
        {
            var isLastCellAndHidingIsEnabled = AutomaticallyHidesLastSeparator &&
              cell == stackView.ArrangedSubviews.Last();
            var cellConformsToSeparatorHiding = cell.contentView is ISeparatorHiding;

            cell.IsSeparatorHidden =
              isLastCellAndHidingIsEnabled ||
              cellConformsToSeparatorHiding ||
              cell.ShouldHideSeparator;
        }

        private StackViewCell CellAbove(StackViewCell cell)
        {
            var index = Array.IndexOf(stackView.ArrangedSubviews, cell);

            if (index > 0)
            {
                return stackView.ArrangedSubviews[index - 1] as StackViewCell;
            }

            return null;
        }

        private static readonly UIColor defaultRowHighlightColor = new UIColor(red: 217 / 255f, green: 217 / 255f, blue: 217 / 255f, alpha: 1f);
        private static readonly UIColor defaultSeparatorColor = new UITableView().SeparatorColor ?? UIColor.Clear;
        private static readonly UIEdgeInsets defaultSeparatorInset = new UITableView().SeparatorInset;

        #endregion
    }
}
