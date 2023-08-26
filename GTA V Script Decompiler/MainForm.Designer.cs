namespace Decompiler
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            fctb1 = new FastColoredTextBoxNS.FastColoredTextBox();
            cmsText = new System.Windows.Forms.ContextMenuStrip(components);
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveCFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            directoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            intStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            intToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            uintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            hexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            showLocalizedTextsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            disabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            gettextStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            commentStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            enumStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            disabledToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            substituteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            commentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            showArraySizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            reverseHashesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            includeNativeNamespaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            declareVariablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            shiftVariablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            globalAndStructHexIndexingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            showFuncPointerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            useMultiThreadingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            includeFunctionPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            includeFunctionHashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            uppercaseNativesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            exportTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            entitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            resetGlobalTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            expandAllBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            collaspeAllBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            showLineNumbersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            navigateForwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            navigateBackwardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            stringsTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            nativeTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            panel1 = new System.Windows.Forms.Panel();
            listView1 = new System.Windows.Forms.ListView();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            Xrefs = new System.Windows.Forms.ColumnHeader();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            timer3 = new System.Windows.Forms.Timer(components);
            isRDR2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)fctb1).BeginInit();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // fctb1
            // 
            fctb1.AutoCompleteBracketsList = new char[] { '(', ')', '{', '}', '[', ']', '"', '"', '\'', '\'' };
            fctb1.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            fctb1.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            fctb1.BackBrush = null;
            fctb1.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            fctb1.CharHeight = 14;
            fctb1.CharWidth = 8;
            fctb1.ContextMenuStrip = cmsText;
            fctb1.Cursor = System.Windows.Forms.Cursors.IBeam;
            fctb1.DisabledColor = System.Drawing.Color.FromArgb(100, 180, 180, 180);
            fctb1.Dock = System.Windows.Forms.DockStyle.Fill;
            fctb1.IsReplaceMode = false;
            fctb1.Language = FastColoredTextBoxNS.Language.CSharp;
            fctb1.LeftBracket = '(';
            fctb1.LeftBracket2 = '{';
            fctb1.Location = new System.Drawing.Point(0, 24);
            fctb1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            fctb1.Name = "fctb1";
            fctb1.Paddings = new System.Windows.Forms.Padding(0);
            fctb1.ReadOnly = true;
            fctb1.RightBracket = ')';
            fctb1.RightBracket2 = '}';
            fctb1.SelectionColor = System.Drawing.Color.FromArgb(60, 0, 0, 255);
            fctb1.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("fctb1.ServiceColors");
            fctb1.Size = new System.Drawing.Size(659, 679);
            fctb1.TabIndex = 1;
            fctb1.Zoom = 100;
            fctb1.MouseClick += fctb1_MouseClick;
            // 
            // cmsText
            // 
            cmsText.Name = "cmsText";
            cmsText.Size = new System.Drawing.Size(61, 4);
            cmsText.Opening += cmsText_Opening;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, optionsToolStripMenuItem, viewToolStripMenuItem, extractToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(945, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openToolStripMenuItem, saveCFileToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator2, closeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveCFileToolStripMenuItem
            // 
            saveCFileToolStripMenuItem.Name = "saveCFileToolStripMenuItem";
            saveCFileToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            saveCFileToolStripMenuItem.Text = "Save";
            saveCFileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            saveCFileToolStripMenuItem.Click += saveCFileToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem1, directoryToolStripMenuItem });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            exportToolStripMenuItem.Text = "Export";
            // 
            // fileToolStripMenuItem1
            // 
            fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            fileToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            fileToolStripMenuItem1.Text = "Single File";
            fileToolStripMenuItem1.Click += fileToolStripMenuItem1_Click;
            // 
            // directoryToolStripMenuItem
            // 
            directoryToolStripMenuItem.Name = "directoryToolStripMenuItem";
            directoryToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            directoryToolStripMenuItem.Text = "Directory";
            directoryToolStripMenuItem.Click += directoryToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(105, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { intStyleToolStripMenuItem, showLocalizedTextsToolStripMenuItem, enumStyleToolStripMenuItem, toolStripSeparator5, isRDR2ToolStripMenuItem, showArraySizeToolStripMenuItem, reverseHashesToolStripMenuItem, includeNativeNamespaceToolStripMenuItem, declareVariablesToolStripMenuItem, shiftVariablesToolStripMenuItem, globalAndStructHexIndexingToolStripMenuItem, showFuncPointerToolStripMenuItem, useMultiThreadingToolStripMenuItem, includeFunctionPositionToolStripMenuItem, includeFunctionHashToolStripMenuItem, uppercaseNativesToolStripMenuItem, toolStripSeparator4, exportTablesToolStripMenuItem, resetGlobalTypesToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // intStyleToolStripMenuItem
            // 
            intStyleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { intToolStripMenuItem, uintToolStripMenuItem, hexToolStripMenuItem });
            intStyleToolStripMenuItem.Name = "intStyleToolStripMenuItem";
            intStyleToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            intStyleToolStripMenuItem.Text = "Int Style";
            intStyleToolStripMenuItem.ToolTipText = "Choose how to display int32 data types";
            // 
            // intToolStripMenuItem
            // 
            intToolStripMenuItem.Name = "intToolStripMenuItem";
            intToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            intToolStripMenuItem.Text = "Int";
            intToolStripMenuItem.Click += intstylechanged;
            // 
            // uintToolStripMenuItem
            // 
            uintToolStripMenuItem.Name = "uintToolStripMenuItem";
            uintToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            uintToolStripMenuItem.Text = "Uint";
            uintToolStripMenuItem.Click += intstylechanged;
            // 
            // hexToolStripMenuItem
            // 
            hexToolStripMenuItem.Name = "hexToolStripMenuItem";
            hexToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            hexToolStripMenuItem.Text = "Hex";
            hexToolStripMenuItem.Click += intstylechanged;
            // 
            // showLocalizedTextsToolStripMenuItem
            // 
            showLocalizedTextsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { disabledToolStripMenuItem, gettextStyleToolStripMenuItem, commentStyleToolStripMenuItem });
            showLocalizedTextsToolStripMenuItem.Name = "showLocalizedTextsToolStripMenuItem";
            showLocalizedTextsToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            showLocalizedTextsToolStripMenuItem.Text = "Localized Text Style";
            showLocalizedTextsToolStripMenuItem.ToolTipText = "Replace text labels with their localized text";
            // 
            // disabledToolStripMenuItem
            // 
            disabledToolStripMenuItem.Name = "disabledToolStripMenuItem";
            disabledToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            disabledToolStripMenuItem.Text = "Disabled";
            disabledToolStripMenuItem.Click += disabledToolStripMenuItem_Click;
            // 
            // gettextStyleToolStripMenuItem
            // 
            gettextStyleToolStripMenuItem.Name = "gettextStyleToolStripMenuItem";
            gettextStyleToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            gettextStyleToolStripMenuItem.Text = "Gettext Style";
            gettextStyleToolStripMenuItem.Click += gettextStyleToolStripMenuItem_Click;
            // 
            // commentStyleToolStripMenuItem
            // 
            commentStyleToolStripMenuItem.Name = "commentStyleToolStripMenuItem";
            commentStyleToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            commentStyleToolStripMenuItem.Text = "Comment Style";
            commentStyleToolStripMenuItem.Click += commentStyleToolStripMenuItem_Click;
            // 
            // enumStyleToolStripMenuItem
            // 
            enumStyleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { disabledToolStripMenuItem1, substituteToolStripMenuItem, commentToolStripMenuItem });
            enumStyleToolStripMenuItem.Name = "enumStyleToolStripMenuItem";
            enumStyleToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            enumStyleToolStripMenuItem.Text = "Enum Style";
            enumStyleToolStripMenuItem.ToolTipText = "Control how enums and flags are displayed";
            // 
            // disabledToolStripMenuItem1
            // 
            disabledToolStripMenuItem1.Name = "disabledToolStripMenuItem1";
            disabledToolStripMenuItem1.Size = new System.Drawing.Size(128, 22);
            disabledToolStripMenuItem1.Text = "Disabled";
            disabledToolStripMenuItem1.ToolTipText = "Disable enums";
            disabledToolStripMenuItem1.Click += disabledToolStripMenuItem1_Click;
            // 
            // substituteToolStripMenuItem
            // 
            substituteToolStripMenuItem.Name = "substituteToolStripMenuItem";
            substituteToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            substituteToolStripMenuItem.Text = "Substitute";
            substituteToolStripMenuItem.ToolTipText = "Replace integers with enum members in-place";
            substituteToolStripMenuItem.Click += substituteToolStripMenuItem_Click;
            // 
            // commentToolStripMenuItem
            // 
            commentToolStripMenuItem.Name = "commentToolStripMenuItem";
            commentToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            commentToolStripMenuItem.Text = "Comment";
            commentToolStripMenuItem.ToolTipText = "Place enum members next to an integer as a comment";
            commentToolStripMenuItem.Click += commentToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(235, 6);
            // 
            // showArraySizeToolStripMenuItem
            // 
            showArraySizeToolStripMenuItem.Name = "showArraySizeToolStripMenuItem";
            showArraySizeToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            showArraySizeToolStripMenuItem.Text = "Show Array Size";
            showArraySizeToolStripMenuItem.ToolTipText = "Shows the size of the items in an array \r\nuLocal_5[index <item_size>]\r\nan array of vector3s would look like this\r\nvStatic_1[0 <3>];";
            showArraySizeToolStripMenuItem.Click += showArraySizeToolStripMenuItem_Click;
            // 
            // reverseHashesToolStripMenuItem
            // 
            reverseHashesToolStripMenuItem.Name = "reverseHashesToolStripMenuItem";
            reverseHashesToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            reverseHashesToolStripMenuItem.Text = "Reverse Hashes";
            reverseHashesToolStripMenuItem.ToolTipText = "Reverse known hashes into their text equivalent\r\ne.g 0xB779A091 -> joaat(\"adder\")";
            reverseHashesToolStripMenuItem.Click += reverseHashesToolStripMenuItem_Click;
            // 
            // includeNativeNamespaceToolStripMenuItem
            // 
            includeNativeNamespaceToolStripMenuItem.Name = "includeNativeNamespaceToolStripMenuItem";
            includeNativeNamespaceToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            includeNativeNamespaceToolStripMenuItem.Text = "Include Native Namespace";
            includeNativeNamespaceToolStripMenuItem.Click += includeNativeNamespaceToolStripMenuItem_Click;
            // 
            // declareVariablesToolStripMenuItem
            // 
            declareVariablesToolStripMenuItem.Name = "declareVariablesToolStripMenuItem";
            declareVariablesToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            declareVariablesToolStripMenuItem.Text = "Declare Variables";
            declareVariablesToolStripMenuItem.ToolTipText = "Include Variable declarations at the start of file and functions";
            declareVariablesToolStripMenuItem.Click += declareVariablesToolStripMenuItem_Click;
            // 
            // shiftVariablesToolStripMenuItem
            // 
            shiftVariablesToolStripMenuItem.Name = "shiftVariablesToolStripMenuItem";
            shiftVariablesToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            shiftVariablesToolStripMenuItem.Text = "Shift Variables";
            shiftVariablesToolStripMenuItem.ToolTipText = resources.GetString("shiftVariablesToolStripMenuItem.ToolTipText");
            shiftVariablesToolStripMenuItem.Click += shiftVariablesToolStripMenuItem_Click;
            // 
            // globalAndStructHexIndexingToolStripMenuItem
            // 
            globalAndStructHexIndexingToolStripMenuItem.Name = "globalAndStructHexIndexingToolStripMenuItem";
            globalAndStructHexIndexingToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            globalAndStructHexIndexingToolStripMenuItem.Text = "Global and Struct Hex Indexing";
            globalAndStructHexIndexingToolStripMenuItem.Click += globalAndStructHexIndexingToolStripMenuItem_Click;
            // 
            // showFuncPointerToolStripMenuItem
            // 
            showFuncPointerToolStripMenuItem.Name = "showFuncPointerToolStripMenuItem";
            showFuncPointerToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            showFuncPointerToolStripMenuItem.Text = "Show Func Pointer";
            showFuncPointerToolStripMenuItem.Click += showFuncPointerToolStripMenuItem_Click;
            // 
            // useMultiThreadingToolStripMenuItem
            // 
            useMultiThreadingToolStripMenuItem.Name = "useMultiThreadingToolStripMenuItem";
            useMultiThreadingToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            useMultiThreadingToolStripMenuItem.Text = "Use Multithreading";
            useMultiThreadingToolStripMenuItem.Click += useMultiThreadingToolStripMenuItem_Click;
            // 
            // includeFunctionPositionToolStripMenuItem
            // 
            includeFunctionPositionToolStripMenuItem.Name = "includeFunctionPositionToolStripMenuItem";
            includeFunctionPositionToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            includeFunctionPositionToolStripMenuItem.Text = "Include Function Position";
            // 
            // includeFunctionHashToolStripMenuItem
            // 
            includeFunctionHashToolStripMenuItem.Name = "includeFunctionHashToolStripMenuItem";
            includeFunctionHashToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            includeFunctionHashToolStripMenuItem.Text = "Include Function Hash";
            includeFunctionHashToolStripMenuItem.ToolTipText = "Shows the persistent hash of the function. Meant to be used for function database building";
            includeFunctionHashToolStripMenuItem.Click += includeFunctionHashToolStripMenuItem_Click;
            // 
            // uppercaseNativesToolStripMenuItem
            // 
            uppercaseNativesToolStripMenuItem.Name = "uppercaseNativesToolStripMenuItem";
            uppercaseNativesToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            uppercaseNativesToolStripMenuItem.Text = "Uppercase Natives";
            uppercaseNativesToolStripMenuItem.Click += uppercaseNativesToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(235, 6);
            // 
            // exportTablesToolStripMenuItem
            // 
            exportTablesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { entitiesToolStripMenuItem });
            exportTablesToolStripMenuItem.Name = "exportTablesToolStripMenuItem";
            exportTablesToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            exportTablesToolStripMenuItem.Text = "Export Tables";
            // 
            // entitiesToolStripMenuItem
            // 
            entitiesToolStripMenuItem.Name = "entitiesToolStripMenuItem";
            entitiesToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            entitiesToolStripMenuItem.Text = "Entities";
            entitiesToolStripMenuItem.ToolTipText = "Export The entites file (entities_exp.dat) built into the program so you can edit it.\r\nThe program will search for entities.dat in its directory and use that for reversing hashes";
            entitiesToolStripMenuItem.Click += entitiesToolStripMenuItem_Click;
            // 
            // resetGlobalTypesToolStripMenuItem
            // 
            resetGlobalTypesToolStripMenuItem.Name = "resetGlobalTypesToolStripMenuItem";
            resetGlobalTypesToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            resetGlobalTypesToolStripMenuItem.Text = "Reset Global Types";
            resetGlobalTypesToolStripMenuItem.Click += resetGlobalTypesToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { expandAllBlocksToolStripMenuItem, collaspeAllBlocksToolStripMenuItem, toolStripSeparator1, showLineNumbersToolStripMenuItem, navigateForwardToolStripMenuItem, navigateBackwardsToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // expandAllBlocksToolStripMenuItem
            // 
            expandAllBlocksToolStripMenuItem.Name = "expandAllBlocksToolStripMenuItem";
            expandAllBlocksToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            expandAllBlocksToolStripMenuItem.Text = "Expand All Blocks";
            expandAllBlocksToolStripMenuItem.Click += expandAllBlocksToolStripMenuItem_Click;
            // 
            // collaspeAllBlocksToolStripMenuItem
            // 
            collaspeAllBlocksToolStripMenuItem.Name = "collaspeAllBlocksToolStripMenuItem";
            collaspeAllBlocksToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            collaspeAllBlocksToolStripMenuItem.Text = "Collaspe All Blocks";
            collaspeAllBlocksToolStripMenuItem.Click += collaspeAllBlocksToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(270, 6);
            // 
            // showLineNumbersToolStripMenuItem
            // 
            showLineNumbersToolStripMenuItem.Name = "showLineNumbersToolStripMenuItem";
            showLineNumbersToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            showLineNumbersToolStripMenuItem.Text = "Show Line Numbers";
            showLineNumbersToolStripMenuItem.Click += showLineNumbersToolStripMenuItem_Click;
            // 
            // navigateForwardToolStripMenuItem
            // 
            navigateForwardToolStripMenuItem.Name = "navigateForwardToolStripMenuItem";
            navigateForwardToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus;
            navigateForwardToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            navigateForwardToolStripMenuItem.Text = "Navigate Forward";
            navigateForwardToolStripMenuItem.Click += navigateForwardToolStripMenuItem_Click;
            // 
            // navigateBackwardsToolStripMenuItem
            // 
            navigateBackwardsToolStripMenuItem.Name = "navigateBackwardsToolStripMenuItem";
            navigateBackwardsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus;
            navigateBackwardsToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            navigateBackwardsToolStripMenuItem.Text = "Navigate Backwards";
            navigateBackwardsToolStripMenuItem.Click += navigateBackwardsToolStripMenuItem_Click;
            // 
            // extractToolStripMenuItem
            // 
            extractToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { stringsTableToolStripMenuItem, nativeTableToolStripMenuItem });
            extractToolStripMenuItem.Enabled = false;
            extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            extractToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            extractToolStripMenuItem.Text = "Extract";
            extractToolStripMenuItem.Visible = false;
            // 
            // stringsTableToolStripMenuItem
            // 
            stringsTableToolStripMenuItem.Name = "stringsTableToolStripMenuItem";
            stringsTableToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            stringsTableToolStripMenuItem.Text = "Strings table";
            stringsTableToolStripMenuItem.Click += stringsTableToolStripMenuItem_Click;
            // 
            // nativeTableToolStripMenuItem
            // 
            nativeTableToolStripMenuItem.Name = "nativeTableToolStripMenuItem";
            nativeTableToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            nativeTableToolStripMenuItem.Text = "Native table";
            nativeTableToolStripMenuItem.Click += nativeTableToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(listView1);
            panel1.Dock = System.Windows.Forms.DockStyle.Right;
            panel1.Location = new System.Drawing.Point(659, 24);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(261, 679);
            panel1.TabIndex = 3;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader2, Xrefs });
            listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            listView1.Location = new System.Drawing.Point(0, 0);
            listView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(261, 679);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = System.Windows.Forms.View.Details;
            listView1.ColumnClick += listView1_ColumnClick;
            listView1.MouseDoubleClick += listView1_MouseDoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Function";
            columnHeader1.Width = 96;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Line";
            columnHeader2.Width = 55;
            // 
            // Xrefs
            // 
            Xrefs.Text = "Xrefs";
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButton1 });
            toolStrip1.Location = new System.Drawing.Point(920, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(25, 679);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.AutoToolTip = false;
            toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Padding = new System.Windows.Forms.Padding(2);
            toolStripButton1.Size = new System.Drawing.Size(22, 72);
            toolStripButton1.Text = "Functions";
            toolStripButton1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            toolStripButton1.ToolTipText = "Show the line numbers for functions and lets you jump to them";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel3, toolStripStatusLabel2 });
            statusStrip1.Location = new System.Drawing.Point(0, 703);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip1.Size = new System.Drawing.Size(945, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            toolStripStatusLabel1.Text = "Ready";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new System.Drawing.Size(889, 17);
            toolStripStatusLabel3.Spring = true;
            toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer3
            // 
            timer3.Interval = 5000;
            // 
            // isRDR2
            // 
            isRDR2ToolStripMenuItem.Name = "isRDR2";
            isRDR2ToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            isRDR2ToolStripMenuItem.Text = "RDR2";
            isRDR2ToolStripMenuItem.ToolTipText = "The decompiler will restart if this setting is changed";
            isRDR2ToolStripMenuItem.Click += isRDR2ToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(945, 725);
            Controls.Add(fctb1);
            Controls.Add(panel1);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "GTA V High Level Decompiler";
            ((System.ComponentModel.ISupportInitialize)fctb1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fctb1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem directoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showArraySizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reverseHashesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem declareVariablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shiftVariablesToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem exportTablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entitiesToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllBlocksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collaspeAllBlocksToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem showLineNumbersToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.ToolStripMenuItem saveCFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip cmsText;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stringsTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nativeTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem navigateForwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem navigateBackwardsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useMultiThreadingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFuncPointerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem includeNativeNamespaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalAndStructHexIndexingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem includeFunctionHashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uppercaseNativesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem includeFunctionPositionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLocalizedTextsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetGlobalTypesToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader Xrefs;
        private System.Windows.Forms.ToolStripMenuItem disabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gettextStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commentStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enumStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disabledToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem substituteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem isRDR2ToolStripMenuItem;
    }
}

