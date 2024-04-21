using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

class Program
{
    static void Main()
    {
        // Create a new Excel file
        string filePath = "Sample.xlsx";
        using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
        {
            // Add a WorkbookPart to the document
            WorkbookPart workbookPart = document.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart
            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet();

            // Add a Sheet to the Workbook
            Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
            Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
            sheets.Append(sheet);

            // Get the SheetData of the WorksheetPart
            SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

            // Insert data into the Excel file
            Row row = new Row();
            Cell cell1 = new Cell() { DataType = CellValues.String, CellValue = new CellValue("Hello") };
            Cell cell2 = new Cell() { DataType = CellValues.String, CellValue = new CellValue("World") };
            row.Append(cell1, cell2);
            sheetData.Append(row);

            // Save the changes to the Excel file
            worksheetPart.Worksheet.Save();
            workbookPart.Workbook.Save();
        }

        // Display the path of the created Excel file
        Console.WriteLine("Excel file created at: " + filePath);
    }
}
