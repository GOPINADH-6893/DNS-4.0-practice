using System;

public interface IDocument
{
    void Open();
}

public class PdfDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening PDF document");
    }
}


public class ExcelDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening Excel document");
    }
}

public class WordDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Opening Word document");
    }
}

public abstract class DocumentFactory
{
    public abstract IDocument CreateDocument();
}

public class WordDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new WordDocument();
    }
}

public class PdfDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new PdfDocument();
    }
}

public class ExcelDocumentFactory : DocumentFactory
{
    public override IDocument CreateDocument()
    {
        return new ExcelDocument();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Factory Method");

        DocumentFactory wordFactory = new WordDocumentFactory();
        IDocument word = wordFactory.CreateDocument();
        word.Open();

        DocumentFactory pdfFactory = new PdfDocumentFactory();
        IDocument pdf = pdfFactory.CreateDocument();
        pdf.Open();

        DocumentFactory excelFactory = new ExcelDocumentFactory();
        IDocument excel = excelFactory.CreateDocument();
        excel.Open();
    }
}