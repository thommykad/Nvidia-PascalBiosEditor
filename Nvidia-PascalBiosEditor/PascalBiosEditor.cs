using Nvidia_PascalBiosEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nvidia_PascalBiosEditor
{
    public partial class PascalBiosEditor : Form
    {
        private bool textChanging;

        private int type;

        private byte[] bios;

        private long checksumValue;

        private int bTDPIndex;

        private int mTDPIndex;

        private int t1bTDPIndex;

        private int t1mTDPIndex;

        private int t2bTDPIndex;

        private int t2mTDPIndex;

        private int t3bTDPIndex;

        private int t3mTDPIndex;

        private int tdpSlideIndex;

        private int throttleTIndex;

        private int bTempIndex;

        private int mTempIndex;

        private int tempSlideIndex;

        private int csIndex;

        private int bsbIndex;

        private bool dateFlag;

        private bool nameFlag;

        private bool verFlag;

        private bool boardFlag;

        private bool bTDPFlag;

        private bool mTDPFlag;

        private bool t1bTDPFlag;

        private bool t1mTDPFlag;

        private bool t2bTDPFlag;

        private bool t2mTDPFlag;

        private bool t3bTDPFlag;

        private bool t3mTDPFlag;

        private bool tdpSlideFlag;

        private bool throttleTFlag;

        private bool bTempFlag;

        private bool mTempFlag;

        private bool tempSlideFlag;

        private bool csFlag;

        private bool bsbFlag;

        private byte[] datePos;

        private byte[] namePos;

        private byte[] verPos;

        private byte[] boardPos;

        private byte[] bTDPPos;

        private byte[] mTDPPos;

        private byte[] t1bTDPPos;

        private byte[] t1mTDPPos;

        private byte[] t2bTDPPos;

        private byte[] t2mTDPPos;

        private byte[] t3bTDPPos;

        private byte[] t3mTDPPos;

        private byte[] tdpSlidePos;

        private byte[] throttleTPos;

        private byte[] bTempPos;

        private byte[] mTempPos;

        private byte[] tempSlidePos;

        private byte[] csPos;

        private byte[] bsbPos;

        private IContainer components;

        private Button openBIOS;

        private Button saveBIOS;

        private TextBox textBox6;

        private Label fileName;

        private Label board;

        private TextBox textBox7;

        private Label name;

        private TextBox textBox8;

        private Label version;

        private TextBox textBox9;

        private Label date;

        private TextBox textBox10;

        private TextBox textBox1;

        private Label checkSum;

        private TextBox textBox2;

        private Label model;

        private RadioButton tempFixed;

        private RadioButton tempAdjustable;

        private Panel panel7;

        private NumericUpDown numericUpDown5;

        private Label tempSlide;

        private Panel panel6;

        private NumericUpDown numericUpDown4;

        private Panel panel5;

        private Panel panel4;

        private NumericUpDown numericUpDown2;

        private Panel panel3;

        private NumericUpDown numericUpDown1;

        private Label maxTemp;

        private Label throttlingTemp;

        private Label tempTarget;

        private RadioButton tdpFixed;

        private RadioButton tdpAdjustable;

        private Label tdpSlide;

        private Label baseTDP;

        private Label maxTDP;

        private Panel panel11;

        private Label label3;

        private Button quickFix;

        private Panel panel13;

        private NumericUpDown numericUpDown6;

        private Label targetCS;

        private Label fixCS;

        private Panel panel14;

        private NumericUpDown numericUpDown8;

        private Panel panel12;

        private NumericUpDown numericUpDown7;

        private Label label2;

        private Label magicNum;

        private Panel panel16;

        private NumericUpDown numericUpDown10;

        private Panel panel17;

        private NumericUpDown numericUpDown9;

        private Label label6;

        private Label label5;

        private Panel panel18;

        private NumericUpDown numericUpDown12;

        private Panel panel19;

        private NumericUpDown numericUpDown11;

        private Label label8;

        private Label label7;

        private Label label9;

        private Label label10;

        private TextBox textBox3;

        private NumericUpDown numericUpDown3;

        private Panel panel20;

        private Panel panel22;

        private TextBox textBox5;

        private Label label13;

        private Panel panel21;

        private TextBox textBox4;

        private Label label12;

        private Label label11;

        private Button preset;

        private TextBox textBox12;

        private TextBox textBox11;

        private TextBox textBox13;

        private Label label14;

        private Label label15;
        private TabControl tabControl1;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private GroupBox groupBox1;
        private Panel panel1;
        private Label label16;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TextBox textBox14;

        public PascalBiosEditor()
        {
            this.InitializeComponent();
        }

        private void openBIOS_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open BIOS";
            openFileDialog.Filter = "BIOS Files (*.rom; *.bin)|*.rom;*.bin";
            bool flag = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.AssignPos(1, 1);
                this.type = 1;
                int num = 0;
                while (flag)
                {
                    this.ClearContent();
                    this.textBox6.Text = openFileDialog.SafeFileName;
                    DateTime lastWriteTime = File.GetLastWriteTime(openFileDialog.FileName);
                    this.textBox3.Text = lastWriteTime.ToString("ddd, MMM d, yyyy HH:mm:ss");
                    this.bios = File.ReadAllBytes(openFileDialog.FileName);
                    this.checksumValue = this.CalculateChecksum(this.bios);
                    int num2 = 0;
                    int num3 = 0;
                    for (int i = 0; i < this.bios.Length; i++)
                    {
                        if (num2 == 17)
                        {
                            break;
                        }
                        if (!this.bsbFlag && i >= 1)
                        {
                            this.bsbIndex = this.SetValue(this.bsbPos, this.bios, i, 3, 0, 0, null, 0);
                            if (this.bsbIndex != -1)
                            {
                                this.bsbFlag = true;
                                this.bsbIndex -= 2;
                                this.ShowTB11TB12();
                                num2++;
                                num3++;
                            }
                        }
                        if (!this.dateFlag && i >= 19 && this.SetText(this.datePos, this.bios, i, 11, 8, 0, this.textBox10) == 1)
                        {
                            this.dateFlag = true;
                            num2++;
                        }
                        if (!this.nameFlag && i >= 27 && this.SetText(this.namePos, this.bios, i, 4, 23, 4, this.textBox8) == 1)
                        {
                            string text = this.textBox8.Text;
                            if (text.Substring(0, 1).Equals("!"))
                            {
                                this.textBox8.Text = text.Substring(text.Length - 26);
                            }
                            this.nameFlag = true;
                            num2++;
                            if (this.textBox8.Text.Contains("GP106 E2914"))
                            {
                                this.textBox2.Text = "GTX 1060";
                            }
                            else if (this.textBox8.Text.Contains("GP106 PG410"))
                            {
                                this.textBox2.Text = "GTX 1060";
                                this.AssignPos(2, 1);
                                this.type = 2;
                            }
                            else if (this.textBox8.Text.Contains("GP104 E2914"))
                            {
                                this.textBox2.Text = "GTX 1070";
                            }
                            else if (this.textBox8.Text.Contains("GP104 PG411"))
                            {
                                this.textBox2.Text = "GTX 1070";
                                this.AssignPos(2, 1);
                                this.type = 2;
                            }
                            else if (this.textBox8.Text.Contains("GP104 E2915"))
                            {
                                this.textBox2.Text = "GTX 1080";
                            }
                            else if (this.textBox8.Text.Contains("GP104 PG413"))
                            {
                                this.textBox2.Text = "GTX 1080";
                                this.AssignPos(2, 2);
                                this.type = 2;
                            }
                            else if (this.textBox8.Text.Contains("GP102 PG611"))
                            {
                                if (this.textBox8.Text.Contains("SKU 0"))
                                {
                                    this.textBox2.Text = "Titan X Pascal";
                                    this.AssignPos(2, 4);
                                }
                                else if (this.textBox8.Text.Contains("SKU 30"))
                                {
                                    this.textBox2.Text = "Titan Xp";
                                    this.AssignPos(2, 5);
                                }
                                else if (this.textBox8.Text.Contains("SKU 50"))
                                {
                                    this.textBox2.Text = "GTX 1080 Ti";
                                    this.AssignPos(2, 3);
                                }
                                this.type = 2;
                            }
                            else
                            {
                                this.textBox2.Text = "Unsupported";
                                this.textBox2.BackColor = Color.AntiqueWhite;
                                this.textBox8.Text = "Unsupported";
                                this.textBox8.BackColor = Color.AntiqueWhite;
                                this.type = 0;
                            }
                        }           ///setposition
                        if (!this.verFlag && i >= 27 && this.SetText(this.verPos, this.bios, i, 13, 14, 0, this.textBox9) == 1)
                        {
                            this.verFlag = true;
                            num2++;
                        }
                        if (!this.boardFlag && i >= 11 && this.SetText(this.boardPos, this.bios, i, 5, 6, 5, this.textBox7) == 1)
                        {
                            this.boardFlag = true;
                            num2++;
                        }
                        if (!this.tdpSlideFlag && i >= 14)
                        {
                            this.tdpSlideIndex = this.SetRadio(this.tdpSlidePos, this.bios, i, 10, -10, 14, this.tdpAdjustable, this.tdpFixed, "02FFFFFFFFFFFF02");
                            if (this.tdpSlideIndex != -1)
                            {
                                this.tdpSlideFlag = true;
                                num2++;
                                num3++;
                            }
                        }
                        if (!this.bTDPFlag && i >= 11)
                        {
                            this.bTDPIndex = this.SetValue(this.bTDPPos, this.bios, i, 11, -15, 19, this.numericUpDown1, 1);
                            if (this.bTDPIndex != -1)
                            {
                                this.bTDPFlag = true;
                                num2++;
                                num3++;
                            }
                        }
                        if (!this.mTDPFlag && i >= 11)
                        {
                            this.mTDPIndex = this.SetValue(this.mTDPPos, this.bios, i, 11, -19, 23, this.numericUpDown2, 1);
                            if (this.mTDPIndex != -1)
                            {
                                this.mTDPFlag = true;
                                num2++;
                                num3++;
                            }
                        }
                        if (!this.t1bTDPFlag && i >= 11)
                        {
                            this.t1bTDPIndex = this.SetValue(this.t1bTDPPos, this.bios, i, 11, -15, 19, this.numericUpDown7, 1);
                            if (this.t1bTDPIndex != -1)
                            {
                                this.t1bTDPFlag = true;
                                num2++;
                            }
                        }
                        if (!this.t1mTDPFlag && i >= 11)
                        {
                            this.t1mTDPIndex = this.SetValue(this.t1mTDPPos, this.bios, i, 11, -19, 23, this.numericUpDown8, 1);
                            if (this.t1mTDPIndex != -1)
                            {
                                this.t1mTDPFlag = true;
                                num2++;
                            }
                        }
                        if (!this.t2bTDPFlag && i >= 11)
                        {
                            this.t2bTDPIndex = this.SetValue(this.t2bTDPPos, this.bios, i, 11, -15, 19, this.numericUpDown9, 1);
                            if (this.t2bTDPIndex != -1)
                            {
                                this.t2bTDPFlag = true;
                                num2++;
                            }
                        }
                        if (!this.t2mTDPFlag && i >= 11)
                        {
                            this.t2mTDPIndex = this.SetValue(this.t2mTDPPos, this.bios, i, 11, -19, 23, this.numericUpDown10, 1);
                            if (this.t2mTDPIndex != -1)
                            {
                                this.t2mTDPFlag = true;
                                num2++;
                            }
                        }
                        if (!this.t3bTDPFlag && i >= 11)
                        {
                            this.t3bTDPIndex = this.SetValue(this.t3bTDPPos, this.bios, i, 11, -15, 19, this.numericUpDown11, 1);
                            if (this.t3bTDPIndex != -1)
                            {
                                this.t3bTDPFlag = true;
                                num2++;
                            }
                        }
                        if (!this.t3mTDPFlag && i >= 11)
                        {
                            this.t3mTDPIndex = this.SetValue(this.t3mTDPPos, this.bios, i, 11, -19, 23, this.numericUpDown12, 1);
                            if (this.t3mTDPIndex != -1)
                            {
                                this.t3mTDPFlag = true;
                                num2++;
                            }
                        }
                        if (!this.tempSlideFlag && i >= 32)
                        {
                            this.tempSlideIndex = this.SetRadio(this.tempSlidePos, this.bios, i, 22, 10, -6, this.tempAdjustable, this.tempFixed, "FF00010001000100");
                            if (this.tempSlideIndex != -1)
                            {
                                this.tempSlideFlag = true;
                                num2++;
                                num3++;
                            }
                        }
                        if (!this.bTempFlag && i >= 32)
                        {
                            this.bTempIndex = this.SetValue(this.bTempPos, this.bios, i, 22, 6, -4, this.numericUpDown3, 2);
                            if (this.bTempIndex != -1)
                            {
                                this.bTempFlag = true;
                                num2++;
                                num3++;
                            }
                        }
                        if (!this.mTempFlag && i >= 32)
                        {
                            this.mTempIndex = this.SetValue(this.mTempPos, this.bios, i, 22, 2, 0, this.numericUpDown4, 2);
                            if (this.mTempIndex != -1)
                            {
                                this.mTempFlag = true;
                                num2++;
                                num3++;
                            }
                        }
                        if (!this.throttleTFlag && i >= 32)
                        {
                            this.throttleTIndex = this.SetValue(this.throttleTPos, this.bios, i, 22, 4, -2, this.numericUpDown5, 2);
                            if (this.throttleTIndex != -1)
                            {
                                this.throttleTFlag = true;
                                num2++;
                                num3++;
                            }
                        }
                        if (!this.csFlag && i >= 14)
                        {
                            this.csIndex = this.SetValue(this.csPos, this.bios, i, 6, 0, 0, null, 0);
                            if (this.csIndex != -1)
                            {
                                this.csFlag = true;
                                num2++;
                                num3++;
                            }
                        }
                    }
                    if (num3 == 9 && this.textBox8.Text.Equals("Unsupported"))
                    {
                        this.textBox2.Text = "Unknown";
                        this.textBox8.Text = "Unknown";
                    }
                    if (!this.dateFlag)
                    {
                        this.textBox10.Text = "Unknown";
                    }
                    if (this.t1bTDPIndex == -1 && this.t1mTDPIndex == -1)
                    {
                        this.numericUpDown7.Value = decimal.Zero;
                        this.numericUpDown8.Value = decimal.Zero;
                    }
                    if (this.t2bTDPIndex == -1 && this.t2mTDPIndex == -1)
                    {
                        this.numericUpDown9.Value = decimal.Zero;
                        this.numericUpDown10.Value = decimal.Zero;
                    }
                    if (this.t3bTDPIndex == -1 && this.t3mTDPIndex == -1)
                    {
                        this.numericUpDown11.Value = decimal.Zero;
                        this.numericUpDown12.Value = decimal.Zero;
                    }
                    if (num3 != 9)
                    {
                        switch (num)
                        {
                            case 3:
                                break;
                            case 0:
                                this.AssignPos(2, 1);
                                goto default;
                            case 1:
                                this.AssignPos(2, 2);
                                goto default;
                            case 2:
                                this.AssignPos(2, 3);
                                goto default;
                            default:
                                num++;
                                continue;
                        }
                    }
                    flag = false;
                }
            }
        }

        private void saveBIOS_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save BIOS";
            saveFileDialog.Filter = "BIOS Files (*.rom; *.bin)|*.rom;*.bin";
            if (this.csIndex == 0)
            {
                MessageBox.Show("Unhandled exception (empty)! BIOS cannot be saved.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (this.csIndex == -1)
            {
                MessageBox.Show("Unhandled exception (corrupted string section)! BIOS cannot be saved.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.bTDPIndex != -1 && this.mTDPIndex != -1)
                {
                    this.ModBIOSValue(this.bios, this.bTDPIndex, (int)this.numericUpDown1.Value, 1);
                    this.ModBIOSValue(this.bios, this.mTDPIndex, (int)this.numericUpDown2.Value, 1);
                }
                if (this.bTempIndex != -1 && this.mTempIndex != -1)
                {
                    this.ModBIOSValue(this.bios, this.bTempIndex, (int)this.numericUpDown3.Value, 2);
                    this.ModBIOSValue(this.bios, this.mTempIndex, (int)this.numericUpDown4.Value, 2);
                }
                if (this.throttleTIndex != -1)
                {
                    this.ModBIOSValue(this.bios, this.throttleTIndex, (int)this.numericUpDown5.Value, 2);
                }
                if (this.tdpSlideIndex != -1)
                {
                    this.ModBIOSSlide(this.bios, this.tdpSlideIndex, this.tdpAdjustable.Checked, this.tdpFixed.Checked, "02FFFFFFFFFFFF02");
                }
                if (this.tempSlideIndex != -1)
                {
                    this.ModBIOSSlide(this.bios, this.tempSlideIndex, this.tempAdjustable.Checked, this.tempFixed.Checked, "FF00010001000100");
                }
                if (this.t1bTDPIndex != -1 && this.t1mTDPIndex != -1)
                {
                    this.ModBIOSValue(this.bios, this.t1bTDPIndex, (int)this.numericUpDown7.Value, 1);
                    this.ModBIOSValue(this.bios, this.t1mTDPIndex, (int)this.numericUpDown8.Value, 1);
                }
                if (this.t2bTDPIndex != -1 && this.t2mTDPIndex != -1)
                {
                    this.ModBIOSValue(this.bios, this.t2bTDPIndex, (int)this.numericUpDown9.Value, 1);
                    this.ModBIOSValue(this.bios, this.t2mTDPIndex, (int)this.numericUpDown10.Value, 1);
                }
                if (this.t3bTDPIndex != -1 && this.t3mTDPIndex != -1)
                {
                    this.ModBIOSValue(this.bios, this.t3bTDPIndex, (int)this.numericUpDown11.Value, 1);
                    this.ModBIOSValue(this.bios, this.t3mTDPIndex, (int)this.numericUpDown12.Value, 1);
                }
                long num = this.CalculateChecksum(this.bios) - this.checksumValue;
                Console.WriteLine(num);
                if (this.CorrectCS(num, 1) == 1)
                {
                    this.BlackScreenFix();
                    File.WriteAllBytes(saveFileDialog.FileName, this.bios);
                }
                else
                {
                    MessageBox.Show("Unhandled exception (corrupted string section)! BIOS cannot be saved.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void quickFix_Click_1(object sender, EventArgs e)
        {
            if (this.csIndex == -1)
            {
                MessageBox.Show("Unhandled exception (corrupted string section)! Checksum cannot be corrected.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (this.csIndex != 0)
            {
                long num = (long)this.numericUpDown6.Value;
                long num2 = this.checksumValue - num;
                Console.WriteLine(num2);
                if (num2 == 0L)
                {
                    MessageBox.Show("Checksum offset is 0. No checksum correction needed.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (this.CorrectCS(num2, 2) == 1)
                {
                    MessageBox.Show("Offset " + num2 + ". Checksum has been corrected.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.checksumValue = num;
                }
                else
                {
                    MessageBox.Show("Unhandled exception (corrupted section/offset too large)! Checksum cannot be corrected.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void preset_Click_1(object sender, EventArgs e)
        {
            if (this.type == 1)
            {
                if (this.textBox2.Text.Equals("GTX 1060"))
                {
                    this.numericUpDown1.Value = 120000m;
                    this.numericUpDown2.Value = 140000m;
                }
                else if (this.textBox2.Text.Equals("GTX 1070"))
                {
                    this.numericUpDown1.Value = 151000m;
                    this.numericUpDown2.Value = 170000m;
                    this.numericUpDown7.Value = 16200m;
                    this.numericUpDown8.Value = 16200m;
                    this.numericUpDown9.Value = 242000m;
                    this.numericUpDown10.Value = 242000m;
                    this.numericUpDown11.Value = 137600m;
                    this.numericUpDown12.Value = 137600m;
                }
                else if (this.textBox2.Text.Equals("GTX 1080"))
                {
                    this.numericUpDown1.Value = 215000m;
                    this.numericUpDown2.Value = 258000m;
                    this.numericUpDown7.Value = 19200m;
                    this.numericUpDown8.Value = 19200m;
                }
                this.tdpAdjustable.Checked = true;
                this.tdpFixed.Checked = false;
                this.numericUpDown5.Value = 91m;
            }
            else if (this.type == 2)
            {
                MessageBox.Show("Preset is only for mobile cards.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        private int BlackScreenFix()
        {
            if (this.type == 1 && this.bsbIndex != 0)
            {
                byte[] array = new byte[this.bios.Length - this.bsbIndex];
                int num = this.bsbIndex;
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = this.bios[num];
                    num++;
                }
                this.bios = array;
                return 1;
            }
            return -1;
        }

        private int CorrectCS(long offset, int mode)
        {
            long num = (long)this.numericUpDown6.Value;
            int num2 = this.csIndex + 70;
            long num3 = offset;
            byte[] array = new byte[this.bios.Length];
            for (int i = 0; i < this.bios.Length; i++)
            {
                array[i] = this.bios[i];
            }
            for (int j = this.csIndex; j < num2; j++)
            {
                int num4 = this.bios[j];
                if (num3 >= num4)
                {
                    num3 -= num4;
                    this.bios[j] = 0;
                }
                else if (num3 > 0 && num3 < num4)
                {
                    this.bios[j] = (byte)(num4 - num3);
                    num3 = 0L;
                }
                else if (num3 < 0)
                {
                    int num5 = 255 - num4;
                    if (-num3 >= num5)
                    {
                        num3 += num5;
                        this.bios[j] = 255;
                    }
                    else if (-num3 < num5)
                    {
                        this.bios[j] = (byte)(-num3 + num4);
                        num3 = 0L;
                    }
                }
                if (num3 == 0L)
                {
                    break;
                }
            }
            long num6 = this.CalculateChecksum(this.bios);
            if (mode == 1 && num6 == this.checksumValue)
            {
                return 1;
            }
            if (mode == 2 && num6 == num)
            {
                return 1;
            }
            this.SetChecksum(this.checksumValue);
            this.bios = array;
            return -1;
        }

        private void ModBIOSValue(byte[] file, int index, int decValue, int type)
        {
            string text = "";
            switch (type)
            {
                case 1:
                    {
                        Console.WriteLine(decValue);
                        text = decValue.ToString("X2");
                        if (text.Length < 8)
                        {
                            text = "00000000" + text;
                            text = text.Substring(text.Length - 8);
                            Console.WriteLine(text);
                        }
                        byte[] array2 = this.HexToDecString(this.ReverseTDP(text));
                        int num2 = index;
                        for (int j = 0; j < array2.Length; j++)
                        {
                            this.bios[num2] = array2[j];
                            num2++;
                        }
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine(decValue);
                        text = (decValue * 32).ToString("X2");
                        if (text.Length < 4)
                        {
                            text = "0000" + text;
                            text = text.Substring(text.Length - 4);
                            Console.WriteLine(text);
                        }
                        byte[] array = this.HexToDecString(this.ReverseTemp(text));
                        int num = index;
                        for (int i = 0; i < array.Length; i++)
                        {
                            this.bios[num] = array[i];
                            num++;
                        }
                        break;
                    }
            }
        }

        private void ModBIOSSlide(byte[] file, int index, bool adj, bool fix, string signal)
        {
            if (adj && !fix)
            {
                byte[] array = this.HexToDecString(signal.Substring(0, 8));
                int num = index;
                for (int i = 0; i < array.Length; i++)
                {
                    this.bios[num] = array[i];
                    num++;
                }
            }
            else if (fix && !adj)
            {
                byte[] array2 = this.HexToDecString(signal.Substring(8, 8));
                int num2 = index;
                for (int j = 0; j < array2.Length; j++)
                {
                    this.bios[num2] = array2[j];
                    num2++;
                }
            }
        }

        private int SetText(byte[] dataPos, byte[] file, int i, int posLen, int dataLen, int shift, TextBox tb)
        {
            int num = i;
            for (int num2 = dataPos.Length - 1; num2 >= 0; num2--)
            {
                if (!dataPos[num2].Equals(file[num]))
                {
                    return -1;
                }
                num--;
                if (num == i - (posLen - 1))
                {
                    string text = "";
                    for (int j = num - dataLen; j < num + shift; j++)
                    {
                        text += file[j].ToString("X2");
                    }
                    tb.Text = this.HexToString(text);
                    return 1;
                }
            }
            return -1;
        }

        private int SetValue(byte[] dataPos, byte[] file, int i, int posLen, int dataLen, int shift, NumericUpDown nud, int type)
        {
            int num = i;
            for (int num2 = dataPos.Length - 1; num2 >= 0; num2--)
            {
                if (!dataPos[num2].Equals(file[num]))
                {
                    return -1;
                }
                num--;
                if (num == i - (posLen - 1))
                {
                    if (type == 0)
                    {
                        return num + posLen;
                    }
                    string text = "";
                    int num3 = num - dataLen;
                    for (int j = num3; j < num + shift; j++)
                    {
                        text += file[j].ToString("X2");
                    }
                    switch (type)
                    {
                        case 1:
                            {
                                int value2 = int.Parse(this.ReverseTDP(text), NumberStyles.HexNumber);
                                if ((decimal)value2 <= nud.Maximum)
                                {
                                    nud.Value = value2;
                                }
                                break;
                            }
                        case 2:
                            {
                                int value = int.Parse(this.ReverseTemp(text), NumberStyles.HexNumber) / 32;
                                if ((decimal)value <= nud.Maximum)
                                {
                                    nud.Value = value;
                                }
                                break;
                            }
                    }
                    return num3;
                }
            }
            return -1;
        }

        private int SetRadio(byte[] dataPos, byte[] file, int i, int posLen, int dataLen, int shift, RadioButton adj, RadioButton fix, string signal)
        {
            int num = i;
            for (int num2 = dataPos.Length - 1; num2 >= 0; num2--)
            {
                if (!dataPos[num2].Equals(file[num]))
                {
                    return -1;
                }
                num--;
                if (num == i - (posLen - 1))
                {
                    string text = "";
                    int num3 = num - dataLen;
                    for (int j = num3; j < num + shift; j++)
                    {
                        text += file[j].ToString("X2");
                    }
                    if (text.Equals(signal.Substring(0, 8)))
                    {
                        adj.Checked = true;
                    }
                    else if (text.Equals(signal.Substring(8, 8)))
                    {
                        fix.Checked = true;
                    }
                    return num3;
                }
            }
            return -1;
        }

        private void AssignPos(int type, int model)
        {
            switch (type)
            {
                case 1:
                    this.bTDPPos = this.HexToDecString("0000000001000000000100");
                    this.mTDPPos = this.HexToDecString("0000000001000000000100");
                    this.tdpSlidePos = this.HexToDecString("30252E14140014000A0F");
                    this.tempSlidePos = (this.mTempPos = (this.bTempPos = (this.throttleTPos = this.HexToDecString("C8001340FFFFFF7F00020000E0FFA0FF8813FF0000FF"))));
                    this.csPos = this.HexToDecString("504C45415345");
                    break;
                case 2:
                    this.bTDPPos = this.HexToDecString("0000000001000000002100");
                    this.mTDPPos = this.HexToDecString("0000000001000000002100");
                    if (model == 1 || model == 2)
                    {
                        this.tdpSlidePos = this.HexToDecString("30252E14140014000A0F");
                        this.tempSlidePos = (this.mTempPos = (this.bTempPos = (this.throttleTPos = this.HexToDecString("C80013C0FFFFFF7F20000000E0FFA0FF8813FF0000FF"))));
                    }
                    if (model == 2)
                    {
                        this.tdpSlidePos = this.HexToDecString("30262E14140014000A0F");
                    }
                    if (model == 3 || model == 4 || model == 5)
                    {
                        this.tdpSlidePos = this.HexToDecString("30262E14140014000A0F");
                        this.tempSlidePos = (this.mTempPos = (this.bTempPos = (this.throttleTPos = this.HexToDecString("C80012C0FFFFFF7F60000000FEFF3EFF8813FF0000FF"))));
                    }
                    if (model == 4)
                    {
                        this.tdpSlidePos = this.HexToDecString("30252E14140014000A0F");
                    }
                    if (model == 5)
                    {
                        this.bTDPPos = this.HexToDecString("0000000000000000002100");
                        this.mTDPPos = this.HexToDecString("0000000000000000002100");
                        this.tdpSlidePos = this.HexToDecString("30263614140014000A0F");
                        this.t1bTDPPos = this.HexToDecString("0000000000000000001503");
                        this.t1mTDPPos = this.HexToDecString("0000000000000000001503");
                        this.t3bTDPPos = this.HexToDecString("0000000000000000001A0B");
                        this.t3mTDPPos = this.HexToDecString("0000000000000000001A0B");
                    }
                    this.csPos = this.HexToDecString("504C45415345");
                    break;
            }
        }

        private byte[] HexToDecString(string hexString)
        {
            if (hexString != null && (hexString.Length & 1) != 1)
            {
                byte[] array = new byte[hexString.Length / 2];
                int num = 0;
                for (int i = 0; i < hexString.Length; i += 2)
                {
                    byte b = byte.Parse(hexString.Substring(i, 2), NumberStyles.HexNumber);
                    array[num] = b;
                    num++;
                }
                return array;
            }
            throw new ArgumentException();
        }

        private string HexToString(string hexString)
        {
            if (hexString != null && (hexString.Length & 1) != 1)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hexString.Length; i += 2)
                {
                    string value = hexString.Substring(i, 2);
                    stringBuilder.Append((char)Convert.ToByte(value, 16));
                }
                return stringBuilder.ToString();
            }
            throw new ArgumentException();
        }

        private string ReverseTDP(string s)
        {
            return s.Substring(6, 2) + s.Substring(4, 2) + s.Substring(2, 2) + s.Substring(0, 2);
        }

        private string Spacing(string s)
        {
            return s.Substring(0, 2) + " " + s.Substring(2, 2) + " " + s.Substring(4, 2) + " " + s.Substring(6, 2);
        }

        private string ReverseTemp(string s)
        {
            return s.Substring(2, 2) + s.Substring(0, 2);
        }

        private long CalculateChecksum(byte[] byteToCalculate)
        {
            long num = 0L;
            foreach (byte b in byteToCalculate)
            {
                num += b;
            }
            num &= 4294967295u;
            this.SetChecksum(num);
            return num;
        }

        private void SetChecksum(long checksum)
        {
            string text = checksum.ToString("X2");
            this.textBox1.Text = "0" + text + " (" + checksum + ")";
            this.numericUpDown6.Value = checksum;
            if (text.Substring(text.Length - 2).Equals("00"))
            {
                this.textBox1.BackColor = Color.LemonChiffon;
            }
            else
            {
                this.textBox1.BackColor = Color.White;
            }
        }

        private void ClearContent()
        {
            this.textBox2.BackColor = Color.LightYellow;
            this.textBox8.BackColor = Color.LightYellow;
            this.textBox2.Text = "";
            this.textBox7.Text = "";
            this.textBox8.Text = "";
            this.textBox9.Text = "";
            this.textBox10.Text = "";
            this.numericUpDown1.Value = decimal.Zero;
            this.numericUpDown2.Value = decimal.Zero;
            this.numericUpDown3.Value = decimal.Zero;
            this.numericUpDown4.Value = decimal.Zero;
            this.numericUpDown5.Value = decimal.Zero;
            this.tdpAdjustable.Checked = false;
            this.tdpFixed.Checked = false;
            this.tempAdjustable.Checked = false;
            this.tempFixed.Checked = false;
            this.dateFlag = false;
            this.nameFlag = false;
            this.verFlag = false;
            this.boardFlag = false;
            this.bTDPFlag = false;
            this.mTDPFlag = false;
            this.t1bTDPFlag = false;
            this.t1mTDPFlag = false;
            this.t2bTDPFlag = false;
            this.t2mTDPFlag = false;
            this.t3bTDPFlag = false;
            this.t3mTDPFlag = false;
            this.tdpSlideFlag = false;
            this.throttleTFlag = false;
            this.bTempFlag = false;
            this.mTempFlag = false;
            this.tempSlideFlag = false;
            this.csFlag = false;
            this.bsbFlag = false;
            this.bsbPos = this.HexToDecString("55AA");
            this.datePos = this.HexToDecString("0000000000000000001050");
            this.namePos = this.HexToDecString("42494F53");
            this.verPos = this.HexToDecString("200D0A00436F70797269676874");
            this.boardPos = this.HexToDecString("426F617264");
            this.t1bTDPPos = this.HexToDecString("0000000001000000001503");
            this.t1mTDPPos = this.HexToDecString("0000000001000000001503");
            this.t2bTDPPos = this.HexToDecString("000000000100000000100A");
            this.t2mTDPPos = this.HexToDecString("000000000100000000100A");
            this.t3bTDPPos = this.HexToDecString("0000000001000000001A0B");
            this.t3mTDPPos = this.HexToDecString("0000000001000000001A0B");
        }

        private void LockContent()
        {
            this.numericUpDown1.Visible = false;
            this.numericUpDown2.Visible = false;
            this.numericUpDown3.Visible = false;
            this.numericUpDown4.Visible = false;
            this.numericUpDown5.Visible = false;
        }

        private void UnlockContent()
        {
            this.numericUpDown1.Visible = true;
            this.numericUpDown2.Visible = true;
            this.numericUpDown3.Visible = true;
            this.numericUpDown4.Visible = true;
            this.numericUpDown5.Visible = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!this.textChanging)
            {
                this.textChanging = true;
                string text = this.textBox4.Text;
                if (text.Length > 0)
                {
                    long num = Convert.ToInt64(text, 16);
                    this.textBox5.Text = string.Concat(num);
                    while (text.Length < 8)
                    {
                        text = "0" + text;
                    }
                    this.textBox13.Text = this.Spacing(text);
                    this.textBox14.Text = this.Spacing(this.ReverseTDP(text));
                }
                else
                {
                    this.textBox5.Text = "";
                    this.textBox13.Text = "";
                    this.textBox14.Text = "";
                }
                this.textChanging = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!this.textChanging)
            {
                this.textChanging = true;
                string text = this.textBox5.Text;
                if (text.Length > 0)
                {
                    string text2 = Convert.ToInt64(text).ToString("X2");
                    while (text2.Length < 8)
                    {
                        text2 = "0" + text2;
                    }
                    this.textBox4.Text = text2;
                    this.textBox13.Text = this.Spacing(text2);
                    this.textBox14.Text = this.Spacing(this.ReverseTDP(text2));
                }
                else
                {
                    this.textBox4.Text = "";
                    this.textBox13.Text = "";
                    this.textBox14.Text = "";
                }
                this.textChanging = false;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Regex.IsMatch(e.KeyChar.ToString(), "[^0-9^+^a-f^+^A-F^+^\\b^]"))
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Regex.IsMatch(e.KeyChar.ToString(), "[^0-9^+^\\b^]"))
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
            }
        }

        private void ShowTB11TB12()
        {
            string text = this.bsbIndex.ToString("X2");
            while (text.Length < 8)
            {
                text = "0" + text;
            }
            this.textBox11.Text = text;
            this.textBox12.Text = this.bios[0].ToString("X2") + this.bios[1].ToString("X2");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.openBIOS = new System.Windows.Forms.Button();
            this.saveBIOS = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.fileName = new System.Windows.Forms.Label();
            this.board = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.version = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.date = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.model = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkSum = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tempFixed = new System.Windows.Forms.RadioButton();
            this.tempAdjustable = new System.Windows.Forms.RadioButton();
            this.panel7 = new System.Windows.Forms.Panel();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.tempSlide = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.panel5 = new System.Windows.Forms.Panel();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.maxTemp = new System.Windows.Forms.Label();
            this.throttlingTemp = new System.Windows.Forms.Label();
            this.tempTarget = new System.Windows.Forms.Label();
            this.tdpFixed = new System.Windows.Forms.RadioButton();
            this.tdpAdjustable = new System.Windows.Forms.RadioButton();
            this.tdpSlide = new System.Windows.Forms.Label();
            this.baseTDP = new System.Windows.Forms.Label();
            this.maxTDP = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.panel18 = new System.Windows.Forms.Panel();
            this.numericUpDown12 = new System.Windows.Forms.NumericUpDown();
            this.panel16 = new System.Windows.Forms.Panel();
            this.numericUpDown10 = new System.Windows.Forms.NumericUpDown();
            this.panel19 = new System.Windows.Forms.Panel();
            this.numericUpDown11 = new System.Windows.Forms.NumericUpDown();
            this.panel14 = new System.Windows.Forms.Panel();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.numericUpDown9 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.magicNum = new System.Windows.Forms.Label();
            this.panel20 = new System.Windows.Forms.Panel();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.panel22 = new System.Windows.Forms.Panel();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel21 = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.quickFix = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.targetCS = new System.Windows.Forms.Label();
            this.fixCS = new System.Windows.Forms.Label();
            this.preset = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).BeginInit();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).BeginInit();
            this.panel19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).BeginInit();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            this.panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).BeginInit();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            this.panel20.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // openBIOS
            // 
            this.openBIOS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openBIOS.Location = new System.Drawing.Point(8, 6);
            this.openBIOS.Margin = new System.Windows.Forms.Padding(1);
            this.openBIOS.Name = "openBIOS";
            this.openBIOS.Size = new System.Drawing.Size(73, 21);
            this.openBIOS.TabIndex = 0;
            this.openBIOS.Text = "Open BIOS";
            this.openBIOS.UseVisualStyleBackColor = true;
            this.openBIOS.Click += new System.EventHandler(this.openBIOS_Click_1);
            // 
            // saveBIOS
            // 
            this.saveBIOS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBIOS.Location = new System.Drawing.Point(247, 6);
            this.saveBIOS.Margin = new System.Windows.Forms.Padding(1);
            this.saveBIOS.Name = "saveBIOS";
            this.saveBIOS.Size = new System.Drawing.Size(73, 21);
            this.saveBIOS.TabIndex = 2;
            this.saveBIOS.Text = "Save BIOS";
            this.saveBIOS.UseVisualStyleBackColor = true;
            this.saveBIOS.Click += new System.EventHandler(this.saveBIOS_Click_1);
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Info;
            this.textBox6.Font = new System.Drawing.Font("Courier New", 7.875F, System.Drawing.FontStyle.Bold);
            this.textBox6.Location = new System.Drawing.Point(141, 28);
            this.textBox6.Margin = new System.Windows.Forms.Padding(1);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(174, 19);
            this.textBox6.TabIndex = 14;
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fileName
            // 
            this.fileName.AutoSize = true;
            this.fileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileName.Location = new System.Drawing.Point(13, 28);
            this.fileName.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(72, 16);
            this.fileName.TabIndex = 15;
            this.fileName.Text = "Filename";
            // 
            // board
            // 
            this.board.AutoSize = true;
            this.board.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.board.Location = new System.Drawing.Point(33, 94);
            this.board.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(50, 16);
            this.board.TabIndex = 17;
            this.board.Text = "Board";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Info;
            this.textBox7.Location = new System.Drawing.Point(141, 95);
            this.textBox7.Margin = new System.Windows.Forms.Padding(1);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(175, 20);
            this.textBox7.TabIndex = 18;
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(7, 7);
            this.name.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(88, 16);
            this.name.TabIndex = 19;
            this.name.Text = "BIOS Name";
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.Info;
            this.textBox8.Font = new System.Drawing.Font("Courier New", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(141, 7);
            this.textBox8.Margin = new System.Windows.Forms.Padding(1);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(263, 19);
            this.textBox8.TabIndex = 20;
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version.Location = new System.Drawing.Point(20, 72);
            this.version.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(61, 16);
            this.version.TabIndex = 21;
            this.version.Text = "Version";
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.SystemColors.Info;
            this.textBox9.Location = new System.Drawing.Point(141, 73);
            this.textBox9.Margin = new System.Windows.Forms.Padding(1);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(174, 20);
            this.textBox9.TabIndex = 22;
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // date
            // 
            this.date.AutoSize = true;
            this.date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date.Location = new System.Drawing.Point(39, 138);
            this.date.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(41, 16);
            this.date.TabIndex = 23;
            this.date.Text = "Date";
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.SystemColors.Info;
            this.textBox10.Location = new System.Drawing.Point(141, 139);
            this.textBox10.Margin = new System.Windows.Forms.Padding(1);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(175, 20);
            this.textBox10.TabIndex = 24;
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox12
            // 
            this.textBox12.Enabled = false;
            this.textBox12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox12.Location = new System.Drawing.Point(319, 162);
            this.textBox12.Margin = new System.Windows.Forms.Padding(2);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(82, 20);
            this.textBox12.TabIndex = 32;
            this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox11
            // 
            this.textBox11.Enabled = false;
            this.textBox11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox11.Location = new System.Drawing.Point(319, 138);
            this.textBox11.Margin = new System.Windows.Forms.Padding(2);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(82, 20);
            this.textBox11.TabIndex = 31;
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Info;
            this.textBox3.Font = new System.Drawing.Font("Courier New", 7.875F, System.Drawing.FontStyle.Bold);
            this.textBox3.Location = new System.Drawing.Point(142, 50);
            this.textBox3.Margin = new System.Windows.Forms.Padding(1);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(173, 19);
            this.textBox3.TabIndex = 30;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(13, 48);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 16);
            this.label10.TabIndex = 29;
            this.label10.Text = "Modified";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Info;
            this.textBox2.Location = new System.Drawing.Point(141, 117);
            this.textBox2.Margin = new System.Windows.Forms.Padding(1);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(175, 20);
            this.textBox2.TabIndex = 28;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // model
            // 
            this.model.AutoSize = true;
            this.model.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.model.Location = new System.Drawing.Point(33, 116);
            this.model.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.model.Name = "model";
            this.model.Size = new System.Drawing.Size(51, 16);
            this.model.TabIndex = 27;
            this.model.Text = "Model";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.Location = new System.Drawing.Point(141, 162);
            this.textBox1.Margin = new System.Windows.Forms.Padding(1);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(175, 20);
            this.textBox1.TabIndex = 26;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkSum
            // 
            this.checkSum.AutoSize = true;
            this.checkSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkSum.Location = new System.Drawing.Point(7, 166);
            this.checkSum.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.checkSum.Name = "checkSum";
            this.checkSum.Size = new System.Drawing.Size(109, 13);
            this.checkSum.TabIndex = 25;
            this.checkSum.Text = "Checksum (32 bit)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(139, 124);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Thermal Limits Control";
            // 
            // tempFixed
            // 
            this.tempFixed.AutoSize = true;
            this.tempFixed.Location = new System.Drawing.Point(301, 205);
            this.tempFixed.Margin = new System.Windows.Forms.Padding(1);
            this.tempFixed.Name = "tempFixed";
            this.tempFixed.Size = new System.Drawing.Size(55, 17);
            this.tempFixed.TabIndex = 5;
            this.tempFixed.Text = "Fixed";
            this.tempFixed.UseVisualStyleBackColor = true;
            // 
            // tempAdjustable
            // 
            this.tempAdjustable.AutoSize = true;
            this.tempAdjustable.Location = new System.Drawing.Point(301, 175);
            this.tempAdjustable.Margin = new System.Windows.Forms.Padding(1);
            this.tempAdjustable.Name = "tempAdjustable";
            this.tempAdjustable.Size = new System.Drawing.Size(84, 17);
            this.tempAdjustable.TabIndex = 4;
            this.tempAdjustable.Text = "Adjustable";
            this.tempAdjustable.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.numericUpDown5);
            this.panel7.Location = new System.Drawing.Point(301, 147);
            this.panel7.Margin = new System.Windows.Forms.Padding(1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(63, 17);
            this.panel7.TabIndex = 31;
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown5.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold);
            this.numericUpDown5.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown5.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            105,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown5.TabIndex = 33;
            // 
            // tempSlide
            // 
            this.tempSlide.AutoSize = true;
            this.tempSlide.Location = new System.Drawing.Point(209, 175);
            this.tempSlide.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.tempSlide.Name = "tempSlide";
            this.tempSlide.Size = new System.Drawing.Size(78, 13);
            this.tempSlide.TabIndex = 3;
            this.tempSlide.Text = "Temp Slider:";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.numericUpDown4);
            this.panel6.Location = new System.Drawing.Point(131, 207);
            this.panel6.Margin = new System.Windows.Forms.Padding(1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(63, 17);
            this.panel6.TabIndex = 30;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown4.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold);
            this.numericUpDown4.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown4.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            105,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown4.TabIndex = 32;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.numericUpDown3);
            this.panel5.Location = new System.Drawing.Point(131, 175);
            this.panel5.Margin = new System.Windows.Forms.Padding(1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(63, 17);
            this.panel5.TabIndex = 29;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown3.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold);
            this.numericUpDown3.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown3.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            105,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown3.TabIndex = 31;
            // 
            // maxTemp
            // 
            this.maxTemp.AutoSize = true;
            this.maxTemp.Location = new System.Drawing.Point(53, 208);
            this.maxTemp.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.maxTemp.Name = "maxTemp";
            this.maxTemp.Size = new System.Drawing.Size(69, 13);
            this.maxTemp.TabIndex = 9;
            this.maxTemp.Text = "Max Temp:";
            // 
            // throttlingTemp
            // 
            this.throttlingTemp.AutoSize = true;
            this.throttlingTemp.Location = new System.Drawing.Point(22, 147);
            this.throttlingTemp.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.throttlingTemp.Name = "throttlingTemp";
            this.throttlingTemp.Size = new System.Drawing.Size(221, 13);
            this.throttlingTemp.TabIndex = 7;
            this.throttlingTemp.Text = "Throttling Temp (affect boost clocks):";
            // 
            // tempTarget
            // 
            this.tempTarget.AutoSize = true;
            this.tempTarget.Location = new System.Drawing.Point(40, 175);
            this.tempTarget.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.tempTarget.Name = "tempTarget";
            this.tempTarget.Size = new System.Drawing.Size(80, 13);
            this.tempTarget.TabIndex = 8;
            this.tempTarget.Text = "Rated Temp:";
            // 
            // tdpFixed
            // 
            this.tdpFixed.AutoSize = true;
            this.tdpFixed.Location = new System.Drawing.Point(270, 42);
            this.tdpFixed.Margin = new System.Windows.Forms.Padding(1);
            this.tdpFixed.Name = "tdpFixed";
            this.tdpFixed.Size = new System.Drawing.Size(55, 17);
            this.tdpFixed.TabIndex = 2;
            this.tdpFixed.Text = "Fixed";
            this.tdpFixed.UseVisualStyleBackColor = true;
            // 
            // tdpAdjustable
            // 
            this.tdpAdjustable.AutoSize = true;
            this.tdpAdjustable.Location = new System.Drawing.Point(270, 11);
            this.tdpAdjustable.Margin = new System.Windows.Forms.Padding(1);
            this.tdpAdjustable.Name = "tdpAdjustable";
            this.tdpAdjustable.Size = new System.Drawing.Size(84, 17);
            this.tdpAdjustable.TabIndex = 1;
            this.tdpAdjustable.Text = "Adjustable";
            this.tdpAdjustable.UseVisualStyleBackColor = true;
            // 
            // tdpSlide
            // 
            this.tdpSlide.AutoSize = true;
            this.tdpSlide.Location = new System.Drawing.Point(172, 11);
            this.tdpSlide.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.tdpSlide.Name = "tdpSlide";
            this.tdpSlide.Size = new System.Drawing.Size(82, 13);
            this.tdpSlide.TabIndex = 0;
            this.tdpSlide.Text = "Power Slider:";
            // 
            // baseTDP
            // 
            this.baseTDP.AutoSize = true;
            this.baseTDP.Location = new System.Drawing.Point(1, 14);
            this.baseTDP.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.baseTDP.Name = "baseTDP";
            this.baseTDP.Size = new System.Drawing.Size(81, 13);
            this.baseTDP.TabIndex = 5;
            this.baseTDP.Text = "Target (mW):";
            // 
            // maxTDP
            // 
            this.maxTDP.AutoSize = true;
            this.maxTDP.Location = new System.Drawing.Point(8, 47);
            this.maxTDP.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.maxTDP.Name = "maxTDP";
            this.maxTDP.Size = new System.Drawing.Size(70, 13);
            this.maxTDP.TabIndex = 6;
            this.maxTDP.Text = "Limit (mW):";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.numericUpDown2);
            this.panel4.Location = new System.Drawing.Point(99, 45);
            this.panel4.Margin = new System.Windows.Forms.Padding(1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(63, 17);
            this.panel4.TabIndex = 1;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown2.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold);
            this.numericUpDown2.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            400000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown2.TabIndex = 30;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.numericUpDown1);
            this.panel3.Location = new System.Drawing.Point(99, 13);
            this.panel3.Margin = new System.Windows.Forms.Padding(1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(63, 17);
            this.panel3.TabIndex = 0;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown1.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            400000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown1.TabIndex = 29;
            // 
            // panel18
            // 
            this.panel18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel18.Controls.Add(this.numericUpDown12);
            this.panel18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel18.Location = new System.Drawing.Point(148, 157);
            this.panel18.Margin = new System.Windows.Forms.Padding(1);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(63, 17);
            this.panel18.TabIndex = 43;
            // 
            // numericUpDown12
            // 
            this.numericUpDown12.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown12.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold);
            this.numericUpDown12.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown12.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown12.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown12.Maximum = new decimal(new int[] {
            330000,
            0,
            0,
            0});
            this.numericUpDown12.Name = "numericUpDown12";
            this.numericUpDown12.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown12.TabIndex = 31;
            // 
            // panel16
            // 
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel16.Controls.Add(this.numericUpDown10);
            this.panel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel16.Location = new System.Drawing.Point(148, 97);
            this.panel16.Margin = new System.Windows.Forms.Padding(1);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(63, 17);
            this.panel16.TabIndex = 39;
            // 
            // numericUpDown10
            // 
            this.numericUpDown10.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown10.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold);
            this.numericUpDown10.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown10.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown10.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown10.Maximum = new decimal(new int[] {
            330000,
            0,
            0,
            0});
            this.numericUpDown10.Name = "numericUpDown10";
            this.numericUpDown10.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown10.TabIndex = 31;
            // 
            // panel19
            // 
            this.panel19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel19.Controls.Add(this.numericUpDown11);
            this.panel19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel19.Location = new System.Drawing.Point(148, 132);
            this.panel19.Margin = new System.Windows.Forms.Padding(1);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(63, 17);
            this.panel19.TabIndex = 41;
            // 
            // numericUpDown11
            // 
            this.numericUpDown11.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown11.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold);
            this.numericUpDown11.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown11.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown11.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown11.Maximum = new decimal(new int[] {
            330000,
            0,
            0,
            0});
            this.numericUpDown11.Name = "numericUpDown11";
            this.numericUpDown11.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown11.TabIndex = 31;
            // 
            // panel14
            // 
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel14.Controls.Add(this.numericUpDown8);
            this.panel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel14.Location = new System.Drawing.Point(148, 36);
            this.panel14.Margin = new System.Windows.Forms.Padding(1);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(63, 17);
            this.panel14.TabIndex = 35;
            // 
            // numericUpDown8
            // 
            this.numericUpDown8.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown8.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold);
            this.numericUpDown8.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown8.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown8.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown8.Maximum = new decimal(new int[] {
            330000,
            0,
            0,
            0});
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown8.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1, 157);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 16);
            this.label8.TabIndex = 44;
            this.label8.Text = "Max Value (mW): ";
            // 
            // panel17
            // 
            this.panel17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel17.Controls.Add(this.numericUpDown9);
            this.panel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel17.Location = new System.Drawing.Point(148, 72);
            this.panel17.Margin = new System.Windows.Forms.Padding(1);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(63, 17);
            this.panel17.TabIndex = 37;
            // 
            // numericUpDown9
            // 
            this.numericUpDown9.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown9.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold);
            this.numericUpDown9.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown9.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown9.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown9.Maximum = new decimal(new int[] {
            330000,
            0,
            0,
            0});
            this.numericUpDown9.Name = "numericUpDown9";
            this.numericUpDown9.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown9.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1, 132);
            this.label7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 16);
            this.label7.TabIndex = 42;
            this.label7.Text = "Def Value (mW): ";
            // 
            // panel12
            // 
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel12.Controls.Add(this.numericUpDown7);
            this.panel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel12.Location = new System.Drawing.Point(148, 11);
            this.panel12.Margin = new System.Windows.Forms.Padding(1);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(63, 17);
            this.panel12.TabIndex = 33;
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown7.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold);
            this.numericUpDown7.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown7.Location = new System.Drawing.Point(-1, -1);
            this.numericUpDown7.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown7.Maximum = new decimal(new int[] {
            330000,
            0,
            0,
            0});
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown7.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1, 97);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 16);
            this.label6.TabIndex = 40;
            this.label6.Text = "Max Value (mW): ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "Max Value (mW): ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 16);
            this.label5.TabIndex = 38;
            this.label5.Text = "Def Value (mW): ";
            // 
            // magicNum
            // 
            this.magicNum.AutoSize = true;
            this.magicNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.magicNum.Location = new System.Drawing.Point(1, 11);
            this.magicNum.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.magicNum.Name = "magicNum";
            this.magicNum.Size = new System.Drawing.Size(124, 16);
            this.magicNum.TabIndex = 34;
            this.magicNum.Text = "Def Value (mW): ";
            // 
            // panel20
            // 
            this.panel20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel20.Controls.Add(this.textBox14);
            this.panel20.Controls.Add(this.label15);
            this.panel20.Controls.Add(this.label14);
            this.panel20.Controls.Add(this.textBox13);
            this.panel20.Controls.Add(this.panel22);
            this.panel20.Controls.Add(this.label13);
            this.panel20.Controls.Add(this.panel21);
            this.panel20.Controls.Add(this.label12);
            this.panel20.Location = new System.Drawing.Point(1, 102);
            this.panel20.Margin = new System.Windows.Forms.Padding(1);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(379, 88);
            this.panel20.TabIndex = 34;
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.Color.Ivory;
            this.textBox14.Enabled = false;
            this.textBox14.Location = new System.Drawing.Point(188, 59);
            this.textBox14.Margin = new System.Windows.Forms.Padding(2);
            this.textBox14.MaxLength = 9;
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(178, 20);
            this.textBox14.TabIndex = 41;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(186, 45);
            this.label15.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(85, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "Little-endian: ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(2, 45);
            this.label14.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "Big-endian: ";
            // 
            // textBox13
            // 
            this.textBox13.BackColor = System.Drawing.Color.Ivory;
            this.textBox13.Enabled = false;
            this.textBox13.Location = new System.Drawing.Point(4, 59);
            this.textBox13.Margin = new System.Windows.Forms.Padding(2);
            this.textBox13.MaxLength = 9;
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(178, 20);
            this.textBox13.TabIndex = 38;
            // 
            // panel22
            // 
            this.panel22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel22.Controls.Add(this.textBox5);
            this.panel22.Location = new System.Drawing.Point(188, 20);
            this.panel22.Margin = new System.Windows.Forms.Padding(1);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(178, 18);
            this.panel22.TabIndex = 32;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.Ivory;
            this.textBox5.Location = new System.Drawing.Point(-1, -1);
            this.textBox5.Margin = new System.Windows.Forms.Padding(2);
            this.textBox5.MaxLength = 9;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(178, 20);
            this.textBox5.TabIndex = 37;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(186, 5);
            this.label13.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "Decimal Value:";
            // 
            // panel21
            // 
            this.panel21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel21.Controls.Add(this.textBox4);
            this.panel21.Location = new System.Drawing.Point(4, 20);
            this.panel21.Margin = new System.Windows.Forms.Padding(1);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(178, 18);
            this.panel21.TabIndex = 31;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.Ivory;
            this.textBox4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox4.Location = new System.Drawing.Point(-1, -1);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.MaxLength = 8;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(178, 20);
            this.textBox4.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 5);
            this.label12.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(117, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "Hex Value (32 bit): ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(145, 80);
            this.label11.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 16);
            this.label11.TabIndex = 35;
            this.label11.Text = "Base Converter";
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel11.Controls.Add(this.label3);
            this.panel11.Controls.Add(this.quickFix);
            this.panel11.Controls.Add(this.panel13);
            this.panel11.Controls.Add(this.targetCS);
            this.panel11.Location = new System.Drawing.Point(1, 28);
            this.panel11.Margin = new System.Windows.Forms.Padding(1);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(379, 39);
            this.panel11.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(186, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 11);
            this.label3.TabIndex = 33;
            this.label3.Text = "* IN DECIMAL";
            // 
            // quickFix
            // 
            this.quickFix.Location = new System.Drawing.Point(283, 5);
            this.quickFix.Margin = new System.Windows.Forms.Padding(1);
            this.quickFix.Name = "quickFix";
            this.quickFix.Size = new System.Drawing.Size(83, 21);
            this.quickFix.TabIndex = 30;
            this.quickFix.Text = "Quick Fix";
            this.quickFix.UseVisualStyleBackColor = true;
            this.quickFix.Click += new System.EventHandler(this.quickFix_Click_1);
            // 
            // panel13
            // 
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel13.Controls.Add(this.numericUpDown6);
            this.panel13.Location = new System.Drawing.Point(188, 5);
            this.panel13.Margin = new System.Windows.Forms.Padding(1);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(69, 18);
            this.panel13.TabIndex = 31;
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.BackColor = System.Drawing.Color.Ivory;
            this.numericUpDown6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown6.Font = new System.Drawing.Font("Times New Roman", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown6.Location = new System.Drawing.Point(-1, -2);
            this.numericUpDown6.Margin = new System.Windows.Forms.Padding(1);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(67, 20);
            this.numericUpDown6.TabIndex = 30;
            // 
            // targetCS
            // 
            this.targetCS.AutoSize = true;
            this.targetCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.targetCS.Location = new System.Drawing.Point(2, 7);
            this.targetCS.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.targetCS.Name = "targetCS";
            this.targetCS.Size = new System.Drawing.Size(188, 16);
            this.targetCS.TabIndex = 30;
            this.targetCS.Text = "Target Checksum (32 bit): ";
            // 
            // fixCS
            // 
            this.fixCS.AutoSize = true;
            this.fixCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fixCS.Location = new System.Drawing.Point(188, 2);
            this.fixCS.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.fixCS.Name = "fixCS";
            this.fixCS.Size = new System.Drawing.Size(238, 16);
            this.fixCS.TabIndex = 34;
            this.fixCS.Text = "Checksum Correction (Advanced)";
            // 
            // preset
            // 
            this.preset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preset.Location = new System.Drawing.Point(129, 6);
            this.preset.Margin = new System.Windows.Forms.Padding(1);
            this.preset.Name = "preset";
            this.preset.Size = new System.Drawing.Size(73, 21);
            this.preset.TabIndex = 34;
            this.preset.Text = "Preset";
            this.preset.UseVisualStyleBackColor = true;
            this.preset.Click += new System.EventHandler(this.preset_Click_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 325);
            this.tabControl1.TabIndex = 35;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.textBox12);
            this.tabPage4.Controls.Add(this.textBox6);
            this.tabPage4.Controls.Add(this.textBox11);
            this.tabPage4.Controls.Add(this.board);
            this.tabPage4.Controls.Add(this.textBox3);
            this.tabPage4.Controls.Add(this.fileName);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.version);
            this.tabPage4.Controls.Add(this.textBox2);
            this.tabPage4.Controls.Add(this.name);
            this.tabPage4.Controls.Add(this.model);
            this.tabPage4.Controls.Add(this.textBox1);
            this.tabPage4.Controls.Add(this.textBox7);
            this.tabPage4.Controls.Add(this.checkSum);
            this.tabPage4.Controls.Add(this.date);
            this.tabPage4.Controls.Add(this.textBox10);
            this.tabPage4.Controls.Add(this.textBox9);
            this.tabPage4.Controls.Add(this.textBox8);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(676, 299);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Info";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label9);
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Controls.Add(this.tempFixed);
            this.tabPage5.Controls.Add(this.tempAdjustable);
            this.tabPage5.Controls.Add(this.label16);
            this.tabPage5.Controls.Add(this.panel7);
            this.tabPage5.Controls.Add(this.tempTarget);
            this.tabPage5.Controls.Add(this.tempSlide);
            this.tabPage5.Controls.Add(this.throttlingTemp);
            this.tabPage5.Controls.Add(this.panel6);
            this.tabPage5.Controls.Add(this.maxTemp);
            this.tabPage5.Controls.Add(this.panel5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(676, 325);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Main";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(10, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 101);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tdpFixed);
            this.panel1.Controls.Add(this.baseTDP);
            this.panel1.Controls.Add(this.tdpAdjustable);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.tdpSlide);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.maxTDP);
            this.panel1.Location = new System.Drawing.Point(7, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 74);
            this.panel1.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(228, 3);
            this.label16.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(153, 13);
            this.label16.TabIndex = 36;
            this.label16.Text = "Board Power Limit Control";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.panel18);
            this.tabPage6.Controls.Add(this.panel16);
            this.tabPage6.Controls.Add(this.magicNum);
            this.tabPage6.Controls.Add(this.panel19);
            this.tabPage6.Controls.Add(this.label5);
            this.tabPage6.Controls.Add(this.panel14);
            this.tabPage6.Controls.Add(this.label2);
            this.tabPage6.Controls.Add(this.label8);
            this.tabPage6.Controls.Add(this.label6);
            this.tabPage6.Controls.Add(this.panel17);
            this.tabPage6.Controls.Add(this.panel12);
            this.tabPage6.Controls.Add(this.label7);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(676, 325);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "Extreme";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.fixCS);
            this.tabPage7.Controls.Add(this.panel20);
            this.tabPage7.Controls.Add(this.label11);
            this.tabPage7.Controls.Add(this.panel11);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(676, 325);
            this.tabPage7.TabIndex = 3;
            this.tabPage7.Text = "Tool";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // PascalBiosEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(684, 366);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.preset);
            this.Controls.Add(this.saveBIOS);
            this.Controls.Add(this.openBIOS);
            this.Font = new System.Drawing.Font("Courier New", 7.875F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MinimumSize = new System.Drawing.Size(700, 405);
            this.Name = "PascalBiosEditor";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pascal Bios Editor 1.0 ";
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).EndInit();
            this.panel16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).EndInit();
            this.panel19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).EndInit();
            this.panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            this.panel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).EndInit();
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.ResumeLayout(false);

        }

       
    }

}
