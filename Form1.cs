using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using ToolTip = System.Windows.Forms.ToolTip;

namespace Dz
{
    public partial class Form1 : Form
    {
        string filenamee;

        public Form1()
        {
            InitializeComponent();

            tabControl1.TabPages.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            openFileDialog1.FileName = @"data\Text2.txt";
            openFileDialog1.Filter =
                     "Текстові файли  (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Filter =
                     "Текстові файли (*.txt)|*.txt|All files (*.*)|*.*";

            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
            {
                toolStripComboBox1.Items.Add(family.Name);
            }

            for (int i = 8; i < 80; i += 2)
            {
                toolStripComboBox2.Items.Add(i);
            }

            tabControl1.AllowDrop = true;
        }

        private void createToolTip(Control controlForToolTip, string toolTipText)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.Active = true;
            toolTip.SetToolTip(controlForToolTip, toolTipText);
            toolTip.IsBalloon = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == String.Empty) return;
            try
            {
                var reader = new System.IO.StreamReader(openFileDialog1.FileName, Encoding.GetEncoding(1251));
                richTextBox1.Text = reader.ReadToEnd();
                reader.Close();
            }
            catch (System.IO.FileNotFoundException exception)
            {
                MessageBox.Show(exception.Message + "\nFile does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception exception)
            { 
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = openFileDialog1.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var writer = new System.IO.StreamWriter(
                    saveFileDialog1.FileName, false,
                                        System.Text.Encoding.GetEncoding(1251));
                    writer.Write(richTextBox1.Text);
                    writer.Close();
                }
                catch (Exception exception)
                { 
                    MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage page = new TabPage($"New Tab {tabControl1.TabPages.Count + 1}");
            page.UseVisualStyleBackColor = true;
            // 
            // richtextbox
            // 
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Location = new System.Drawing.Point(0, 0);
            richTextBox.Name = "richTextBox1";
            richTextBox.Size = new System.Drawing.Size(985, 459);
            richTextBox.TabIndex = 2;
            richTextBox.Text = "";
            page.Controls.Add(richTextBox);
            tabControl1.TabPages.Add(page);
  
           

            //if (richTextBox1.Text != string.Empty)
            //{
            //    DialogResult result = MessageBox.Show("Would you like to save your changes? Editor is not empty.", "Save Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //    if (result == DialogResult.Yes)
            //    {
            //        saveFileDialog1.ShowDialog();
            //        string file;
            //        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //        {
            //            string filename = saveFileDialog1.FileName;
            //            richTextBox1.SaveFile(filename, RichTextBoxStreamType.PlainText);
            //            file = Path.GetFileName(filename);
            //            MessageBox.Show("File " + file + " was saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }

            //        richTextBox1.ResetText();
            //        richTextBox1.Focus();
            //    }
            //    else if (result == DialogResult.No)
            //    {
            //        richTextBox1.ResetText();
            //        richTextBox1.Focus();
            //    }
            //}
            //else
            //{
            //    richTextBox1.ResetText();
            //    richTextBox1.Focus();
            //}
        }

        private RichTextBox GetActiveEditor()
        {
            TabPage tp = tabControl1.SelectedTab;
            RichTextBox rtb = null;
            if (tp != null)
            {
                rtb = tp.Controls[0] as RichTextBox;
            }
            return rtb;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveEditor().Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveEditor().Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveEditor().Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveEditor().SelectAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveEditor().SelectedText = "";
            GetActiveEditor().Focus();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveEditor().Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetActiveEditor().Redo();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string file;

            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;

                richTextBox1.SaveFile(fileName, RichTextBoxStreamType.PlainText);
            }
            file = Path.GetFileName(filenamee);    
            MessageBox.Show("File " + file + " was saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            fontDialog1.FontMustExist = true;    

            richTextBox1.Font = fontDialog1.Font;    
            richTextBox1.ForeColor = fontDialog1.Color;   

            foreach (Control containedControl in richTextBox1.Controls)
            {
                containedControl.Font = fontDialog1.Font;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                fontDialog1.ShowDialog();    
                System.Drawing.Font oldFont = this.Font;  

                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    fontDialog1_Apply(richTextBox1, new System.EventArgs());
                }
                else if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    this.Font = oldFont;

                    foreach (Control containedControl in richTextBox1.Controls)
                    {
                        containedControl.Font = oldFont;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Warning); // error
            }
        }

        private void colorOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                // this.BackColor = color.Color;
                richTextBox1.BackColor = color.Color;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.ShowDialog(); 
                string file;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog1.FileName;
                    GetActiveEditor().SaveFile(filename, RichTextBoxStreamType.PlainText);
                    file = Path.GetFileName(filename); 
                    MessageBox.Show("File " + file + " was saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            GetActiveEditor().Undo();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            GetActiveEditor().Redo();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();    
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filenamee = openFileDialog1.FileName;
             
                GetActiveEditor().LoadFile(filenamee, RichTextBoxStreamType.PlainText);    
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont == null)
            {
                richTextBox1.SelectionFont = new Font(toolStripComboBox1.Text, GetActiveEditor().Font.Size);
            }
            richTextBox1.SelectionFont = new Font(toolStripComboBox1.Text, GetActiveEditor().SelectionFont.Size);
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetActiveEditor().SelectionFont == null)
            {
                return;
            }
            GetActiveEditor().SelectionFont = new Font(GetActiveEditor().SelectionFont.FontFamily, Convert.ToInt32(toolStripComboBox2.Text), GetActiveEditor().SelectionFont.Style);
        }

        private void tabControl1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tabControl1_DragDrop(object sender, DragEventArgs e)
        {
            int i;
            string s;

            i = GetActiveEditor().SelectionStart;
            s = GetActiveEditor().Text.Substring(i);
            GetActiveEditor().Text = GetActiveEditor().Text.Substring(0, i);

            GetActiveEditor().Text = GetActiveEditor().Text +
               e.Data.GetData(DataFormats.Text).ToString();
            GetActiveEditor().Text = GetActiveEditor().Text + s;
        }

        private void increaseToolStripButton1_Click(object sender, EventArgs e)
        {
            string fontSizeNum = toolStripComboBox2.Text;           
            try
            {
                int size = Convert.ToInt32(fontSizeNum) + 1;  
                if (GetActiveEditor().SelectionFont == null)
                {
                    return;
                }
                GetActiveEditor().SelectionFont = new Font(GetActiveEditor().SelectionFont.FontFamily, size, GetActiveEditor().SelectionFont.Style);
                toolStripComboBox2.Text = size.ToString();   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }

        private void decreaseToolStripButton1_Click(object sender, EventArgs e)
        {
            string fontSizeNum = toolStripComboBox2.Text;           
            try
            {
                int size = Convert.ToInt32(fontSizeNum) - 1;  
                if (GetActiveEditor().SelectionFont == null)
                {
                    return;
                }
                GetActiveEditor().SelectionFont = new Font(GetActiveEditor().SelectionFont.FontFamily, size, GetActiveEditor().SelectionFont.Style);
                toolStripComboBox2.Text = size.ToString();   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }

        private void AToolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            KnownColor selectedColor;
            selectedColor = (KnownColor)System.Enum.Parse(typeof(KnownColor), e.ClickedItem.Text);    
            GetActiveEditor().SelectionColor = Color.FromKnownColor(selectedColor);   
        }

        private void eraserToolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripComboBox1.Text = "Font Family";
            toolStripComboBox2.Text = "Font Size";
            string pureText = GetActiveEditor().Text;    
            GetActiveEditor().Clear();    
            GetActiveEditor().ForeColor = Color.Black;    
            GetActiveEditor().Font = default(Font);   
            GetActiveEditor().Text = pureText;   
            rightAlignToolStripButton1.Checked = false;
            centerAlignToolStripButton1.Checked = false;
            leftAlignToolStripButton1.Checked = true;
        }

        private void rightAlignToolStripButton1_Click(object sender, EventArgs e)
        {
            leftAlignToolStripButton1.Checked = false;
            centerAlignToolStripButton1.Checked = false;

            if (rightAlignToolStripButton1.Checked == false)
            {
                rightAlignToolStripButton1.Checked = true;    
            }
            else if (rightAlignToolStripButton1.Checked == true)
            {
                rightAlignToolStripButton1.Checked = false;   
            }
            GetActiveEditor().SelectionAlignment = HorizontalAlignment.Right;    
        }

        private void leftAlignToolStripButton1_Click(object sender, EventArgs e)
        {
            centerAlignToolStripButton1.Checked = false;
            rightAlignToolStripButton1.Checked = false;
            if (leftAlignToolStripButton1.Checked == false)
            {
                leftAlignToolStripButton1.Checked = true;    
            }
            else if (leftAlignToolStripButton1.Checked == true)
            {
                leftAlignToolStripButton1.Checked = false;    
            }
            GetActiveEditor().SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerAlignToolStripButton1_Click(object sender, EventArgs e)
        {
            leftAlignToolStripButton1.Checked = false;
            rightAlignToolStripButton1.Checked = false;
            if (centerAlignToolStripButton1.Checked == false)
            {
                centerAlignToolStripButton1.Checked = true;    
            }
            else if (centerAlignToolStripButton1.Checked == true)
            {
                centerAlignToolStripButton1.Checked = false;    
            }
            GetActiveEditor().SelectionAlignment = HorizontalAlignment.Center;     
        }
    }
}