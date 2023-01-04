/*-------------------------------------------------------------------------------------------------------------
** MainWindow_TableMenuItem_DropDownOpening.cs File
**
** Description: Provides all SubmenuOpened handlers associated with 'Table' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
using System.Windows;

namespace TXTextControl.Words {
	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** Table_SubmenuOpened Handler
		**
		** Updates the IsEnabled state of 'Table' drop down menu items.
		** 
		** Item: 'Table'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Table_SubmenuOpened(object sender, RoutedEventArgs e) {
			// Get current table states
			Table tblCurrentTable = m_txTextControl.Tables.GetItem();
			bool bIsTable = tblCurrentTable != null;
			bool bCanTableFormat = m_txTextControl.CanTableFormat;
			TableCellCollection colTableCells = bIsTable ? tblCurrentTable.Cells : null;
			TableColumnCollection colTableColumns = bIsTable ? tblCurrentTable.Columns : null;
			TableRowCollection colTableRows = bIsTable ? tblCurrentTable.Rows : null;


			// 'Insert'
			m_miTable_Insert.IsEnabled = bCanTableFormat;
			m_miTable_Insert_ColumnToTheLeft.IsEnabled =
			m_miTable_Insert_ColumnToTheRight.IsEnabled = bCanTableFormat && bIsTable && colTableColumns.CanAdd;
			m_miTable_Insert_RowAbove.IsEnabled =
			m_miTable_Insert_RowBelow.IsEnabled = bCanTableFormat && bIsTable && colTableRows.CanAdd;

			// 'Delete'
			m_miTable_Delete.IsEnabled =
			m_miTable_Delete_Table.IsEnabled = bCanTableFormat && bIsTable;
			m_miTable_Delete_Cells.IsEnabled = bCanTableFormat && bIsTable && colTableCells.CanRemove;
			m_miTable_Delete_Columns.IsEnabled = bCanTableFormat && bIsTable && colTableColumns.CanRemove;
			m_miTable_Delete_Rows.IsEnabled = bCanTableFormat && bIsTable && colTableRows.CanRemove;

			// 'Select'
			m_miTable_Select.IsEnabled =
			m_miTable_Select_Table.IsEnabled = bCanTableFormat && bIsTable;
			m_miTable_Select_Cell.IsEnabled = bCanTableFormat && bIsTable && colTableCells.GetItem() != null;
			m_miTable_Select_Column.IsEnabled = bCanTableFormat && bIsTable && colTableColumns.GetItem() != null;
			m_miTable_Select_Row.IsEnabled = bCanTableFormat && bIsTable && colTableRows.GetItem() != null;

			// 'Merge Cells'
			m_miTable_MergeCells.IsEnabled = bCanTableFormat && bIsTable && tblCurrentTable.CanMergeCells;

			// 'Split Cells'
			m_miTable_SplitCells.IsEnabled = bCanTableFormat && bIsTable && tblCurrentTable.CanSplitCells;

			// 'Split Table'
			m_miTable_SplitTable.IsEnabled = bCanTableFormat && bIsTable && tblCurrentTable.CanSplit;

			// 'Formulas'
			m_miTable_Formulas_A1ReferenceStyle.IsEnabled =
			m_miTable_Formulas_R1C1ReferenceStyle.IsEnabled =
			m_miTable_Formulas_AutomaticCalculation.IsEnabled = bCanTableFormat;
			m_miTable_Formulas_EditFormula.IsEnabled = bCanTableFormat && bIsTable;

			// 'Properties...'
			m_miTable_Properties.IsEnabled = bCanTableFormat && bIsTable;
		}

		/*-------------------------------------------------------------------------------------------------------------
		** Formulas_SubmenuOpened Handler
		**
		** Updates the checked state of 'Formulas' drop down menu items.
		** 
		** Item: 'Formulas'
		**-----------------------------------------------------------------------------------------------------------*/
		private void Formulas_SubmenuOpened(object sender, RoutedEventArgs e) {
			// Set the check states of the 'Formulas' drop down items.
			FormulaReferenceStyle frsReferenceStyle = m_txTextControl.FormulaReferenceStyle;
			m_miTable_Formulas_A1ReferenceStyle.IsChecked = frsReferenceStyle == FormulaReferenceStyle.A1;
			m_miTable_Formulas_R1C1ReferenceStyle.IsChecked = frsReferenceStyle == FormulaReferenceStyle.R1C1;
			m_miTable_Formulas_AutomaticCalculation.IsChecked = m_txTextControl.IsFormulaCalculationEnabled;
		}
	}
}
