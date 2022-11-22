using System.Security.Cryptography;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Document = Spire.Doc.Document;
using Paragraph = Spire.Doc.Documents.Paragraph;
using Range = Microsoft.Office.Interop.Word.Range;
using Section = Spire.Doc.Section;

namespace wordAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void import_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { ValidateNames = true, Multiselect = false, Filter = "Word 97-2003|* .doc|Word Document|*.docx"})
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    object readOnly = false;
                    object visible = true;
                    object save = false;
                    object fileName = ofd.FileName;
                    object newTeamplate = false;
                    object docType = 0;
                    object missing = Type.Missing;

                    Microsoft.Office.Interop.Word._Document document;
                    Microsoft.Office.Interop.Word._Application application = new Microsoft.Office.Interop.Word.Application() { Visible = true};
                    document = application.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref visible, ref missing, ref missing, ref missing, ref missing);
                    document.ActiveWindow.Selection.WholeStory();
                    document.ActiveWindow.Selection.Copy();
                    IDataObject dataObject = Clipboard.GetDataObject();
                    textToImport.Rtf = dataObject.GetData(DataFormats.Rtf).ToString();
                    application.Quit(ref missing, ref missing, ref missing);
                }
            }
        }
       
        private void export_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox1.Text == " ")
            {
                MessageBox.Show("Numele fileului nu poate fi pustiu");
            }
            else
            {
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                word.Visible = true;
                Microsoft.Office.Interop.Word.Document doc = word.Documents.Add();
                Microsoft.Office.Interop.Word.Paragraph paragraph;
                paragraph = doc.Paragraphs.Add();
                paragraph.Range.Text = textToExport.Text;
                Range docRange = doc.Range();

                if(radioButton1.Checked)
                {
                    paragraph.Range.Font.Name = "Times New Roman";
                }
                if(radioButton2.Checked)
                {
                    paragraph.Range.Font.Name = "Arial";
                }
                if(radioButton3.Checked)
                {
                    paragraph.Range.Font.Name = "Comic Sans";
                }

                if (checkBox1.Checked)
                {
                    string imagePath = @"D:\projects\wordAPI\cSharpIcon.png";

                    object oMissing = System.Reflection.Missing.Value;
                    object oEndOfDoc = "\\endofdoc";

                    Microsoft.Office.Interop.Word.Range range = doc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    range.Application.ActiveDocument.InlineShapes.AddPicture(imagePath);
                }

                doc.SaveAs2(@"D:\" + textBox1.Text + ".doc");
                doc.Close();
                word.Quit();
                textToExport.Text = "";
                textBox1.Text = "";


            }
        }
    }
}