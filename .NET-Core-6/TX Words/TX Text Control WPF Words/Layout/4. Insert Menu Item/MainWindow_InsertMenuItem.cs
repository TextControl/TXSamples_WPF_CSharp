/*-------------------------------------------------------------------------------------------------------------
** MainWindow_InsertMenuItem.cs File
**
** Description: Provides methods to set the layout of the 'Insert' menu items.
**     
**
** copyright:		© Text Control GmbH
**-----------------------------------------------------------------------------------------------------------*/
namespace TXTextControl.Words {
	using TXTextControl.WPF;

	partial class MainWindow {
		/*-------------------------------------------------------------------------------------------------------------
		** M E M B E R   V A R I A B L E S
		**-----------------------------------------------------------------------------------------------------------*/
		private const int m_iDefaultEmptyWidth = 2000;  // The actual default empty width of a form field is 2000 twips when setting the default width integer flag '0'


		/*-------------------------------------------------------------------------------------------------------------
		** Shape Types
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** 'Lines' Item
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly Drawing.ShapeType[] m_rstShape_Lines = new Drawing.ShapeType[]{
			Drawing.ShapeType.Line, Drawing.ShapeType.BentConnector3, Drawing.ShapeType.CurvedConnector3
		};

		/*-------------------------------------------------------------------------------------------------------------
		** 'Rectangles' Items
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly Drawing.ShapeType[] m_rstShape_Rectangles = new Drawing.ShapeType[] {
			Drawing.ShapeType.Rectangle, Drawing.ShapeType.RoundRectangle, Drawing.ShapeType.Snip1Rectangle, Drawing.ShapeType.Snip2SameRectangle,
			Drawing.ShapeType.Snip2DiagonalRectangle, Drawing.ShapeType.SnipRoundRectangle, Drawing.ShapeType.Round1Rectangle, Drawing.ShapeType.Round2SameRectangle,
			Drawing.ShapeType.Round2DiagonalRectangle
		};

		/*-------------------------------------------------------------------------------------------------------------
		** 'Basic Shapes' Items
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly Drawing.ShapeType[] m_rstShape_BasicShapes = new Drawing.ShapeType[] {
			Drawing.ShapeType.Ellipse, Drawing.ShapeType.Triangle, Drawing.ShapeType.RightTriangle, Drawing.ShapeType.Parallelogram,
			Drawing.ShapeType.NonIsoscelesTrapezoid, Drawing.ShapeType.Diamond, Drawing.ShapeType.Pentagon, Drawing.ShapeType.Hexagon,
			Drawing.ShapeType.Heptagon, Drawing.ShapeType.Octagon, Drawing.ShapeType.Decagon, Drawing.ShapeType.Dodecagon,
			Drawing.ShapeType.Pie, Drawing.ShapeType.Chord, Drawing.ShapeType.Teardrop, Drawing.ShapeType.Frame,
			Drawing.ShapeType.HalfFrame, Drawing.ShapeType.Corner, Drawing.ShapeType.DiagonalStripe, Drawing.ShapeType.Plus,
			Drawing.ShapeType.Plaque, Drawing.ShapeType.Can, Drawing.ShapeType.Cube, Drawing.ShapeType.Bevel,
			Drawing.ShapeType.Donut, Drawing.ShapeType.NoSmoking, Drawing.ShapeType.BlockArc, Drawing.ShapeType.FoldedCorner,
			Drawing.ShapeType.SmileyFace, Drawing.ShapeType.Heart, Drawing.ShapeType.LightningBolt, Drawing.ShapeType.Sun,
			Drawing.ShapeType.Moon, Drawing.ShapeType.Cloud, Drawing.ShapeType.Arc,  Drawing.ShapeType.BracketPair,
			Drawing.ShapeType.BracePair,Drawing.ShapeType.LeftBracket, Drawing.ShapeType.RightBracket, Drawing.ShapeType.LeftBrace,
			Drawing.ShapeType.RightBrace

		};

		/*-------------------------------------------------------------------------------------------------------------
		** 'Block Arrows' Items
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly Drawing.ShapeType[] m_rstShape_BlockArrows = new Drawing.ShapeType[] {
			Drawing.ShapeType.RightArrow, Drawing.ShapeType.LeftArrow, Drawing.ShapeType.UpArrow, Drawing.ShapeType.DownArrow,
			Drawing.ShapeType.LeftRightArrow, Drawing.ShapeType.UpDownArrow, Drawing.ShapeType.QuadArrow, Drawing.ShapeType.LeftRightUpArrow,
			Drawing.ShapeType.BentArrow, Drawing.ShapeType.UTurnArrow, Drawing.ShapeType.LeftUpArrow, Drawing.ShapeType.BentUpArrow,
			Drawing.ShapeType.CurvedRightArrow, Drawing.ShapeType.CurvedLeftArrow, Drawing.ShapeType.CurvedUpArrow, Drawing.ShapeType.CurvedDownArrow,
			Drawing.ShapeType.StripedRightArrow, Drawing.ShapeType.NotchedRightArrow, Drawing.ShapeType.NotchedRightArrow, Drawing.ShapeType.Chevron,
			Drawing.ShapeType.RightArrowCallout, Drawing.ShapeType.DownArrowCallout, Drawing.ShapeType.LeftArrowCallout, Drawing.ShapeType.UpArrowCallout,
			Drawing.ShapeType.LeftRightArrowCallout, Drawing.ShapeType.QuadArrowCallout, Drawing.ShapeType.CircularArrow
		};


		/*-------------------------------------------------------------------------------------------------------------
		** 'Equation Shapes' Items
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly Drawing.ShapeType[] m_rstShape_EquationShapes = new Drawing.ShapeType[] {
			Drawing.ShapeType.MathPlus, Drawing.ShapeType.MathMinus, Drawing.ShapeType.MathMultiply, Drawing.ShapeType.MathDivide,
			Drawing.ShapeType.MathEqual, Drawing.ShapeType.MathNotEqual
		};

		/*-------------------------------------------------------------------------------------------------------------
		** 'Flowchart' Items
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly Drawing.ShapeType[] m_rstShape_Flowchart = new Drawing.ShapeType[] {
			Drawing.ShapeType.FlowChartProcess, Drawing.ShapeType.FlowChartAlternateProcess, Drawing.ShapeType.FlowChartDecision, Drawing.ShapeType.FlowChartInputOutput,
			Drawing.ShapeType.FlowChartPredefinedProcess, Drawing.ShapeType.FlowChartInternalStorage, Drawing.ShapeType.FlowChartDocument, Drawing.ShapeType.FlowChartMultidocument,
			Drawing.ShapeType.FlowChartTerminator, Drawing.ShapeType.FlowChartPreparation, Drawing.ShapeType.FlowChartManualInput, Drawing.ShapeType.FlowChartManualOperation,
			Drawing.ShapeType.FlowChartConnector, Drawing.ShapeType.FlowChartOffpageConnector, Drawing.ShapeType.FlowChartPunchedCard, Drawing.ShapeType.FlowChartPunchedTape,
			Drawing.ShapeType.FlowChartSummingJunction, Drawing.ShapeType.FlowChartOr, Drawing.ShapeType.FlowChartCollate, Drawing.ShapeType.FlowChartSort,
			Drawing.ShapeType.FlowChartExtract, Drawing.ShapeType.FlowChartMerge, Drawing.ShapeType.FlowChartOnlineStorage, Drawing.ShapeType.FlowChartDelay,
			Drawing.ShapeType.FlowChartMagneticTape, Drawing.ShapeType.FlowChartMagneticDisk, Drawing.ShapeType.FlowChartMagneticDrum, Drawing.ShapeType.FlowChartDisplay
		};

		/*-------------------------------------------------------------------------------------------------------------
		** 'Stars and Banners' Items
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly Drawing.ShapeType[] m_rstShape_StarsAndBanners = new Drawing.ShapeType[] {
			Drawing.ShapeType.IrregularSeal1,  Drawing.ShapeType.IrregularSeal2, Drawing.ShapeType.Star4, Drawing.ShapeType.Star5,
			Drawing.ShapeType.Star6, Drawing.ShapeType.Star7, Drawing.ShapeType.Star8, Drawing.ShapeType.Star10,
			Drawing.ShapeType.Star12, Drawing.ShapeType.Star16, Drawing.ShapeType.Star24, Drawing.ShapeType.Star32,
			Drawing.ShapeType.Ribbon2, Drawing.ShapeType.Ribbon, Drawing.ShapeType.EllipseRibbon2, Drawing.ShapeType.EllipseRibbon,
			Drawing.ShapeType.VerticalScroll, Drawing.ShapeType.HorizontalScroll, Drawing.ShapeType.Wave, Drawing.ShapeType.DoubleWave
		};

		/*-------------------------------------------------------------------------------------------------------------
		** 'Callouts' Items
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly Drawing.ShapeType[] m_rstShape_Callouts = new Drawing.ShapeType[] {
			Drawing.ShapeType.WedgeRectangleCallout, Drawing.ShapeType.WedgeRoundRectangleCallout, Drawing.ShapeType.WedgeEllipseCallout, Drawing.ShapeType.CloudCallout,
			Drawing.ShapeType.BorderCallout1,  Drawing.ShapeType.BorderCallout2,  Drawing.ShapeType.BorderCallout3,  Drawing.ShapeType.AccentCallout1,
			Drawing.ShapeType.AccentCallout2,  Drawing.ShapeType.AccentCallout3,  Drawing.ShapeType.Callout1, Drawing.ShapeType.Callout2,
			Drawing.ShapeType.Callout3, Drawing.ShapeType.AccentBorderCallout1, Drawing.ShapeType.AccentBorderCallout2, Drawing.ShapeType.AccentBorderCallout3
		};


		/*-------------------------------------------------------------------------------------------------------------
		** Barcode Types
		**-----------------------------------------------------------------------------------------------------------*/
		private readonly Barcode.BarcodeType[] m_rbtBarcodeTypes = new Barcode.BarcodeType[] {
			Barcode.BarcodeType.QRCode, Barcode.BarcodeType.Code128, Barcode.BarcodeType.EAN13, Barcode.BarcodeType.UPCA,
			Barcode.BarcodeType.EAN8, Barcode.BarcodeType.Interleaved2of5, Barcode.BarcodeType.Postnet, Barcode.BarcodeType.Code39,
			Barcode.BarcodeType.AztecCode, Barcode.BarcodeType.IntelligentMail, Barcode.BarcodeType.Datamatrix, Barcode.BarcodeType.PDF417,
			Barcode.BarcodeType.MicroPDF, Barcode.BarcodeType.Codabar, Barcode.BarcodeType.FourState,  Barcode.BarcodeType.Code11,
			Barcode.BarcodeType.Code93, Barcode.BarcodeType.PLANET, Barcode.BarcodeType.RoyalMail, Barcode.BarcodeType.Maxicode
		};



		/*-------------------------------------------------------------------------------------------------------------
		** M E T H O D S
		**-----------------------------------------------------------------------------------------------------------*/

		/*-------------------------------------------------------------------------------------------------------------
		** SetInsertItemsTexts Method
		**
		** Sets the texts of the 'Insert' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetInsertItemsTexts() {	
			// 'Shape'	
			SetItemText(m_miInsert_Shape_Lines);
			SetItemText(m_miInsert_Shape_Rectangles);
			SetItemText(m_miInsert_Shape_BasicShapes);
			SetItemText(m_miInsert_Shape_BlockArrows);
			SetItemText(m_miInsert_Shape_EquationShapes);
			SetItemText(m_miInsert_Shape_Flowchart);
			SetItemText(m_miInsert_Shape_StarsAndBanners);
			SetItemText(m_miInsert_Shape_Callouts);
			SetItemText(m_miInsert_Shape_DrawingCanvas);

			// 'Barcode'
			SetItemText(m_miInsert_Barcode);
		}

		/*-------------------------------------------------------------------------------------------------------------
		** SetInsertItemsImages Method
		**
		** Sets the images of the 'Insert' menu items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void SetInsertItemsImages() {
			// 'File...'
			SetItemImage(m_miInsert_File, RibbonInsertTab.RibbonItem.TXITEM_InsertFile.ToString());

			// 'Image...'
			SetItemImage(m_miInsert_Image, RibbonInsertTab.RibbonItem.TXITEM_InsertImage.ToString());

			// 'Text Frame'
			SetItemImage(m_miInsert_TextFrame, RibbonInsertTab.RibbonItem.TXITEM_InsertTextFrame.ToString());

			// 'Shape'	
			SetItemImage(m_miInsert_Shape, RibbonInsertTab.RibbonItem.TXITEM_InsertShape.ToString());
			SetItemImage(m_miInsert_Shape_Lines, ResourceProvider.ShapeItem.TXITEM_SHAPE_Line.ToString());
			SetItemImage(m_miInsert_Shape_Rectangles, ResourceProvider.ShapeItem.TXITEM_SHAPE_Rectangle.ToString());
			SetItemImage(m_miInsert_Shape_BasicShapes, ResourceProvider.ShapeItem.TXITEM_SHAPE_RightTriangle.ToString());
			SetItemImage(m_miInsert_Shape_BlockArrows, ResourceProvider.ShapeItem.TXITEM_SHAPE_RightArrow.ToString());
			SetItemImage(m_miInsert_Shape_EquationShapes, ResourceProvider.ShapeItem.TXITEM_SHAPE_Plus.ToString());
			SetItemImage(m_miInsert_Shape_Flowchart, ResourceProvider.ShapeItem.TXITEM_SHAPE_FlowChartMultidocument.ToString());
			SetItemImage(m_miInsert_Shape_StarsAndBanners, ResourceProvider.ShapeItem.TXITEM_SHAPE_Star7.ToString());
			SetItemImage(m_miInsert_Shape_Callouts, ResourceProvider.ShapeItem.TXITEM_SHAPE_WedgeRoundRectangleCallout.ToString());
			SetItemImage(m_miInsert_Shape_DrawingCanvas, RibbonInsertTab.RibbonDropDownItem.TXITEM_InsertDrawingCanvas.ToString());

			// 'Barcode'	
			SetItemImage(m_miInsert_Barcode, RibbonInsertTab.RibbonItem.TXITEM_InsertBarcode.ToString());

			// 'Header'
			SetItemImage(m_miInsert_Header, RibbonInsertTab.RibbonItem.TXITEM_InsertHeader.ToString());
			SetItemImage(m_miInsert_Header_Insert, RibbonInsertTab.RibbonDropDownItem.TXITEM_EditHeader.ToString());
			SetItemImage(m_miInsert_Header_Remove, RibbonInsertTab.RibbonDropDownItem.TXITEM_RemoveHeader.ToString());

			// 'Footer'
			SetItemImage(m_miInsert_Footer, RibbonInsertTab.RibbonItem.TXITEM_InsertFooter.ToString());
			SetItemImage(m_miInsert_Footer_Insert, RibbonInsertTab.RibbonDropDownItem.TXITEM_EditFooter.ToString());
			SetItemImage(m_miInsert_Footer_Remove, RibbonInsertTab.RibbonDropDownItem.TXITEM_RemoveFooter.ToString());

			// 'Page Number'
			SetItemImage(m_miInsert_PageNumber, RibbonInsertTab.RibbonItem.TXITEM_InsertPage.ToString());
			SetItemImage(m_miInsert_PageNumber_Insert, RibbonInsertTab.RibbonDropDownItem.TXITEM_InsertStandardPageNumber.ToString());
			SetItemImage(m_miInsert_PageNumber_Delete, RibbonInsertTab.RibbonDropDownItem.TXITEM_RemovePageNumber.ToString());

			// 'Form Fields'
			SetItemImage(m_miInsert_FormField, RibbonFormFieldsTab.RibbonItem.TXITEM_InsertComboBoxField.ToString());
			SetItemImage(m_miInsert_FormField_TextFormField, RibbonFormFieldsTab.RibbonItem.TXITEM_InsertTextFormField.ToString());
			SetItemImage(m_miInsert_FormField_CheckBox, RibbonFormFieldsTab.RibbonItem.TXITEM_InsertCheckBoxField.ToString());
			SetItemImage(m_miInsert_FormField_ComboBox, RibbonFormFieldsTab.RibbonItem.TXITEM_InsertComboBoxField.ToString());
			SetItemImage(m_miInsert_FormField_DropDownList, RibbonFormFieldsTab.RibbonItem.TXITEM_InsertDropDownListField.ToString());
			SetItemImage(m_miInsert_FormField_DateFormField, RibbonFormFieldsTab.RibbonItem.TXITEM_InsertDateFormField.ToString());
			SetItemImage(m_miInsert_FormField_Delete, RibbonFormFieldsTab.RibbonItem.TXITEM_DeleteFormField.ToString());

			// 'Symbol'
			SetItemImage(m_miInsert_Symbol, RibbonInsertTab.RibbonItem.TXITEM_InsertSymbol.ToString());

			// 'Hyperlink...'
			SetItemImage(m_miInsert_Hyperlink, RibbonInsertTab.RibbonItem.TXITEM_InsertHyperlink.ToString());

			// 'Bookmark...'
			SetItemImage(m_miInsert_Bookmark, RibbonInsertTab.RibbonItem.TXITEM_InsertBookmark.ToString());
			SetItemImage(m_miInsert_Bookmark_Insert, RibbonInsertTab.RibbonDropDownItem.TXITEM_EditBookmark.ToString());
			SetItemImage(m_miInsert_Bookmark_Delete, RibbonInsertTab.RibbonDropDownItem.TXITEM_DeleteBookmark.ToString());

			// 'Table of Contents'
			SetItemImage(m_miInsert_TableOfContents, RibbonReferencesTab.RibbonItem.TXITEM_InsertTableOfContents.ToString());
			SetItemImage(m_miInsert_TableOfContents_Insert, RibbonReferencesTab.RibbonItem.TXITEM_InsertTableOfContents.ToString());
			SetItemImage(m_miInsert_TableOfContents_Delete, RibbonReferencesTab.RibbonItem.TXITEM_DeleteTableOfContents.ToString());
			SetItemImage(m_miInsert_TableOfContents_Update, RibbonReferencesTab.RibbonItem.TXITEM_UpdateTableOfContents.ToString());

			// 'Columns'
			SetItemImage(m_miInsert_Columns, RibbonPageLayoutTab.RibbonItem.TXITEM_Columns.ToString());
			SetItemImage(m_miInsert_Columns_One, RibbonPageLayoutTab.RibbonDropDownItem.TXITEM_Columns_One.ToString());
			SetItemImage(m_miInsert_Columns_Two, RibbonPageLayoutTab.RibbonDropDownItem.TXITEM_Columns_Two.ToString());
			SetItemImage(m_miInsert_Columns_MoreColumns, RibbonPageLayoutTab.RibbonDropDownItem.TXITEM_Columns_MoreColumns.ToString());

			// 'Page Breaks'
			SetItemImage(m_miInsert_PageBreaks, RibbonPageLayoutTab.RibbonItem.TXITEM_Breaks.ToString());
			SetItemImage(m_miInsert_PageBreaks_Page, RibbonPageLayoutTab.RibbonItem.TXITEM_Breaks.ToString());
			SetItemImage(m_miInsert_PageBreaks_Column, RibbonPageLayoutTab.RibbonDropDownItem.TXITEM_Breaks_Column.ToString());
			SetItemImage(m_miInsert_PageBreaks_TextWrapping, RibbonPageLayoutTab.RibbonDropDownItem.TXITEM_Breaks_TextWrapping.ToString());

			// 'Section Breaks'
			SetItemImage(m_miInsert_SectionBreaks, RibbonPageLayoutTab.RibbonDropDownItem.TXITEM_Breaks_NextPage.ToString());
			SetItemImage(m_miInsert_SectionBreaks_NextPage, RibbonPageLayoutTab.RibbonDropDownItem.TXITEM_Breaks_NextPage.ToString());
			SetItemImage(m_miInsert_SectionBreaks_Continuous, RibbonPageLayoutTab.RibbonDropDownItem.TXITEM_Breaks_Continuous.ToString());
		}

		/*-------------------------------------------------------------------------------------------------------------
		** CreateShapeAndBarcodeItems Method
		**
		** Creates Shape and Barcode items.
		**-----------------------------------------------------------------------------------------------------------*/
		private void CreateShapeAndBarcodeItems() {
			// 'Shape'	
			foreach (Drawing.ShapeType shapeType in m_rstShape_Lines) {             // 'Lines'
				AddShapeItem(m_miInsert_Shape_Lines.Items, shapeType);
			}

			foreach (Drawing.ShapeType shapeType in m_rstShape_Rectangles) {        // 'Rectangles'
				AddShapeItem(m_miInsert_Shape_Rectangles.Items, shapeType);
			}

			foreach (Drawing.ShapeType shapeType in m_rstShape_BasicShapes) {       // 'Basic Shapes'
				AddShapeItem(m_miInsert_Shape_BasicShapes.Items, shapeType);
			}

			foreach (Drawing.ShapeType shapeType in m_rstShape_BlockArrows) {       // 'Block Arrows'
				AddShapeItem(m_miInsert_Shape_BlockArrows.Items, shapeType);
			}

			foreach (Drawing.ShapeType shapeType in m_rstShape_EquationShapes) {    // 'Equation Shapes'
				AddShapeItem(m_miInsert_Shape_EquationShapes.Items, shapeType);
			}

			foreach (Drawing.ShapeType shapeType in m_rstShape_Flowchart) {         // 'Flowchart'
				AddShapeItem(m_miInsert_Shape_Flowchart.Items, shapeType);
			}

			foreach (Drawing.ShapeType shapeType in m_rstShape_StarsAndBanners) {   // 'Stars and Banners'
				AddShapeItem(m_miInsert_Shape_StarsAndBanners.Items, shapeType);
			}

			foreach (Drawing.ShapeType shapeType in m_rstShape_Callouts) {          // 'Callouts'
				AddShapeItem(m_miInsert_Shape_Callouts.Items, shapeType);
			}

			// 'Barcode'	
			foreach (Barcode.BarcodeType barcodeType in m_rbtBarcodeTypes) {
				AddBarcodeItem(m_miInsert_Barcode.Items, barcodeType);
			}
		}
	}
}