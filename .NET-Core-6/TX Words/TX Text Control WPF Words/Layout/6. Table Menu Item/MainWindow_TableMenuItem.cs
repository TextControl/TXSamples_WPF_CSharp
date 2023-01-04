/*-------------------------------------------------------------------------------------------------------------
** MainWindow_TableMenuItem.cs File
**
** Description: Provides methods to set the layout of the 'Table' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using TXTextControl.WPF;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** SetTableItemsImages Method
		**
		** Sets the images of the 'Table' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetTableItemsImages() {
			// 'Insert'
			SetItemImage(m_miTable_Insert, RibbonInsertTab.RibbonItem.TXITEM_InsertTable.ToString());
			SetItemImage(m_miTable_Insert_Table, RibbonInsertTab.RibbonItem.TXITEM_InsertTable.ToString());
			SetItemImage(m_miTable_Insert_ColumnToTheLeft, RibbonTableLayoutTab.RibbonItem.TXITEM_InsertTableColLeft.ToString());
			SetItemImage(m_miTable_Insert_ColumnToTheRight, RibbonTableLayoutTab.RibbonItem.TXITEM_InsertTableColRight.ToString());
			SetItemImage(m_miTable_Insert_RowAbove, RibbonTableLayoutTab.RibbonItem.TXITEM_InsertTableRowAbove.ToString());
			SetItemImage(m_miTable_Insert_RowBelow, RibbonTableLayoutTab.RibbonItem.TXITEM_InsertTableRowBelow.ToString());

			// 'Delete'
			SetItemImage(m_miTable_Delete, RibbonTableLayoutTab.RibbonItem.TXITEM_DeleteTable.ToString());
			SetItemImage(m_miTable_Delete_Table, RibbonTableLayoutTab.RibbonItem.TXITEM_DeleteTable.ToString());
			SetItemImage(m_miTable_Delete_Columns, RibbonTableLayoutTab.RibbonDropDownItem.TXITEM_DeleteTableCol.ToString());
			SetItemImage(m_miTable_Delete_Rows, RibbonTableLayoutTab.RibbonDropDownItem.TXITEM_DeleteTableRow.ToString());
			SetItemImage(m_miTable_Delete_Cells, RibbonTableLayoutTab.RibbonDropDownItem.TXITEM_DeleteTableCell.ToString());

			// 'Select'
			SetItemImage(m_miTable_Select, RibbonTableLayoutTab.RibbonDropDownItem.TXITEM_SelectTableRow.ToString());
			SetItemImage(m_miTable_Select_Table, RibbonTableLayoutTab.RibbonDropDownItem.TXITEM_SelectTableAll.ToString());
			SetItemImage(m_miTable_Select_Column, RibbonTableLayoutTab.RibbonDropDownItem.TXITEM_SelectTableCol.ToString());
			SetItemImage(m_miTable_Select_Row, RibbonTableLayoutTab.RibbonDropDownItem.TXITEM_SelectTableRow.ToString());
			SetItemImage(m_miTable_Select_Cell, RibbonTableLayoutTab.RibbonDropDownItem.TXITEM_SelectTableCell.ToString());

			// 'Merge Cells'
			SetItemImage(m_miTable_MergeCells, RibbonTableLayoutTab.RibbonItem.TXITEM_MergeTableCells.ToString());

			// 'Split Cells'
			SetItemImage(m_miTable_SplitCells, RibbonTableLayoutTab.RibbonItem.TXITEM_SplitTableCells.ToString());

			// 'Split Table'
			SetItemImage(m_miTable_SplitTable, RibbonTableLayoutTab.RibbonItem.TXITEM_SplitTable.ToString());
			SetItemImage(m_miTable_SplitTable_Above, RibbonTableLayoutTab.RibbonDropDownItem.TXITEM_SplitTableAbove.ToString());
			SetItemImage(m_miTable_SplitTable_Below, RibbonTableLayoutTab.RibbonDropDownItem.TXITEM_SplitTableBelow.ToString());

			// 'Formulas'
			SetItemImage(m_miTable_Formulas, RibbonFormulaTab.RibbonItem.TXITEM_AddFunction.ToString());
			SetItemImage(m_miTable_Formulas_A1ReferenceStyle, RibbonFormulaTab.RibbonItem.TXITEM_EnableA1Style.ToString());
			SetItemImage(m_miTable_Formulas_R1C1ReferenceStyle, RibbonFormulaTab.RibbonItem.TXITEM_EnableR1C1Style.ToString());
			SetItemImage(m_miTable_Formulas_EditFormula, "TXITEM_FormulaEditing");
			SetItemImage(m_miTable_Formulas_AutomaticCalculation, RibbonFormulaTab.RibbonItem.TXITEM_EnableFormulaCalculation.ToString());

			// 'Properties...'
			SetItemImage(m_miTable_Properties, RibbonInsertTab.RibbonDropDownItem.TXITEM_InsertTableDialog.ToString());
		}
	}
}
