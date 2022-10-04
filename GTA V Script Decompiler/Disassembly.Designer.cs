namespace Decompiler
{
    partial class Disassembly
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Disassembly));
            this.fctb1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkPatternUniquenessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CreatePatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchNopInstructionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchPlaceFunctionReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fctb1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fctb1
            // 
            this.fctb1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fctb1.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.\\(\\)]+\\s*(?<range>=)\\s*(?<range>.+)\r\n";
            this.fctb1.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fctb1.BackBrush = null;
            this.fctb1.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fctb1.CharHeight = 14;
            this.fctb1.CharWidth = 8;
            this.fctb1.CommentPrefix = "\'";
            this.fctb1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctb1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctb1.IsReplaceMode = false;
            this.fctb1.Language = FastColoredTextBoxNS.Language.VB;
            this.fctb1.LeftBracket = '(';
            this.fctb1.Location = new System.Drawing.Point(0, 24);
            this.fctb1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fctb1.Name = "fctb1";
            this.fctb1.Paddings = new System.Windows.Forms.Padding(0);
            this.fctb1.ReadOnly = true;
            this.fctb1.RightBracket = ')';
            this.fctb1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctb1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctb1.ServiceColors")));
            this.fctb1.Size = new System.Drawing.Size(978, 679);
            this.fctb1.TabIndex = 2;
            this.fctb1.Zoom = 100;
            this.fctb1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fctb1_MouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(978, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkPatternUniquenessToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // checkPatternUniquenessToolStripMenuItem
            // 
            this.checkPatternUniquenessToolStripMenuItem.Name = "checkPatternUniquenessToolStripMenuItem";
            this.checkPatternUniquenessToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.checkPatternUniquenessToolStripMenuItem.Text = "Check Pattern Uniqueness";
            this.checkPatternUniquenessToolStripMenuItem.Click += new System.EventHandler(this.checkPatternUniquenessToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreatePatternToolStripMenuItem,
            this.patchNopInstructionsToolStripMenuItem,
            this.patchPlaceFunctionReturnToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(227, 70);
            // 
            // CreatePatternToolStripMenuItem
            // 
            this.CreatePatternToolStripMenuItem.Name = "CreatePatternToolStripMenuItem";
            this.CreatePatternToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.CreatePatternToolStripMenuItem.Text = "Create Pattern";
            this.CreatePatternToolStripMenuItem.Click += new System.EventHandler(this.CreatePatternToolStripMenuItem_Click);
            // 
            // patchNopInstructionsToolStripMenuItem
            // 
            this.patchNopInstructionsToolStripMenuItem.Name = "patchNopInstructionsToolStripMenuItem";
            this.patchNopInstructionsToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.patchNopInstructionsToolStripMenuItem.Text = "Patch: Nop Instruction(s)";
            this.patchNopInstructionsToolStripMenuItem.Click += new System.EventHandler(this.patchNopInstructionsToolStripMenuItem_Click);
            // 
            // patchPlaceFunctionReturnToolStripMenuItem
            // 
            this.patchPlaceFunctionReturnToolStripMenuItem.Name = "patchPlaceFunctionReturnToolStripMenuItem";
            this.patchPlaceFunctionReturnToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.patchPlaceFunctionReturnToolStripMenuItem.Text = "Patch: Place Function Return";
            this.patchPlaceFunctionReturnToolStripMenuItem.Click += new System.EventHandler(this.patchPlaceFunctionReturnToolStripMenuItem_Click);
            // 
            // Disassembly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 703);
            this.Controls.Add(this.fctb1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Disassembly";
            this.Text = "Disassembly";
            ((System.ComponentModel.ISupportInitialize)(this.fctb1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fctb1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkPatternUniquenessToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CreatePatternToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patchNopInstructionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patchPlaceFunctionReturnToolStripMenuItem;
    }
}